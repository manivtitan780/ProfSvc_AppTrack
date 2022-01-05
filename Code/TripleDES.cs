#region Header

// /*****************************************
// Copyright:           Titan-Techs.
// Location:            Newtown, PA, USA
// Solution:            ProfSvc_AppTrack
// Project:             ProfSvc_AppTrack
// File Name:           TripleDES.cs
// Created By:          Narendra Kumaran Kadhirvelu, Jolly Joseph Paily, DonBosco Paily
// Created On:          12-16-2021 19:27
// Last Updated On:     01-04-2022 16:03
// *****************************************/

#endregion

namespace ProfSvc_AppTrack.Code;

/// <summary>
///     Summary description for TripleDES
/// </summary>
public class TripleDes : IDisposable
{
    #region Constructors

    public TripleDes(byte[] key, byte[] iv)
    {
        _tripleKey = key;
        _vectorByte = iv;
    }

    #endregion

    #region Fields

    private byte[] _tripleKey;

    private byte[] _vectorByte;

    private TripleDES _tripleDes1 = TripleDES.Create();
    private UTF8Encoding _encoder = new();

    #endregion

    #region Properties

    private static byte[] Transform(byte[] input, ICryptoTransform cryptoTransform)
    {
        using MemoryStream _stream2 = new();
        using CryptoStream _stream = new(_stream2, cryptoTransform, CryptoStreamMode.Write);
        _stream.Write(input, 0, input.Length);
        _stream.FlushFinalBlock();
        _stream2.Position = 0L;
        byte[] _buffer = new byte[(int)(_stream2.Length - 1L) + 1];
        _stream2.Read(_buffer, 0, _buffer.Length);

        return _buffer;
    }

    #endregion

    #region IDisposable Members

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    #endregion

    #region Methods

    public byte[] Decrypt(byte[] input) => Transform(input, _tripleDes1.CreateDecryptor(_tripleKey, _vectorByte));

    public byte[] Encrypt(byte[] input) => Transform(input, _tripleDes1.CreateEncryptor(_tripleKey, _vectorByte));

    public string Decrypt(string text)
    {
        byte[] _input = Convert.FromBase64String(text);
        byte[] _bytes = Transform(_input, _tripleDes1.CreateDecryptor(_tripleKey, _vectorByte));

        return _encoder.GetString(_bytes);
    }

    public string Encrypt(string text)
    {
        byte[] _bytes = _encoder.GetBytes(text);

        return Convert.ToBase64String(Transform(_bytes, _tripleDes1.CreateEncryptor(_tripleKey, _vectorByte)));
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposing)
        {
            return;
        }

        _encoder = null;
        _tripleKey = null;
        _vectorByte = null;

        if (_tripleDes1 == null)
        {
            return;
        }

        _tripleDes1.Dispose();
        _tripleDes1 = null;
    }

    #endregion
}