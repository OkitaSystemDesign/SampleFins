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

    public partial class frmFinsUdpASync : Form
    {
        System.Net.Sockets.UdpClient U;

        public frmFinsUdpASync()
        {
            InitializeComponent();
        }

        private void FinsUdpASync_Load(object sender, EventArgs e)
        {

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            UdpFinsMessageASync();
        }

        private void UdpFinsMessageASync()
        {
            if (U == null)
            {
                System.Net.IPEndPoint EP = new System.Net.IPEndPoint(System.Net.IPAddress.Any, 9600);
                U = new System.Net.Sockets.UdpClient(EP);
                U.BeginReceive(ReceiveCallbak, U);
            }

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

            // 受信再開
            U.BeginSend(cmd, cmd.Length, "192.168.250.1", 9600, SendCallback, U);

            AddStringDelegete dlg = new AddStringDelegete(AddText);
            this.Invoke(dlg, new object[] { cmd });
        }

        private void SendCallback(IAsyncResult ar)
        {
            System.Net.Sockets.UdpClient udp = (System.Net.Sockets.UdpClient)ar.AsyncState;

            try
            {
                udp.EndSend(ar);
            }
            catch (System.Net.Sockets.SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ObjectDisposedException ex)
            {
                Console.WriteLine("Already Closed");
            }
        }


        private delegate void AddStringDelegete(byte[] str);
        private void AddText(byte[] msg)
        {
            if (msg == null)
                return;

            textBox1.AppendText(BitConverter.ToString(msg) + "\r\n");

        }

        private void ReceiveCallbak(IAsyncResult ar)
        {
            if (U != null)
            {
                System.Net.Sockets.UdpClient udp = (System.Net.Sockets.UdpClient)ar.AsyncState;
                System.Net.IPEndPoint remoteEP = null;
                byte[] receiveBytes = udp.EndReceive(ar, ref remoteEP);
                System.Net.IPAddress receiveAddress = remoteEP.Address;

                AddStringDelegete dlg = new AddStringDelegete(AddText);
                this.Invoke(dlg, new object[] { receiveBytes });

                U.BeginReceive(ReceiveCallbak, U);
            }
        }

        private void frmFinsUdpASync_FormClosed(object sender, FormClosedEventArgs e)
        {
            U.Close();
            U = null;
        }
    }
}
