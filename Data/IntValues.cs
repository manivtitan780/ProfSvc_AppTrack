#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           IntValues.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:03
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Data;

public class IntValues
{
    #region Constructors

    public IntValues()
    {
        ClearData();
    }

    public IntValues(int key, string value)
    {
        Key = key;
        Value = value;
    }

    #endregion

    #region Properties

    public int Key
    {
        get;
        set;
    }

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
	public IntValues Copy() => (IntValues)MemberwiseClone();

	/// <summary>
	///     Clears this object data.
	/// </summary>
	public void ClearData()
    {
        Key = -1;
        Value = "";
    }

    #endregion
}