#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           DialogFooter.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-31-2022 21:25
// Last Updated On:     09-14-2022 15:41
// *****************************************/

#endregion

#region Using

using Syncfusion.Blazor.Buttons;

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

    public SfButton CancelButton
    {
        get;
        set;
    }

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

    public SfButton SaveButton
    {
        get;
        set;
    }

    public bool ButtonsDisabled()
    {
        return CancelButton.Disabled;
    }

    public void DisableButtons()
    {
        CancelButton.Disabled = true;
        SaveButton.Disabled = true;
    }

    public void EnableButtons()
    {
        CancelButton.Disabled = false;
        SaveButton.Disabled = false;
    }
}