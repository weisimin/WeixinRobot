using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace TaoHua_Insertor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        public void Insert_Taohua()
        {
            FileStream fs = new FileStream("C:\\Program Files (x86)\\十里桃花挂机软件\\OpenCode\\CQSSC.txt", FileMode.Open, FileAccess.Read);
            Int32 ReadCount = 0;
            StreamReader sr = new StreamReader(fs);
            dbDataContext db = new dbDataContext();
            while (ReadCount < 120 && sr.EndOfStream == false)
            {
                string NewLine = sr.ReadLine();
                Console.Write( NewLine);
                textBox1.Text += NewLine;
                string[] datas = NewLine.Split("\t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                TaoHua_GameResult findr = db.TaoHua_GameResult.SingleOrDefault(t => t.GamePeriod == datas[0] & t.GameResult == datas[1]);
                if (findr == null)
                {
                    TaoHua_GameResult gr = new TaoHua_GameResult();
                    gr.GamePeriod = datas[0];
                    gr.GameResult = datas[1];
                    gr.InsertTime = DateTime.Now;
                    db.TaoHua_GameResult.InsertOnSubmit(gr);
                    db.SubmitChanges();

                }
                ReadCount += 1;
                Application.DoEvents();

            }


            fs.Flush();
            fs.Close();


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = "";
                Insert_Taohua();
                textBox1.Text += "Complete";
                Console.Write("Complete");
            }
            catch (Exception AnyError)
            {
                textBox1.Text += AnyError.Message;
                textBox1.Text += AnyError.StackTrace;
                Console.Write(AnyError.Message);
                Console.Write(AnyError.StackTrace);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }


    }
}
