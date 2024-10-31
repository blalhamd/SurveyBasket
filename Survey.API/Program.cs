var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddIdentity<ApplicationUser,ApplicationRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders(); // for confirm-email

//builder.Services.AddCorsConfig();

builder.Services.AddDistributedMemoryCache();

builder.Services.OptionsPatternConfig(builder.Configuration); // belong IOptions Pattern

builder.Services.AddDependencies(builder.Configuration);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSwaggerGen(options =>
{
    builder.Services.AddSwaggerCongig(options);
});

builder.Services.AddAuthentication(builder.Configuration);

builder.Services.HandlerConfig(); // for ExceptionHandler

//builder.Services.AddSerilog();

builder.Services.AddConfigure(); // must before in the last and before Build.....

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStatusCodePagesWithRedirects("/Errors/{0}");

//app.UseSerilogRequestLogging(); // can remove it and can remove builder.Services.AddSerilog(); too...

app.UseHttpsRedirection();

/*
app.UseCors(options =>
                    options
                    .WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()!)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    );
*/

app.UseCors(options =>
                    options
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    );

//app.UseCors(); --> in case multiple policies

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

//app.UseMiddleware<ErrorHandlingMiddleware>();

app.MapControllers();

app.UseMiddleware<RequestDurationTimeMiddleWare>();

app.UseExceptionHandler();

app.UseRateLimiter(); // built-in Middleware that belong Rate Limiting... 

app.Run();
