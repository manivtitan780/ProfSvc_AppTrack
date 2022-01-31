#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           LabelTextBox.razor.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          01-01-2022 14:53
// Last Updated On:     01-04-2022 16:04
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Admin.Controls;

public partial class LabelTextBox
{
    #region Fields

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

    [CascadingParameter]
    public EditContext EditContext
    {
        get;
        set;
    } = default!;

    [Parameter]
    public EventCallback<string> ValueChanged
    {
        get;
        set;
    }

    [Parameter]
    public Expression<Func<string>> ValidationMessage
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
    }

    /*private bool IsTextBoxInvalid()
    {
        bool _invalid = EditContext.GetValidationMessages(Field).Any();
        Box?.UpdateParentClass("", "");
        if (_invalid)
        {
            Box.CssClass = "e-error invalid";
        }
        else
        {
            Box.CssClass = "e-success";
        }
        return _invalid;
    }*/

    [Parameter]
    public string Value
    {
        get => _value;
        set
        {
            if (value == null)
            {
                _value = "";
            }

            if (_value == value)
            {
                return;
            }

            _value = value;
            ValueChanged.InvokeAsync(value);
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

    private FieldIdentifier Field
    {
        get;
        set;
    }

    private SfTextBox Box
    {
        get;
        set;
    }

    #endregion

    #region Methods

    protected override void OnInitialized()
    {
        Field = EditContext.Field("Text");
        EditContext.OnValidationStateChanged += HandleValidationStateChanged;
    }

    private void HandleValidationStateChanged(object sender, ValidationStateChangedEventArgs e)
    {
        StateHasChanged();
        bool _invalid = EditContext.GetValidationMessages(Field).Any();
        Box?.UpdateParentClass("", "");
        if (Box != null)
        {
            Box.CssClass = _invalid ? "e-error invalid" : "e-success";
        }

        StateHasChanged();
    }

    #endregion
}