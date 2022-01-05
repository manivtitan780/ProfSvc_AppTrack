#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           BasicInfoPanel.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-04-2022 15:33
// Last Updated On:     01-04-2022 19:06
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Candidates;

/// <summary>
/// </summary>
public partial class BasicInfoPanel
{
	#region Properties

    /// <summary>
    /// </summary>
    [Parameter]
	public CandidateDetails ModelObject
	{
		get;
		set;
	}

    /// <summary>
    /// </summary>
    [Parameter]
	public EventCallback EditCandidate
	{
		get;
		set;
	}

    /// <summary>
    /// </summary>
    [Parameter]
	public EventCallback EditRating
	{
		get;
		set;
	}

    /// <summary>
    /// </summary>
    [Parameter]
	public EventCallback EditMPC
	{
		get;
		set;
	}

    /// <summary>
    /// </summary>
    [Parameter]
	public MarkupString SetupAddress
	{
		get;
		set;
	}

    /// <summary>
    /// </summary>
	[Parameter]
    public MarkupString SetEligibility
	{
		get;
		set;
	}

    /// <summary>
    /// </summary>
	[Parameter]
    public MarkupString SetExperience
	{
		get;
		set;
	}

    /// <summary>
    /// </summary>
	[Parameter]
    public MarkupString SetTaxTerm
	{
		get;
		set;
	}

    /// <summary>
    /// </summary>
	[Parameter]
    public MarkupString SetJobOption
	{
		get;
		set;
	}

    /// <summary>
    /// </summary>
	[Parameter]
    public MarkupString SetCommunication
	{
		get;
		set;
	}

    /// <summary>
    /// </summary>
	[Parameter]
    public MarkupString GetRatingDate
	{
		get;
		set;
	}

    /// <summary>
    /// </summary>
	[Parameter]
    public MarkupString GetRatingNote
	{
		get;
		set;
	}

    /// <summary>
    /// </summary>
	[Parameter]
    public MarkupString GetMPCNote
	{
		get;
		set;
	}

    /// <summary>
    /// </summary>
	[Parameter]
    public MarkupString GetMPCDate
	{
		get;
		set;
	}

	#endregion
}