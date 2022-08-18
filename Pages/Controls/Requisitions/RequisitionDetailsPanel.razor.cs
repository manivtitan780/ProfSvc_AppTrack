#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           RequisitionDetailsPanel.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          04-27-2022 20:20
// Last Updated On:     07-08-2022 20:46
// *****************************************/

#endregion

#region Using

using ActionCompleteEventArgs = Syncfusion.Blazor.Inputs.ActionCompleteEventArgs;
using BeforeOpenEventArgs = Syncfusion.Blazor.Popups.BeforeOpenEventArgs;

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Requisitions;

public partial class RequisitionDetailsPanel
{
    private List<CompanyContact> _filteredCompanyContacts = new();

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

    public Query ContactQuery
    {
        get; 
        set;
    } = null;

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

    [Parameter]
    public string Title
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

    protected override void OnAfterRender(bool firstRender)
    {
        ContactQuery = new Query().Where(new WhereFilter {Field = "ClientID", Operator = "equal", value = ModelObject.CompanyID});
        base.OnAfterRender(firstRender);
    }

    [Parameter]
    public EventCallback CancelRequisition
    {
        get;
        set;
    }

    [Parameter]
    public List<IntValues> Skills
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<UploadChangeEventArgs> OnFileUpload
    {
        get;
        set;
    }

    public DialogFooter Footer
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<EditContext> SaveRequisitionDialog
    {
        get;
        set;
    }

    [Parameter]
    public List<KeyValues> Recruiters
    {
        get;
        set;
    }

    private AutoCompleteButton AutoCompleteControl
    {
        get;
        set;
    }

    private async Task CancelDialog(MouseEventArgs args)
    {
        await Task.Delay(1);
        await CancelRequisition.InvokeAsync(args);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }

    private async Task CompanyChanged(ChangeEventArgs<int, Company> args)
    {
        await Task.Delay(1);
        ContactQuery = new Query().Where(new WhereFilter {Field = "ClientID", Operator = "equal", value = args.Value});
    }

    private async Task RequisitionOpen(BeforeOpenEventArgs args)
    {
        await Task.Delay(1);
        ContactQuery = new Query().Where(new WhereFilter {Field = "ClientID", Operator = "equal", value = ModelObject.CompanyID});
    }

    private async Task ZipChange(ChangeEventArgs<string, KeyValues> arg)
    {
        await Task.Delay(1);
        throw new NotImplementedException();
    }

    public class ZipDropDownAdaptor : DataAdaptor
    {
        #region Methods

        /// <summary>Performs data Read operation synchronously.</summary>
        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) => General.GetAutocompleteAsync("SearchZip", "@Zip", dm);

        #endregion
    }
}