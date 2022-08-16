#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           AdvancedRequisitionSearch.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          08-13-2022 21:06
// Last Updated On:     08-15-2022 18:52
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Requisitions;

public partial class AdvancedRequisitionSearch
{
    [Parameter]
    public object AutoCompleteCityZip
    {
        get;
        set;
    }

    public AutoCompleteButton AutoCompleteControl
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback Cancel
    {
        get;
        set;
    }

    public SfDialog Dialog
    {
        get;
        set;
    }

    public DialogFooter DialogFooter
    {
        get;
        set;
    }

    [Parameter]
    public List<IntValues> EligibilityDropDown
    {
        get;
        set;
    } = new();

    [Parameter]
    public List<KeyValues> JobOption
    {
        get;
        set;
    }

    [Parameter]
    public List<KeyValues> JobOptionsDropDown
    {
        get;
        set;
    } = new();

    [Parameter]
    public RequisitionSearch ModelSearchObject
    {
        get;
        set;
    } = new();

    [Parameter]
    public EventCallback<EditContext> Search
    {
        get;
        set;
    }

    public SfSpinner Spinner
    {
        get;
        set;
    }

    [Parameter]
    public List<IntValues> StateDropDown
    {
        get;
        set;
    } = new();

    [Parameter]
    public List<KeyValues> StatusDropDown
    {
        get;
        set;
    }

    private DateControl CreatedMax
    {
        get;
        set;
    }

    private DateControl DueMax
    {
        get;
        set;
    }

    private List<IntValues> ProximityUnit
    {
        get;
        //set;
    } = new();

    private List<IntValues> ProximityValue
    {
        get;
        //set;
    } = new();

    private List<KeyValues> RelocateDropDown
    {
        get;
        set;
    } = new();

    private List<KeyValues> SecurityClearanceDropDown
    {
        get;
        set;
    } = new();

    private List<KeyValues> ShowRequisitions
    {
        get;
    } = new();

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(1);
        ShowRequisitions.Add(new("My Requisitions Only", "M"));
        ShowRequisitions.Add(new("My Requisitions and Assigned Requisitions", "A"));
        ShowRequisitions.Add(new("All Assigned Requisitions", "R"));
        ShowRequisitions.Add(new("All Requisitions", "%"));
    }

    private async Task CancelSearchDialog(MouseEventArgs args)
    {
        await Task.Delay(1);
        await Cancel.InvokeAsync(args);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }

    private async Task CreatedOnSelect(ChangedEventArgs<DateTime> args)
    {
        await Task.Delay(1);
        CreatedMax.Min = args.Value;
        CreatedMax.Max = args.Value.AddMonths(6);
        /*if (ModelSearchObject.CreatedOnEnd < args.Value)
        {
            args.Value = CreatedMax.Min;
        }

        if (ModelSearchObject.CreatedOnEnd > CreatedMax.Max)
        {
            args.Value = CreatedMax.Max;
        }*/
    }

    private async Task DueOnSelect(ChangedEventArgs<DateTime> args)
    {
        await Task.Delay(1);
        DueMax.Min = args.Value;
        DueMax.Max = args.Value.AddMonths(6);
        /*if (ModelSearchObject.DueEnd < args.Value)
        {
            args.Value = DueMax.Min;
        }

        if (ModelSearchObject.DueEnd > DueMax.Max)
        {
            args.Value = DueMax.Max;
        }*/
    }

    private async Task SearchCandidateDialog(EditContext editContext)
    {
        await Task.Delay(1);
        await Spinner.ShowAsync();
        await Search.InvokeAsync(editContext);
        await Spinner.HideAsync();
        Dialog.Height = "200px";
        await Dialog.HideAsync();
    }

    public class CandidateCityZipAdaptor : DataAdaptor
    {
        #region Methods

        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) => General.GetAutocompleteAsync("GetCityZipList", "@CityZip", dm);

        #endregion
    }
}