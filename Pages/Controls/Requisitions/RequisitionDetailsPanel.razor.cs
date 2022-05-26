namespace ProfSvc_AppTrack.Pages.Controls.Requisitions
{
    public partial class RequisitionDetailsPanel
    {
        public SfDialog Dialog
        {
            get;
            set;
        }

        public SfSpinner Spinner
        {
            get;
            set;
        }

        [Parameter]
        public RequisitionDetails ModelObject
        {
            get;
            set;
        } = new();

        [Parameter]
        public List<KeyValues> Companies
        {
            get;
            set;
        }

        private async Task SaveRequisitionDialog(EditContext arg)
        {
            await Task.Delay(1);
        }

        [Parameter]
        public EventCallback<ChangeEventArgs<int, IntValues>> StateIDChanged
        {
            get;
            set;
        }

        [Parameter]
        public List<IntValues> States
        {
            get;
            set;
        }

        public List<KeyValues> AssignedTo
        {
            get;
            set;
        }

        public List<KeyValues> Eligibility
        {
            get;
            set;
        }

        public List<KeyValues> Priority
        {
            get;
            set;
        }

        public List<KeyValues> Experience
        {
            get;
            set;
        }

        public List<KeyValues> Education
        {
            get;
            set;
        }

        public List<KeyValues> JobOptions
        {
            get;
            set;
        }

        private async Task CancelDialog(MouseEventArgs arg)
        {
            await Task.Delay(1);
        }
    }
}
