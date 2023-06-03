namespace ManagerServer.StartUp
{
    public static class StartUp
    {
        public static WebApplicationBuilder AddServicesBase(this WebApplicationBuilder builder)
        {

            builder.Services.AddControllers ();
            builder.Services.AddEndpointsApiExplorer ();
            builder.Services.AddScoped<IAuthService, AuthService> ();
            builder.Services.AddScoped<UserManager<AppUser>> ();
            builder.Services.AddScoped<SignInManager<AppUser>> ();
            builder.Services.AddScoped<RoleManager<IdentityRole>> ();
            builder.Services.AddScoped<IRoleService, RoleService> ();
            builder.Services.AddScoped<IAuthService, AuthService> ();
            builder.Services.AddScoped<IUserService, UserService> ();
            builder.Services.AddScoped<IFarmService, FarmService> ();
            builder.Services.AddScoped<IZoneService, ZoneService> ();

            builder.Services.AddCors (options =>
            {
                options.AddPolicy ("AllowAllHeaders",
                    builder =>
                    {
                        builder.AllowAnyOrigin ()
                               .AllowAnyHeader ()
                               .AllowAnyMethod ();
                    });
            });

            return builder;
        }
        public static WebApplicationBuilder AddBackgroundServices(this WebApplicationBuilder builder)
        {
            //builder.Services.AddHostedService<ListeningService>();
            //builder.Services.AddHostedService<ProcessDataService>();
            return builder;
        }
        public static WebApplicationBuilder AddServicesContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ManagerDbContext> (options =>
                  options.UseSqlServer (builder.Configuration.GetConnectionString ("DefaultConnection")));
            builder.Services.AddSwaggerGen (opt =>
            {
                opt.SwaggerDoc ("v1", new OpenApiInfo { Title = "ManagerServer", Version = "v1" });
                opt.AddSecurityDefinition ("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                opt.AddSecurityRequirement (new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
            return builder;
        }
        public static WebApplicationBuilder AddServicesIdentity(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<AppUser, IdentityRole> ()
                    .AddEntityFrameworkStores<ManagerDbContext> ()
                    .AddDefaultTokenProviders ();
            builder.Services
                .AddAuthentication (options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer (options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters ()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                        ValidAudience = builder.Configuration["JWT:ValidAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (builder.Configuration["JWT:Secret"]!))
                    };
                });
            builder.Services.AddAuthorization (options =>
            {
                options.AddPolicy ("Admin", policy => policy.RequireRole ("Admin"));
                options.AddPolicy ("User", policy => policy.RequireRole ("User"));
                options.AddPolicy ("Owner", policy => policy.RequireRole ("Owner"));
            });
            return builder;
        }
        public static WebApplication UsesService(this WebApplication app)
        {

            app.UseSwagger ();
            app.UseSwaggerUI ();
            app.UseCors ("AllowAllHeaders");
            app.UseHttpsRedirection ();
            app.UseAuthentication ();
            app.UseAuthorization ();
            //app.UseMiddleware<ApiResponseMiddleware> ();
            app.MapControllers ();
            return app;
        }
    }
}
