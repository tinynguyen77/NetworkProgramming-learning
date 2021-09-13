using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
class RemoteControlClient
{
    public static void Main()
    {
        byte[] data = new byte[1024];
        string stringData;
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
        // tạo ipendpoint = 127.0.0.1.9050
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        // tạo socket để kết nối đến server
        string welcome = "Hello, are you there?";
        data = Encoding.ASCII.GetBytes(welcome);
        // gán welcome đã convert thành dạng byte vào data
        server.SendTo(data, data.Length, SocketFlags.None, ipep);
        // socket gửi tất cả dữ liệu trong data ở dạng byte qua server thông qua ipep
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        // tạo IPendpoint để gửi dữ liệu đến server có giá trị là IP bất kỳ chấp nhận port 
        EndPoint Remote = (EndPoint)sender;
        // sender chấp nhận các kết nối từ endpoint Remote 
        data = new byte[1024];
        // tạo mảng data mới
        int recv = server.ReceiveFrom(data, ref Remote);
        // nhận dữ liệu từ Remote rồi lưu vào data
        // gán dữ liệu đã nhận vào recv
        Console.WriteLine("Message received from {0}:", Remote.ToString());
        // xuất thông tin Remote (server) ở dạng chuỗi
        Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
        // convert dữ liệu dạng byte trong recv sang dạng chuỗi rồi xuất ra màn hình
        while (true)
        {
            data = new byte[1024];
            // tái tạo mảng
            recv = server.ReceiveFrom(data, ref Remote);
            // nhận dữ liệu từ Remote rồi lưu vào data
            // gán dữ liệu đã nhận vào recv
            stringData = Encoding.ASCII.GetString(data, 0, recv);
            // convert dữ liệu dạng byte trong recv sang dạng chuỗi rồi gán vào stringData
            Console.WriteLine(stringData);
            if (stringData == "exit")
                break;
                // nếu nhận được exit thì thoát vòng lặp
            if (stringData == "shutdown")
                Process.Start("shutdown.exe", "-s -f -t 1");
                // nếu nhận được shutdown thì tắt máy
            if (stringData == "restart")
                Process.Start("shutdown.exe", "-r -f -t 1");
                // nếu nhận được restart thì khởi động lại máy
            if (stringData == "lock")
                Process.Start(@"C:\Windows\system32\rundll32.exe", "user32.dll,LockWorkStation");
                // nếu nhận được lock thì khoá user đang dùng lại
            if (stringData == "log off")
                Process.Start("shutdown.exe", "-l");
                // nếu nhận được log off thì thoát user đang dùng
        }
        Console.WriteLine("Stopping client");
        server.Close();
    }
}