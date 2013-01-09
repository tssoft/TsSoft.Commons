using System.Security.Cryptography;
using System.Text;

namespace TsSoft.Commons.Utils
{
    public class Md5Hash
    {
        public static string Compute(string input)
        {
            MD5 md5 = MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder hash = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                hash.Append(data[i].ToString("x2"));
            }
            return hash.ToString();
        }
    }
}