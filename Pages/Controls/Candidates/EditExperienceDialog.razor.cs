#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           EditExperienceDialog.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          02-06-2022 19:38
// Last Updated On:     02-06-2022 19:44
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Candidates;

public partial class EditExperienceDialog
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
    public CandidateExperience ModelObject
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback<EditContext> SaveExperience
    {
        get;
        set;
    }

    public SfSpinner Spinner
    {
        get;
        set;
    }

    private async Task CancelExperienceDialog(MouseEventArgs args)
    {
        await Task.Delay(1);
        await Cancel.InvokeAsync(args);
        await Spinner.HideAsync();
        await Dialog.HideAsync();

    }

    private async Task SaveExperienceDialog(EditContext editContext)
    {
        await Task.Delay(1);
        await Spinner.ShowAsync();
        await SaveExperience.InvokeAsync(editContext);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }
}