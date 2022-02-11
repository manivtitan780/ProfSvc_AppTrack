#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           LabelTextBox.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-26-2022 19:30
// Last Updated On:     01-29-2022 19:31
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin.Controls;

public partial class LabelTextBox
{
    private string _value;

    [Parameter]
    public bool CreateDivTag
    {
        get;
        set;
    } = true;

    [Parameter]
    public bool CreateTooltip
    {
        get;
        set;
    } = true;

    [Parameter]
    public string CssClass
    {
        get;
        set;
    }

    [CascadingParameter]
    public EditContext EditContext
    {
        get;
        set;
    } = default!;

    [Parameter]
    public string FieldName
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
    public int MaxLength
    {
        get;
        set;
    }

    [Parameter]
    public int MinLength
    {
        get;
        set;
    } = 0;

    [Parameter]
    public bool Multiline
    {
        get;
        set;
    } = false;

    [Parameter]
    public string Placeholder
    {
        get;
        set;
    }

    [Parameter]
    public bool Readonly
    {
        get;
        set;
    } = false;

    [Parameter]
    public Expression<Func<string>> ValidationMessage
    {
        get;
        set;
    }

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

    [Parameter]
    public EventCallback<string> ValueChanged
    {
        get;
        set;
    }

    [Parameter]
    public Expression<Func<string>> ValueExpression
    {
        get;
        set;
    }

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

    [Parameter]
    public string Height
    {
        get;
        set;
    } = "inherit";

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