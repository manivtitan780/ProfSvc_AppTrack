#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           KeyValues.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:03
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Data;

public class KeyValues
{
    #region Constructors

	/// <summary>
	/// </summary>
	public KeyValues()
    {
        ClearData();
    }

	/// <summary>
	/// </summary>
	/// <param name="key"></param>
	/// <param name="value"></param>
	public KeyValues(string key, string value)
    {
        Key = key;
        Value = value;
    }

    #endregion

    #region Properties

	/// <summary>
	/// </summary>
	public string Key
    {
        get;
        set;
    }

	/// <summary>
	/// </summary>
	public string Value
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
	public KeyValues Copy() => (KeyValues)MemberwiseClone();

	/// <summary>
	///     Clears this object data.
	/// </summary>
	public void ClearData()
    {
        Key = "";
        Value = "";
    }

    #endregion
}