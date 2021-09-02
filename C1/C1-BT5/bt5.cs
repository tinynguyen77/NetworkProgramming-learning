using System;
using System.Net;
class GetResolveInfo
{
    public static void Main(string[] argv)
    {
        if (argv.Length != 1) 
        {
            Console.WriteLine("Usage: GetResolveInfo address");
            return;
        }
        // kiểm tra nếu mảng không có giá trị truyền vào thì báo lỗi như trên
        // nếu có giá trị truyền vào thì làm tiếp bên dưới
        IPHostEntry iphe = Dns.GetHostEntry(argv[0]);
        // khởi tạo đối tượng iphe thuộc class IPHostEntry rồi truyền giá trị trong mảng vào 
        Console.WriteLine("Information for {0}", argv[0]);
        // in ra thông tin trong mảng
        Console.WriteLine("Host name: {0}", iphe.HostName);
        // in ra thông tin hostname của iphe
        foreach (string alias in iphe.Aliases)
        {
            Console.WriteLine("Alias: {0}", alias);
            // in ra bí danh của iphe
        }
        foreach (IPAddress address in iphe.AddressList)
        {
            Console.WriteLine("Address: {0}", address.ToString());
            // in ra địa chỉ IP ở dạng chuỗi
        }
    }
}