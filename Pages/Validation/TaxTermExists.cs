﻿#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           TaxTermExists.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 19:59
// Last Updated On:     01-04-2022 16:11
// *****************************************/

#endregion

#region Using

using System.Data;

//using Bunit.Extensions;

#endregion

namespace ProfSvc_AppTrack.Pages.Validation;

public class TaxTermExists : ComponentBase
{
    #region Fields

    private string _connectionString = "";

    private ValidationMessageStore messageStore;

    #endregion

    #region Properties

    [Parameter]
    public bool IsAdd
    {
        get;
        set;
    }

    [Parameter]
    public ValidatorTemplateContext Context
    {
        get;
        set;
    }

    [CascadingParameter]
    private EditContext CurrentEditContext
    {
        get;
        set;
    }

    [Inject]
    private IConfiguration Configuration
    {
        get;
        set;
    }

    #endregion

    #region Methods

    protected void HandleValidation(FieldIdentifier identifier)
    {
        if (identifier.Model is TextBoxControl _box && _box.ID != "textCode")
        {
            return;
        }

        if (!IsAdd)
        {
            return;
        }

        messageStore.Clear(identifier);
        using SqlConnection _con = new(_connectionString);
        _con.Open();
        try
        {
            using SqlCommand _command = new("Admin_CheckTaxTermCode", _con)
                                        {
                                            CommandType = CommandType.StoredProcedure
                                        };
            _command.AddCharParameter("@Code", 1, (Context.Data as TaxTerm)?.Code);

            bool _exists = _command.ExecuteScalar().ToBoolean();
            if (_exists)
            {
                messageStore.Add(identifier, "Tax Term Code already exists. Enter another Code.");
            }
            else
            {
                messageStore.Clear();
            }
        }
        catch
        {
            messageStore.Add(identifier, "Could not verify. Try again.");
        }
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
        if (firstRender || _connectionString.NullOrWhiteSpace())
        {
            _connectionString = Configuration.GetConnectionString("DBConnect");
        }
    }

    protected override void OnInitialized()
    {
        messageStore = new(CurrentEditContext);

        CurrentEditContext.OnValidationRequested += ValidateRequested;
        CurrentEditContext.OnFieldChanged += ValidateField;
    }

    protected void ValidateField(object editContext, FieldChangedEventArgs fieldChangedEventArgs)
    {
        HandleValidation(fieldChangedEventArgs.FieldIdentifier);
    }

    private void ValidateRequested(object editContext, ValidationRequestedEventArgs validationEventArgs)
    {
        HandleValidation(CurrentEditContext.Field("Field"));
    }

    #endregion
}