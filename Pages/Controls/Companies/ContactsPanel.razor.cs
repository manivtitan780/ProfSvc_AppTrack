#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           ContactsPanel.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          08-24-2022 21:06
// Last Updated On:     08-24-2022 21:29
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Controls.Companies;

public partial class ContactsPanel
{
    [Parameter]
    public EventCallback<int> DeleteContact
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<int> EditContact
    {
        get;
        set;
    }

    [Parameter]
    public List<CompanyContact> ModelObject
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

    public CompanyContact SelectedRow
    {
        get;
        set;
    }

    [Parameter]
    public string UserName
    {
        get;
        set;
    }

    private SfGrid<CompanyContact> GridContacts
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

    private async Task DeleteContactMethod(int id)
    {
        double _index = await GridContacts.GetRowIndexByPrimaryKeyAsync(id);
        await GridContacts.SelectRowAsync(_index);
        if (await JsRuntime.Confirm($"Are you sure you want to delete this Contact?{Environment.NewLine}Note: This action cannot be reversed."))
        {
            await DeleteContact.InvokeAsync(id);
        }
    }

    private async Task EditContactDialog(int id)
    {
        double _index = await GridContacts.GetRowIndexByPrimaryKeyAsync(id);
        await GridContacts.SelectRowAsync(_index);
        await EditContact.InvokeAsync(id);
    }

    private void RowSelected(RowSelectEventArgs<CompanyContact> contact)
    {
        if (contact != null)
        {
            SelectedRow = contact.Data;
        }
    }
}