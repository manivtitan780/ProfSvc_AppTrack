namespace ProfSvc_AppTrack.Pages.Controls.Candidates
{
    public partial class ActivityPanel
    {
        [Parameter]
        public List<CandidateActivity> ModelObject
        {
            get;
            set;
        }

        public SfGrid<CandidateActivity> GridActivity
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
        public EventCallback<int> EditActivity
        {
            get;
            set;
        }
    }
}
