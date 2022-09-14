#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           AddCompanyDocument.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          09-14-2022 19:42
// Last Updated On:     09-14-2022 19:42
// *****************************************/

#endregion

using ActionCompleteEventArgs = Syncfusion.Blazor.Inputs.ActionCompleteEventArgs;
using FailureEventArgs = Syncfusion.Blazor.Inputs.FailureEventArgs;

namespace ProfSvc_AppTrack.Pages.Controls.Companies;

public partial class AddCompanyDocument
{
    public EventCallback<MouseEventArgs> Cancel
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
    public RequisitionDocuments ModelObject
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback<UploadChangeEventArgs> OnFileUpload
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback<EditContext> Save
    {
        get;
        set;
    }

    public DialogFooter DialogFooter
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
    public EventCallback<BeforeUploadEventArgs> BeforeUpload
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<ActionCompleteEventArgs> AfterUpload
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<UploadingEventArgs> UploadStart
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<SuccessEventArgs> Success
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<FailureEventArgs> Failure
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<SelectedEventArgs> FileSelect
    {
        get;
        set;
    }

    private async Task CancelDocumentDialog(MouseEventArgs args)
    {
        await Task.Delay(1);
        await Cancel.InvokeAsync(args);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }

    private async Task SaveDocumentDialog(EditContext args)
    {
        await Task.Delay(1);
        await Spinner.ShowAsync();
        await Save.InvokeAsync(args);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }
}