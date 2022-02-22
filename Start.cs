#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           Start.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          11-18-2021 20:14
// Last Updated On:     01-04-2022 16:11
// *****************************************/

#endregion

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace ProfSvc_AppTrack;

public class Start
{
    public static string ApiHost
    {
        get;
        set;
    }

    public static string ConnectionString
    {
        get;
        set;
    }

    public static IMemoryCache MemCache
    {
        get;
        set;
    }

    public static async void SetCache()
    {
        if (MemCache == null)
        {
            return;
        }

        RestClient _restClient = new($"{ApiHost}");
        RestRequest _request = new("Admin/GetCache");
        Dictionary<string, object> _restResponse = await _restClient.GetAsync<Dictionary<string, object>>(_request);
        MemoryCacheEntryOptions _cacheOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(1440));

        if (_restResponse == null)
        {
            return;
        }

        List<IntValues> _states = General.DeserializeObject<List<IntValues>>(_restResponse["States"]);
        List<IntValues> _eligibility = General.DeserializeObject<List<IntValues>>(_restResponse["Eligibility"]);
        List<KeyValues> _jobOptions = General.DeserializeObject<List<KeyValues>>(_restResponse["JobOptions"]);
        List<KeyValues> _taxTerms = General.DeserializeObject<List<KeyValues>>(_restResponse["TaxTerms"]);
        List<CandidateSkills> _skills = General.DeserializeObject<List<CandidateSkills>>(_restResponse["Skills"]);
        List<IntValues> _experience = General.DeserializeObject<List<IntValues>>(_restResponse["Experience"]);
        List<Template> _templates = General.DeserializeObject<List<Template>>(_restResponse["Templates"]);
        List<User> _users = General.DeserializeObject<List<User>>(_restResponse["Users"]);
        List<StatusCode> _statusCodes = General.DeserializeObject<List<StatusCode>>(_restResponse["StatusCodes"]);
        List<Zip> _zips = General.DeserializeObject<List<Zip>>(_restResponse["Zips"]);
        List<IntValues> _education = General.DeserializeObject<List<IntValues>>(_restResponse["Education"]);
        List<Company> _companies = General.DeserializeObject<List<Company>>(_restResponse["Companies"]);
        List<CompanyContact> _companyContacts = General.DeserializeObject<List<CompanyContact>>(_restResponse["CompanyContacts"]);
        List<Role> _roles = General.DeserializeObject<List<Role>>(_restResponse["Roles"]);
        List<IntValues> _titles = General.DeserializeObject<List<IntValues>>(_restResponse["Titles"]);
        List<IntValues> _leadSources = General.DeserializeObject<List<IntValues>>(_restResponse["LeadSources"]);
        List<IntValues> _leadIndustries = General.DeserializeObject<List<IntValues>>(_restResponse["LeadIndustries"]);
        List<IntValues> _leadStatus = General.DeserializeObject<List<IntValues>>(_restResponse["LeadStatus"]);
        List<CommissionConfigurator> _commissionConfigurators = General.DeserializeObject<List<CommissionConfigurator>>(_restResponse["CommissionConfigurators"]);
        List<VariableCommission> _variableCommissions = General.DeserializeObject<List<VariableCommission>>(_restResponse["VariableCommissions"]);
        List<Workflow> _workflows = General.DeserializeObject<List<Workflow>>(_restResponse["Workflow"]);
        List<KeyValues> _communications = new();
        _communications.AddRange(new[]
                                 {
                                     new KeyValues("A", "Average"), new KeyValues("X", "Excellent"), new KeyValues("F", "Fair"),
                                     new KeyValues("G", "Good")
                                 });

        MemCache.Set("States", _states, _cacheOptions);
        MemCache.Set("Eligibility", _eligibility, _cacheOptions);
        MemCache.Set("JobOptions", _jobOptions, _cacheOptions);
        MemCache.Set("TaxTerms", _taxTerms, _cacheOptions);
        MemCache.Set("Skills", _skills, _cacheOptions); 
        MemCache.Set("Experience", _experience, _cacheOptions);
        MemCache.Set("Templates", _templates, _cacheOptions);
        MemCache.Set("Users", _users, _cacheOptions);
        MemCache.Set("StatusCodes", _statusCodes, _cacheOptions);
        MemCache.Set("Zips", _zips, _cacheOptions);
        MemCache.Set("Education", _education, _cacheOptions);
        MemCache.Set("Companies", _companies, _cacheOptions);
        MemCache.Set("CompanyContacts", _companyContacts, _cacheOptions);
        MemCache.Set("Roles", _roles, _cacheOptions);
        MemCache.Set("Titles", _titles, _cacheOptions);
        MemCache.Set("LeadSources", _leadSources, _cacheOptions);
        MemCache.Set("LeadIndustries", _leadIndustries, _cacheOptions);
        MemCache.Set("LeadStatus", _leadStatus, _cacheOptions);
        MemCache.Set("CommissionConfigurators", _commissionConfigurators, _cacheOptions);
        MemCache.Set("VariableCommissions", _variableCommissions, _cacheOptions);
        MemCache.Set("Communication", _communications, _cacheOptions);
        MemCache.Set("Workflow", _workflows, _cacheOptions);
    }
}