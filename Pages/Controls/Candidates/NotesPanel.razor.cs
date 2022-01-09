namespace ProfSvc_AppTrack.Pages.Controls.Candidates
{
    public partial class NotesPanel
    {
         [Parameter]
        public List<CandidateNotes> ModelObject
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
        public int RowHeight
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
   }
}
