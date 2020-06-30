using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 貪食蛇
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                StreamReader sr = new StreamReader(".\\玩家資料.txt");
                string cc = "";
                cc = sr.ReadToEnd();
                cc += textBox1.Text + "\n";
                sr.Close();
                StreamWriter sw = new StreamWriter(".\\玩家資料.txt");
                sw.Write(cc);
                sw.Close();


                this.Hide();
                Form2 frm = new Form2();
                frm.Show();
            }
            else
            {
                MessageBox.Show("請輸入名字");
            }
            
        }
        
        
    }
}
