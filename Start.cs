#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           Start.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 20:14
// Last Updated On:     01-04-2022 16:11
// *****************************************/

#endregion

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace ProfSvc_AppTrack;

public class Start
{
    public static string ApiHost
    {
        get;
        set;
    }

    public static string ConnectionString
    {
        get;
        set;
    }

    public static IMemoryCache MemCache
    {
        get;
        set;
    }
}