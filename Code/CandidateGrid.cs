#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           CandidateGrid.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-17-2021 20:58
// Last Updated On:     01-04-2022 16:03
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Code;

/// <summary>
/// </summary>
public class CandidateGrid
{
    #region Constructors

    /// <summary>
    /// </summary>
    public CandidateGrid()
    {
        Page = 1;
        ItemCount = 50;
        Name = "";
    }

    /// <summary>
    /// </summary>
    /// <param name="page"></param>
    /// <param name="itemCount"></param>
    /// <param name="name"></param>
    public CandidateGrid(int page, int itemCount, string name)
    {
        Page = page;
        ItemCount = itemCount;
        Name = name;
    }

    #endregion

    #region Properties

    /// <summary>
    /// </summary>
    public int Page
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public int ItemCount
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public string Name
    {
        get;
        set;
    }

    #endregion
}