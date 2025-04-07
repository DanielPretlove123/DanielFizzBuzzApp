using InfoTrackFizzBuzz.Infrastructure.Data;
using InfoTrackFizzBuzz.Web.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactAppPolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:3000")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Add services to the container.
builder.AddApplicationServices();
builder.AddInfrastructureServices();
builder.AddWebServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.InitialiseDatabaseAsync();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("ReactAppPolicy");


app.UseHealthChecks("/health");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseOpenApi();
app.UseSwaggerUi(settings =>
{
    settings.Path = "/api";
});


app.UseExceptionHandler(new ExceptionHandlerOptions
{
    AllowStatusCode404Response = true,
    ExceptionHandlingPath = "/error"
});

app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        var exceptionHandlerPathFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
        var exception = exceptionHandlerPathFeature?.Error;

        int statusCode = StatusCodes.Status500InternalServerError;
        var message = "An unexpected error occurred.";
        if (exception?.Message.Contains("not found") == true)
        {
            statusCode = StatusCodes.Status404NotFound;
            message = exception.Message;
        }
        else if (exception is InvalidOperationException)
        {
            statusCode = StatusCodes.Status400BadRequest;
            message = exception.Message;
        }

        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsJsonAsync(new
        {
            status = statusCode,
            message = message
        });
    });
});

app.Map("/", () => Results.Redirect("/api"));

app.MapEndpoints();
app.Run();

public partial class Program { }
