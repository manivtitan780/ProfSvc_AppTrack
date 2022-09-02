#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           DocumentsCompanyPanel.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          08-29-2022 15:23
// Last Updated On:     08-29-2022 15:24
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Companies;

public partial class DocumentsCompanyPanel
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