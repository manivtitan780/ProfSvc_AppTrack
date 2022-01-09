namespace ProfSvc_AppTrack.Pages.Controls.Candidates
{
    public partial class ExperiencePanel
    {
        [Parameter]
        public List<CandidateExperience> ModelObject
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
        public int RowHeight
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
    }
}
