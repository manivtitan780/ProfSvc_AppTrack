#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           DownloadsPanel.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          02-22-2022 20:32
// Last Updated On:     09-14-2022 15:49
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Candidates;

public partial class DownloadsPanel
{
    [Parameter]
    public EventCallback<int> DeleteDocument
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<int> DownloadDocument
    {
        get;
        set;
    }

    [Parameter]
    public List<CandidateDocument> ModelObject
    {
        get;
        set;
    }

    [Parameter]
    public int RowHeight
    {
        get;
        set;
    }

    public CandidateDocument SelectedRow
    {
        get;
        set;
    }

    private SfGrid<CandidateDocument> GridDownload
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

    private async Task DeleteDocumentMethod(int id)
    {
        double _index = await GridDownload.GetRowIndexByPrimaryKeyAsync(id);
        await GridDownload.SelectRowAsync(_index);
        if (await JsRuntime.Confirm("Are you sure you want to delete this Document?" + Environment.NewLine + "Note: This action cannot be reversed."))
        {
            await DeleteDocument.InvokeAsync(id);
        }
    }

    private async Task DownloadDocumentDialog(int id)
    {
        double _index = await GridDownload.GetRowIndexByPrimaryKeyAsync(id);
        await GridDownload.SelectRowAsync(_index);
        await DownloadDocument.InvokeAsync(id);
    }

    private void RowSelected(RowSelectEventArgs<CandidateDocument> companyDocument)
    {
        if (companyDocument != null)
        {
            SelectedRow = companyDocument.Data;
        }
    }
}