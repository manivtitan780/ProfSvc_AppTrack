#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           EditDialog.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-26-2022 19:30
// Last Updated On:     02-12-2022 19:44
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin.Controls;

/// <summary>
/// </summary>
public partial class EditDialog : ComponentBase
{
    private EditContext _context;

    /// <summary>
    /// </summary>
    [Parameter]
    public AdminList AdminContext
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    [Parameter]
    public Dictionary<string, object> Attributes
    {
        get;
        set;
    } = new()
        {
            {
                "maxlength", "100"
            },
            {
                "minlength", "2"
            }
        };

    /// <summary>
    /// </summary>
    [Parameter]
    public string ID
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
    public bool Readonly
    {
        get;
        set;
    }

    /// <summary>
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public override Task SetParametersAsync(ParameterView parameters)
    {
        if (_context != null && AdminContext != null)
        {
            _context = new(AdminContext);
        }

        return base.SetParametersAsync(parameters);
    }

    /// <summary>
    /// </summary>
    /// <param name="args"></param>
    private void ToolTipOpen(TooltipEventArgs args)
    {
        _context?.Validate();
        args.Cancel = !args.HasText;
    }
}