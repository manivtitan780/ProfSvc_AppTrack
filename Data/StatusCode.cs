﻿#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           StatusCode.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:03
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Data;

/// <summary>
///     Summary description for Status
/// </summary>
public class StatusCode
{
    #region Constructors

    public StatusCode()
    {
        ClearData();
    }

	/// <summary>
	///     Initializes the StatusCode class with supplied values.
	/// </summary>
	/// <param name="id">ID of the Status Code.</param>
	/// <param name="code">Status Code.</param>
	/// <param name="status">Description of Status Code.</param>
	/// <param name="description">Description of the Status Code.</param>
	/// <param name="icon">Icon to show for Status Code where applicable.</param>
	/// <param name="appliesTo">Applies to which entity.</param>
	/// <param name="appliesToCode">Code of the entity to which this status code applies.</param>
	/// <param name="submitCandidate">
	///     Can this Status Code submit a candidate. THis will have effect only on Requisitions. Will
	///     be ignored in other entities.
	/// </param>
	/// <param name="showCommission">Show the Commission calculation screen? Applicable only for Submissions.</param>
	/// <param name="color">Defines the color the Status Code is shown in.</param>
	/// <param name="createdDate">Date on which the Status Code was created.</param>
	/// <param name="updatedDate">Date on which the Status Code was last updated.</param>
	public StatusCode(int id, string code, string status, string description, string appliesToCode, string appliesTo, string icon, bool submitCandidate,
					  bool showCommission,
					  string color, string createdDate, string updatedDate)
    {
        ID = id;
        Code = code;
        Description = description;
        Status = status;
        Icon = icon;
        AppliesTo = appliesTo;
        AppliesToCode = appliesToCode;
        SubmitCandidate = submitCandidate;
        ShowCommission = showCommission;
        Color = color;
        CreatedDate = createdDate;
        UpdatedDate = updatedDate;
    }

    #endregion

    #region Properties

	/// <summary>
	///     Show the Commission calculation screen? Applicable only for Submissions.
	/// </summary>
	public bool ShowCommission
    {
        get;
        set;
    }

	/// <summary>
	///     Can this Status Code submit a candidate. THis will have effect only on Requisitions. Will be ignored in other
	///     entities.
	/// </summary>
	public bool SubmitCandidate
    {
        get;
        set;
    }

	/// <summary>
	///     ID of the Status Code.
	/// </summary>
	public int ID
    {
        get;
        set;
    }

	/// <summary>
	///     Applies to which entity.
	/// </summary>
	public string AppliesTo
    {
        get;
        set;
    }

	/// <summary>
	///     Code of the entity to which this status code applies.
	/// </summary>
	public string AppliesToCode
    {
        get;
        set;
    }

	/// <summary>
	///     Status Code.
	/// </summary>
	[Required(ErrorMessage = "Status Code is required."), StringLength(3, MinimumLength = 1, ErrorMessage = "Status Code is required.")]
    public string Code
    {
        get;
        set;
    }

	/// <summary>
	///     Defines the color the Status Code is shown in.
	/// </summary>
	public string Color
    {
        get;
        set;
    }

	/// <summary>
	///     Date on which the Status Code was created.
	/// </summary>
	public string CreatedDate
    {
        get;
        set;
    }

	/// <summary>
	///     Description of the Status Code.
	/// </summary>
	[StringLength(100, MinimumLength = 0, ErrorMessage = "Description should not be more than 100 characters long.")]
    public string Description
    {
        get;
        set;
    }

	/// <summary>
	///     Icon to show for Status Code where applicable.
	/// </summary>
	public string Icon
    {
        get;
        set;
    }

	/// <summary>
	///     Description of Status Code.
	/// </summary>
	[Required(ErrorMessage = "Status is required."), StringLength(50, MinimumLength = 1, ErrorMessage = "Status should be between 1 and 50 characters long.")]
    public string Status
    {
        get;
        set;
    }

	/// <summary>
	///     Date on which the Status Code was last updated.
	/// </summary>
	public string UpdatedDate
    {
        get;
        set;
    }

    #endregion

    #region Methods

	/// <summary>
	///     Initializes the StatusCode class with default values.
	/// </summary>
	/// <summary>
	///     Creates a shallow copy of the current object.
	/// </summary>
	/// <returns></returns>
	public StatusCode Copy() => (StatusCode)MemberwiseClone();

	/// <summary>
	///     Clears this object data.
	/// </summary>
	public void ClearData()
    {
        Code = "";
        Status = "";
        Icon = "";
        AppliesTo = "";
        AppliesToCode = "";
        SubmitCandidate = false;
        ShowCommission = false;
        Color = "#222222";
        CreatedDate = "";
        UpdatedDate = "";
    }

    #endregion
}