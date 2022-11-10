using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SampleFins
{
    public partial class frmFinsUdp : Form
    {
        public frmFinsUdp()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            UdpFinsMessage();
        }

        private void UdpFinsMessage()
        {
            System.Net.IPAddress LocalIP = System.Net.IPAddress.Parse("192.168.250.48");    // PCのIPアドレス
            System.Net.IPEndPoint LocalEP = new System.Net.IPEndPoint(LocalIP, 9600);
            System.Net.Sockets.UdpClient udp = new System.Net.Sockets.UdpClient(LocalEP);
            udp.Client.ReceiveTimeout = 1000;

            byte[] cmd = new byte[18];
            // ----------------- FINSヘッダ
            cmd[0] = 0x80;      // ICF
            cmd[1] = 0x00;      // RSV
            cmd[2] = 0x02;      // GCT
            cmd[3] = 0;         // DNA  相手先ネットワークアドレス
            cmd[4] = 1;         // DA1  相手先ノードアドレス （PLCのIPアドレスの最終桁と合わせる）
            cmd[5] = 0;         // DA2  相手先号機アドレス
            cmd[6] = 0;         // SNA  発信元ネットワークアドレス
            cmd[7] = 48;        // SA1  発信元ノードアドレス  （PCのIPアドレスの最終桁と合わせる）
            cmd[8] = 0;         // SA2  発信元号機アドレス
            cmd[9] = 1;         // SID  識別子 00-FFの任意の数値
                                // ----------------- FINSコマンド
            cmd[10] = 0x01;     // MRC  読出しコマンド 0101
            cmd[11] = 0x01;     // SRC
            cmd[12] = 0x82;     // MemoryType = DM
            cmd[13] = 0x00;     // ReadAddress = 100CH
            cmd[14] = 0x64;
            cmd[15] = 0x00;
            cmd[16] = 0x00;     // ReadSize = 10CH
            cmd[17] = 0x0A;

            udp.Send(cmd, cmd.Length, "192.168.250.1", 9600);
            textBox1.AppendText("-> " + BitConverter.ToString(cmd) + "\r\n");

            System.Net.IPEndPoint TargetIp = null;
            byte[] rcv = udp.Receive(ref TargetIp);

            textBox1.AppendText("<- " + BitConverter.ToString(rcv) + "\r\n\r\n");
            // 期待レスポンス
            // C0-00-02-00-30-00-00-01-00-01-01-01-00-00-00-01-00-02-00-03-00-04-00-05-00-06-00-07-00-08-00-09-00-0A
            // ~~~~~~~|~~~~~~~~~~~~~~~~~~~~~ ~~|~~ ~~|~~ ~~~~~~|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
            //        +FINSヘッダ              |     |         +-- 読出し値=000100020003000400050006000700080009000A
            //                                 |     +-- 正常終了
            //                                 +--読出しコマンド

            udp.Close();
        }


    }
}
