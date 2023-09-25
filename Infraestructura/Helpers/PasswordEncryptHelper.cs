using System.Security.Cryptography;
using System.Text;

namespace Infraestructura.Helpers
{
    public class PasswordEncryptHelper
    {
        public static string EncryptPassword(string password)
        {
            var sha256 = SHA256.Create();
            var encoding = new ASCIIEncoding();
            var stream = Array.Empty<byte>();
            var sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(password));
            for (int i = 0; i < stream.Length; i++)
            {
                sb.AppendFormat("{0:x2}", stream[i]);
            }
            return sb.ToString();
        }

        private static string CreateSalt(int id)
        {
            byte[] saltBytes;
            string saltStr;
            saltBytes = BitConverter.GetBytes(id);
            long XORED = 0x00;

            foreach (int x in saltBytes)
            {
                XORED = XORED ^ x;
            }

            Random rand = new Random(Convert.ToInt32(XORED));
            saltStr = rand.Next().ToString();
            saltStr += rand.Next().ToString();
            saltStr += rand.Next().ToString();
            saltStr += rand.Next().ToString();
            return saltStr;
        }


    }
}
