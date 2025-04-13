using WebApi.Extensions;
using WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Extensions
builder.Services.ConfigureServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

// HTTPS
app.UseHttpsRedirection();

app.UseRouting();

app.MapControllers();

// Auth
app.UseAuthentication();
app.UseAuthorization();

// Middlewares
app.UseMiddleware<RoleMiddleware>();

app.UseStaticFiles();

app.Run();