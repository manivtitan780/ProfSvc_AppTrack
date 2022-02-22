#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           EditActivityDialog.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          02-17-2022 19:33
// Last Updated On:     02-18-2022 20:12
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Candidates;

public partial class EditActivityDialog
{
    [Parameter]
    public EventCallback CancelActivity
    {
        get;
        set;
    }

    public SfDialog Dialog
    {
        get;
        set;
    }

    [Parameter]
    public List<StatusCode> StatusCodes
    {
        get;
        set;
    }

    [Parameter]
    public CandidateActivity ModelObject
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
    public List<KeyValues> ModelSteps
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<EditContext> SaveActivity
    {
        get;
        set;
    }

    private async Task CancelDialog(MouseEventArgs args)
    {
        await Task.Delay(1);
        await CancelActivity.InvokeAsync(args);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }

    private async Task SaveActivityDialog(EditContext editContext)
    {
        await Task.Delay(1);
        await Spinner.ShowAsync();
        await SaveActivity.InvokeAsync(editContext);
        await Spinner.HideAsync();
        Dialog.Height = "200px";
        await Dialog.HideAsync();
    }

    [Parameter]
    public List<Workflow> Status
    {
        get;
        set;
    } = new();

    private DateTime Max
    {
        get;
        set;
    }

    private DateTime Min
    {
        get;
        set;
    }

    private List<KeyValues> InterviewTypes
    {
        get;
        set;
    } = new();

    private ElementReference DivSchedule
    {
        get;
        set;
    }

    private bool IsShow
    {
        get;
        set;
    } = false;

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(1);
        InterviewTypes.Add(new() {Value = "I", Key = "In-Person Interview"});
        InterviewTypes.Add(new() {Value = "P", Key = "Telephonic Interview"});
        InterviewTypes.Add(new() {Value = "O", Key = "Others"});
        InterviewTypes.Add(new() {Value = "", Key = "None"});
        Min = DateTime.Today.AddMonths(-1);
        Max = DateTime.Today.AddMonths(3);
    }

    private async Task ChangeStatus(ChangeEventArgs<string, KeyValues> status)
    {
        await Task.Delay(1);
        IsShow = false;
        if (Status != null)
        {
            IsShow = Status.Any(_ => _.Step == status.Value && _.Schedule);
            /*foreach (Workflow _ in Status.Any(_ => _.Step == status.Value && _.Schedule))
            {
                IsShow = true;
                break;
            }*/
        }

        Dialog.Height = IsShow ? "98vh" : "460px";
        StateHasChanged();
    }
}