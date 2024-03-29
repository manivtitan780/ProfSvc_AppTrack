﻿#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           EditActivityDialog.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          02-17-2022 19:33
// Last Updated On:     07-09-2022 15:56
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

    [Parameter]
    public bool IsCandidate
    {
        get;
        set;
    } = true;

    public SfDialog Dialog
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

    [Parameter]
    public List<KeyValues> ModelSteps
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<EditContext> Save
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
    public List<Workflow> Status
    {
        get;
        set;
    } = new();

    [Parameter]
    public List<StatusCode> StatusCodes
    {
        get;
        set;
    }

    private ElementReference DivSchedule
    {
        get;
        set;
    }

    private List<KeyValues> InterviewTypes
    {
        get;
        set;
    } = new()
        {
            new("In-Person Interview", "I"),
            new("Telephonic Interview", "P"),
            new("Others", "O"),
            new("None", "")
        };

    private bool IsShow
    {
        get;
        set;
    } = false;

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

    protected override async Task OnInitializedAsync()
    {
        await Task.Delay(1);
        Min = DateTime.Today.AddMonths(-1);
        Max = DateTime.Today.AddMonths(3);
    }

    private async Task CancelDialog(MouseEventArgs args)
    {
        await Task.Delay(1);
        await CancelActivity.InvokeAsync(args);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }

    private async Task ChangeStatus(ChangeEventArgs<string, KeyValues> status)
    {
        await Task.Delay(1);
        IsShow = false;
        if (Status != null)
        {
            IsShow = Status.Any(_ => _.Step == status.Value && _.Schedule);
        }

        Dialog.Height = IsShow ? "98vh" : "460px";
        StateHasChanged();
    }

    private async Task SaveActivityDialog(EditContext editContext)
    {
        await Task.Delay(1);
        await Spinner.ShowAsync();
        await Save.InvokeAsync(editContext);
        await Spinner.HideAsync();
        Dialog.Height = "200px";
        await Dialog.HideAsync();
    }
}