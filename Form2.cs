using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Forms;
using System.Diagnostics;

namespace 貪食蛇
{


    public partial class Form2 : Form
    {
        public int playerid;
        public int n = 0;
        public bool iseat = true;
        public int startuse = 3;
        public int ourtime = 5;
        public int yummy = 0;
        public int statruse = 0;
        food fo = new food();
        snakebody Fistbody;
        snakebody body = new snakebody();
        snakebody addbody = new snakebody();
        public SoundPlayer bgm = new SoundPlayer("Original Tetris theme (Tetris Soundtrack).wav");
        public SoundPlayer over = new SoundPlayer("遊戲結束.wav");

        
        public bool game = true;
        public Form2()
        {
            InitializeComponent();          

        }

        public class snakebody
        {
            public Label body = new Label();
            public snakebody boo;
            public void setting()
            {
                body.Size = new Size(20, 20);
                body.BackColor = Color.Lime;
                //body.BackColor = Color.FromArgb(200, 100, 0);
            }
        }

        class food   //食物相關資料
        {
            public Label fd = new Label();
            public int x = 0;
            public int y = 0;
            public int bodynum = 0;
            public void lo(int x, int y)
            {
                fd.AutoSize = false;
                fd.Size = new Size(20, 20);
                fd.ForeColor = Color.Lime;
                fd.Location = new Point(x, y);
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {          
            label8.Visible = false;
            label8.BackColor = Color.Red;
            label7.ForeColor = Color.Lime;
            ///////////////////////////// Button setting///////////////////////////
            button1.FlatStyle = FlatStyle.Flat;
            button1.BackColor = Color.Transparent;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseOverBackColor = Color.Transparent;
            button1.FlatAppearance.MouseDownBackColor = Color.Transparent;
            ///////////////////////////// Button setting///////////////////////////

            ///////////////////////////// label setting///////////////////////////
            label7.FlatStyle = FlatStyle.Flat;
            label7.BackColor = Color.Transparent;
            label7.Image?.Dispose();


            label5.Font = new Font("JackeyFont", 10, FontStyle.Regular, GraphicsUnit.Point);
            label6.Font = new Font("JackeyFont", 10, FontStyle.Regular, GraphicsUnit.Point);

            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            ///////////////////////////// label setting///////////////////////////
            bgm.PlayLooping();           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer3.Enabled = true;
            button1.Visible = false;
            bgm.Stop();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void playeat() //播放音效
        {
            var player1 = new WMPLib.WindowsMediaPlayer();
            player1.URL = "吃.wav";
        }
        private void playsecond() //播放音效
        {
            var player2 = new WMPLib.WindowsMediaPlayer();
            player2.URL = "倒數用.wav";
        }
        private void playstart() //播放音效
        {
            var player3 = new WMPLib.WindowsMediaPlayer();
            player3.URL = "開始用.wav";
        }
        private void Form2_KeyDown(object sender, KeyEventArgs e)   //按鍵操控
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (n != 2)
                        n = 1;
                    break;
                case Keys.Down:
                    if (n != 1)
                        n = 2;
                    break;
                case Keys.Left:
                    if (n != 4)
                        n = 3;
                    break;
                case Keys.Right:
                    if (n != 3)
                        n = 4;
                    break;
            }
        }

        private void snakemove()
        {           
            timer1.Enabled = false;
            if (yummy != 0)
            {
                snakebody nowbody = Fistbody;
                snakebody tempbody = new snakebody();
                snakebody lastbody = new snakebody();
                bool fst = true;
                while (nowbody.boo.boo != null)
                {
                    tempbody.body.Location = nowbody.boo.body.Location;
                    if (fst)
                    {
                        nowbody.boo.body.Location = nowbody.body.Location;
                        fst = false;
                    }
                    else
                    {
                        nowbody.boo.body.Location = lastbody.body.Location;
                    }
                    lastbody.body.Location = tempbody.body.Location;
                    nowbody = nowbody.boo;
                }
                Fistbody.body.Location = label8.Location;
                addbody.body.Location = lastbody.body.Location;               
            }
            timer1.Enabled = true;
        }

        private void snakeadd()
        {
            if (yummy == 1)
            {
                body.body.Location = label8.Location;
                Fistbody = body;
                body.setting();
                this.Controls.Add(body.body);
                body.boo = new snakebody();
                body = body.boo;
            }
            else
            {
                body.body.Location = addbody.body.Location;
            }
            body.setting();
            this.Controls.Add(body.body);
            body.boo = new snakebody();
            body = body.boo;
        }

        private void snakeeat(int n , int step)
        {
            if (iseat == false)
            {                
                int HT = label8.Top;
                int HL = label8.Left;
                int fx = fo.x;
                int fy = fo.y;
                switch (n)
                {
                    case 1:
                        if (fy >= HT && fy <= HT + step && fx >= HL && fx <= HL + 20)
                            Checkeat();
                        break;
                    case 2:
                        if (fy >= HT -step && fy <= HT  && fx >= HL && fx <= HL + 20)
                            Checkeat();
                        break;
                    case 3:
                        if (fy >= HT && fy <= HT +20  && fx >= HL && fx <= HL + step)
                            Checkeat();
                        break;
                    case 4:
                        if (fy >= HT  && fy <= HT +20  && fx >= HL -step && fx <= HL )
                            Checkeat();
                        break;
                }
            }
            
        }
        

        private void Checkeat()
        {
            iseat = true;
            playeat();
            timer4.Enabled = false;
            ourtime = 5;
            yummy++;
            snakeadd();
            this.Controls.Remove(fo.fd);
        }

