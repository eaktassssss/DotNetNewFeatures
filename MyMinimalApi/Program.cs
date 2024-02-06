using Microsoft.AspNetCore.Mvc;
using MyMinimalApi.Entities;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Text.Json;





var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(s => s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Version = "1", Description = "My first minimal API", Title = "Minimal API" }));
builder.Services.AddHttpClient();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
app.MapGet("/posts", async (IHttpClientFactory factory) =>
{
    var client = factory.CreateClient();
    var stream = await client.GetStreamAsync("https://jsonplaceholder.typicode.com/posts");
    var posts = await JsonSerializer.DeserializeAsync<List<Post>>(stream);
    return posts;
});
app.Run();
