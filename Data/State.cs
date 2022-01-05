#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           State.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:03
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Data;

/// <summary>
///     Summary description for States
/// </summary>
public class State
{
    #region Constructors

    public State()
    {
        ClearData();
    }

	/// <summary>
	///     Initialize the States Class with supplied data.
	/// </summary>
	/// <param name="id">ID of the State.</param>
	/// <param name="state">Name of the State.</param>
	/// <param name="code">Code for the State.</param>
	public State(int id, string state, string code)
    {
        ID = id;
        StateName = state;
        Code = code;
    }

    #endregion

    #region Properties

	/// <summary>
	///     ID of the State.
	/// </summary>
	public int ID
    {
        get;
        set;
    }

	/// <summary>
	///     Code for the State.
	/// </summary>
	[StateCodeExistsValidation]
    public string Code
    {
        get;
        set;
    }

	/// <summary>
	///     Name of the State.
	/// </summary>
	[Required(ErrorMessage = "State Name is required."),
	 StringLength(50, MinimumLength = 1, ErrorMessage = "State Name should be between 1 and 50 characters.")]
    public string StateName
    {
        get;
        set;
    }

    #endregion

    #region Methods

	/// <summary>
	///     Initialize the States Class with default data.
	/// </summary>
	/// <summary>
	///     Creates a shallow copy of the current object.
	/// </summary>
	/// <returns></returns>
	public State Copy() => (State)MemberwiseClone();

	/// <summary>
	///     Clears this object data.
	/// </summary>
	public void ClearData()
    {
        ID = 0;
        StateName = "";
        Code = "";
    }

    #endregion
}