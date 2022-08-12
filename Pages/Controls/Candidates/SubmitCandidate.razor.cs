#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           SubmitCandidate.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          08-10-2022 21:20
// Last Updated On:     08-11-2022 15:27
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Candidates;

public partial class SubmitCandidate
{
    [Parameter]
    public EventCallback CancelSubmit
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
    public SubmitCandidateRequisition ModelObject
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<EditContext> SubmitCandidateToRequisition
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
        await CancelSubmit.InvokeAsync(args);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }

    private async Task SubmitCandidateToRequisitionDialog(EditContext editContext)
    {
        await Task.Delay(1);
        await Spinner.ShowAsync();
        await SubmitCandidateToRequisition.InvokeAsync(editContext);
        await Task.Delay(1);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }
}