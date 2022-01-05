#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           _Host.cshtml.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 19:59
// Last Updated On:     01-04-2022 16:11
// *****************************************/

#endregion

#region Using

#endregion

namespace ProfSvc_AppTrack.Pages;

public class HostModel : PageModel
{
    #region Constructors

    public HostModel(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
    {
        _httpContextAccssor = httpContextAccessor;
        _configuration = configuration;
    }

    #endregion

    #region Fields

    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccssor;

    #endregion

    #region Properties

    public string ConnectionString { get; set; }

    public string IPAddress { get; set; }

    public string Test { get; set; }

    public string UserAgent { get; set; }

    #endregion

    #region Methods

    // The following links may be useful for getting the IP Adress:
    // https://stackoverflow.com/questions/35441521/remoteipaddress-is-always-null
    // https://stackoverflow.com/questions/28664686/how-do-i-get-client-ip-address-in-asp-net-core

    public void OnGet()
    {
        UserAgent = Request.Headers["User-Agent"];
        // Note that the RemoteIpAddress property returns an IPAdrress object 
        // which you can query to get required information. Here, however, we pass 
        // its string representation
        if (Request.HttpContext.Connection.RemoteIpAddress != null)
        {
            IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        Test = "ffff";
        ConnectionString = _configuration.GetConnectionString("DBConnect");
    }

    #endregion
}