/*
 Note, I can use UserManager alternative of AppIdentityDbContext, but AppIdentityDbContext is best because not select all 
 columns like UserManager
 */
namespace Survey.Business.Services.User
{
    public class UserService : IUserService
    {
        private readonly AppIdentityDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        public UserService(AppIdentityDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }


        // I didn't validation on user in methods because I make sure that user can't access action without authrization that use it in fetch userId..
        public async Task<UserProfileResponse> GetUserProfile(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            var userProfileRes = _mapper.Map<UserProfileResponse>(user);

            return userProfileRes;
        }

        public async Task<UserProfileResponse> UpdateUserProfile(int userId, UpdateUserProfileRequest userProfileRequest)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x=> x.Id == userId);   
            
            var IsEmailExist = await _context.Users.AnyAsync(x=> x.Email == userProfileRequest.Email);

            if (IsEmailExist)
                throw new BadRequest("Email already in use");

            user!.Fname = userProfileRequest.Fname;
            user.Lname = userProfileRequest.Lname;
            user.Email = userProfileRequest.Email;
            user.PhoneNumber = userProfileRequest.PhoneNumber;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            var userProfileResponse = _mapper.Map<UserProfileResponse>(user);

            return userProfileResponse;
        }

        public async Task<bool> ChangePassword(int userId,ChangePasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            var result = await _userManager.ChangePasswordAsync(user!,request.CurrentPassword,request.NewPassword);

            if (!result.Succeeded)
                throw new InvalidOperation(400, result.Errors.Select(x=> x.Description).First());

            return true;
        }

        public async Task<IList<UserResponse>> GetUsers()
        {
            var users = await _userManager.Users.ToListAsync();

            if (users is null)
                throw new ItemNotFound("not exist users");
            
            var usersResponses = new List<UserResponse>();
           
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if(roles.Where(x=> x != DefaultRole.Member).Count() > 0 || roles.Count() == 0)
                {
                    usersResponses.Add(new UserResponse()
                    {
                        Id = user.Id,
                        Fname = user.Fname,
                        Lname = user.Lname,
                        Email = user.Email!,
                        IsDisabled = user.IsDisabled,
                        Roles = roles.Except(roles.Where(x => x == DefaultRole.Member))
                    });
                }
            }

            return usersResponses;
        }

        public async Task<UserResponse> GetUserDetail(int userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user is null)
                throw new ItemNotFound("not exist user");

            var roles = await _userManager.GetRolesAsync(user);

            UserResponse userResponse = null!;
          
            if (roles.Where(x => x.ToLower() != "member").Count() > 0 || roles.Count() == 0)
            {
                userResponse = new UserResponse()
                {
                    Id = user.Id,
                    Fname = user.Fname,
                    Lname = user.Lname,
                    Email = user.Email!,
                    IsDisabled = user.IsDisabled,
                    Roles = roles.Except(roles.Where(x => x.ToLower() == "member"))
                };
            }

            return userResponse;
        }

        public async Task<UserResponse> CreateUser(CreateUserRequest request)
        {
            var IsUserExist = await _userManager.FindByEmailAsync(request.Email);

            if (IsUserExist is not null)
                throw new ItemAlreadyExist("this email is in use");

            var avaliableRoles = await _context.Roles.Select(x => x.Name).ToListAsync();

            if (request.Roles.Except(avaliableRoles).Any())
                throw new BadRequest("Invalid Roles");

            var appUser = new ApplicationUser()
            {
                Fname = request.Fname,
                Lname = request.Lname,
                Email = request.Email,
                UserName = request.Email.Split('@').First(),
                EmailConfirmed = true // Based on requirments...
            };

            var result = await _userManager.CreateAsync(appUser, request.Password);

            if (!result.Succeeded)
            {
                var error = result.Errors.FirstOrDefault();

                if (error != null)
                    throw new BadRequest($"{error.Description}");
            }

            await _userManager.AddToRolesAsync(appUser, request.Roles);


            return new UserResponse()
            {
                Id = appUser.Id,
                Fname = request.Fname,
                Lname = request.Lname,
                Email = request.Email,
                IsDisabled = false,
                Roles = request.Roles
            };
        }

        public async Task<UserResponse> UpdateUser(int id, UpdateUserRequest request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user is null)
                throw new ItemNotFound("user is not exist");

            var availableRoles = await _context.Roles.Select(x => x.Name).ToListAsync();

            if (request.Roles.Except(availableRoles).Any())
                throw new BadRequest("Invalid Roles");

            user.Fname = request.Fname;
            user.Lname = request.Lname;
            user.Email = request.Email;
            user.UserName = request.Email.Split('@')[0];
              
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (roles is not null && roles.Count > 0)
                {
                     await _context.UserRoles.Where(x => x.UserId == user.Id).ExecuteDeleteAsync();
                }

                await _userManager.AddToRolesAsync(user, request.Roles);

                return new UserResponse()
                {
                    Id = user.Id,
                    Fname = request.Fname,
                    Lname = request.Lname,
                    IsDisabled = false,
                    Email = request.Email,
                    Roles = request.Roles
                };
            }

            var error = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new BadRequest($"Failed to remove roles: {error}");
        }

        public async Task ToggleUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user is null)
                throw new ItemNotFound("user is not exist");

            user.IsDisabled = !user.IsDisabled;
            var result = await _userManager.UpdateAsync(user);

            if(!result.Succeeded)
            {
                var error = result.Errors.Select(x=> x.Description).FirstOrDefault();

                if(error != null)
                {
                    throw new BadRequest($"{error}");
                }
            }
        }

        public async Task UnLockUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user is null)
                throw new ItemNotFound("user is not exist");

           await _userManager.SetLockoutEndDateAsync(user, null);
        }
    }
}
