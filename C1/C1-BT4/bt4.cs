using System;
using System.Net;

class GetDNSHostInfo
{
    public static void Main(string[] argv)
    {
        if (argv.Length != 1)
        {
            Console.WriteLine("Usage: GetDNSHostInfo hostname");
            return;
        }

        IPHostEntry results = Dns.GetHostEntry(argv[0]);
        // lấy thông tin của host được truyền vào rồi gán vào results
        Console.WriteLine("Host name: {0}", results.HostName);
        // xuất ra màn hình host name

        foreach (string alias in results.Aliases)
        {
            Console.WriteLine("Alias: {0}", alias);
            // xuất định dạnh
        }

        foreach (IPAddress address in results.AddressList)
        {
            Console.WriteLine("Address: {0}", address.ToString());
            // xuất ipaddress
        }



    }

}