#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           NotesPanel.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-26-2022 19:30
// Last Updated On:     02-07-2022 16:17
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Candidates;

public partial class NotesPanel
{
    [Parameter]
    public EventCallback<int> DeleteNotes
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<int> EditNotes
    {
        get;
        set;
    }

    public SfGrid<CandidateNotes> GridNotes
    {
        get;
        set;
    }

    [Parameter]
    public List<CandidateNotes> ModelObject
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

    public CandidateNotes SelectedRow
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

    private async Task DeleteNotesMethod(int id)
    {
        double _index = await GridNotes.GetRowIndexByPrimaryKey(id);
        await GridNotes.SelectRowAsync(_index);
        if (await JsRuntime.Confirm("Are you sure you want to delete this Notes?"))
        {
            await DeleteNotes.InvokeAsync(id);
        }
    }

    private async Task EditNotesDialog(int id)
    {
        double _index = await GridNotes.GetRowIndexByPrimaryKey(id);
        await GridNotes.SelectRowAsync(_index);
        await EditNotes.InvokeAsync(id);
    }

    private void RowSelected(RowSelectEventArgs<CandidateNotes> note)
    {
        if (note != null)
        {
            SelectedRow = note.Data;
        }
    }
}