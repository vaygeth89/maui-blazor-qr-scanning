using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScanQrApi;
using ScanQrShared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
// builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IQrCodeVerification, QrCodeVerification>();
builder.Services.AddSignalR();
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        b =>
        {
            b.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

// app.UseHttpsRedirection();
// var group = app.MapGroup("api/identity").WithTags("Auth");
// group.MapIdentityApi<IdentityUser>();
app.MapHub<ChatHub>("/Chat");

var group = app.MapGroup("persons");

group.MapGet("/person-information-message",async ([FromServices] IQrCodeVerification services) =>
{
    var response = await services.SendPersonInformationMessage();
    return Results.Ok(response);
});


app.UseCors("AllowAllOrigins");
// app.UseAuthentication();
// app.UseAuthorization();


app.Run();