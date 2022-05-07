﻿namespace ProfSvc_AppTrack.Pages.Controls.Requisitions
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

        private async Task CancelDialog(MouseEventArgs arg)
        {
            await Task.Delay(1);
        }
    }
}