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

using Microsoft.Extensions.Caching.Memory;

using Radzen;

#endregion

WebApplicationBuilder _builder = WebApplication.CreateBuilder(args);

// Add services to the container.
_builder.Services.AddSyncfusionBlazor();
_builder.Services.AddScoped<DialogService>();
_builder.Services.AddScoped<NotificationService>();
_builder.Services.AddScoped<TooltipService>();
_builder.Services.AddScoped<ContextMenuService>();
_builder.Services.AddRazorPages();
_builder.Services.AddServerSideBlazor();
//_builder.Services.AddSingleton<WeatherForecastService>();
_builder.Services.AddHttpContextAccessor();
_builder.Services.AddHttpClient();
_builder.Services.AddBlazoredLocalStorage();                                                            // local storage
_builder.Services.AddBlazoredLocalStorage(config => config.JsonSerializerOptions.WriteIndented = true); // local storage
_builder.Services.AddMemoryCache();
_builder.Services.AddSignalR(e =>
                                {
                                    e.MaximumReceiveMessageSize = 10485760;
                                    e.EnableDetailedErrors = true;
                                });

WebApplication _app = _builder.Build();

// Configure the HTTP request pipeline.
SyncfusionLicenseProvider.RegisterLicense("NTUxOTI3QDMxMzkyZTM0MmUzMGY3TWJ0TWx3Z3lXY2ZaOG1mb1pBNFhpamYxWm92d0N1RGFSTmRFQUd4NGM9");

if (!_app.Environment.IsDevelopment())
{
    _app.UseExceptionHandler("/Error");
}

_app.UseStaticFiles();
_app.UseRouting();

_app.MapBlazorHub();
_app.MapFallbackToPage("/_Host"); 

//Start.SetCache();
IMemoryCache _memoryCache = new MemoryCache(new MemoryCacheOptions());
if (_memoryCache.TryGetValue("Skills", out string _skillObject))
{
    return;
}

MemoryCacheEntryOptions _cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(60));
_skillObject = DateTime.Now.ToLongTimeString();
_memoryCache.Set("Skills", _skillObject, _cacheOptions);
Start.MemCache = _memoryCache;

Start.ApiHost = _app.Configuration.GetValue(typeof(string), "APIHost").ToString();
Start.ConnectionString = _app.Configuration.GetConnectionString("DBConnect");



_app.Run();