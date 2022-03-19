namespace ProfSvc_AppTrack.Pages.Controls.Candidates;

public partial class EditNotesDialog
{
    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> Cancel
    {
        get;
        set;
    }

    [Parameter]
    public SfDialog Dialog
    {
        get;
        set;
    }

    [Parameter]
    public CandidateNotes ModelObject
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

    [Parameter]
    public SfSpinner Spinner
    {
        get;
        set;
    }

    private async Task CancelNotesDialog(MouseEventArgs args)
    {
        await Task.Delay(1);
        await Cancel.InvokeAsync(args);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }

    private async Task SaveNotesDialog(EditContext editContext)
    {
        await Task.Delay(1);
        await Spinner.ShowAsync();
        await Save.InvokeAsync(editContext);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }
}