using System;
using System.Net;
using System.Net.Sockets;
class SockProp
{
    public static void Main()
    {
        IPAddress ia = IPAddress.Parse("127.0.0.1");
        // khởi tạo đối tượng ia có địa chỉ ip = 127.0.0.1 từ lớp IPAddress 
        IPEndPoint ie = new IPEndPoint(ia, 8000);
        // khởi tạo đối tượng ie có địa chỉ ip endpoint = 127.0.0.1:8000 từ lớp IPEndPoint
        Socket test = new Socket(AddressFamily.InterNetwork,SocketType.Stream, ProtocolType.Tcp);
        // khởi tạo đối tượng test từ lớp Socket với các thuộc tính đc khai báo trên
        Console.WriteLine("AddressFamily: {0}", test.AddressFamily);
        // in ra thuọc tính AddressFamily của test
        Console.WriteLine("SocketType: {0}", test.SocketType);
        // in ra thuộc tính SocketType của test
        Console.WriteLine("ProtocolType: {0}", test.ProtocolType);
        // in ra thuộc tính ProtocolType của test
        Console.WriteLine("Blocking: {0}", test.Blocking);
        // in ra kết quả kiểm tra test có bị block hay không? mặc định là true
        test.Blocking = false;
        // chuyển test về dạng non-block
        Console.WriteLine("new Blocking: {0}", test.Blocking);
        // kiểm tra lại xem test còn bị block không?
        Console.WriteLine("Connected: {0}", test.Connected);
        // in ra kết quả kiểm tra test có kết nối với một thiết bị từ xa không?
        test.Bind(ie);

        IPEndPoint iep = (IPEndPoint)test.LocalEndPoint;
        // khởi tạo đối tượng iep từ lớp IPEndPoint với giá trị IPEndPoint của test
        Console.WriteLine("Local EndPoint: {0}", iep.ToString());
        // in ra iep ở dạng chuỗi
        test.Close();
        //comment bay ba thoi
    }
}
