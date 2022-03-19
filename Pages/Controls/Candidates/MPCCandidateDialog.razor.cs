#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           MPCCandidateDialog.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-26-2022 19:30
// Last Updated On:     02-04-2022 21:11
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Candidates;

/// <summary>
/// </summary>
public partial class MPCCandidateDialog
{
    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> CancelMPC
    {
        get;
        set;
    }

    [Parameter]
    public SfDialog Dialog
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public List<CandidateMPC> GridObject
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public Dictionary<string, object> HtmlAttributes1
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public CandidateRatingMPC ModelObject
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public int RowHeight
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback<EditContext> Save
    {
        get;
        set;
    }

    [Parameter]
    public bool SpinnerVisible
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback<TooltipEventArgs> ToolTipOpen
    {
        get;
        set;
    }

    private SfSpinner Spinner
    {
        get;
        set;
    }

    private async Task CancelMPCDialog(MouseEventArgs args)
    {
        await Task.Delay(1);
        await CancelMPC.InvokeAsync(args);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }

    private async Task SaveMPCDialog(EditContext obj)
    {
        await Task.Delay(1);
        await Spinner.ShowAsync();
        await Save.InvokeAsync(obj);
        await Task.Delay(1);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }
}