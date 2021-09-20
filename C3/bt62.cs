using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
class RetryUdpClient
{
        private byte[] data = new byte[1024];
        // Tạo mảng data thuộc tính private chứa dữ liệu dạng byte

        private static IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        // Tạo IPEndpoint sender có giá trị bất kỳ với port 0

        private static EndPoint Remote = (EndPoint)sender;
        // Tạo endpoint Remote để lưu giá trị endpoint của sender

        private int SndRcvData(Socket s, byte[] message, EndPoint rmtdevice)
        // Khai báo Hàm/phương thức/method SndRcvData
        // 3 tham số: Socket của client, mảng Byte dữ liệu và endpoint mà server kết nói đến
        {
            int recv;
            int retry = 0;
            while (true)
            {
                Console.WriteLine("Attempt #{0}", retry);
                // In ra màn hình chuỗi attempt với thứ tự là giá trị 'retry'
                try // thử
                {
                    s.SendTo(message, message.Length, SocketFlags.None, rmtdevice);
                    // Gửi dữ liệu trong mảng message từ vị trí chỉ định message.Length dến endpoint chỉ định rmtdevice
                    data = new byte[1024];
                    // Làm mới lại mạng data
                    recv = s.ReceiveFrom(data, ref Remote);
                    // Nhận dữ liệu lưu vào mảng data, gán vào recv, lưu lại thông tin của Remote
                }
                // Nếu không gửi hoặc nhận dữ liệu được thì quăng catch ra
                // Nếu thực hiện được thì bỏ qua catch rồi thực hiện tiếp phía dưới

                catch (SocketException)
                {
                    recv = 0;
                }
                
                if (recv > 0)
                {
                    return recv;
                }
                else
                {
                    retry++;
                    if (retry > 4)
                    {
                        return 0;
                    }
                }
            }
        // Vòng lặp kiểm tra, nếu nhận được dữ liệu thì hàm SndRcvData trả về recv,
        // nếu không nhận được dữ liệu, chạy lại vòng lặp kiểm tra 4 lần.
        // nếu sau 4 lần kiểm trả vẫn không nhận được dữ liệu thì hàm trả về 0
    }
    public RetryUdpClient()
    {
        string input, stringData;
        int recv;
        IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 9050);
        // Tạo ipendpoint = 127.0.0.1:9050
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        // Tạo socket để kết nối đến server
        int sockopt = (int)server.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
        // trả về giá trị ReceiveTimeout của socket ở dạng int rồi gán vào sockopt
        // do chưa set nên mặc định là 0
        Console.WriteLine("Default timeout: {0}", sockopt);
        // xuất ra màn hình giá trị timeout đã gán trong socket
        server.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout, 3000);
        // thiết đặt giá trị ReceiveTimeout cho socket = 3000ms
        sockopt = (int)server.GetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReceiveTimeout);
        // trả về giá trị ReceiveTimeout của socket ở dạng int rồi gán vào sockopt
        Console.WriteLine("New timeout: {0}", sockopt);
        string welcome = "Hello, are you there?";
        data = Encoding.ASCII.GetBytes(welcome);
        // gán welcome đã convert thành dạng byte vào data
        recv = SndRcvData(server, data, ipep);
        // dữ liệu nhận được qua xử lý của hàm SndRcvData đc gán vào recv
        if (recv > 0) // nếu nhận đượC dữ liệu
        {
            stringData = Encoding.ASCII.GetString(data, 0, recv);
            // convert dữ liệu của recv thành dạng chuỗi rồi gán vào stringData
            Console.WriteLine(stringData);
            // xuất ra màn hình
        }
        
        else // nêus không nhận được dữ liệu từ server
        {
            Console.WriteLine("Unable to communicate with remote host");
            // In thông báo không giao tiếp đc với host
            return;
        }
        // Nếu không kết nối được thì hiện thông báo
        while (true)
        {
            input = Console.ReadLine();
            // nhập từ bàn phím
            if (input == "exit")
                break;
                // nếu nhận được exit thì thoát vòng lặp
            recv = SndRcvData(server, Encoding.ASCII.GetBytes(input), ipep);
            //  nếu nhận đc dữ liệu convert dữ liệu đó thành byte rồi thực hiện gửi bằng hàm hàm SndRcvData
            // hàm SndRcvData trả về dữ liệu thì gán vào recv
            if (recv > 0) // nếu recv có dữ liệu

            {
                stringData = Encoding.ASCII.GetString(data, 0, recv);
                // convert dữ liệu của recv thành dạng chuỗi rồi gán vào stringData
                Console.WriteLine(stringData);
                // xuất ra màn hình
            }
            else
                Console.WriteLine("Did not receive an answer");
            // In thông báo khi recv không nhận được dữ liệu nào đc gửi từ server
        }
        Console.WriteLine("Stopping client");
        // In thông báo kết thúc

        server.Close();
        // Đóng server
    }
    public static void Main()
    {
        RetryUdpClient ruc = new RetryUdpClient();
        // tạo đối tượng ruc thực hiện hàm RetryUdpClient
    }
}