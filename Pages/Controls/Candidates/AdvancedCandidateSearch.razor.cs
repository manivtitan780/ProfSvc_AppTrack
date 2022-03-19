#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           AdvancedCandidateSearch.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          03-02-2022 19:30
// Last Updated On:     03-03-2022 20:30
// *****************************************/

#endregion

//using Microsoft.AspNetCore.Components.Forms;

//using System.Runtime.CompilerServices;

namespace ProfSvc_AppTrack.Pages.Controls.Candidates;

public partial class AdvancedCandidateSearch
{
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

    public SfSpinner Spinner
    {
        get;
        set;
    }

    [Parameter]
    public CandidateSearch ModelSearchObject
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

    private async Task SearchCandidateDialog(EditContext editContext)
    {
        await Task.Delay(1);
        await Spinner.ShowAsync();
        await Search.InvokeAsync(editContext);
        await Spinner.HideAsync();
        Dialog.Height = "200px";
        await Dialog.HideAsync();
    }

    private async Task CancelSearchDialog(MouseEventArgs args)
    {
        await Task.Delay(1);
        await Cancel.InvokeAsync(args);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }

    public DialogFooter DialogFooter
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

    public AutoCompleteButton AutoCompleteControl
    {
        get;
        set;
    }

    [Parameter]
    public object AutoCompleteCityZip
    {
        get;
        set;
    }

    private List<IntValues> ProximityValue
    {
        get;
        //set;
    } = new();

    private List<IntValues> ProximityUnit
    {
        get;
        //set;
    } = new();

    [Parameter]
    public List<IntValues> EligibilityDropDown
    {
        get;
        set;
    } = new();

    [Parameter]
    public List<KeyValues> JobOptionsDropDown
    {
        get;
        set;
    } = new();

    private List<KeyValues> SecurityClearanceDropDown
    {
        get;
        set;
    } = new();

    private List<KeyValues> RelocateDropDown
    {
        get;
        set;
    } = new();

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(1);
        ProximityValue.Add(new(1));
        ProximityValue.Add(new(5));
        ProximityValue.Add(new(10));
        ProximityValue.Add(new(20));
        ProximityValue.Add(new(25));
        ProximityValue.Add(new(30));
        ProximityValue.Add(new(40));
        ProximityValue.Add(new(50));
        ProximityValue.Add(new(60));
        ProximityValue.Add(new(70));
        ProximityValue.Add(new(80));
        ProximityValue.Add(new(90));
        ProximityValue.Add(new(100));
        ProximityValue.Add(new(125));
        ProximityValue.Add(new(150));
        ProximityValue.Add(new(175));
        ProximityValue.Add(new(200));
        ProximityValue.Add(new(250));
        ProximityValue.Add(new(300));
        ProximityValue.Add(new(400));
        ProximityValue.Add(new(500));
        ProximityValue.Add(new(600));
        ProximityValue.Add(new(700));
        ProximityValue.Add(new(800));
        ProximityValue.Add(new(900));
        ProximityValue.Add(new(1000));

        ProximityUnit.Add(new(1, "miles"));
        ProximityUnit.Add(new(2, "kilometers"));

        //StateDropDown.Add(new(0, "All"));

        //JobOptionsDropDown.Add(new("0", "All"));

        SecurityClearanceDropDown.Add(new("%", "All"));
        SecurityClearanceDropDown.Add(new("0", "No"));
        SecurityClearanceDropDown.Add(new("1", "Yes"));

        RelocateDropDown.Add(new("%", "All"));
        RelocateDropDown.Add(new("0", "No"));
        RelocateDropDown.Add(new("1", "Yes"));
    }

    public class CandidateCityZipAdaptor : DataAdaptor
    {
        #region Methods

        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) => General.GetAutocompleteAsync("GetCityZipList", "@CityZip", dm);

        #endregion
    }
}