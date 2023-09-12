using IacLabb2.Data;
using IacLabb2.Data.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddSingleton<PhonebookDBcontext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


app.MapGet("/GetPhonebook",  (PhonebookDBcontext context) =>
{
    var phonebooks = context.Phonebooks.Find(_ => true).ToList();
    return  Results.Ok(phonebooks);
});

app.MapPost("/Phonebook", (PhonebookDBcontext context, Phonebook phonebook) =>
{
    context.Phonebooks.InsertOne(phonebook);
    return Results.Created($"/Phonebook/{phonebook.Id}", phonebook);
});


app.Run();
