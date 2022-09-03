#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           AdvancedCompanySearch.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          09-03-2022 20:15
// Last Updated On:     09-03-2022 20:15
// *****************************************/

#endregion

using System.Threading.Channels;

namespace ProfSvc_AppTrack.Pages.Controls.Companies;

public partial class AdvancedCompanySearch
{
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

    public DialogFooter DialogFooter
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
}