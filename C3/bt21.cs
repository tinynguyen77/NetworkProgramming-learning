using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class OddUdpSvr
{
    public static void Main()
    {
        int recv;
        byte[] data = new byte[1024];
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
        Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        newsock.Bind(ipep);
        Console.WriteLine("Waiting for client...");
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        EndPoint Remote = (EndPoint)(sender);
        recv = newsock.ReceiveFrom(data, ref Remote);
        Console.WriteLine("Messenger received form {0}:", Remote.ToString());
        Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
        string welcome = "Welcome to my test server";
        data = Encoding.ASCII.GetBytes(welcome);
        newsock.SendTo(data, data.Length, SocketFlags.None, Remote);
        while (true)
        {
            data = new byte[1024];
            // làm mới mảng byte
            recv = newsock.ReceiveFrom(data, ref Remote);
            // lưu lại endpoint của client, dữ liệu nhận từ client  đc đưa vào mảng data, gán cho rev
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
            // dữ liệu lưu trong mảng data được convert thành chuỗi rồi xuất ra 
            newsock.SendTo(data, recv, SocketFlags.None, Remote);
            // Gửi số byte dữ liệu được chỉ định (data) đến điểm cuối được chỉ định (Remote), 
            // bắt đầu từ vị trí được chỉ định trong bộ đệm (rev) và sử dụng SocketFlags được chỉ định.
        }
        newsock.Close();
    }
}