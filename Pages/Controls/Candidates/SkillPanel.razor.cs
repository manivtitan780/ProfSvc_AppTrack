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
    }
}
