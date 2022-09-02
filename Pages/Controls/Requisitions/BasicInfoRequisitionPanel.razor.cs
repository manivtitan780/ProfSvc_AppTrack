﻿#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           BasicInfoRequisitionPanel.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          04-04-2022 19:57
// Last Updated On:     04-25-2022 15:53
// *****************************************/

#endregion

using Syncfusion.Blazor.PivotView;

namespace ProfSvc_AppTrack.Pages.Controls.Requisitions;

public partial class BasicInfoRequisitionPanel
{
    [Parameter]
    public RequisitionDetails ModelObject
    {
        get;
        set;
    } = new();

    [Parameter]
    public List<IntValues> States
    {
        get;
        set;
    }

    [Parameter]
    public MarkupString SkillsText
    {
        get;
        set;
    }

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

    protected override async void OnAfterRender(bool firstRender)
    {
        /*while (ModelObject == null)
        {
            ModelObject = new();
        }*/
        await base.OnAfterRenderAsync(firstRender);
    }

    protected override async Task OnParametersSetAsync()
    {
        while (ModelObject == null)
        {
            ModelObject = new();
        }
        await base.OnParametersSetAsync();

    }

    private string GetLocation(string location)
    {
        if (location.ToInt32() == 0 || States == null)
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