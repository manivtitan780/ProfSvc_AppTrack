#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           Requisition.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          03-15-2022 19:54
// Last Updated On:     08-13-2022 20:06
// *****************************************/

#endregion

#region Using

using ProfSvc_AppTrack.Pages.Controls.Requisitions;

using ActionCompleteEventArgs = Syncfusion.Blazor.Inputs.ActionCompleteEventArgs;
using ChangeEventArgs = Microsoft.AspNetCore.Components.ChangeEventArgs;
using FileInfo = Syncfusion.Blazor.Inputs.FileInfo;
using SelectEventArgs = Syncfusion.Blazor.Navigations.SelectEventArgs;

#endregion

namespace ProfSvc_AppTrack.Pages;

public partial class Requisition
{
    private readonly List<IntValues> _showRecords =
        new() {new(10, "10 rows"), new(25, "25 rows"), new(50, "50 rows"), new(75, "75 rows"), new(100, "100 rows")};

    private readonly List<IntValues> _statesCopy = new();

    private List<CandidateActivity> _candidateActivityObject = new();

    private int _currentPage = 1;

    private List<IntValues> _education;

    private List<IntValues> _eligibility;

    private List<IntValues> _experience;

    private List<KeyValues> _jobOptions;
    private List<KeyValues> _recruiters;

    private MarkupString _requisitionDetailSkills = "".ToMarkupString();

    private RequisitionDetails _requisitionDetailsObject = new();
    private RequisitionDetails _requisitionDetailsObjectClone = new();
    private List<RequisitionDocuments> _requisitionDocumentsObject = new();
    private int _selectedTab;

    private List<IntValues> _skills;

    private List<IntValues> _states;
    private List<StatusCode> _statusCodes;

    private Requisitions _target;

    private List<Workflow> _workflows;

    public static int Count
    {
        get;
        set;
    }

    public static int EndRecord
    {
        get;
        set;
    }

    public List<KeyValues> NextSteps
    {
        get;
    } = new();

    public static int PageCount
    {
        get;
        set;
    }

    public int RowHeight
    {
        get;
        set;
    }

    public SortDirection SortDirectionProperty
    {
        get;
        set;
    }

    public string SortField
    {
        get;
        set;
    }

    public static int StartRecord
    {
        get;
        set;
    }

    internal static List<Company> Companies
    {
        get;
        set;
    }

    internal static List<CompanyContact> CompanyContacts
    {
        get;
        set;
    }

    internal static List<IntValues> Skills
    {
        get;
        set;
    }

    private ActivityPanelRequisition ActivityPanel
    {
        get;
        set;
    }

    private UploadFiles AddedDocument
    {
        get;
        set;
    }

    private AutoCompleteButton AutoCompleteControl
    {
        get;
        set;
    }

    private EditActivityDialog DialogActivity
    {
        get;
        set;
    }

    private AddRequisitionDocument DialogDocument
    {
        get;
        set;
    }

    private RequisitionDetailsPanel DialogEditRequisition
    {
        get;
        set;
    }

    private DocumentsPanel DocumentsPanel
    {
        get;
        set;
    }

    private MemoryStream FileData
    {
        get;
        set;
    }

    private FileInfo FileInformation
    {
        get;
        set;
    }

    private string FileName
    {
        get;
        set;
    }

    private double FileSize
    {
        get;
        set;
    }

    private bool FirstRender
    {
        get;
        set;
    } = true;

    private static SfGrid<Requisitions> Grid
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

    [Inject]
    private ILocalStorageService LocalStorageBlazored
    {
        get;
        set;
    }

    private LoginCooky LoginCookyUser
    {
        get;
        set;
    }

    private string MimeType
    {
        get;
        set;
    }

    [Inject]
    private NavigationManager NavManager
    {
        get;
        set;
    }

    private RequisitionDocuments NewDocument
    {
        get;
    } = new();

