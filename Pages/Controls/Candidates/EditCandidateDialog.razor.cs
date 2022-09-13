#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           EditCandidateDialog.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-26-2022 19:30
// Last Updated On:     02-04-2022 21:13
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Candidates;

public partial class EditCandidateDialog
{
    [Parameter]
    public EventCallback CancelCandidate
    {
        get;
        set;
    }

    [Parameter]
    public IEnumerable<KeyValues> Communication
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
    public IEnumerable<IntValues> Eligibility
    {
        get;
        set;
    }

    [Parameter]
    public IEnumerable<IntValues> Experience
    {
        get;
        set;
    }

    public DialogFooter Footer
    {
        get;
        set;
    }

    [Parameter]
    public Dictionary<string, object> HtmlAttributes
    {
        get;
        set;
    }

    [Parameter]
    public Dictionary<string, object> HtmlAttributes1
    {
        get;
        set;
    }

    [Parameter]
    public IEnumerable<KeyValues> JobOptions
    {
        get;
        set;
    }

    [Parameter]
    public CandidateDetails ModelObject
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

    [Parameter]
    public IEnumerable<KeyValues> TaxTerms
    {
        get;
        set;
    }

    [Parameter]
    public List<ToolbarItemModel> Tools
    {
        get;
        set;
    }

    private SfSpinner Spinner
    {
        get;
        set;
    }

    private async Task CancelDialog(MouseEventArgs args)
    {
        await Task.Delay(1);
        await CancelCandidate.InvokeAsync(args);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }

    private async Task SaveCandidateDialog(EditContext editContext)
    {
        await Task.Delay(1);
        await Spinner.ShowAsync();
        await Save.InvokeAsync(editContext);
        await Task.Delay(1);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }
}