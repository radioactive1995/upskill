using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Quote_Machine;
using Quote_Machine.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor().AddCircuitOptions(o =>
{
    o.DetailedErrors = true;
});

builder.Services.AddDbContextFactory<QuoteContext>(options =>
{
    options.UseSqlServer("Server=tcp:upskilldbserver.database.windows.net,1433;Initial Catalog=upskill-db;Persist Security Info=False;User ID=upskilladmin;Password=Passord1.;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
});

builder.Services.AddSingleton<IQuoteService, QuoteService>();
builder.Services.AddSingleton<ISpeechService, SpeechService>();

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

app.Run();
