#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           CandidateEducation.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:03
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Data;

/// <summary>
/// </summary>
public class CandidateEducation
{
    #region Constructors

	/// <summary>
	/// </summary>
	public CandidateEducation()
    {
        ClearData();
    }

	/// <summary>
	/// </summary>
	/// <param name="id"></param>
	/// <param name="degree"></param>
	/// <param name="college"></param>
	/// <param name="state"></param>
	/// <param name="country"></param>
	/// <param name="year"></param>
	/// <param name="updatedBy"></param>
	public CandidateEducation(int id, string degree, string college, string state, string country, string year, string updatedBy)
    {
        ID = id;
        Degree = degree;
        College = college;
        State = state;
        Country = country;
        Year = year;
        UpdatedBy = updatedBy;
    }

    #endregion

    #region Properties

    public int ID
    {
        get;
        set;
    }

    public string College
    {
        get;
        set;
    }

    public string Country
    {
        get;
        set;
    }

    public string Degree
    {
        get;
        set;
    }

    public string State
    {
        get;
        set;
    }

    public string UpdatedBy
    {
        get;
        set;
    }

    public string Year
    {
        get;
        set;
    }

    #endregion

    #region Methods

	/// <summary>
	///     Creates a shallow copy of the current object.
	/// </summary>
	/// <returns></returns>
	public CandidateEducation Copy() => (CandidateEducation)MemberwiseClone();

	/// <summary>
	/// </summary>
	public void ClearData()
    {
        ID = 0;
        Degree = "";
        College = "";
        State = "";
        Country = "";
        Year = "";
        UpdatedBy = "";
    }

    #endregion
}