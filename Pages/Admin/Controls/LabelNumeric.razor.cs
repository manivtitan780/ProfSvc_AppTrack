#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           LabelNumeric.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 19:59
// Last Updated On:     01-04-2022 16:04
// *****************************************/

#endregion

#region Using

#endregion

namespace ProfSvc_AppTrack.Pages.Admin.Controls;

public partial class LabelNumeric
{
    #region Fields

    private decimal _value;
    private EditContext _context;

    #endregion

    #region Properties

    [Parameter]
    public decimal BindValue
    {
        get => _value;
        set
        {
            if (_value == value)
            {
                return;
            }

            _value = value;
            BindValueChanged.InvokeAsync(value);
        }
    }

    [Parameter]
    public decimal Min
    {
        get;
        set;
    }

    [Parameter]
    public decimal Step
    {
        get;
        set;
    } = 1;

    [Parameter]
    public EventCallback<decimal> BindValueChanged
    {
        get;
        set;
    }

    [Parameter]
    public int? Decimals
    {
        get;
        set;
    } = 0;

    [Parameter]
    public object Context
    {
        get;
        set;
    }

    [Parameter]
    public RenderFragment ValidationTemplate
    {
        get;
        set;
    }

    [Parameter]
    public string FormatNumericBox
    {
        get;
        set;
    }

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
        if (Context != null)
        {
            _context = new(Context);
        }

        return base.SetParametersAsync(parameters);
    }

    public void ToolTipOpen(TooltipEventArgs args)
    {
        _context?.Validate();
        args.Cancel = !args.HasText;
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
        _context.Validate();
        _context.NotifyValidationStateChanged();
        //StateHasChanged();
    }

    protected override async void OnInitialized()
    {
        if (Context != null)
        {
            _context = new(Context);
        }

        await base.OnInitializedAsync();
    }

    #endregion
}