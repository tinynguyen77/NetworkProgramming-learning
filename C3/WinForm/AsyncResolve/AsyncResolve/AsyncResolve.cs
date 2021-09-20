using System;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AsyncResolve
{
    public partial class DnsResolve : Form
    {
        private AsyncCallback OnResolved;
        // gọi hàm bất đồng bộ OnResolved

        public DnsResolve()
        {
            InitializeComponent();
            OnResolved = new AsyncCallback(Resolved);
            // thực hiện biên dịch 1 phần của hàm Resolved để xử lý bất đồng bộ
        }



        private void Resolved(IAsyncResult ar) 
            // thực hiện phân giải bất đồng bộ ar
        {
            // 
            string buffer;
            IPHostEntry iphe = Dns.EndGetHostEntry(ar);
            // trả về IPHostEntry được lưu trong hàm bất đồng bộ
            // gán cho đối tượng IPHostEntry iphe được tạo
            // kết thúc xử lý bất đồng bộ
            buffer = "Host name: " + iphe.HostName;
            // gán thông tin hostname của iphe cho buffee
            CheckForIllegalCrossThreadCalls = false;
            // tắt các cuộc gọi xử lý lỗi trên luồng truy xuất thông tin
            results.Items.Add(buffer);
            // add vào listbox results dữ liệu buffer
            foreach (string alias in iphe.Aliases)
                // vòng lặp xuất bí danh của IPhostentry
            {
                buffer = "Alias: " + alias;
                results.Items.Add(buffer);
            }
            foreach (IPAddress addrs in iphe.AddressList)
                // vòng lặp xuất ipaddress của iphostentry
            {
                buffer = "Address: " + addrs.ToString();
                results.Items.Add(buffer);
            }

        }


        private void btnResolve_Click(object sender, EventArgs e)
        {
            results.Items.Clear();
            // xoá all items trong results
            string addr = address.Text;
            // gán giá trị Text của "address" vào phần tử addr
            Object state = new Object();
            // khởi tạo một class Object
            Dns.BeginGetHostEntry(addr, OnResolved, state);
            // define: thực hiện phân giải không đồng bộ 1 địa chỉ IP thành IPHostEntry
            //BeginGetHostEntry (System.Net.IPAddress address, AsyncCallback? requestCallback, object? stateObject)
            // addr: địa chỉ Ip cần phân giải
            // OnResolved: hàm bất đồng bộ đc uỷ nhiệm, đc gọi khi hoạt động hoàn tất
            // state: đối tượng chứa thông tin về hoạt động của chương trình,
            // sau khi hoàn tất hoạt động trên thì chuyển giao cho hàm bất đồng bộ 

        }
    }
}
