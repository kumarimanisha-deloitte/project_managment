using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using project_managment_hu.DbContest;
using project_managment_hu.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration["ConnectionString:ProjectManagmentDB"];//connection string from appsetting.json
builder.Services.AddDbContext<UserContext>(opts =>
                                                 opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
                                                 
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IIssueService, IssueService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IlabelsService, labelsService>();
builder.Services.AddScoped<IRoleService, RoleService>();





builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(swagger =>{
    swagger.SwaggerDoc("v1",new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version="v1",
        Title="JWT Token Authentication API",
        Description="ASP.NETWeb API"

    });
    swagger.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme()
    {
        Name="Authorization",
        Type=Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme="Bearer",
        BearerFormat="JWT",
        In=Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description="JWT Authorization header using the Bearer scheme"

    });
    swagger.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[] {}


        }

    });
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
