using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class HighScore : Form
    {
        private string[] scores;
        private string scoreStr;
        private Pacman pman;
        private string name;
        private int click = 1;
        private int i,j;
        private string tempstr;
        private bool LoadOnce;
        Graphics grfx;
        SolidBrush brush = new SolidBrush(Color.White);
        Font hs = new Font("Arial", 13, FontStyle.Bold);
        int typeOfHighsShow;

        public HighScore(Pacman pman)
        {
            this.typeOfHighsShow = 1;
            this.pman = pman;
            InitializeComponent();
            LoadOnce = true;
            timer1.Stop();
        }

        public HighScore()
        {
            this.typeOfHighsShow = 2;
            InitializeComponent();
            lbl.Visible = false;
            txt.Visible = false;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            if (this.typeOfHighsShow == 2)
                Close();

            if (this.typeOfHighsShow == 1)
            {
                if (click == 2)
                    Close();

                if (click == 1)
                {
                    lbl.Visible = false;
                    txt.Visible = false;
                    DecryptFile("data.txt", "data1.txt");
                    scores = File.ReadAllLines("data1.txt");
                    File.Delete("data.txt");

                    j = 0;
                    name = txt.Text;
                    if (name.Length == 0)
                        name = "Anonymous";

                    for (i = 0; i < 10; i++)
                    {
                        while (scores[i][j] != '-')
                        {
                            j++;
                        }
                        j++;
                        if (this.pman.GetScore() > int.Parse(scores[i].Substring(j)))
                        {
                            for (j = 9; j > i; j--)
                            {
                                scores[j] = scores[j - 1];
                            }

                            for (j = 0; j < 10; j++)
                            {
                                tempstr = scores[j].Substring(2);
                                scores[j] = (j + 1) + "." + tempstr;
                            }

                            scores[i] = (i + 1) + "." + name + " - " + pman.GetScore();
                            break;
                        }
                        j = 0;

                    }

                    for (i = 0; i < 10; i++)
                        if (scores[i][2] != '-' & scores[i][3] != '-')
                            scoreStr = scoreStr + scores[i] + "\r\n";

                    File.WriteAllLines("data1.txt", scores);
                    EncryptFile("data1.txt", "data.txt");
                    File.Delete("data1.txt");
                    click++;
                    timer1.Start();
                }
            }
        }

        private void EncryptFile(string inputFile, string outputFile)
        {
                const string password = @"M5e2w4SD";
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                string cryptFile = outputFile;
                FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateEncryptor(key, key),
                    CryptoStreamMode.Write);

                FileStream fsIn = new FileStream(inputFile, FileMode.Open);

                int data;
                while ((data = fsIn.ReadByte()) != -1)
                    cs.WriteByte((byte)data);


                fsIn.Close();
                cs.Close();
                fsCrypt.Close();
        }
        private void DecryptFile(string inputFile, string outputFile)
        {
            try
            {
                const string password = @"M5e2w4SD";

                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(password);

                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateDecryptor(key, key),
                    CryptoStreamMode.Read);

                FileStream fsOut = new FileStream(outputFile, FileMode.Create);

                int data;
                while ((data = cs.ReadByte()) != -1)
                    fsOut.WriteByte((byte)data);

                fsOut.Close();
                cs.Close();
                fsCrypt.Close();
            }
            catch
            {
                timer1.Stop();
                MessageBox.Show("data.txt file is corrupted!");
                Application.Exit();
            }
        }

        private void HighScore_Load_1(object sender, EventArgs e)
        {
            grfx = CreateGraphics();        
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!LoadOnce)
            {
                DecryptFile("data.txt", "data1.txt");
                scores = File.ReadAllLines("data1.txt");
                File.Delete("data.txt");
                for (i = 0; i < 10; i++)
                {

                    if (scores[i][2] != '-' & scores[i][3] != '-')
                        scoreStr = scoreStr + scores[i] + "\r\n";
                }
                File.WriteAllLines("data1.txt", scores);
                EncryptFile("data1.txt", "data.txt");
                File.Delete("data1.txt");
                LoadOnce = true;
            }
                try
                {
                    grfx.DrawString(scoreStr, hs, brush, 0, 180);
                }
                catch
                {

                }
        }
    }
}
