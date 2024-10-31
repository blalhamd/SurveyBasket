namespace Survey.Core.IServices.User
{
    public interface IUserService
    {
        Task<UserProfileResponse> GetUserProfile(int id);
        Task<UserProfileResponse> UpdateUserProfile(int userId, UpdateUserProfileRequest userProfileRequest);
        Task<bool> ChangePassword(int userId,ChangePasswordRequest request);
        Task<IList<UserResponse>> GetUsers();
        Task<UserResponse> GetUserDetail(int userId);
        Task<UserResponse> CreateUser(CreateUserRequest request);
        Task<UserResponse> UpdateUser(int id,UpdateUserRequest request);
        Task ToggleUser(int id);
        Task UnLockUser(int id);
    }
}