        private food ieat()   //食物製作
        {
            food fod = new food();
            Random a = new Random();
            fod.x = a.Next(25, 530);
            fod.y = a.Next(25, 535);
            while (fod.x %10 != 5)
            {
                fod.x = a.Next(25, 530);
            }
            while(fod.y %10 != 5)
            {
                fod.y = a.Next(25, 535);
            }
            //fod.bodynum = a.Next(1, 10);
            fod.fd.Text = "🍎";
            fod.lo(fod.x, fod.y);
            this.Controls.Add(fod.fd);            
            return fod;
        }


        public void showscore()
        {
            if (yummy == 0)
            {
                label5.Text = "Score:";
            }
            else
            {
                label5.Text = "Score:" + yummy;
            }
        }
        private void wrplayer()
        {
            StreamReader sr = new StreamReader(".\\玩家分數.txt");
            string cc = "";
            cc = sr.ReadToEnd();
            cc += yummy.ToString() + "分" + "\n";
            sr.Close();
            StreamWriter sw = new StreamWriter(".\\玩家分數.txt");
            sw.Write(cc);
            sw.Close();
        }

        private void replayer()
        {
            label9.Visible = true;
            label11.Visible = true;
            label9.Text = "玩家名稱" + "\n";
            int cont = 0;
            int con = 0;
            StreamReader sr = new StreamReader(".\\玩家資料.txt");
            string cc = "";
            while (sr.Peek() != -1)
            {
                cont++;
                sr.ReadLine();
            }
            sr.Close();
            StreamReader sr1 = new StreamReader(".\\玩家資料.txt");
            while (sr1.Peek() != -1)
            {
                con++;
                if (con< cont)
                {
                    cc += sr1.ReadLine() + "\n";
                }
                else
                {
                    label11.Text = "目前玩家名:" + sr1.ReadLine();
                    break;
                }
            }
            sr1.Close();
            label9.Text += cc;
        }

        private void rescore()
        {
            label10.Visible = true;
            label10.Text = "得分" + "\n";
            StreamReader sr = new StreamReader(".\\玩家分數.txt");
            string cc = "";
            while (sr.Peek() != -1)
            {
                cc += sr.ReadLine() + "\n";
            }            
            sr.Close();
            label10.Text += cc;
        }

        private void timer1_Tick(object sender, EventArgs e)   //蛇移動
        {
            showscore();           
            replayer();
            rescore();
            timer4.Enabled = true;
            int step = 10;
            
            if (n == 0 && startuse == 0)
            {
                label8.Visible = true;
                label8.Left += step;
                label7.Visible = false;
            }
            if (yummy >= 10 && yummy <20)
                step = 10;
            else if (yummy >= 20 && yummy <25)
                step = 15;
            else if (yummy >= 25)
                step = 20;

            
            switch (n)
            {
                case 1:
                    label8.Top -= step;
                    snakeeat(n , step);
                    break;
                case 2:
                    label8.Top += step;
                    snakeeat(n , step);
                    break;
                case 3:
                    label8.Left -=step;
                    snakeeat(n , step);
                    break;
                case 4:
                    label8.Left += step;
                    snakeeat(n , step);
                    break;
            } 
            snakemove();
            if (label8.Top <= 5 || label8.Top >= 535 || label8.Left <= 5 || label8.Left >= 525)
            {
                wrplayer();
                timer1.Enabled = false;
                timer2.Enabled = false;
                bgm.Stop();
                over.Play();               
                game = false;
                MessageBox.Show("遊戲結束");
                DialogResult result = MessageBox.Show("是否繼續遊戲", "詢問",MessageBoxButtons.OKCancel);
                Form2 frm = new Form2();
                Form1 f = new Form1();
                if (result == DialogResult.OK)
                {                   
                    this.Hide();
                    f.Show();
                }
                else
                {
                    frm.Close();
                    this.Close();
                    Environment.Exit(Environment.ExitCode);
                    InitializeComponent();
                }
            }         
        }

        private void timer2_Tick(object sender, EventArgs e)   //食物產生
        {
            if (iseat == true)
            {
                iseat = false;
                fo = ieat();
                
            }
        }

        private void timer3_Tick_1(object sender, EventArgs e)   //開始時倒數
        {
            var img0 = Image.FromFile("Start(綠色字體).png");
            var img1 = Image.FromFile("1(綠色字體).png");
            var img2 = Image.FromFile("2(綠色字體).png");
            var img3 = Image.FromFile("3(綠色字體).png");
            if (startuse == 0)
            {
                //playstart();
                label7.Image = img0;                
                timer1.Enabled = true;
                timer2.Enabled = true;
                timer3.Enabled = false;
                bgm.PlayLooping();
            }
            else if(startuse ==1)
            {
                label7.Image = img1;
                playsecond();
                startuse--;               
            }
            else if (startuse == 2)
            {
                label7.Image = img2;
                playsecond();
                startuse--;
            }
            else if (startuse == 3)
            {
                label7.Image = img3;
                playsecond();
                startuse--;
            }
        }

        private void timer4_Tick(object sender, EventArgs e)   //倒數吃到沒
        {
           if (iseat == false)
            {              
                if (ourtime != 0)
                {
                    label6.Text =ourtime.ToString() + "Seconds";
                    ourtime--;
                }
                else
                {
                    iseat = true;
                    label6.Text =ourtime.ToString() + "Seconds";
                    ourtime = 5;
                    this.Controls.Remove(fo.fd);
                    timer4.Enabled = false;                    
                }
            }        
        }      
    }
}
