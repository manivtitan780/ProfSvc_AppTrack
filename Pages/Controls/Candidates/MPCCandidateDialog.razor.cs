#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           MPCCandidateDialog.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-04-2022 15:31
// Last Updated On:     01-04-2022 16:10
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Candidates;

/// <summary>
/// </summary>
public partial class MPCCandidateDialog
{
    #region Fields

    private bool _value;

    #endregion

    #region Properties

    /// <summary>
    /// </summary>
    [Parameter]
    public bool VisibleMPCCandidate
    {
        get => _value;
        set
        {
            if (_value == value)
            {
                return;
            }

            _value = value;
            VisibleMPCCandidateChanged.InvokeAsync(value);
        }
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public CandidateRatingMPC ModelObject
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public Dictionary<string, object> HtmlAttributes1
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback CancelMPC
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback<bool> VisibleMPCCandidateChanged
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback<EditContext> SaveMPC
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

    /// <summary>
    /// </summary>
    [Parameter]
    public int RowHeight
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public List<CandidateMPC> GridObject
    {
        get;
        set;
    }

    #endregion
}