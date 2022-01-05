#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           Program.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 19:58
// Last Updated On:     01-04-2022 16:11
// *****************************************/

#endregion

#region Using

using Radzen;

#endregion

WebApplicationBuilder _builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//_builder.Services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
_builder.Services.AddSyncfusionBlazor();
_builder.Services.AddScoped<DialogService>();
_builder.Services.AddScoped<NotificationService>();
_builder.Services.AddScoped<TooltipService>();
_builder.Services.AddScoped<ContextMenuService>();
_builder.Services.AddRazorPages();
_builder.Services.AddServerSideBlazor();
_builder.Services.AddSingleton<WeatherForecastService>();
_builder.Services.AddHttpContextAccessor();
_builder.Services.AddHttpClient();
_builder.Services.AddBlazoredLocalStorage();                                                            // local storage
_builder.Services.AddBlazoredLocalStorage(config => config.JsonSerializerOptions.WriteIndented = true); // local storage
_builder.Services.AddMemoryCache();

WebApplication _app = _builder.Build();

// Configure the HTTP request pipeline.
SyncfusionLicenseProvider.RegisterLicense("NTUxOTI3QDMxMzkyZTM0MmUzMGY3TWJ0TWx3Z3lXY2ZaOG1mb1pBNFhpamYxWm92d0N1RGFSTmRFQUd4NGM9");

if (!_app.Environment.IsDevelopment())
{
    _app.UseExceptionHandler("/Error");
}

_app.UseStaticFiles();
//_app.UseMvcWithDefaultRoute();
_app.UseRouting();

_app.MapBlazorHub();
_app.MapFallbackToPage("/_Host");
//app.UsePathBase("/ProfSvc_AppTrack");

Start.ApiHost = _app.Configuration.GetValue(typeof(string), "APIHost").ToString();
Start.ConnectionString = _app.Configuration.GetConnectionString("DBConnect");

//_builder.Services.AddScoped<DialogService>();
//_builder.Services.AddScoped<NotificationService>();
//_builder.Services.AddScoped<TooltipService>();
//_builder.Services.AddScoped<ContextMenuService>();

_app.Run();