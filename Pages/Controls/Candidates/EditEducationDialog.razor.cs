#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           EditEducationDialog.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          02-04-2022 21:43
// Last Updated On:     02-04-2022 21:44
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Candidates;

public partial class EditEducationDialog
{
    #region Fields

    private bool _value;

    #endregion

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
    public CandidateEducation ModelObject
    {
        get;
        set;
    } = new();

    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback<EditContext> SaveEducation
    {
        get;
        set;
    }

    public SfSpinner Spinner
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public bool VisibleEducationCandidate
    {
        get => _value;
        set
        {
            if (_value == value)
            {
                return;
            }

            _value = value;
            VisibleEducationCandidateChanged.InvokeAsync(value);
        }
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback<bool> VisibleEducationCandidateChanged
    {
        get;
        set;
    }

    private async Task CancelEducationDialog(MouseEventArgs args)
    {
        await Task.Delay(1);
        await Cancel.InvokeAsync(args);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }

    private async Task SaveEducationDialog(EditContext args)
    {
        await Task.Delay(1);
        await Spinner.ShowAsync();
        await SaveEducation.InvokeAsync(args);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }
}