#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           CodeExistsValidationAttribute.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 19:59
// Last Updated On:     01-04-2022 16:11
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Pages.Validation;

public class CodeExistsValidationAttribute : ValidationAttribute
{
    #region Methods

    protected override ValidationResult IsValid(object value, ValidationContext context)
    {
        string[] _memberNames =
        {
            context.MemberName
        };
        if (context.ObjectInstance is not AdminList _currentContext)
        {
            return new("Could not verify. Try again.", _memberNames);
        }

        if (!AdminListDefault.IsString)
        {
            return ValidationResult.Success;
        }

        if (_currentContext.Code.NullOrWhiteSpace() || _currentContext.Code.Length != 1)
        {
            return new($"{AdminListDefault.Type} is required and should be exactly 1 character long.", _memberNames);
        }

        if (!AdminListDefault.IsAdd || !AdminListDefault.IsString)
        {
            return ValidationResult.Success;
        }

        //string _url =
        //    $"{Start.ApiHost}admin/CheckCode?methodName={AdminListDefault.MethodName}&code={_currentContext.Code}&isString={(AdminListDefault.IsString ? "true" : "false")}";

        Thread.Sleep(1);
        RestClient _restClient = new($"{Start.ApiHost}");
        RestRequest _request = new("admin/CheckCode", Method.Get)
        {
            RequestFormat = DataFormat.Json
        };
        _request.AddQueryParameter("methodName", AdminListDefault.MethodName);
        _request.AddQueryParameter("code", _currentContext.Code);
        _request.AddQueryParameter("isString", AdminListDefault.IsString.ToString());
        Task<string> _response = _restClient.GetAsync<string>(_request);

        return _response.Result == "false" ? ValidationResult.Success : new($"{AdminListDefault.Type} already exists. Try again.", _memberNames);

        /*if (AdminListDefault.ClientFactory == null)
        {
            return new("Could not verify. Try again.", _memberNames);
        }

        HttpClient _client = AdminListDefault.ClientFactory.CreateClient("app");

        Task<HttpResponseMessage> _response = _client.GetAsync(_url);

        Task<string> _responseStream = _response.Result.Content.ReadAsStringAsync();

        return _responseStream.Result == "false" ? ValidationResult.Success : new($"{AdminListDefault.Type} already exists. Try again.", _memberNames);*/
    }

    #endregion
}