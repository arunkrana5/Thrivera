using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDbWebApi.BCryptEncryption
{
    public class BCryptLibrary
    {

        public static string BcryptEncryption(string pass)
        {
            var hashToStoreInWebsqldatabase = BCrypt.Net.BCrypt.HashPassword(pass + "~Pc9~SS", BCrypt.Net.BCrypt.GenerateSalt(11)) ??
                                                 throw new ArgumentNullException(
                                                     "BCrypt.Net.BCrypt.HashPassword(pass + \"~Pc9~SS\", BCrypt.Net.BCrypt.GenerateSalt(11))");
            return hashToStoreInWebsqldatabase;
        }


        public static bool VerifyPassword(string password, string hash)
        {
          return   BCrypt.Net.BCrypt.Verify(password + "~Pc9~SS", hash);
        }


    }
}