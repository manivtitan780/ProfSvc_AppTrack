namespace ProfSvc_AppTrack.Pages.Controls.Candidates
{
    public partial class SkillPanel
    {
        [Parameter]
        public CandidateDetails ModelObject
        {
            get;
            set;
        }

        public CandidateSkills SelectedRow
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
        
        public SfGrid<CandidateSkills> GridSkill
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

        private void RowSelected(RowSelectEventArgs<CandidateSkills> skill)
        {
            if (skill != null)
            {
                SelectedRow = skill.Data;
            }
        }

        private async void EditSkillDialog(int id)
        {
            double _index = await GridSkill.GetRowIndexByPrimaryKey(id);
            await GridSkill.SelectRowAsync(_index);
            await EditSkill.InvokeAsync(id);
        }
    }
}
