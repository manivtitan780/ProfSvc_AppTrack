#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           CandidateMPC.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-30-2021 15:18
// Last Updated On:     01-04-2022 16:03
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Data;

/// <summary>
/// </summary>
public class CandidateMPC
{
    #region Constructors

    /// <summary>
    /// </summary>
    public CandidateMPC()
    {
        ClearData();
    }

    /// <summary>
    /// </summary>
    /// <param name="date"></param>
    /// <param name="user"></param>
    /// <param name="mpc"></param>
    /// <param name="comments"></param>
    public CandidateMPC(DateTime date, string user, bool mpc, string comments)
    {
        Date = date;
        User = user;
        MPC = mpc;
        Comments = comments;
    }

    #endregion

    #region Properties

    /// <summary>
    /// </summary>
    public bool MPC
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public DateTime Date
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string User
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string Comments
    {
        get;
        set;
    }

    #endregion

    #region Methods

    /// <summary>
    /// </summary>
    public void ClearData()
    {
        Date = DateTime.Today;
        User = "";
        MPC = false;
        Comments = "";
    }

    #endregion
}