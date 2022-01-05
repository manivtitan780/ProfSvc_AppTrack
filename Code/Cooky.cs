#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           Cooky.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:03
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Code;

[Serializable]
public class Cooky
{
    #region Properties

    /// <summary>
    ///     User Last Login Date and Time
    /// </summary>
    public DateTime Login
    {
        get;
        set;
    }

    /// <summary>
    ///     User ID
    /// </summary>
    public string UserID
    {
        get;
        set;
    }

    /// <summary>
    ///     User Role ID
    /// </summary>
    public string RoleID
    {
        get;
        set;
    }

    /// <summary>
    ///     User Role Description
    /// </summary>
    public string Role
    {
        get;
        set;
    }

    /// <summary>
    ///     User Email Address
    /// </summary>
    public string Email
    {
        get;
        set;
    }

    /// <summary>
    ///     User First Name
    /// </summary>
    public string FirstName
    {
        get;
        set;
    }

    /// <summary>
    ///     User Last Name
    /// </summary>
    public string LastName
    {
        get;
        set;
    }

    /// <summary>
    ///     User Last Login IP
    /// </summary>
    public string LoginIp
    {
        get;
        set;
    }

    #endregion
}