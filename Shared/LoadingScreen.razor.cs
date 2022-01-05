#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           LoadingScreen.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 19:59
// Last Updated On:     01-04-2022 16:11
// *****************************************/

#endregion

#region Using

#endregion

namespace ProfSvc_AppTrack.Shared;

public partial class LoadingScreen
{
    #region Fields

    private bool contentLoaded;

    #endregion

    #region Properties

    [Parameter]
    public RenderFragment ChildContent
    {
        get;
        set;
    }

    #endregion

    #region Methods

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(2);
        contentLoaded = true;
    }

    #endregion
}