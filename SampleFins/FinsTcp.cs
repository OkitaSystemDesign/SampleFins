using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SampleFins
{
    public partial class frmFinsTcp : Form
    {
        public frmFinsTcp()
        {
            InitializeComponent();
        }

        private void frmFinsTcp_Load(object sender, EventArgs e)
        {

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            TcpFinsMessage();
        }

        private void TcpFinsMessage()
        {
            System.Net.Sockets.TcpClient tcp = new System.Net.Sockets.TcpClient("192.168.250.1", 9600);
            System.Net.Sockets.NetworkStream ns = tcp.GetStream();

            ns.ReadTimeout = 1000;
            ns.WriteTimeout = 1000;

            // FINS ノードアドレス情報送信コマンド
            byte[] FinsTcpHeader = new byte[20];
            FinsTcpHeader[0] = 0x46;       // "F"
            FinsTcpHeader[1] = 0x49;       // "I"
            FinsTcpHeader[2] = 0x4E;       // "N"
            FinsTcpHeader[3] = 0x53;       // "S"
            FinsTcpHeader[4] = 0x00;
            FinsTcpHeader[5] = 0x00;
            FinsTcpHeader[6] = 0x00;
            FinsTcpHeader[7] = 0x0C;
            FinsTcpHeader[8] = 0x00;
            FinsTcpHeader[9] = 0x00;
            FinsTcpHeader[10] = 0x00;
            FinsTcpHeader[11] = 0x00;
            FinsTcpHeader[12] = 0x00;
            FinsTcpHeader[13] = 0x00;
            FinsTcpHeader[14] = 0x00;
            FinsTcpHeader[15] = 0x00;
            FinsTcpHeader[16] = 0x00;
            FinsTcpHeader[17] = 0x00;
            FinsTcpHeader[18] = 0x00;
            FinsTcpHeader[19] = 0x00;

            ns.Write(FinsTcpHeader, 0, FinsTcpHeader.Length);

            byte[] resHeader = new byte[256];
            int resHeaderSize = ns.Read(resHeader, 0, resHeader.Length);

            if ((resHeader[8] == 0x00 && resHeader[9] == 0x00) &&
               (resHeader[10] == 0x00 && resHeader[11] == 0x01))
            {

                byte ClientNodeNo = resHeader[19];
                byte ServerNodeNo = resHeader[23];


                //FINS-TCPヘッダ＋FINSフレームの送信
                byte[] cmd = new byte[34];
                // ----------------- FINS-TCPヘッダ
                cmd[0] = 0x46;          // "F"
                cmd[1] = 0x49;          // "I"
                cmd[2] = 0x4E;          // "N"
                cmd[3] = 0x53;          // "S"
                cmd[4] = 0x00;          // Length
                cmd[5] = 0x00;
                cmd[6] = 0x00;
                cmd[7] = 18 + 8;        // (Command以降のバイト数)
                cmd[8] = 0x00;          // Command
                cmd[9] = 0x00;
                cmd[10] = 0x00;
                cmd[11] = 0x02;
                cmd[12] = 0x00;         // ErrorCode
                cmd[13] = 0x00;
                cmd[14] = 0x00;
                cmd[15] = 0x00;
                // ----------------- FINSヘッダ
                cmd[16] = 0x80;          // ICF
                cmd[17] = 0x00;          // RSV
                cmd[18] = 0x02;          // GCT
                cmd[19] = 0;             // DNA  相手先ネットワークアドレス
                cmd[20] = ServerNodeNo;  // DA1  相手先ノードアドレス
                cmd[21] = 0;             // DA2  相手先号機アドレス
                cmd[22] = 0;             // SNA  発信元ネットワークアドレス
                cmd[23] = ClientNodeNo;  // SA1  発信元ノードアドレス
                cmd[24] = 0;             // SA2  発信元号機アドレス
                cmd[25] = 1;             // SID  識別子 00-FFの任意の数値
                                         // ----------------- FINSコマンド
                cmd[26] = 0x01;         // MRC  読出しコマンド 0101
                cmd[27] = 0x01;         // SRC
                cmd[28] = 0x82;         // MemoryType = DM
                cmd[29] = 0x00;         // ReadAddress = 100CH
                cmd[30] = 0x64;
                cmd[31] = 0x00;
                cmd[32] = 0x00;         // ReadSize = 10CH
                cmd[33] = 0x0A;

                ns.Write(cmd, 0, cmd.Length);
                textBox1.AppendText("-> " + BitConverter.ToString(cmd) + "\r\n");

                byte[] resCmd = new byte[2048];
                int resCmdSize = ns.Read(resCmd, 0, resCmd.Length);

                if ((resCmd[8] == 0x00 && resCmd[9] == 0x00) &&
                    (resCmd[10] == 0x00 && resCmd[11] == 0x02))
                {
                    byte[] rcvData = new byte[resCmdSize];
                    Array.Copy(resCmd, rcvData, resCmdSize);

                    textBox1.AppendText("<- " + BitConverter.ToString(rcvData) + "\r\n\r\n");
                }
            }
            ns.Close();
            tcp.Close();
        }
    }
}
