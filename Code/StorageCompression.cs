#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           StorageCompression.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:03
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Code;

public class StorageCompression
{
    #region Constructors

    public StorageCompression(ProtectedLocalStorage storage) => LocalStorage = storage;

    #endregion

    #region Properties

    private ProtectedLocalStorage LocalStorage
    {
        get;
    }

    #endregion

    #region Methods

    public async Task<bool> Delete(string cookieName)
    {
        try
        {
            await LocalStorage.DeleteAsync(cookieName);

            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<CandidateGrid> GetCandidateGrid()
    {
        try
        {
            await Task.Yield();
            //ProtectedBrowserStorageResult<CandidateGrid> _resultValue = await LocalStorage.GetAsync<CandidateGrid>("CandidateGrid");

            //CandidateGrid _candidateGrid;
            //if (_resultValue.Value == null)
            //{
            //    _candidateGrid = new();
            //    try
            //    {
            //        await LocalStorage.SetAsync("CandidateGrid", _candidateGrid);
            //    }
            //    catch
            //    {
            //    }
            //}
            //else
            //{
            //_candidateGrid = _resultValue.Value ?? new();
            //}

            return null; //_candidateGrid;
        }
        catch
        {
            return null;
            //return new();
        }
    }

    public async Task<Cooky> Get(string cookieName)
    {
        try
        {
            ProtectedBrowserStorageResult<Cooky> _resultValue = await LocalStorage.GetAsync<Cooky>(cookieName);
            Cooky _cooky;
            //if (_resultValue.Value == null)
            //{
            //    _cooky = new();
            //       await LocalStorage.SetAsync("GridVal", _cooky);
            //}
            //else
            //{
            _cooky = _resultValue.Value ?? new();
            //}

            return _cooky;
        }
        catch
        {
            return new();
        }
    }

    public async Task<int> Set(string cookieName, Cooky cookieValue)
    {
        try
        {
            await LocalStorage.SetAsync(cookieName, cookieValue);
        }
        catch (Exception)
        {
            return 0;
        }

        return 0;
    }

    public async Task<int> SetCandidateGrid()
    {
        try
        {
            await Task.Yield();
            //await LocalStorage.SetAsync("CandidateGrid", Candidate.CandidateGridPersistValues);
        }
        catch (Exception)
        {
            return 0;
        }

        return 0;
    }

    #endregion
}