#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           BasicInfoCompanyPanel.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          08-22-2022 21:15
// Last Updated On:     08-23-2022 15:33
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Companies;

public partial class BasicInfoCompanyPanel
{
    [Parameter]
    public CompanyDetails ModelObject
    {
        get;
        set;
    }

    [Parameter]
    public MarkupString SetupAddress
    {
        get;
        set;
    }
}