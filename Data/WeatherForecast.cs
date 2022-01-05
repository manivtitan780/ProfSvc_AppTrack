#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           WeatherForecast.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:03
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Data;

public class WeatherForecast
{
    #region Properties

    public DateTime Date
    {
        get;
        set;
    }

    public int TemperatureC
    {
        get;
        set;
    }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string Summary
    {
        get;
        set;
    }

    #endregion
}