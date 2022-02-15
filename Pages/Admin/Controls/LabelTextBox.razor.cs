#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           LabelTextBox.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-31-2022 19:26
// Last Updated On:     02-12-2022 19:29
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin.Controls;

/// <inheritdoc />
public partial class LabelTextBox
{
    private string _value;

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public bool CreateDivTag
    {
        get;
        set;
    } = true;

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public bool CreateTooltip
    {
        get;
        set;
    } = true;

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string CssClass
    {
        get;
        set;
    }

    /// <summary>
    /// 
    /// </summary>
    [CascadingParameter]
    public EditContext EditContext
    {
        get;
        set;
    } = default!;

    /*/// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string FieldName
    {
        get;
        set;
    }*/

    /// <summary>
    /// </summary>
    [Parameter]
    public string Height
    {
        get;
        set;
    } = "inherit";

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string ID
    {
        get;
        set;
    }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public int MaxLength
    {
        get;
        set;
    }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public int MinLength
    {
        get;
        set;
    } = 0;

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public bool Multiline
    {
        get;
        set;
    } = false;

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string Placeholder
    {
        get;
        set;
    }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public bool Readonly
    {
        get;
        set;
    } = false;

    /// <summary>
    /// </summary>
    [Parameter]
    public InputType TextBoxType
    {
        get;
        set;
    } = InputType.Text;

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public Expression<Func<string>> ValidationMessage
    {
        get;
        set;
    }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string Value
    {
        get => _value;
        set
        {
            if (EqualityComparer<string>.Default.Equals(value, _value))
            {
                return;
            }

            _value = value;
            ValueChanged.InvokeAsync(value);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public EventCallback<string> ValueChanged
    {
        get;
        set;
    }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public Expression<Func<string>> ValueExpression
    {
        get;
        set;
    }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public string Width
    {
        get;
        set;
    } = "98%";

    private SfTextBox Box
    {
        get;
        set;
    }

    private FieldIdentifier Field
    {
        get;
        set;
    }

    /*protected override void OnInitialized()
    {
        if (FieldName.NullOrWhiteSpace())
        {
            return;
        }

        Field = EditContext.Field(FieldName);
        EditContext.OnValidationStateChanged += HandleValidationStateChanged;
    }

    private async Task HandleValidationStateChanged(object sender, ValidationStateChangedEventArgs e)
    {
        await Task.Delay(1);
        Box?.UpdateParentClass("", "");
        StateHasChanged();
    }*/

    private void ToolTipOpen(TooltipEventArgs args) => args.Cancel = !args.HasText;
}