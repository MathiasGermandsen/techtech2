using techtech2.Components;
using techtech2.MailService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("wwwroot/mailCred.json", optional: true, reloadOnChange: true)
    .Build();

var smtpSettingsSection = config.GetSection("SmtpSettings");

var smtpSettings = smtpSettingsSection.Get<SmtpSettings>();


builder.Services.AddSingleton(sp => new MailService(
    smtpHost: smtpSettings.Host,
    smtpPort: smtpSettings.Port,
    fromAddressParameter: smtpSettings.FromAddress,
    smtpUsername: smtpSettings.Username,
    smtpPassword: smtpSettings.Password
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
