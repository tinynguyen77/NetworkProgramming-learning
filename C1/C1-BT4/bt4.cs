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
        // nếu độ dài mảng khác 1 thì báo lỗi ở dưới,
        // tức phải truyền vào mảng 1 giá trị địa chỉ web
        IPHostEntry results = Dns.GetHostEntry(argv[0]);
        // khởi tạo đối tượng results từ class IPHostEntry với giá trị là thông tin lấy từ mảng
        Console.WriteLine("Host name: {0}", results.HostName);
        // xuất ra màn hình hostname của results đã đc truyền vào
        foreach (string alias in results.Aliases)
        {
            Console.WriteLine("Alias: {0}", alias);
            // xuất ra bí danh của đối tượng 
        }
        foreach (IPAddress address in results.AddressList)
        {
            Console.WriteLine("Address: {0}",address.ToString()); 
            // xuất ra màn hình địa chỉ IP dạng chuỗi
        }
    }
}