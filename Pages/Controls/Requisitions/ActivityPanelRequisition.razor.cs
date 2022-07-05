#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           ActivityPanelRequisition.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          04-25-2022 15:52
// Last Updated On:     04-25-2022 15:53
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Requisitions;

public partial class ActivityPanelRequisition
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