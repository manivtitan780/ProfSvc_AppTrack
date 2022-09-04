#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           AdvancedCompanySearch.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          09-03-2022 20:15
// Last Updated On:     09-03-2022 20:36
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Companies;

public partial class AdvancedCompanySearch
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

    public DialogFooter DialogFooter
    {
        get;
        set;
    }

    [Parameter]
    public CompanySearch ModelSearchObject
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

    [Inject]
    private IJSRuntime JsRuntime
    {
        get; 
        set;
    }

    private async Task CancelSearchDialog(MouseEventArgs args)
    {
        await Task.Delay(1);
        await Cancel.InvokeAsync(args);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }

    private async Task SearchCompanyDialog(EditContext editContext)
    {
        await Task.Delay(1);
        await Spinner.ShowAsync();
        await Search.InvokeAsync(editContext);
        await Spinner.HideAsync();
        Dialog.Height = "200px";
        await Dialog.HideAsync();
    }

    [Parameter]
    public List<KeyValues> StatusDropDown
    {
        get;
        set;
    }

    [Parameter]
    public List<IntValues> StateDropDown
    {
        get;
        set;
    }

    private async Task NumbersOnly(object args)
    {
        await JsRuntime.InvokeVoidAsync("onCreate", "textPhone"); 
    }
}