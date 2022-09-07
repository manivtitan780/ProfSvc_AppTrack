#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           EditCompanyDialog.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          09-05-2022 16:27
// Last Updated On:     09-05-2022 16:27
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Companies;

public partial class EditCompanyDialog
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

    private AutoCompleteButton AutoCompleteControl
    {
        get;
        set;
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

    public class ZipDropDownAdaptor : DataAdaptor
    {
        #region Methods

        /// <summary>Performs data Read operation synchronously.</summary>
        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) => General.GetAutocompleteZipAsync(dm);

        #endregion
    }

    [Parameter]
    public string Title
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<EditContext> SaveCompany
    {
        get;
        set;
    }

    [Parameter]
    public CompanyDetails ModelObject
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
    public EventCallback CancelCompany
    {
        get;
        set;
    }

    private async Task SaveCompanyDialog(EditContext args)
    {
        await Task.Delay(1);
        await Spinner.ShowAsync();
        await SaveCompany.InvokeAsync(args);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }

    private async Task CancelDialog(MouseEventArgs args)
    {
        await Task.Delay(1);
        await Spinner.ShowAsync();
        await CancelCompany.InvokeAsync(args);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }
}