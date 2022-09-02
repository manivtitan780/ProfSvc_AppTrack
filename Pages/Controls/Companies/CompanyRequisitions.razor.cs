#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           CompanyRequisitions.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          08-30-2022 15:48
// Last Updated On:     08-31-2022 16:17
// *****************************************/

#endregion

#region Using

using Microsoft.JSInterop;

using ProfSvc_AppTrack.Pages.Admin;
using ProfSvc_AppTrack.Pages.Controls.Requisitions;

using ProfSvc_Classes;

using SelectEventArgs = Syncfusion.Blazor.Navigations.SelectEventArgs;
using Workflow = ProfSvc_Classes.Workflow;

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Companies;

public partial class CompanyRequisitions
{
    private int _selectedReqTab;
    private ProfSvc_Classes.Requisitions _targetRequisitions;
    private RequisitionDetails _requisitionDetailsObject;
    private List<CandidateActivity> _candidateActivityObject = new();
    private List<RequisitionDocuments> _requisitionDocumentsObject = new();
    private MarkupString _requisitionDetailSkills = "".ToMarkupString();
    private List<IntValues> _states = new();

    [Parameter]
    public List<ProfSvc_Classes.Requisitions> ModelObject
    {
        get;
        set;
    }

    private bool FirstRender
    {
        get;
        set;
    }

    private SfGrid<ProfSvc_Classes.Requisitions> GridInnerRequisition
    {
        get;
        set;
    }

    private SfSpinner Spinner
    {
        get;
        set;
    }

    private async Task DataHandler(object obj)
    {
        DotNetObjectReference<CompanyRequisitions> _dotNetReference = DotNetObjectReference.Create(this); // create dotnet ref
        await Runtime.InvokeAsync<string>("detail", _dotNetReference);
        //  send the dotnet ref to JS side
        FirstRender = false;
        //Count = Count;
        if (GridInnerRequisition.TotalItemCount > 0)
        {
            await GridInnerRequisition.SelectRowAsync(0);
        }
    }

    private DocumentsPanel DocumentsPanel
    {
        get;
        set;
    }

    [JSInvokable("DetailCollapse")]
    public void DetailRowCollapse() => _targetRequisitions = null;

    private async Task DetailDataBind(DetailDataBoundEventArgs<ProfSvc_Classes.Requisitions> requisition)
    {
        //VisibleProperty = true;

        if (_targetRequisitions != null)
        {
            if (_targetRequisitions != requisition.Data) // return when target is equal to args.data
            {
                await GridInnerRequisition.ExpandCollapseDetailRowAsync(_targetRequisitions);
            }
        }

        _targetRequisitions = requisition.Data;

        //_requisitionDetailsObject.ClearData();
        await Task.Delay(1);
        await Spinner.ShowAsync();
        //await Task.Delay(1000);
        RestClient _restClient = new($"{Start.ApiHost}");
        RestRequest request = new("Requisition/GetRequisitionDetails");
        request.AddQueryParameter("requisitionID", _targetRequisitions.ID);

        Dictionary<string, object> _restResponse = await _restClient.GetAsync<Dictionary<string, object>>(request);

        if (_restResponse != null)
        {
            _requisitionDetailsObject = JsonConvert.DeserializeObject<RequisitionDetails>(_restResponse["Requisition"]?.ToString() ?? string.Empty);
            //_candidateSkillsObject = General.DeserializeObject<List<CandidateSkills>>(_restResponse["Skills"]);
            //_candidateEducationObject = General.DeserializeObject<List<CandidateEducation>>(_restResponse["Education"]);
            //_candidateExperienceObject = General.DeserializeObject<List<CandidateExperience>>(_restResponse["Experience"]);
            _requisitionDocumentsObject = General.DeserializeObject<List<RequisitionDocuments>>(_restResponse["Documents"]);
            _candidateActivityObject = General.DeserializeObject<List<CandidateActivity>>(_restResponse["Activity"]);
            //_candidateNotesObject = General.DeserializeObject<List<CandidateNotes>>(_restResponse["Notes"]);
            //_candidateRatingObject = General.DeserializeObject<List<CandidateRating>>(_restResponse["Rating"]);
            //_candidateMPCObject = General.DeserializeObject<List<CandidateMPC>>(_restResponse["MPC"]);
            //_candidateDocumentsObject = General.DeserializeObject<List<CandidateDocument>>(_restResponse["Document"]);
            SetSkills();
        }
        
        //_selectedReqTab = _candidateActivityObject.Count > 0 ? 2 : 0;

        await Spinner.HideAsync();
    }

    //private List<IntValues> _skills;

    private void SetSkills()
    {
        if (_requisitionDetailsObject == null || Skills == null)
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
            IntValues _skill = Skills.FirstOrDefault(skill => skill.Key == _skillString.ToInt32());
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
            IntValues _skill = Skills.FirstOrDefault(skill => skill.Key == _skillString.ToInt32());
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

    [Parameter]
    public List<IntValues> Skills
    {
        get;
        set;
    }

    [Parameter]
    public double RowHeight
    {
        get;
        set;
    }

    [Parameter]
    public string User
    {
        get;
        set;
    } = "JOLLY";

    private async Task OnActionBegin(ActionEventArgs<ProfSvc_Classes.Requisitions> arg)
    {
        await Task.Delay(1);
    }

    private async Task OnActionComplete(ActionEventArgs<ProfSvc_Classes.Requisitions> arg)
    {
        await Task.Delay(1);
    }

    private async Task TabSelected(SelectEventArgs args)
    {
        await Task.Delay(1);
        _selectedReqTab = args.SelectedIndex;
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
            _request.AddQueryParameter("user", User.ToUpperInvariant());

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

    private CandidateActivity SelectedActivity
    {
        get;
        set;
    } = new();

    private List<KeyValues> NextSteps
    {
        get;
    } = new();

    private List<Workflow> _workflows = new();

    private ActivityPanelRequisition ActivityPanel
    {
        get;
        set;
    }

    private List<StatusCode> _statusCodes = new();

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

        //await DialogActivity.Dialog.ShowAsync();
    }

    [Inject]
    private IJSRuntime JsRuntime
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

    private RequisitionDocuments SelectedDownload
    {
        get;
        set;
    } = new();

    [Parameter]
    public int RowHeightActivity
    {
        get;
        set;
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
            _request.AddQueryParameter("user", User.ToUpperInvariant());
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

    private async Task DownloadDocument(int args)
    {
        await Task.Delay(1);
        SelectedDownload = DocumentsPanel.SelectedRow;
        string _queryString = (SelectedDownload.DocumentFileName + "^" + _targetRequisitions.ID + "^" + SelectedDownload.OriginalFileName + "^1").ToBase64String();
        //NavManager.NavigateTo(NavManager.BaseUri + "Download/" + _queryString);
        await JsRuntime.InvokeVoidAsync("open", $"{NavManager.BaseUri}Download/{_queryString}", "_blank");
        /*string _filePath = Path.Combine(Environment.WebRootPath, "Uploads", "Candidate", _target.ID.ToString(), SelectedDownload.InternalFileName).ToBase64String();
        byte[] _fileBytes = await File.ReadAllBytesAsync(_filePath);
        return File(_fileBytes, "application/force-download", _decodedStringArray[2]);*/
    }
}