using System;
using System.Net;
class IPEndPointSample
{
    public static void Main()
    {
        IPAddress test1 = IPAddress.Parse("192.168.1.1");   // khởi tạo biến test1 truyền vào địa chỉ IP 192.168.1.1
        IPEndPoint ie = new IPEndPoint(test1, 8000);        // khởi tạo biết ie truyền vào IPEndpoint 192.168.1.1:8000
        Console.WriteLine("The IPEndPoint is: {0}", ie.ToString());     // in ra màn hình biến ie ở dạng chuỗi
        Console.WriteLine("The AddressFamily is: {0}", ie.AddressFamily);   // in ra màn hình giao thức mạng ie sử dụng
        Console.WriteLine("The address is: {0}, and the port is: {1}\n", ie.Address, ie.Port); 
        Console.WriteLine("The min port number is: {0}", IPEndPoint.MinPort);      //in ra màn hình số port min
        Console.WriteLine("The max port number is: {0}\n", IPEndPoint.MaxPort); // in ra màn hình số port manx
        ie.Port = 80; // truyền port 80 vào ie
        Console.WriteLine("The changed IPEndPoint value is: {0}", ie.ToString()); //in ra màn hình ie = 192.168.1.1:80
        SocketAddress sa = ie.Serialize(); 
        // khởi tạo đối tượng sa từ class SocketAddress với phương thức Serialize dùng cho đối tượng ie
        // tức là truyền vào sa giá trị của ie một cách tuần tự (serialize)
        Console.WriteLine("The SocketAddress is: {0}", sa.ToString()); 
        // in ra màn hình các thông tin lưu trữ trong sa ở dạng chuỗi
    }
}