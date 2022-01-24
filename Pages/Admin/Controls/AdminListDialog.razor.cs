
namespace ProfSvc_AppTrack.Pages.Admin.Controls
{
    public partial class AdminListDialog
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
        public string HeaderString
        {
            get;
            set;
        }

        [Parameter]
        public AdminList Model
        {
            get;
            set;
        }

        [Parameter]
        public EventCallback<EditContext> Save
        {
            get;
            set;
        }

        [Parameter]
        public EventCallback<MouseEventArgs> Cancel
        {
            get;
            set;
        }

        public void ToolTipOpen(TooltipEventArgs args)
        {
            //new EditContext(Model).Validate();
            args.Cancel = !args.HasText;
        }

        [Parameter]
        public Dictionary<string, object> HtmlAttributes
        {
            get;
            set;
        }

        [Parameter]
        public string Placeholder
        {
            get;
            set;
        }

        public async Task SaveAdminList(EditContext editContext)
        {
            await Task.Delay(1);
            await Spinner.ShowAsync();
            await Save.InvokeAsync(editContext);
            await Task.Delay(1);
            await Spinner.HideAsync();
            await Dialog.HideAsync();
        }

        public async Task CancelAdminList(MouseEventArgs args)
        {
            await Task.Delay(1);
            await Cancel.InvokeAsync(args);
            await Spinner.HideAsync();
            await Dialog.HideAsync();
        }
    }
}
