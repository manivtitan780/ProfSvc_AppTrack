#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           RequisitionDetailsPanel.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          04-27-2022 20:20
// Last Updated On:     07-05-2022 19:06
// *****************************************/

#endregion

#region Using

using ActionCompleteEventArgs = Syncfusion.Blazor.Inputs.ActionCompleteEventArgs;

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Requisitions;

public partial class RequisitionDetailsPanel
{
    public List<IntValues> AssignedTo
    {
        get;
        set;
    }

    [Parameter]
    public List<Company> Companies
    {
        get;
        set;
    }

    [Parameter]
    public List<CompanyContact> CompanyContacts
    {
        get;
        set;
    }

    public SfDialog Dialog
    {
        get;
        set;
    }

    [Parameter]
    public List<IntValues> Education
    {
        get;
        set;
    }

    [Parameter]
    public List<IntValues> Eligibility
    {
        get;
        set;
    }

    [Parameter]
    public List<IntValues> Experience
    {
        get;
        set;
    }

    [Parameter]
    public List<KeyValues> JobOptions
    {
        get;
        set;
    }

    [Parameter]
    public RequisitionDetails ModelObject
    {
        get;
        set;
    } = new();

    public SfSpinner Spinner
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<ChangeEventArgs<int, IntValues>> StateIDChanged
    {
        get;
        set;
    }

    [Parameter]
    public List<IntValues> States
    {
        get;
        set;
    }

    private List<KeyValues> DurationCode
    {
        get;
    } = new() {new("M", "Months"), new("Y", "Years")};

    private List<IntValues> Priority
    {
        get;
    } = new() {new(0, "Low"), new(1, "Medium"), new(2, "High")};

    [Parameter]
    public string Title
    {
        get;
        set;
    }

    private async Task AfterUpload(ActionCompleteEventArgs arg)
    {
        await Task.Delay(1);
    }

    private async Task BeforeUpload(BeforeUploadEventArgs arg)
    {
        await Task.Delay(1);
    }

    private async Task CancelDialog(MouseEventArgs arg)
    {
        await Task.Delay(1);
    }

    private async Task OnFileUpload(UploadChangeEventArgs arg)
    {
        await Task.Delay(1);
    }

    private async Task SaveRequisitionDialog(EditContext arg)
    {
        await Task.Delay(1);
    }
}