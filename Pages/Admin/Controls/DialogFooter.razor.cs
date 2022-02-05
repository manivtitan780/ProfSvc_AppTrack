#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           DialogFooter.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-31-2022 21:25
// Last Updated On:     01-31-2022 21:27
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin.Controls;

public partial class DialogFooter
{
    [Parameter]
    public string Cancel
    {
        get;
        set;
    } = "Cancel";

    [Parameter]
    public EventCallback<MouseEventArgs> CancelMethod
    {
        get;
        set;
    }

    [Parameter]
    public string Save
    {
        get;
        set;
    } = "Save";
}