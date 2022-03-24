#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           Requisition.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          03-15-2022 19:54
// Last Updated On:     03-15-2022 21:12
// *****************************************/

#endregion

#region Using

using ChangeEventArgs = Microsoft.AspNetCore.Components.ChangeEventArgs;

#endregion

namespace ProfSvc_AppTrack.Pages;

public partial class Requisition
{
    private readonly List<IntValues> _showRecords =
        new() {new(10, "10 rows"), new(25, "25 rows"), new(50, "50 rows"), new(75, "75 rows"), new(100, "100 rows")};

    private int _currentPage = 1;

    public static int PageCount
    {
        get;
        set;
    }

    private AutoCompleteButton AutoCompleteControl
    {
        get;
        set;
    }

    private static SfGrid<Requisitions> Grid
    {
        get;
        set;
    }

    [Inject]
    private ILocalStorageService LocalStorageBlazored
    {
        get;
        set;
    }

    private static RequisitionSearch SearchModel
    {
        get;
    } = new();

    private bool VisibleNewCandidate
    {
        get;
        set;
    }

    public string SortField
    {
        get;
        set;
    }

    public SortDirection SortDirectionProperty
    {
        get;
        set;
    }

    public static int StartRecord
    {
        get;
        set;
    }

    public static int EndRecord
    {
        get;
        set;
    }

    public static int Count
    {
        get;
        set;
    }

    private void AddNewCandidate()
    {
        VisibleNewCandidate = true;
    }

    private async Task AdvancedSearch(MouseEventArgs arg)
    {
        await Task.Delay(1);
    }

    private async Task ChangeItemCount(ChangeEventArgs<int, IntValues> obj)
    {
        _currentPage = 1;
        SearchModel.Page = _currentPage;
        SearchModel.ItemCount = obj.Value;

        await LocalStorageBlazored.SetItemAsync("RequisitionGrid", SearchModel);
        Grid.Refresh();
        StateHasChanged();
    }

    private async Task DataHandler(object obj)
    {
        await Task.Delay(1);
    }

    private async Task DetailDataBind(DetailDataBoundEventArgs<Requisitions> arg)
    {
        await Task.Delay(1);
    }

    private async Task FilterGrid(ChangeEventArgs<string, KeyValues> arg)
    {
        await Task.Delay(1);
    }

    private async Task FirstClick()
    {
        if (_currentPage < 1)
        {
            _currentPage = 1;
        }

        _currentPage = 1;
        SearchModel.Page = _currentPage;
        await LocalStorageBlazored.SetItemAsync("RequisitionGrid", SearchModel);
        //_ = new StorageCompression(SessionStorage).SetRequisitionGrid();
        Grid.Refresh();
    }

    private async Task LastClick()
    {
        if (_currentPage < 1)
        {
            _currentPage = 1;
        }

        _currentPage = PageCount.ToInt32();
        SearchModel.Page = _currentPage;
        await LocalStorageBlazored.SetItemAsync("RequisitionGrid", SearchModel);
        //_ = new StorageCompression(SessionStorage).SetRequisitionGrid();
        Grid.Refresh();
    }

    private async Task NextClick()
    {
        if (_currentPage < 1)
        {
            _currentPage = 1;
        }

        _currentPage = SearchModel.Page >= PageCount.ToInt32() ? PageCount.ToInt32() : SearchModel.Page + 1;
        SearchModel.Page = _currentPage;
        await LocalStorageBlazored.SetItemAsync("RequisitionGrid", SearchModel);
        //_ = new StorageCompression(SessionStorage).SetRequisitionGrid();
        Grid.Refresh();
    }

    private async Task OnActionBegin(ActionEventArgs<Requisitions> arg)
    {
        await Task.Delay(1);
    }

    private async Task OnActionComplete(ActionEventArgs<Requisitions> arg)
    {
        await Task.Delay(1);
    }

    private async Task PageNumberChanged(ChangeEventArgs obj)
    {
        decimal _currentValue = obj.Value.ToDecimal();
        if (_currentValue < 1)
        {
            _currentPage = 1;
        }
        else if (_currentValue > PageCount)
        {
            _currentPage = PageCount.ToInt32();
        }

        SearchModel.Page = _currentPage;
        await LocalStorageBlazored.SetItemAsync("RequisitionGrid", SearchModel);
        //_ = new StorageCompression(SessionStorage).SetRequisitionGrid();
        Grid.Refresh();
    }

    private async Task PreviousClick()
    {
        if (_currentPage < 1)
        {
            _currentPage = 1;
        }

        _currentPage = SearchModel.Page <= 1 ? 1 : SearchModel.Page - 1;
        SearchModel.Page = _currentPage;
        await LocalStorageBlazored.SetItemAsync("RequisitionGrid", SearchModel);
        //_ = new StorageCompression(SessionStorage).SetRequisitionGrid();
        Grid.Refresh();
    }

    private static void RefreshGrid() => Grid.Refresh();

    public class AdminRequisitionDropDownAdaptor : DataAdaptor
    {
        #region Methods

        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null) => General.GetAutocompleteAsync("SearchCandidate", "@Candidate", dm);

        #endregion
    }

    public class AdminRequisitionAdaptor : DataAdaptor
    {
        #region Methods

        /// <summary>Performs data Read operation synchronously.</summary>
        public override Task<object> ReadAsync(DataManagerRequest dm, string key = null)
        {
            Task<object> _requisitionReturn = General.GetRequisitionReadAdaptor(SearchModel, dm);
            //Count = ((DataResult)_candidateReturn.Result).Count;
            Grid.SelectRowAsync(0);
            return _requisitionReturn;
        }

        #endregion
    }

    private void SetAlphabet(string alphabet)
    {
        return;
    }

    private async Task AllAlphabet(MouseEventArgs arg)
    {
        await Task.Delay(1);
    }

    private async Task ClearFilter(MouseEventArgs arg)
    {
        await Task.Delay(1);
    }
}