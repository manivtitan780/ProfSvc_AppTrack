namespace ProfSvc_AppTrack.Pages.Controls.Candidates
{
    public partial class EducationPanel
    {
        [Parameter]
        public List<CandidateEducation> ModelObject
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
        public int RowHeight
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
    }
}
