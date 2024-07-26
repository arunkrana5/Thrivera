using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;


namespace MyDbWebApi.Classes
{
    public static class Common
    {
        public static string ipStackKey = "";// ConfigurationManager.AppSettings["ipStackKey"].ToString();

        public const string GetUnApprovedFacilityPhysicianList = "CallingByCPRtoRH";
        public const string GetUnCompletedPathTecOrderList = "SP_GetUnCompletedPathTecOrderList";

        public static string CityStateCountByIp(object IP)
        {   
            string url = "http://api.ipstack.com/" + IP + "?access_key="+ ipStackKey;
            var request = System.Net.WebRequest.Create(url);

            using (WebResponse wrs = request.GetResponse())
            {
                using (Stream stream = wrs.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string json = reader.ReadToEnd();
                        var obj = JObject.Parse(json);
                        string City = (string)obj["city"];
                        string Country = (string)obj["region_name"];
                        string CountryCode = (string)obj["country_code"];

                        return (CountryCode + " - " + Country + "," + City);
                    }
                }
            }
          
        }

        public static string GetIPAddress()
            {
                return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            public static string EnsureString(string str)
            {
                if (string.IsNullOrEmpty(str))
                {
                    str = "";
                }
                return str.Replace("'", "''").Replace("\"", "" + "").Trim();
            }

            public static string Encrypt(string clearText)
            {
                try
                {
                    if (!string.IsNullOrEmpty(clearText))
                    {
                        string EncryptionKey = "RAISOFT";
                        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
                        using (Aes encryptor = Aes.Create())
                        {
                            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                            encryptor.Key = pdb.GetBytes(32);
                            encryptor.IV = pdb.GetBytes(16);
                            using (MemoryStream ms = new MemoryStream())
                            {
                                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                                {
                                    cs.Write(clearBytes, 0, clearBytes.Length);
                                    cs.Close();
                                }
                                clearText = Convert.ToBase64String(ms.ToArray());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                        }
                return clearText;
            }
            public static string Decrypt(string cipherText)
            {
                try
                {
                    if (!string.IsNullOrEmpty(cipherText))
                    {
                        string EncryptionKey = "RAISOFT";
                        cipherText = cipherText.Replace(" ", "+");
                        byte[] cipherBytes = Convert.FromBase64String(cipherText);
                        using (Aes encryptor = Aes.Create())
                        {
                            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                            encryptor.Key = pdb.GetBytes(32);
                            encryptor.IV = pdb.GetBytes(16);
                            using (MemoryStream ms = new MemoryStream())
                            {
                                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                                {
                                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                                    cs.Close();
                                }
                                cipherText = Encoding.Unicode.GetString(ms.ToArray());
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                   // Common_SPU.LogError("Error during Decrypt. The query was executed :", ex.ToString(), "ClsCommon()", "ClsCommon", "ClsCommon", 0, GetIPAddress());
                }
                return cipherText;
            }

            public static string ConnectionString()
            {
                return ConfigurationManager.ConnectionStrings["connectionstring"].ConnectionString.ToString();
            }
        public static byte[] CreateThumbnail(byte[] PassedImage, int LargestSide)
        {
            byte[] ReturnedThumbnail = null;

            using (MemoryStream StartMemoryStream = new MemoryStream(),
              NewMemoryStream = new MemoryStream())
            {
                try
                {


                    // write the string to the stream  
                    StartMemoryStream.Write(PassedImage, 0, PassedImage.Length);

                    // create the start Bitmap from the MemoryStream that contains the image                
                    Bitmap startBitmap = new Bitmap(StartMemoryStream);

                    // set thumbnail height and width proportional to the original image.  
                    int newHeight;
                    int newWidth;
                    double HW_ratio;
                    if (startBitmap.Height > startBitmap.Width)
                    {
                        newHeight = LargestSide;
                        HW_ratio = (double)((double)LargestSide / (double)startBitmap.Height);
                        newWidth = (int)(HW_ratio * (double)startBitmap.Width);
                    }
                    else
                    {
                        newWidth = LargestSide;
                        HW_ratio = (double)((double)LargestSide / (double)startBitmap.Width);
                        newHeight = (int)(HW_ratio * (double)startBitmap.Height);
                    }

                    // create a new Bitmap with dimensions for the thumbnail.  
                    Bitmap newBitmap = new Bitmap(newWidth, newHeight);

                    // Copy the image from the START Bitmap into the NEW Bitmap.  
                    // This will create a thumnail size of the same image.  
                    newBitmap = ResizeImage(startBitmap, newWidth, newHeight);

                    // Save this image to the specified stream in the specified format.  
                    newBitmap.Save(NewMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);

                    // Fill the byte[] for the thumbnail from the new MemoryStream.  
                    ReturnedThumbnail = NewMemoryStream.ToArray();
                }
                catch (Exception)
                {

                }
            }
            // return the resized image as a string of bytes.  
            return ReturnedThumbnail;
        }

        // Resize a Bitmap  
        private static Bitmap ResizeImage(Bitmap image, int width, int height)
        {
            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics gfx = Graphics.FromImage(resizedImage))
            {
                gfx.DrawImage(image, new Rectangle(0, 0, width, height),
                    new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            }
            return resizedImage;
        }
    }
    


}