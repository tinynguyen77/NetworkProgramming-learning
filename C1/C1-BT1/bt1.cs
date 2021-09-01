using System;
using System.Net;

internal class AddressSample
{
    public static void Main()
    {
        IPAddress test1 = IPAddress.Parse("192.168.1.1"); // khởi tạo biến test1 có địa chỉ IP chuẩn = 192.168.1.1
        IPAddress test2 = IPAddress.Loopback; // khởi tạo biến test2 trả về 1 địa chỉ IP lặp
        IPAddress test3 = IPAddress.Broadcast; // khởi tạo biến test3 trả về 1 địa chỉ IP ở dạng số long
        IPAddress test4 = IPAddress.Any; // khơi tạo biến test4 trả về địa chỉ IP của server phải listen activities của client trên tất cả card mạng
        IPAddress test5 = IPAddress.None; // khởi tạo biến test5 trả về địa chỉ IP cho biết không nên sử dụng Network interface
        IPHostEntry ihe = Dns.GetHostEntry(Dns.GetHostName()); 
        // khởi tạo biến ihe cung cấp mảng chứa thông tin địa chỉ (IPHostEntry) có giá trị trả về thông tin DNS của máy đang dùng
        IPAddress myself = ihe.AddressList[0];  // khởi tạo biến myself bắt đầu từ vị trí 0 trong mảng ihe
        if (IPAddress.IsLoopback(test2)) //cho biết giá trị test2 có phải là địa chỉ ip lặp hay không? 
            Console.WriteLine("The Loopback address is: {0}", test2.ToString());
        else 
            Console.WriteLine("Error obtaining the loopback address");
        Console.WriteLine("The Local IP address is: {0}\n", myself.ToString()); // in ra màn hình địa chỉ ip myself
        if (myself == test2) 
            Console.WriteLine("The loopback address is the same as local address.\n");
        else
            Console.WriteLine("The loopback address is not the local address.\n");
        Console.WriteLine("The test address is: {0}", test1.ToString()); // trả về địa chỉ IP dạng chuỗi có chấm
        Console.WriteLine("Broadcast address: {0}", test3.ToString());
        Console.WriteLine("The ANY address is: {0}", test4.ToString());
        Console.WriteLine("The NONE address is: {0}", test5.ToString());
    }
}