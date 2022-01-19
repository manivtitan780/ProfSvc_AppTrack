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
    } = new CandidateSkills();

    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback Cancel
    {
        get;
        set;
    }

    public SfSpinner SpinnerSkill
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

    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback<TooltipEventArgs> ToolTipOpen
    {
        get;
        set;
    }
}
