using ActionCompleteEventArgs = Syncfusion.Blazor.Inputs.ActionCompleteEventArgs;

namespace ProfSvc_AppTrack.Pages.Controls.Requisitions
{
    public partial class AddRequisitionDocument
    {
        /// <summary>
        /// </summary>
        [Parameter]
        public EventCallback<MouseEventArgs> Cancel
        {
            get;
            set;
        }

        public SfDialog Dialog
        {
            get;
            set;
        }

        [Parameter]
        public RequisitionDocuments ModelDocumentObject
        {
            get;
            set;
        }

        /// <summary>
        /// </summary>
        [Parameter]
        public EventCallback<UploadChangeEventArgs> OnFileUpload
        {
            get;
            set;
        }

        /// <summary>
        /// </summary>
        [Parameter]
        public EventCallback<EditContext> Save
        {
            get;
            set;
        }

        public DialogFooter DialogFooter
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
        public EventCallback<BeforeUploadEventArgs> BeforeUpload
        {
            get;
            set;
        }

        [Parameter]
        public EventCallback<ActionCompleteEventArgs> AfterUpload
        {
            get;
            set;
        }

        [Parameter]
        public List<IntValues> DocumentTypes
        {
            get;
            set;
        }

        private async Task CancelDocumentDialog(MouseEventArgs args)
        {
            await Task.Delay(1);
            await Cancel.InvokeAsync(args);
            await Spinner.HideAsync();
            await Dialog.HideAsync();
        }

        private async Task SaveDocumentDialog(EditContext args)
        {
            await Task.Delay(1);
            await Spinner.ShowAsync();
            await Save.InvokeAsync(args);
            await Spinner.HideAsync();
            await Dialog.HideAsync();
        }
    }
}
