#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           Designation.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:03
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Data;

/// <summary>
///     Class to store the Designation.
/// </summary>
public class Designation
{
    #region Constructors

    public Designation()
    {
        ClearData();
    }

	/// <summary>
	///     Initializes the Designation Class.
	/// </summary>
	/// <param name="id">ID of the Designation.</param>
	/// <param name="designation">Name of the Designation.</param>
	public Designation(int id, string designation)
    {
        ID = id;
        DesignationValue = designation;
    }

    #endregion

    #region Properties

	/// <summary>
	///     ID of the Designation.
	/// </summary>
	public int ID
    {
        get;
        set;
    }

	/// <summary>
	///     Name of the Designation.
	/// </summary>
	public string DesignationValue
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
	public CandidateDetails Copy() => (CandidateDetails)MemberwiseClone();

	/// <summary>
	///     Clears this object data.
	/// </summary>
	public void ClearData()
    {
        ID = 0;
        DesignationValue = "";
    }

    #endregion
}