    private static RequisitionSearch SearchModel
    {
        get;
        set;
    } = new();

    private CandidateActivity SelectedActivity
    {
        get;
        set;
    } = new();

    private RequisitionDocuments SelectedDownload
    {
        get;
        set;
    } = new();

    private SfSpinner Spinner
    {
        get;
        set;
    } = new();

    private SfSpinner SpinnerTop
    {
        get;
        set;
    } = new();

    private static string Title
    {
        get;
        set;
    } = "Edit";

    private bool VisibleNewCandidate
    {
        get;
        set;
    }

    private AdvancedRequisitionSearch DialogSearch
    {
        get;
        set;
    }

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(1);
        LoginCookyUser = await NavManager.RedirectInner(LocalStorageBlazored);
 
         string _cookyString = await LocalStorageBlazored.GetItemAsync<string>("RequisitionGrid");
         if (!_cookyString.NullOrWhiteSpace())
         {
             SearchModel = JsonConvert.DeserializeObject<RequisitionSearch>(_cookyString);
         }
         else
         {
             await LocalStorageBlazored.SetItemAsync("RequisitionGrid", SearchModel);
         }
        IMemoryCache _memoryCache = Start.MemCache;
        while (_states == null)
        {
            _memoryCache.TryGetValue("States", out _states);
        }

        _statesCopy.Clear();
        _statesCopy.Add(new(0, "All"));
        _statesCopy.AddRange(_states);
        while (_eligibility == null)
        {
            _memoryCache.TryGetValue("Eligibility", out _eligibility);
        }

        while (_education == null)
        {
            _memoryCache.TryGetValue("Education", out _education);
        }

        /*_eligibilityCopy.Clear();
        _eligibilityCopy.Add(new(0, "All"));
        _eligibilityCopy.AddRange(_eligibility);*/

        while (_experience == null)
        {
            _memoryCache.TryGetValue("Experience", out _experience);
        }

        /*_memoryCache.TryGetValue("TaxTerms", out _taxTerms);*/
        while (_jobOptions == null)
        {
            _memoryCache.TryGetValue("JobOptions", out _jobOptions);
        }

        while (_recruiters == null)
        {
            _memoryCache.TryGetValue("Users", out List<User> _users);
            if (_users == null)
            {
                continue;
            }

            _recruiters = new();
            foreach (User _user in _users.Where(user => user.Role is "Recruiter" or "Recruiter & Sales Manager"))
            {
                _recruiters?.Add(new(_user.UserName, _user.UserName));
            }
        }

        while (_skills == null)
        {
            _memoryCache.TryGetValue("Skills", out _skills);
        }

        _memoryCache.TryGetValue("StatusCodes", out _statusCodes);
        _memoryCache.TryGetValue("Workflow", out _workflows);
        /*_jobOptionsCopy.Clear();
         _jobOptionsCopy.Add(new("%", "All"));
         _jobOptionsCopy.AddRange(_jobOptions); 
 
         _memoryCache.TryGetValue("StatusCodes", out _statusCodes);
         _memoryCache.TryGetValue("Communication", out _communication);
         _memoryCache.TryGetValue("DocumentTypes", out _documentTypes);*/

         await Grid.Refresh();

