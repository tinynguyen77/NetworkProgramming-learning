using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class BadTcpSrvr
{   
    public static void Main()
    {
        int recv;
        byte[] data = new byte[1024];
        // khởi tạo mảng dếm 
        IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 9050);
        // tạo ipendpoint ở có giá trị là bất kì ip nào chấp nhận port 9050
        Socket newsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        // tạo socket
        newsock.Bind(ipep);
        // gán ipep cho socket
        newsock.Listen(10);
        // socket chuyển vào trạng thái lắng nghe tối đa 10 kết nối       
        Console.WriteLine("Waiting for a client...");
        Socket client = newsock.Accept();
        // socket của server chấp nhật kết nối với client
        string welcome = ("Welcome to my test server");
        data = Encoding.ASCII.GetBytes(welcome);
        // convert chuỗi welcome thành dữ liệu bytes rồi đưa vào mảng data
        client.Send(data, data.Length, SocketFlags.None);
        // gửi dữ liệu cho client
        IPEndPoint newclient = (IPEndPoint)client.RemoteEndPoint;
        // liên kết đến IPEndPoint của client
        Console.WriteLine("Connected with {0} at port {1}", newclient.Address, newclient.Port);
        // in ra thông tin IPEndpoint của client
        for (int i = 0; i<8; i++)
        {
            recv = client.Receive(data);
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
        }
        // vòng lặp:
        // cho biến recv nhận vào data gửi tử client
        // convert dữ liệu nhận được từ client sang dạng chuỗi rồi xuất ra màn hình
        // vòng lặp kết thúc khi i = 8
        Console.WriteLine("Disconnecting from {0}", newclient.Address);
        client.Close();
        newsock.Close();
    }
}