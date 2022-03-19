#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           RatingCandidateDialog.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-26-2022 19:30
// Last Updated On:     02-02-2022 18:49
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Candidates;

/// <summary>
/// </summary>
public partial class RatingCandidateDialog
{
    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback CancelRating
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public List<CandidateRating> GridObject
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

    public SfDialog Dialog
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

    public SfSpinner Spinner
    {
        get;
        set;
    }

    private async Task CancelRatingDialog(MouseEventArgs args)
    {
        await Task.Delay(1);
        await CancelRating.InvokeAsync(args);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }

    private async Task SaveRatingDialog(EditContext obj)
    {
        await Task.Delay(1);
        await Spinner.ShowAsync();
        await Save.InvokeAsync(obj);
        await Task.Delay(1);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }

}