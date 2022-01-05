#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           EditCandidateDialog.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-04-2022 15:31
// Last Updated On:     01-04-2022 16:10
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Candidates;

public partial class EditCandidateDialog
{
    #region Fields

    private bool _value;

    #endregion

    #region Properties

    [Parameter]
    public bool VisibleCandidate
    {
        get => _value;
        set
        {
            if (_value == value)
            {
                return;
            }

            _value = value;
            VisibleCandidateChanged.InvokeAsync(value);
        }
    }

    [Parameter]
    public CandidateDetails ModelObject
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
    public EventCallback CancelCandidate
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<bool> VisibleCandidateChanged
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
    public EventCallback<EditContext> SaveCandidate
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<TooltipEventArgs> ToolTipOpen
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

    [Parameter]
    public IEnumerable<KeyValues> Communication
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
    public IEnumerable<KeyValues> JobOptions
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
    public List<ToolbarItemModel> Tools
    {
        get;
        set;
    }

    #endregion
}