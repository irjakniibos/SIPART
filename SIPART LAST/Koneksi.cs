using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIPART_LAST
{
    internal class Koneksi
    {
        public string connectionString()
        {
            string connectStr = "";
            try
            {
                string localIP = GetLocalIPAddress();
                connectStr = $"Data Source={localIP};Initial Catalog=SIPART;Integrated Security=True;" +
                             $"Integrated Security=True;";
                return connectStr;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }
        public static string GetLocalIPAddress()
        {
            var host = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
