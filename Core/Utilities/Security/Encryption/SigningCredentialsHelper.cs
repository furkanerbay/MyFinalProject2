using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Encryption
{
    public class SigningCredentialsHelper
    {//JWT servislerinde WEB API kullanabileceği JWT'larının olusturulması ıcın Credentials ->
        //kullanıcı adı parola sisteme girmek ıcın  elımızde olanlar
        //Burada anahtar var. elimizde.
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
            //Sen hashing islemi yapacaksın. Anahtar olarak securityKey kullan.
            //Sifreleme olarakta HMACSHA512 kullan.
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
        }
    }
}
