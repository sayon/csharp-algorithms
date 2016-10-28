
using Algorithmis.Numbers;
using System.Text;
namespace Algorithmis.Demos
{
    class RsaDemo : IDemo
    {

        public string run()
        {
            StringBuilder sb = new StringBuilder();
            BigInt message = 422;
            var rsa = new RSA(3, 32);
            var enc = rsa.Encrypt(message);
            var dec = rsa.Decrypt(enc);

            sb.AppendLine(message.ToString());
            sb.AppendLine(enc.ToString());
            sb.AppendLine(dec.ToString());

            return sb.ToString();
        }
    }
}
