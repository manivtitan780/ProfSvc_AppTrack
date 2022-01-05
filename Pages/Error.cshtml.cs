#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           Error.cshtml.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 19:58
// Last Updated On:     01-04-2022 16:11
// *****************************************/

#endregion

#region Using

using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

#endregion

namespace ProfSvc_AppTrack.Pages;

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true), IgnoreAntiforgeryToken]
public class ErrorModel : PageModel
{
    #region Constructors

    public ErrorModel(ILogger<ErrorModel> logger) => _logger = logger;

    #endregion

    #region Fields

    private readonly ILogger<ErrorModel> _logger;

    #endregion

    #region Properties

    public bool ShowRequestID => !string.IsNullOrEmpty(RequestID);

    public string RequestID { get; set; }

    #endregion

    #region Methods

    public void OnGet()
    {
        RequestID = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    }

    #endregion
}