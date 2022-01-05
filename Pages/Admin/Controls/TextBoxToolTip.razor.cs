#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           TextBoxToolTip.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 19:59
// Last Updated On:     01-04-2022 16:04
// *****************************************/

#endregion

#region Using

#endregion

namespace ProfSvc_AppTrack.Pages.Admin.Controls;

public partial class TextBoxToolTip : ComponentBase
{
    #region Fields

    private EditContext _context;
    private string _value;

    #endregion

    #region Properties

    [Parameter]
    public bool Multiline
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
                "maxlength", _maxLength
            },
            {
                "minlength", _minLength
            },
            {
                "rows", _rows
            }
        };

    [Parameter]
    public EventCallback<string> BindValueChanged
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback<TooltipEventArgs> OnOpen
    {
        get;
        set;
    }

    [Parameter]
    public int MaxLength
    {
        get => _maxLength;
        set => _maxLength = value;
    }

    [Parameter]
    public int MinLength
    {
        get => _minLength;
        set => _minLength = value;
    }

    [Parameter]
    public int Rows
    {
        get => _rows;
        set => _rows = value;
    }

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
    public string BindValue
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

    private static int _maxLength;
    private static int _minLength;
    private static int _rows;

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