#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           DialogFooter.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-31-2022 21:25
// Last Updated On:     02-12-2022 19:44
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin.Controls;

/// <summary>
/// </summary>
public partial class DialogFooter
{
    /// <summary>
    /// </summary>
    [Parameter]
    public string Cancel
    {
        get;
        set;
    } = "Cancel";

    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> CancelMethod
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public string Save
    {
        get;
        set;
    } = "Save";
}