        await base.OnInitializedAsync();
    }

    private async Task AddDocument(MouseEventArgs arg)
    {
        await Task.Delay(1);

        NewDocument.ClearData();

        await DialogDocument.Dialog.ShowAsync();
    }

    private void AddNewCandidate()
    {
        VisibleNewCandidate = true;
    }

    private async Task AdvancedSearch(MouseEventArgs arg)
    {
        await Task.Delay(1);
        await DialogSearch.Dialog.ShowAsync();
    }

    private void AfterDocument(ActionCompleteEventArgs arg)
    {
        DialogDocument.DialogFooter.SaveButton.Disabled = false;
        DialogDocument.DialogFooter.CancelButton.Disabled = false;
    }

    private async Task AllAlphabet(MouseEventArgs arg)
    {
        SearchModel.Title = "";
        _currentPage = 1;
        SearchModel.Page = _currentPage;
        await LocalStorageBlazored.SetItemAsync("RequisitionGrid", SearchModel);
        //_ = new StorageCompression(SessionStorage).SetCandidateGrid();
        await Grid.Refresh();
    }

    private void BeforeDocument(BeforeUploadEventArgs arg)
    {
        DialogDocument.DialogFooter.SaveButton.Disabled = true;
        DialogDocument.DialogFooter.CancelButton.Disabled = true;
    }

    private async Task ChangeItemCount(ChangeEventArgs<int, IntValues> obj)
    {
        _currentPage = 1;
        SearchModel.Page = _currentPage;
        SearchModel.ItemCount = obj.Value;

        await LocalStorageBlazored.SetItemAsync("RequisitionGrid", SearchModel);
        await Grid.Refresh();
        StateHasChanged();
    }

    private async Task ClearFilter(MouseEventArgs arg)
    {
        _currentPage = 1;
        SearchModel.Page = _currentPage;
        int _currentPageItemCount = SearchModel.ItemCount;
        SearchModel.ClearData();
        SearchModel.ItemCount = _currentPageItemCount;
        SearchModel.User = LoginCookyUser == null || LoginCookyUser.UserID.NullOrWhiteSpace() ? "JOLLY" : LoginCookyUser.UserID.ToUpperInvariant();
        await LocalStorageBlazored.SetItemAsync("RequisitionGrid", SearchModel);
        await Grid.Refresh();
    }

    private async Task DataHandler(object obj)
    {
        DotNetObjectReference<Requisition> _dotNetReference = DotNetObjectReference.Create(this); // create dotnet ref
        await Runtime.InvokeAsync<string>("detail", _dotNetReference);
        //  send the dotnet ref to JS side
        FirstRender = false;
        //Count = Count;
        if (Grid.TotalItemCount > 0)
        {
            await Grid.SelectRowAsync(0);
        }
    }

    private async Task DeleteDocument(int args)
    {
        await Task.Delay(1);
        try
        {
            RestClient _client = new($"{Start.ApiHost}");
            RestRequest _request = new("Requisition/DeleteRequisitionDocument", Method.Post)
                                   {
                                       RequestFormat = DataFormat.Json
                                   };
            _request.AddQueryParameter("documentID", args.ToString());
            _request.AddQueryParameter("user", LoginCookyUser == null || LoginCookyUser.UserID.NullOrWhiteSpace() ? "JOLLY" : LoginCookyUser.UserID.ToUpperInvariant());

            Dictionary<string, object> _response = await _client.PostAsync<Dictionary<string, object>>(_request);
            if (_response == null)
            {
                return;
            }

            _requisitionDocumentsObject = General.DeserializeObject<List<RequisitionDocuments>>(_response["Document"]);
        }
        catch
        {
            //
        }

        await Task.Delay(1);
    }

    private async Task DetailDataBind(DetailDataBoundEventArgs<Requisitions> requisition)
    {
        //VisibleProperty = true;

        if (_target != null)
        {
            if (_target != requisition.Data) // return when target is equal to args.data
            {
                await Grid.ExpandCollapseDetailRowAsync(_target);
            }
        }

        _target = requisition.Data;

        //_requisitionDetailsObject.ClearData();
        await Task.Delay(1);
        await Spinner.ShowAsync();
        //await Task.Delay(1000);
        RestClient _restClient = new($"{Start.ApiHost}");
        RestRequest request = new("Requisition/GetRequisitionDetails");
        request.AddQueryParameter("requisitionID", _target.ID);

        Dictionary<string, object> _restResponse = await _restClient.GetAsync<Dictionary<string, object>>(request);

        if (_restResponse != null)
        {
            _requisitionDetailsObject = JsonConvert.DeserializeObject<RequisitionDetails>(_restResponse["Requisition"]?.ToString() ?? string.Empty);
            //_candidateSkillsObject = General.DeserializeObject<List<CandidateSkills>>(_restResponse["Skills"]);
            //_candidateEducationObject = General.DeserializeObject<List<CandidateEducation>>(_restResponse["Education"]);
            //_candidateExperienceObject = General.DeserializeObject<List<CandidateExperience>>(_restResponse["Experience"]);
            _candidateActivityObject = General.DeserializeObject<List<CandidateActivity>>(_restResponse["Activity"]);
            _requisitionDocumentsObject = General.DeserializeObject<List<RequisitionDocuments>>(_restResponse["Documents"]);
            //_candidateNotesObject = General.DeserializeObject<List<CandidateNotes>>(_restResponse["Notes"]);
            //_candidateRatingObject = General.DeserializeObject<List<CandidateRating>>(_restResponse["Rating"]);
            //_candidateMPCObject = General.DeserializeObject<List<CandidateMPC>>(_restResponse["MPC"]);
            //_candidateDocumentsObject = General.DeserializeObject<List<CandidateDocument>>(_restResponse["Document"]);
            //RatingMPC = JsonConvert.DeserializeObject<CandidateRatingMPC>(_restResponse["RatingMPC"]?.ToString() ?? string.Empty);
            //GetMPCDate();
            //GetMPCNote();
            //GetRatingDate();
            //GetRatingNote();
            //SetupAddress();
            //SetCommunication();
            //SetEligibility();
            //SetJobOption();
            //SetTaxTerm();
            //SetExperience();
            SetSkills();
        }

        _selectedTab = _candidateActivityObject.Count > 0 ? 2 : 0;

        await Task.Delay(1);
        await Spinner.HideAsync();
    }

    private async Task DownloadDocument(int args)
    {
        await Task.Delay(1);
        SelectedDownload = DocumentsPanel.SelectedRow;
        string _queryString = (SelectedDownload.DocumentFileName + "^" + _target.ID + "^" + SelectedDownload.OriginalFileName + "^1").ToBase64String();
        //NavManager.NavigateTo(NavManager.BaseUri + "Download/" + _queryString);
        await JsRuntime.InvokeVoidAsync("open", $"{NavManager.BaseUri}Download/{_queryString}", "_blank");
        /*string _filePath = Path.Combine(Environment.WebRootPath, "Uploads", "Candidate", _target.ID.ToString(), SelectedDownload.InternalFileName).ToBase64String();
        byte[] _fileBytes = await File.ReadAllBytesAsync(_filePath);
        return File(_fileBytes, "application/force-download", _decodedStringArray[2]);*/
    }

    private async Task EditActivity(int args)
    {
        await Task.Delay(1);

        SelectedActivity = ActivityPanel.SelectedRow;
        NextSteps.Clear();
        NextSteps.Add(new("No Change", ""));
        try
        {
            foreach (string[] _next in _workflows.Where(flow => flow.Step == SelectedActivity.StatusCode).Select(flow => flow.Next.Split(',')))
            {
                foreach (string _nextString in _next)
                {
                    foreach (StatusCode _status in _statusCodes.Where(status => status.Code == _nextString && status.AppliesToCode == "SCN"))
                    {
                        NextSteps.Add(new(_status.Status, _nextString));
                        break;
                    }
                }

                break;
            }
        }
        catch
        {
            //
        }

        await DialogActivity.Dialog.ShowAsync();
    }

    private async Task EditRequisition(bool isAdd)
    {
        await Task.Delay(1);
        if (isAdd)
        {
            await SpinnerTop.ShowAsync();
        }
        else
        {
            await Spinner.ShowAsync();
        }

        if (isAdd)
        {
            Title = "Add";
            _requisitionDetailsObjectClone.ClearData();
        }
        else
        {
            Title = "Edit";
            _requisitionDetailsObjectClone = _requisitionDetailsObject.Copy();
        }

        StateHasChanged();
        await DialogEditRequisition.Dialog.ShowAsync();
        if (isAdd)
        {
            await SpinnerTop.HideAsync();
        }
        else
        {
            await Spinner.HideAsync();
        }
    }

    private async Task FilterGrid(ChangeEventArgs<string, KeyValues> requisition)
    {
        SearchModel.Title = requisition.Value ?? "";
        _currentPage = 1;
        SearchModel.Page = _currentPage;
        await LocalStorageBlazored.SetItemAsync("RequisitionGrid", SearchModel);
        //_ = new StorageCompression(SessionStorage).SetCandidateGrid();
        await Grid.Refresh();
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
        await Grid.Refresh();
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
        await Grid.Refresh();
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
        await Grid.Refresh();
    }

    private async Task OnActionBegin(ActionEventArgs<Requisitions> arg)
    {
        await Task.Delay(1);
    }

    private async Task OnActionComplete(ActionEventArgs<Requisitions> arg)
    {
        await Task.Delay(1);
    }

    private async Task OnFileUpload(UploadChangeEventArgs file)
    {
        await Task.Delay(1);
        foreach (UploadFiles _file in file.Files)
        {
            MimeType = _file.FileInfo.MimeContentType;
            FileName = _file.FileInfo.Name;
            FileSize = _file.FileInfo.Size;
            FileData = _file.Stream;

            //List<AdminList> _dataSource = new();
            DialogEditRequisition.Footer.CancelButton.Disabled = true;
            DialogEditRequisition.Footer.SaveButton.Disabled = true;
            try
            {
                //string _url = ;

                RestClient _client = new($"{Start.ApiHost}");
                RestRequest _request = new("Requisition/UploadBenefitsDocument", Method.Post)
                                       {
                                           AlwaysMultipartFormData = true
                                       };
                //request.AddParameter("file", new ByteArrayContent(FileData.ToArray()));
                _request.AddParameter("fileData",
                                      $"{FileName}^{FileSize}^{MimeType}^{(LoginCookyUser == null || LoginCookyUser.UserID.NullOrWhiteSpace() ? "JOLLY" : LoginCookyUser.UserID.ToUpperInvariant())}",
                                      ParameterType.RequestBody);
                _request.AddFile("file", FileData.ToArray(), FileName, MimeType);

                await _client.PostAsync(_request);
                //await Grid.Refresh();
            }
            catch
            {
                //
            }
            finally
            {
                DialogEditRequisition.Footer.SaveButton.Disabled = false;
                DialogEditRequisition.Footer.CancelButton.Disabled = false;
            }
        }
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
        await Grid.Refresh();
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
        await Grid.Refresh();
    }

    private static void RefreshGrid() => Grid.Refresh();

    private async Task SaveActivity(EditContext activity)
    {
        await Task.Delay(1);

        try
        {
            RestClient _client = new($"{Start.ApiHost}");
            RestRequest _request = new("Candidates/SaveCandidateActivity", Method.Post)
                                   {
                                       RequestFormat = DataFormat.Json
                                   };
            _request.AddJsonBody(activity.Model);
            _request.AddQueryParameter("user", LoginCookyUser == null || LoginCookyUser.UserID.NullOrWhiteSpace() ? "JOLLY" : LoginCookyUser.UserID.ToUpperInvariant());
            _request.AddQueryParameter("candidateID", _target.ID);
            _request.AddQueryParameter("isCandidateScreen", false);

            Dictionary<string, object> _response = await _client.PostAsync<Dictionary<string, object>>(_request);
            if (_response == null)
            {
                return;
            }

            _candidateActivityObject = General.DeserializeObject<List<CandidateActivity>>(_response["Activity"]);
        }
        catch
        {
            //
        }

        await Task.Delay(1);
    }

    private async Task SaveDocument(EditContext document)
    {
        await Task.Delay(1);
        try
        {
            if (document.Model is RequisitionDocuments _document)
            {
                RestClient _client = new($"{Start.ApiHost}");
                RestRequest _request = new("Requisition/UploadDocument", Method.Post)
                                       {
                                           AlwaysMultipartFormData = true
                                       };
                _request.AddFile("file", AddedDocument.Stream.ToArray(), AddedDocument.FileInfo.Name, AddedDocument.FileInfo.MimeContentType);
                //request.AddParameter("file", new ByteArrayContent(FileData.ToArray()));
                _request.AddParameter("name", _document.DocumentName, ParameterType.GetOrPost);
                _request.AddParameter("notes", _document.DocumentNotes, ParameterType.GetOrPost);
                _request.AddParameter("requisitionID", _target.ID.ToString(), ParameterType.GetOrPost);
                _request.AddParameter("user", LoginCookyUser == null || LoginCookyUser.UserID.NullOrWhiteSpace() ? "JOLLY" : LoginCookyUser.UserID.ToUpperInvariant(), ParameterType.GetOrPost);
                _request.AddParameter("path", Start.UploadsPath, ParameterType.GetOrPost);
                Dictionary<string, object> _response = await _client.PostAsync<Dictionary<string, object>>(_request);
                if (_response == null)
                {
                    return;
                }

                _requisitionDocumentsObject = General.DeserializeObject<List<RequisitionDocuments>>(_response["Document"]);
            }
        }
        catch
        {
            //
        }

        await Task.Delay(1);
    }

    private async Task SaveRequisition(EditContext arg)
    {
        //SpinnerVisible = true;
        await Spinner.ShowAsync();
        DialogEditRequisition.Footer.CancelButton.Disabled = true;
        DialogEditRequisition.Footer.SaveButton.Disabled = true;
        await Task.Delay(1);

        RestClient _client = new($"{Start.ApiHost}");
        RestRequest _request = new("Requisition/SaveRequisition", Method.Post)
                               {
                                   RequestFormat = DataFormat.Json
                               };
        _request.AddJsonBody(_requisitionDetailsObjectClone);
        _request.AddQueryParameter("fileName", FileName);
        _request.AddQueryParameter("mimeType", MimeType);

        await _client.PostAsync<int>(_request);
        _requisitionDetailsObject = _requisitionDetailsObjectClone.Copy();

        /*_target.Name = _candidateDetailsObject.FirstName + " " + _candidateDetailsObject.LastName;
        _target.Phone = $"{_candidateDetailsObject.Phone1.ToInt64(): (###) ###-####}";
        _target.Email = _candidateDetailsObject.Email;
        _target.Location = _candidateDetailsObject.City + ", " + GetState(_candidateDetailsObject.StateID) + ", " + _candidateDetailsObject.ZipCode;
        _target.Updated = DateTime.Today.ToString("d", new CultureInfo("en-US")) + "[ADMIN]";
        _target.Status = "Available";
        SetupAddress();
        SetCommunication();
        SetEligibility();
        SetJobOption();
        SetTaxTerm();
        SetExperience();*/
        //SpinnerVisible = false;
        await Spinner.HideAsync();
        DialogEditRequisition.Footer.CancelButton.Disabled = false;
        DialogEditRequisition.Footer.SaveButton.Disabled = false;
        await DialogEditRequisition.Dialog.HideAsync();
        await Task.Delay(1);
        //VisibleCandidateInfo = false;
        StateHasChanged();
    }

    private async void SetAlphabet(string alphabet)
    {
        SearchModel.Title = alphabet;
        _currentPage = 1;
        SearchModel.Page = _currentPage;
        await LocalStorageBlazored.SetItemAsync("RequisitionGrid", SearchModel);
        //_ = new StorageCompression(SessionStorage).SetCandidateGrid();
        await Grid.Refresh();
    }

    private void SetSkills()
    {
        if (_requisitionDetailsObject == null)
        {
            return;
        }

        if (_requisitionDetailsObject.SkillsRequired.NullOrWhiteSpace() && _requisitionDetailsObject.Optional.NullOrWhiteSpace())
        {
            return;
        }

        _requisitionDetailSkills = "".ToMarkupString();

        string[] _skillRequiredStrings = { }, _skillOptionalStrings = { };
        if (_requisitionDetailsObject.SkillsRequired != "")
        {
            _skillRequiredStrings = _requisitionDetailsObject.SkillsRequired.Split(',');
        }

        if (_requisitionDetailsObject.Optional != "")
        {
            _skillOptionalStrings = _requisitionDetailsObject.Optional.Split(',');
        }

        string _skillsRequired = "", _skillsOptional = "";
        foreach (string _skillString in _skillRequiredStrings)
        {
            IntValues _skill = _skills.FirstOrDefault(skill => skill.Key == _skillString.ToInt32());
            if (_skill == null)
            {
                continue;
            }

            if (_skillsRequired == "")
            {
                _skillsRequired = _skill.Value;
            }
            else
            {
                _skillsRequired += ", " + _skill.Value;
            }
        }

        foreach (string _skillString in _skillOptionalStrings)
        {
            IntValues _skill = _skills.FirstOrDefault(skill => skill.Key == _skillString.ToInt32());
            if (_skill == null)
            {
                continue;
            }

            if (_skillsOptional == "")
            {
                _skillsOptional = _skill.Value;
            }
            else
            {
                _skillsOptional += ", " + _skill.Value;
            }
        }

        string _skillStringTemp = "";

        if (!_skillsRequired.NullOrWhiteSpace())
        {
            _skillStringTemp = "Required Skills: <br/>" + _skillsRequired + "<br/>";
        }

        if (!_skillsOptional.NullOrWhiteSpace())
        {
            _skillStringTemp += "Optional Skills: <br/>" + _skillsOptional;
        }

        _requisitionDetailSkills = _skillStringTemp.ToMarkupString();
    }

    private async Task TabSelected(SelectEventArgs args)
    {
        await Task.Delay(1);
        _selectedTab = args.SelectedIndex;
    }

    private async Task UndoActivity(int activityID)
    {
        await Task.Delay(1);

        try
        {
            RestClient _client = new($"{Start.ApiHost}");
            RestRequest _request = new("Candidates/UndoCandidateActivity", Method.Post)
                                   {
                                       RequestFormat = DataFormat.Json
                                   };
            _request.AddQueryParameter("submissionID", activityID);
            _request.AddQueryParameter("user", LoginCookyUser == null || LoginCookyUser.UserID.NullOrWhiteSpace() ? "JOLLY" : LoginCookyUser.UserID.ToUpperInvariant());
            _request.AddQueryParameter("isCandidateScreen", false);

            Dictionary<string, object> _response = await _client.PostAsync<Dictionary<string, object>>(_request);
            if (_response == null)
            {
                return;
            }

            _candidateActivityObject = General.DeserializeObject<List<CandidateActivity>>(_response["Activity"]);
        }
        catch
        {
            //
        }

        await Task.Delay(1);
    }

    private async Task UploadDocument(UploadChangeEventArgs file)
    {
        await Task.Delay(1);
        foreach (UploadFiles _file in file.Files)
        {
            AddedDocument = _file;
        }
    }

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
            bool _getInformation = true;
            if (Companies != null)
            {
                _getInformation = Companies.Count > 0;
            }

            Task<object> _requisitionReturn = General.GetRequisitionReadAdaptor(SearchModel, dm, _getInformation);
            //Count = ((DataResult)_candidateReturn.Result).Count;
            Grid.SelectRowAsync(0);
            return _requisitionReturn;
        }

        #endregion
    }
}