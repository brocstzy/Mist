using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mist.Helper
{
    internal class Captcha
    {
        public string captchaChars = "qwertyuopasdfghjkzxcvbnm" +
            "QWERTYUOPASDFGHJKZXCVBNM" +
            "1234567890";
        public void CreateCaptcha()
        {
            string captcha = "";
            for (int i = 0; i< 5; i++)
            {
                captcha.Concat(captcha, new Random().Next(0, captchaChars.Length));
            }
        }
    }
}
