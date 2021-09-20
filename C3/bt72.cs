using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
class BestUdpClient
{
    private byte[] data = new byte[1024];
    // tạo mảng data chứa dữ liệu dạng byte phạm vi truy cập là private
    private static IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
    // tạo đối tượng IPEndPoint sender chấp nhận ip bất kì chấp nhận port 0
    // phạm vi truy cập private
    private static EndPoint Remote = (EndPoint)sender;
    // tạo đối tượng EndPoint Remote để lưu trữ Endpoint của sender
    private static int size = 30;
    // định mức kích thước chứa dữ liệu 
    private static int AdvSndRcvData(Socket s, byte[] message,EndPoint rmtdevice)
    // khởi tạo hàm AdvSndRcvData (Socket client, data dạng byte, endpoint chỉ định để giao tiếp)
    // phạm vi truy cập private
    {
        int recv = 0;
        int retry = 0;
        while (true)
        {
            Console.WriteLine("Attempt #{0}", retry);
            // thứ tự vòng lặp lại, bắt đầu 0
            try // thử
            {
                s.SendTo(message, message.Length, SocketFlags.None, rmtdevice);
                // socket s sử dụng phương thức Sendto 
                // gửi dữ liệu đến server có endpoint chỉ định là rmtdevice
                data = new byte[size];
                // tạo mảng data mới có kích thước = size (30)
                recv = s.ReceiveFrom(data, ref Remote);
                // socket s sử dụng phương thức ReceiveFrom
                // nhận dữ liệu từ server, lưu vào mảng data
                // lưu lại endpoint đã giao tiếp với server vào Remote
                // gán thông tin trên vào recv
            }
            catch (SocketException e) // nếu không nhận được dữ liệu 
            {
                if (e.ErrorCode == 10054) 
                    recv = 0;
                    // nếu lỗi có mã 10054 trả về recv = 0
                else if (e.ErrorCode == 10040) 
                {
                    Console.WriteLine("Error receiving packet");
                    size += 10;
                    recv = 0;
                    // nếu lỗi có mã 10040 thực hiện thông báo lỗi, 
                    // tăng độ rộng của mảng data thêm 10
                    // trả về recv = 0
                }
                    
            }
            if (recv > 0) // nếu nhận được dữ liệu (từ try), hàm AdvSndRcvData trả về recv
            {
                return recv;
            }
            
            else
            // nếu không nhận đc dữ liệu (catch) tăng retry rồi thực hiện lại vòng lặp
            // nếu retry > 4 thì trả về 0, ngắt vòng lặp
            {
                retry++;
                if (retry > 4)
                {
                    return 0;
                }
            }
        }
    }
    public static void Main()
    {
        string input, stringData;
        int recv;
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
        // tạo ipendpoint = 127.0.0.1:9050
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        // tạo socket để kết nối đến server
        int sockopt = (int)server.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
        // khởi tạo biến sockopt để lưu trữ giá trị trả về ReceiveTimeout của hàm GetSocketOption
        Console.WriteLine("Default timeout: {0}", sockopt);
        // xuất ra màn hình giá trị sockopt (mặc định là 0 do chưa set)
        server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 3000);
        // thiết đặt giá trị ReceiveTimeout cho socket = 3000ms
        sockopt = (int)server.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
        // gán giá trị ReceiveTimeout đã thiết đặt ở trên cho sockopt
        Console.WriteLine("New timeout: {0}", sockopt);
        // xuất ra màn hình giá trị sockopt = 3000
        string welcome = "Hello, are you there?";
        data = Encoding.ASCII.GetBytes(welcome);
        // convert chuỗi welcome thành dạng byte rồi gán vào mảng data
        recv = AdvSndRcvData(server, data, ipep);
        // thực hiện hàm AdvSndRcvData, giá trị trả về gán vào recv 
        if (recv > 0) // nếu có nhận được dữ liệu
        {
            stringData = Encoding.ASCII.GetString(data, 0, recv);
            // convert dữ liệu của recv thành dạng chuỗi rồi gán vào stringData
            Console.WriteLine(stringData);
            // xuất ra màn hình
        }
        else // nếu không nhận được dữ liệu thì thông báo rồi kết thúc chương trình
        {
            Console.WriteLine("Unable to communicate with remote host");
            return;
        }
        while (true) // nếu nhận được dữ liệu thì thực hiện vòng lặp dưới
        {
            input = Console.ReadLine();
            // nhập từ bàn phím
            if (input == "exit")
                break;
            recv = AdvSndRcvData(server, Encoding.ASCII.GetBytes(input), ipep); 
            // convert dữ liệu đã nhập ở trên thành dạng byte 
            // gửi dữ liệu đến server bằng hàm AdvSndRcvData
            // giá trị trả về của hàm AdvSndRcvData gán vào recv
            if (recv > 0) // nếu nhận được dữ liệu (xem lại hàm AdvSndRcvData)
            {
                stringData = Encoding.ASCII.GetString(data, 0, recv);
                // convert dữ liệu của recv thành dạng chuỗi rồi gán vào stringData
                Console.WriteLine(stringData);
                // xuất ra màn hình
            }
            else // nếu không nhận được dữ liệu (AdvSndRcvData trả về 0) xuất thông báo dưới
                Console.WriteLine("Did not receive an answer");
        }// nếu nhận được exit thì thoát vòng lặp
        // xuất thông báo, đóng socket
        Console.WriteLine("Stopping client");
        server.Close();
    }
}