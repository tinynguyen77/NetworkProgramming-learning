using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
class RemoteControlSrvr
{
    public static void Main()
    {
        int recv;
        byte[] data = new byte[1024];
        // tạo mảng 
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
        // tạo IPendpoint ipep có giá trị là IP bất kỳ chấp nhận port 9050
        Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        // tạo socket
        newsock.Bind(ipep);
        // truyền ipendpoint cho socket
        Console.WriteLine("Waiting for a client...");
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        // tạo IPendpoint để gửi dữ liệu có giá trị là IP bất kỳ chấp nhận port 0
        EndPoint Remote = (EndPoint)(sender);
        // endpoint remote từ client kết nối tới sender
        recv = newsock.ReceiveFrom(data, ref Remote);
        // ReceiveFrom(Byte[], EndPoint)
        // nhận dữ liệu từ Remote và lưu trữ vào data
        Console.WriteLine("Message received from {0}:", Remote.ToString());
        // xuất ipendpoint của remote (client) dạng chuỗi
        Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
        // convert dữ liệu nhận được thành dạng chuỗi rồi xuất ra màn hình
        string welcome = "Welcome to my test server";
        data = Encoding.ASCII.GetBytes(welcome);
        // gán welcome đã convert thành dạng byte vào data
        newsock.SendTo(data, data.Length, SocketFlags.None, Remote);
        // socket gửi tất cả dữ liệu trong data ở dạng byte sang endpoint của Remote
        while (true)
        {
            string input = Console.ReadLine();
            // nhập từ bàn phím
            newsock.SendTo(Encoding.ASCII.GetBytes(input), Remote);
            // gửi dữ liệu đã convert sang dạng byte cho Remote
            if (input == "exit")
                break;
        }
        Console.WriteLine("Stopping client");
        newsock.Close();
    }
}