using System;
using System.Security.Cryptography;
using System.Text;

// Token: 0x02000005 RID: 5
public static class Pikolo
{
	// Token: 0x0600000B RID: 11 RVA: 0x00002CF8 File Offset: 0x00000EF8
	public static string PikoloData(string Message)
	{
		UTF8Encoding utf8Encoding = new UTF8Encoding();
		MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
		byte[] key = md5CryptoServiceProvider.ComputeHash(utf8Encoding.GetBytes("CqbkTHriRRbQjaArtJfF"));
		TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
		tripleDESCryptoServiceProvider.Key = key;
		tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
		tripleDESCryptoServiceProvider.Padding = PaddingMode.PKCS7;
		byte[] bytes = utf8Encoding.GetBytes(Message);
		byte[] inArray;
		try
		{
			inArray = tripleDESCryptoServiceProvider.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
		}
		finally
		{
			tripleDESCryptoServiceProvider.Clear();
			md5CryptoServiceProvider.Clear();
		}
		return Convert.ToBase64String(inArray);
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00002D88 File Offset: 0x00000F88
	public static string DePikoloData(string Message)
	{
		UTF8Encoding utf8Encoding = new UTF8Encoding();
		MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
		byte[] key = md5CryptoServiceProvider.ComputeHash(utf8Encoding.GetBytes("CqbkTHriRRbQjaArtJfF"));
		TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
		tripleDESCryptoServiceProvider.Key = key;
		tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
		tripleDESCryptoServiceProvider.Padding = PaddingMode.PKCS7;
		byte[] array = Convert.FromBase64String(Message);
		byte[] bytes;
		try
		{
			bytes = tripleDESCryptoServiceProvider.CreateDecryptor().TransformFinalBlock(array, 0, array.Length);
		}
		finally
		{
			tripleDESCryptoServiceProvider.Clear();
			md5CryptoServiceProvider.Clear();
		}
		return utf8Encoding.GetString(bytes);
	}
}
