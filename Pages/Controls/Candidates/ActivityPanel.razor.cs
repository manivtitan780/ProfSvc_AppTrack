#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           ActivityPanel.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-26-2022 19:30
// Last Updated On:     02-21-2022 21:33
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Candidates;

public partial class ActivityPanel
{
    [Parameter]
    public EventCallback<int> EditActivity
    {
        get;
        set;
    }

    [Parameter]
    public List<CandidateActivity> ModelActivityObject
    {
        get;
        set;
    }

    [Parameter]
    public int RowHeight
    {
        get;
        set;
    }

    public CandidateActivity SelectedRow
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<int> UndoCandidateActivity
    {
        get;
        set;
    }

    private SfGrid<CandidateActivity> GridActivity
    {
        get;
        set;
    }

    [Inject]
    private IJSRuntime JsRuntime
    {
        get;
        set;
    }

    [Parameter]
    public bool IsRequisition
    {
        get;
        set;
    }

    private async Task EditActivityDialog(int id)
    {
        double _index = await GridActivity.GetRowIndexByPrimaryKeyAsync(id);
        await GridActivity.SelectRowAsync(_index);
        await EditActivity.InvokeAsync(id);
    }

    private void RowSelected(RowSelectEventArgs<CandidateActivity> activity)
    {
        if (activity != null)
        {
            SelectedRow = activity.Data;
        }
    }

    private async Task UndoActivity(int activityID)
    {
        await Task.Delay(1);
        double _index = await GridActivity.GetRowIndexByPrimaryKeyAsync(activityID);
        await GridActivity.SelectRowAsync(_index);
        if (await JsRuntime.Confirm("Are you sure you want to undo the previous Submission Status?\n Note: This operation cannot be reversed and the Status has to be submitted again."))
        {
            await UndoCandidateActivity.InvokeAsync(activityID);
        }
    }
}