using Syncfusion.Blazor.Spinner;

namespace ProfSvc_AppTrack.Pages.Controls.Candidates;

public partial class EditSkillDialog
{
    #region Fields

    private bool _value;

    #endregion

    [Parameter]
    public CandidateSkills ModelObject
    {
        get;
        set;
    } = new();

    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> Cancel
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
    public bool VisibleSkillCandidate
    {
        get => _value;
        set
        {
            if (_value == value)
            {
                return;
            }

            _value = value;
            VisibleSkillCandidateChanged.InvokeAsync(value);
        }
    }

    [Parameter]
    public bool SpinnerVisible
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback<bool> VisibleSkillCandidateChanged
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback<EditContext> SaveSkill
    {
        get;
        set;
    }

    public SfDialog Dialog
    {
        get;
        set;
    }

    private async Task SaveSkillDialog(EditContext args)
    {
        await Task.Delay(1);
        await Spinner.ShowAsync();
        await SaveSkill.InvokeAsync(args);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }

    private async Task CancelSkillDialog(MouseEventArgs args)
    {
        await Task.Delay(1);
        await Cancel.InvokeAsync(args);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }
}
