#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           AdminListDialog.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-26-2022 19:30
// Last Updated On:     02-12-2022 19:41
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin.Controls;

/// <summary>
/// </summary>
public partial class AdminListDialog
{
    /// <summary>
    /// </summary>
    [Parameter]
    public EventCallback<MouseEventArgs> Cancel
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    public SfDialog Dialog
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public string HeaderString
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public Dictionary<string, object> HtmlAttributes
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public AdminList Model
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public string Placeholder
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

    /// <summary>
    /// </summary>
    public SfSpinner Spinner
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    public async Task CancelAdminList(MouseEventArgs args)
    {
        await Task.Delay(1);
        await Cancel.InvokeAsync(args);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }

    /// <summary>
    /// </summary>
    /// <param name="editContext"></param>
    public async Task SaveAdminList(EditContext editContext)
    {
        await Task.Delay(1);
        await Spinner.ShowAsync();
        await Save.InvokeAsync(editContext);
        await Task.Delay(1);
        await Spinner.HideAsync();
        await Dialog.HideAsync();
    }

    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    public void ToolTipOpen(TooltipEventArgs args)
    {
        //new EditContext(Model).Validate();
        args.Cancel = !args.HasText;
    }
}