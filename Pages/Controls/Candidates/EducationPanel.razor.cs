#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           EducationPanel.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-26-2022 19:30
// Last Updated On:     02-05-2022 19:08
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Candidates;

public partial class EducationPanel
{
    [Parameter]
    public EventCallback<int> DeleteEducation
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<int> EditEducation
    {
        get;
        set;
    }

    public SfGrid<CandidateEducation> GridEducation
    {
        get;
        set;
    }

    [Parameter]
    public List<CandidateEducation> ModelObject
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

    public CandidateEducation SelectedRow
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

    private async Task DeleteEducationMethod(int id)
    {
        double _index = await GridEducation.GetRowIndexByPrimaryKeyAsync(id);
        await GridEducation.SelectRowAsync(_index);
        if (await JsRuntime.Confirm("Are you sure you want to delete this Education?"))
        {
            await DeleteEducation.InvokeAsync(id);
        }
    }

    private async Task EditEducationDialog(int id)
    {
        double _index = await GridEducation.GetRowIndexByPrimaryKeyAsync(id);
        await GridEducation.SelectRowAsync(_index);
        await EditEducation.InvokeAsync(id);
    }

    private void RowSelected(RowSelectEventArgs<CandidateEducation> education)
    {
        if (education != null)
        {
            SelectedRow = education.Data;
        }
    }
}