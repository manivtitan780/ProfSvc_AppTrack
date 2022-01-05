#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           EditDialog.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 19:59
// Last Updated On:     01-04-2022 16:03
// *****************************************/

#endregion

#region Using

#endregion

namespace ProfSvc_AppTrack.Pages.Admin.Controls;

public partial class EditDialog
{
    #region Fields

    private EditContext _context;

    #endregion

    #region Properties

    [Parameter]
    public AdminList AdminContext
    {
        get;
        set;
    }

    [Parameter]
    public bool Readonly
    {
        get;
        set;
    }

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

    [Parameter]
    public string ID
    {
        get;
        set;
    }

    [Parameter]
    public string Placeholder
    {
        get;
        set;
    }

    #endregion

    #region Methods

    public override Task SetParametersAsync(ParameterView parameters)
    {
        if (_context != null && AdminContext != null)
        {
            _context = new(AdminContext);
        }

        return base.SetParametersAsync(parameters);
    }

    public void ToolTipOpen(TooltipEventArgs args)
    {
        _context?.Validate();
        args.Cancel = !args.HasText;
    }

    #endregion
}