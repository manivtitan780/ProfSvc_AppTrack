#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           SkillPanel.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-26-2022 19:30
// Last Updated On:     02-05-2022 19:09
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Candidates;

public partial class SkillPanel
{
    [Parameter]
    public EventCallback<int> DeleteSkill
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<int> EditSkill
    {
        get;
        set;
    }

    public SfGrid<CandidateSkills> GridSkill
    {
        get;
        set;
    }

    [Parameter]
    public CandidateDetails ModelObject
    {
        get;
        set;
    }

    [Parameter]
    public List<CandidateSkills> ModelSkillObject
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

    public CandidateSkills SelectedRow
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

    private async Task DeleteSkillMethod(int id)
    {
        double _index = await GridSkill.GetRowIndexByPrimaryKeyAsync(id);
        await GridSkill.SelectRowAsync(_index);
        if (await JsRuntime.Confirm("Are you sure you want to delete this Skill?"))
        {
            await DeleteSkill.InvokeAsync(id);
        }
    }

    private async Task EditSkillDialog(int id)
    {
        double _index = await GridSkill.GetRowIndexByPrimaryKeyAsync(id);
        await GridSkill.SelectRowAsync(_index);
        await EditSkill.InvokeAsync(id);
    }

    private void RowSelected(RowSelectEventArgs<CandidateSkills> skill)
    {
        if (skill != null)
        {
            SelectedRow = skill.Data;
        }
    }
}