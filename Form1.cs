using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{

    public partial class Form1 : Form
    {
        Bitmap off;

        class AllImage
        {
            public int dX;
            public int dY;
            public int sX;
            public int sY;
            public int W;
            public int H;
            public int dLoc;
            public int sLoc;
            public Bitmap im;
        }

        

        List<AllImage> L1 = new List<AllImage>();

        Timer t = new Timer();
        Random RR;

        int[] vRRarray = new int[9];
        int arrayValue = -1;
        int vRR;
        bool StartDraw = false;
        int iWhich = -1;
        bool isWin = false;

        public Form1()
        {
            InitializeComponent();
            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp);
            this.WindowState = FormWindowState.Maximized;
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.KeyUp += Form1_KeyUp;
            this.Text = " Assignment 14B";
            this.KeyDown += Form1_KeyDown;
            t.Tick += T_Tick;
            t.Start();
        }



        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
           
        }

        private void T_Tick(object sender, EventArgs e)
        {

        }

       

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {


        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if(StartDraw)
            DrawDouble(e.Graphics);

        }

        void Form1_MouseUp(object sender, MouseEventArgs e)
        {

            

        }


        void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !isWin)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (e.X > L1[i].dX && e.X < L1[i].dX + L1[i].W
                        && e.Y > L1[i].dY && e.Y < L1[i].dY + L1[i].H)
                    {
                        iWhich = i;
                        SwapLocations();

                        break;
                    }
                }

            }
        }

        void SwapLocations()
        {
            if(L1[iWhich].dLoc == L1[8].dLoc - 3 || L1[iWhich].dLoc == L1[8].dLoc + 3 || 
                L1[iWhich].dLoc == L1[8].dLoc - 1 || L1[iWhich].dLoc == L1[8].dLoc + 1)
            {
                int Z1 = L1[iWhich].dX;
                L1[iWhich].dX = L1[8].dX;
                L1[8].dX = Z1;

                int Z2 = L1[iWhich].dY;
                L1[iWhich].dY = L1[8].dY;
                L1[8].dY = Z2;
                

                int Z3 = L1[iWhich].dLoc;
                L1[iWhich].dLoc = L1[8].dLoc;
                L1[8].dLoc = Z3;

                for (int j = 0; j < 8; j++)
                {
                    isWin = true;
                    if (L1[j].dLoc != L1[j].sLoc)
                    {
                        isWin = false;
                        break;
                    }
                }

                DrawDouble(this.CreateGraphics());
            }


            
        }

        void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Text = "X = " + e.X + "  Y = " + e.Y;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            CreateActor();
        }

        void CreateActor()
        {
            int ax = 600;
            int ay = 200;
            int bx = 0;
            int by = 0;
            for (int i = 1; i < 10; i++)
            {
                AllImage pnn = new AllImage();
                if (i != 9)
                {
                    pnn.im = new Bitmap("animals_dog.jpg");
                }

                pnn.dLoc = i - 1;

                pnn.dX = ax;
                pnn.dY = ay;

                pnn.sX = bx;
                pnn.sY = by;
                if (i != 9)
                {
                    pnn.W = pnn.im.Width / 3;
                    pnn.H = pnn.im.Height / 3;
                }
                else
                {
                    pnn.W = L1[0].im.Width / 3;
                    pnn.H = L1[0].im.Height / 3;
                }
                
                L1.Add(pnn);
                ax += L1[0].im.Width / 3;
                bx += L1[0].im.Width / 3;

                if(i % 3 == 0)
                {
                    ax = 600;
                    bx = 0;
                    ay += L1[0].im.Height / 3;
                    by += L1[0].im.Height / 3;
                }

            }

            for(int i = 0; i < vRRarray.Length; i++)
            {
                vRRarray[i] = -1;
            }

            for (int i = 0; i < 8; i++)
            {
                arrayValue++;
                
                for (; ; )
                {
                    RR = new Random();
                    for (int k = 0; k < 99999; k++)
                    {
                        vRR = RR.Next(0, 8);
                    }

                    if (vRRisNotRepeated(vRR))
                    {
                        vRRarray[arrayValue] = vRR;
                        L1[i].sLoc = vRR;
                        break;
                    }
                }
                
            }
            StartDraw = true;
        }

        bool vRRisNotRepeated(int vRR)
        {
            for(int i = 0; i < arrayValue + 1; i++)
            {
                if(vRRarray[i] == vRR)
                {
                    return false;
                }
            }
            return true;
        }

        void DrawScene(Graphics g)
        {
            g.Clear(Color.Black);

            for(int i = 0; i < 8; i++)
            { 
                g.DrawImage(L1[vRRarray[i]].im,
                    new Rectangle(L1[i].dX, L1[i].dY, L1[i].W, L1[i].H),                                                                                        // Dest - Screen
                    new Rectangle(L1[vRRarray[i]].sX, L1[vRRarray[i]].sY, L1[vRRarray[i]].W, L1[vRRarray[i]].H),                                                // Src - Image
                    GraphicsUnit.Pixel);

                //string abLoc = "S:" + L1[i].sLoc + " D:" + L1[i].dLoc;
                //g.DrawString(abLoc, new Font("Arial", 8, FontStyle.Bold), Brushes.Black, L1[i].dX + (L1[i].W) / 3, L1[i].dY + (L1[i].H) / 3);             // Just for mine

                if (isWin)
                {
                    g.DrawString("Congrats, you won! The game has ended.", new Font("Arial", 14, FontStyle.Bold), Brushes.SpringGreen, 600, 620);
                }
                else
                {
                    g.DrawString("If you won, I will change :D", new Font("Arial", 14, FontStyle.Bold), Brushes.LightGray, 600, 620);

                }
            }

            for(int i = 8; i < 9; i++)
            {
                g.FillRectangle(Brushes.DarkRed, L1[i].dX, L1[i].dY, L1[i].W, L1[i].H);
            }

        }

        void DrawDouble(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }

       
    }
}


