#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           ExperiencePanel.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-26-2022 19:30
// Last Updated On:     02-06-2022 19:28
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Candidates;

public partial class ExperiencePanel
{
    [Parameter]
    public EventCallback<int> DeleteExperience
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<int> EditExperience
    {
        get;
        set;
    }

    public SfGrid<CandidateExperience> GridExperience
    {
        get;
        set;
    }

    [Parameter]
    public List<CandidateExperience> ModelObject
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

    public CandidateExperience SelectedRow
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

    private async Task DeleteExperienceMethod(int id)
    {
        double _index = await GridExperience.GetRowIndexByPrimaryKeyAsync(id);
        await GridExperience.SelectRowAsync(_index);
        if (await JsRuntime.Confirm("Are you sure you want to delete this Experience?"))
        {
            await DeleteExperience.InvokeAsync(id);
        }
    }

    private async Task EditExperienceDialog(int id)
    {
        double _index = await GridExperience.GetRowIndexByPrimaryKeyAsync(id);
        await GridExperience.SelectRowAsync(_index);
        await EditExperience.InvokeAsync(id);
    }

    private void RowSelected(RowSelectEventArgs<CandidateExperience> experience)
    {
        if (experience != null)
        {
            SelectedRow = experience.Data;
        }
    }
}