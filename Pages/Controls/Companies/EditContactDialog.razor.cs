#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           EditContactDialog.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          09-12-2022 20:39
// Last Updated On:     09-12-2022 20:39
// *****************************************/

#endregion

#region Using

using ProfSvc_AppTrack.Pages.Controls.Companies;

using ActionCompleteEventArgs = Syncfusion.Blazor.Inputs.ActionCompleteEventArgs;
using ChangeEventArgs = Microsoft.AspNetCore.Components.ChangeEventArgs;
using IApplicationLifetime = Microsoft.AspNetCore.Hosting.IApplicationLifetime;
using SelectEventArgs = Syncfusion.Blazor.Navigations.SelectEventArgs;

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Companies;

public partial class EditContactDialog
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
    public string Title
    {
        get;
        set;
    }

    [Parameter]
    public CompanyContact ModelObject
    {
        get;
        set;
    }

    [Parameter]
    public List<IntValues> TitleTypes
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<MouseEventArgs> CancelCompanyContact
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<EditContext> SaveCompanyContact
    {
        get;
        set;
    }

    public DialogFooter Footer
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

    private AutoCompleteButton AutoCompleteControl
    {
        get;
        set;
    }

    private async Task NumbersOnly(object args)
    {
        await JsRuntime.InvokeVoidAsync("onCreate", "textExtension");
    }

    private async Task CancelDialog(MouseEventArgs args)
    {
        await Task.Delay(1);
        await Spinner.ShowAsync();
        await CancelCompanyContact.InvokeAsync(args);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }

    private async Task SaveContactDialog(EditContext args)
    {
        await Task.Delay(1);
        await Spinner.ShowAsync();
        await SaveCompanyContact.InvokeAsync(args);
        await Task.Delay(1);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }

    private async Task ValueChangeTitle(ChangeEventArgs<int, IntValues> args)
    {
        await Task.Delay(1);
        ModelObject.Title = args.ItemData.Value;
    }

    private async Task ZipChange(ChangeEventArgs<string, KeyValues> arg)
    {
        await Task.Delay(1);
        IMemoryCache _memoryCache = Start.MemCache;
        List<Zip> _zips = null;
        while (_zips == null)
        {
            _memoryCache.TryGetValue("Zips", out _zips);
        }

        if (_zips.Count > 0)
        {
            foreach (Zip _zip in _zips.Where(zip => zip.ZipCode == arg.Value))
            {
                ModelObject.City = _zip.City;
                ModelObject.StateID = _zip.StateID;
            }
        }
    }

    [Parameter]
    public List<IntValues> States
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

    public class ZipDropDownAdaptor : DataAdaptor
    {
        #region Methods

        /// <summary>Performs data Read operation synchronously.</summary>
        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) => General.GetAutocompleteZipAsync(dm);

        #endregion
    }
}
