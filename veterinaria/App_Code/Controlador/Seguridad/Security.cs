using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de Security
/// </summary>
public class Security
{
    private string parametroEncriptar;
    private string parametroDesencriptar;

    public Security()
    { 
    
    }

    public Security(string parametro)
	{
        this.parametroEncriptar = parametro;
        this.parametroDesencriptar = parametro;
	}

    //Metodo para cifrar
    public string Encriptar()
    {
        string result = string.Empty;
        byte[] encryted = System.Text.Encoding.Unicode.GetBytes(parametroEncriptar);
        result = Convert.ToBase64String(encryted);
        return result;
    }

    //Metodo para quitar cifrado
    public string DesEncriptar()
    {
        string result = string.Empty;
        byte[] decryted = Convert.FromBase64String(parametroDesencriptar);
        //result = System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
        result = System.Text.Encoding.Unicode.GetString(decryted);
        return result;
    }


    public string getSha512(string Password)
    {
        System.Security.Cryptography.SHA512Managed HashTool = new System.Security.Cryptography.SHA512Managed();
        Byte[] PasswordAsByte = System.Text.Encoding.UTF8.GetBytes(Password);
        Byte[] EncryptedBytes = HashTool.ComputeHash(PasswordAsByte);
        HashTool.Clear();
        return Convert.ToBase64String(EncryptedBytes);

    }

    public String CreaHash(String cadena, String llave) {
        System.Text.UTF8Encoding sEncoding = new System.Text.UTF8Encoding();

        Byte[] key = sEncoding.GetBytes(llave);
        Byte[] Text = sEncoding.GetBytes(cadena);
        System.Security.Cryptography.HMACSHA1 sHMACSHA1=new System.Security.Cryptography.HMACSHA1(key);
        Byte[] HashCode = sHMACSHA1.ComputeHash(Text);
        String hash = BitConverter.ToString(HashCode).Replace("-", "").ToUpper();

        return hash;
    }


}