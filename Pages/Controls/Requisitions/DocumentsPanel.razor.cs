#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           DocumentsPanel.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          07-22-2022 20:51
// Last Updated On:     07-22-2022 21:03
// *****************************************/

#endregion

using Microsoft.JSInterop;

namespace ProfSvc_AppTrack.Pages.Controls.Requisitions;

public partial class DocumentsPanel
{
    [Parameter]
    public EventCallback<int> DownloadDocument
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<int> DeleteDocument
    {
        get;
        set;
    }

    public SfGrid<RequisitionDocuments> GridDownload
    {
        get;
        set;
    }

    [Parameter]
    public List<RequisitionDocuments> ModelObject
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

    public RequisitionDocuments SelectedRow
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

    private void RowSelected(RowSelectEventArgs<RequisitionDocuments> row)
    {
        if (row != null)
        {
            SelectedRow = row.Data;
        }
    }
}