using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;

public class common
{
    public common()
    {
    }

    public static string projectName = "Morya";
    public static string projectAdmin = "Morya Admin";

    #region "Encrypt/Decrypt functions"

    public static string Encrypt_New(string toEncrypt, bool useHashing)
    {
        byte[] keyArray;
        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

        // Get the key from config file

        string key = ConfigurationManager.AppSettings["EncryptionKey"].ToString();

        //If hashing use get hashcode regards to your key
        if (useHashing)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            //Always release the resources and flush data
            // of the Cryptographic service provide. Best Practice

            hashmd5.Clear();
        }
        else
            keyArray = UTF8Encoding.UTF8.GetBytes(key);

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        //set the secret key for the tripleDES algorithm
        tdes.Key = keyArray;
        //mode of operation. there are other 4 modes.
        //We choose ECB(Electronic code Book)
        tdes.Mode = CipherMode.ECB;
        //padding mode(if any extra byte added)

        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateEncryptor();
        //transform the specified region of bytes array to resultArray
        byte[] resultArray =
          cTransform.TransformFinalBlock(toEncryptArray, 0,
          toEncryptArray.Length);
        //Release resources held by TripleDes Encryptor
        tdes.Clear();
        //Return the encrypted data into unreadable string format
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    public string Encrypt(string toEncrypt, bool useHashing)
    {
        byte[] keyArray;
        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

        // Get the key from config file

        string key = ConfigurationManager.AppSettings["EncryptionKey"].ToString();

        //If hashing use get hashcode regards to your key
        if (useHashing)
        {
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            //Always release the resources and flush data
            // of the Cryptographic service provide. Best Practice

            hashmd5.Clear();
        }
        else
            keyArray = UTF8Encoding.UTF8.GetBytes(key);

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        //set the secret key for the tripleDES algorithm
        tdes.Key = keyArray;
        //mode of operation. there are other 4 modes.
        //We choose ECB(Electronic code Book)
        tdes.Mode = CipherMode.ECB;
        //padding mode(if any extra byte added)

        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateEncryptor();
        //transform the specified region of bytes array to resultArray
        byte[] resultArray =
          cTransform.TransformFinalBlock(toEncryptArray, 0,
          toEncryptArray.Length);
        //Release resources held by TripleDes Encryptor
        tdes.Clear();
        //Return the encrypted data into unreadable string format
        return Convert.ToBase64String(resultArray, 0, resultArray.Length);
    }

    public string Decrypt(string cipherString, bool useHashing)
    {
        byte[] keyArray;
        //get the byte code of the string

        byte[] toEncryptArray = Convert.FromBase64String(cipherString.Replace(' ', '+'));

        //Get your key from config file to open the lock!
        string key = ConfigurationManager.AppSettings["EncryptionKey"].ToString();

        if (useHashing)
        {
            //if hashing was used get the hash code with regards to your key
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            //release any resource held by the MD5CryptoServiceProvider

            hashmd5.Clear();
        }
        else
        {
            //if hashing was not implemented get the byte code of the key
            keyArray = UTF8Encoding.UTF8.GetBytes(key);
        }

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        //set the secret key for the tripleDES algorithm
        tdes.Key = keyArray;
        //mode of operation. there are other 4 modes. 
        //We choose ECB(Electronic code Book)

        tdes.Mode = CipherMode.ECB;
        //padding mode(if any extra byte added)
        tdes.Padding = PaddingMode.PKCS7;

        ICryptoTransform cTransform = tdes.CreateDecryptor();
        byte[] resultArray = cTransform.TransformFinalBlock(
                             toEncryptArray, 0, toEncryptArray.Length);
        //Release resources held by TripleDes Encryptor                
        tdes.Clear();
        //return the Clear decrypted TEXT
        return UTF8Encoding.UTF8.GetString(resultArray);
    }

    #endregion "Encrypt/Decrypt functions"

    #region "Create Thumbnail Code"

    public Bitmap CreateThumbnail1(string lcFilename, int lnWidth, int lnHeight, string dir, string filename)
    {
        System.Drawing.Bitmap bmpOut = null;
        string filenamename = string.Empty;
        try
        {
            lcFilename = HttpContext.Current.Server.MapPath(HttpContext.Current.Request.ApplicationPath) + "\\" + lcFilename + filename;
            Bitmap loBMP = new Bitmap(lcFilename);
            ImageFormat loFormat = loBMP.RawFormat;
            decimal wper, hper, res;
            int lnNewWidth = 0;
            int lnNewHeight = 0;
            //*** If the image is smaller than a thumbnail just return it
            if (loBMP.Width <= lnWidth && loBMP.Height <= lnHeight)
            {
                lnNewWidth = loBMP.Width;//lnWidth;

                lnNewHeight = loBMP.Height;//lnHeight;
            }
            else if (loBMP.Width > lnWidth || loBMP.Height > lnHeight)
            {
                wper = (decimal)lnWidth / loBMP.Width;
                hper = (decimal)lnHeight / loBMP.Height;
                if (wper < hper)
                {
                    res = wper;
                }
                else
                {
                    res = hper;
                }

                decimal lnTemp = loBMP.Width * res;
                decimal lnTemp1 = loBMP.Height * res;

                lnNewWidth = Convert.ToInt32(Math.Round(lnTemp));
                lnNewHeight = Convert.ToInt32(Math.Round(lnTemp1));
            }
            bmpOut = new Bitmap(lnNewWidth, lnNewHeight);
            Graphics g = Graphics.FromImage(bmpOut);
            g.CompositingMode = CompositingMode.SourceCopy;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.DrawImage(loBMP, 0, 0, lnNewWidth, lnNewHeight);
            loBMP.Dispose();
        }
        catch
        {
            return null;
        }

        bmpOut.Save(HttpContext.Current.Server.MapPath(dir) + filename);
        return bmpOut;
    }

    #endregion "Create Thumbnail Code"
}



