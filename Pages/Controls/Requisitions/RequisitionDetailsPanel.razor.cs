using ActionCompleteEventArgs = Syncfusion.Blazor.Inputs.ActionCompleteEventArgs;

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
        public List<Company> Companies
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

        public List<IntValues> AssignedTo
        {
            get;
            set;
        }

        public List<IntValues> Eligibility
        {
            get;
            set;
        }

        public List<IntValues> Priority
        {
            get;
            set;
        }

        public List<IntValues> Experience
        {
            get;
            set;
        }

        public List<IntValues> Education
        {
            get;
            set;
        }

        public List<KeyValues> JobOptions
        {
            get;
            set;
        }

        public List<CompanyContact> CompanyContacts
        {
            get;
            set;
        }

        public List<KeyValues> DurationCode
        {
            get;
            set;
        }

        private async Task CancelDialog(MouseEventArgs arg)
        {
            await Task.Delay(1);
        }

        private async Task OnFileUpload(UploadChangeEventArgs arg)
        {
            await Task.Delay(1);
        }

        private async Task BeforeUpload(BeforeUploadEventArgs arg)
        {
            await Task.Delay(1);
        }

        private async Task AfterUpload(ActionCompleteEventArgs arg)
        {
            await Task.Delay(1);
        }
    }
}
