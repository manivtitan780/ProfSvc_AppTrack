#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           SubmitCandidate.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          08-10-2022 21:20
// Last Updated On:     08-10-2022 21:20
// *****************************************/

#endregion

using Microsoft.AspNetCore.Components.Forms;

namespace ProfSvc_AppTrack.Pages.Controls.Candidates;

public partial class SubmitCandidate
{
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

    public SfDialog Dialog
    {
        get;
        set;
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