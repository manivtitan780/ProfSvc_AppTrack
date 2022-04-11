#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           BasicInfoRequisitionPanel.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          04-04-2022 19:57
// Last Updated On:     04-04-2022 20:08
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Requisitions;

public partial class BasicInfoRequisitionPanel
{
    [Parameter]
    public RequisitionDetails ModelObject
    {
        get;
        set;
    } = new();

    private string GetDurationCode(string durationCode)
    {
        return durationCode.ToLower() switch
               {
                   "m" => "months",
                   "w" => "weeks",
                   "d" => "days",
                   _ => "years"
               };
    }

    [Parameter]
    public List<IntValues> States
    {
        get;
        set;
    }

    private string GetLocation(string location)
    {
        if (location.ToInt32() == 0)
        {
            return location;
        }

        foreach (IntValues _intValues in States.Where(intValues => location.ToInt32() == intValues.Key))
        {
            return _intValues.Value;
        }

        return location;
    }
}