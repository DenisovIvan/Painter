using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace test
{
    public partial class Form1 : Form
    {
        Graphics g;
        Pen DrawPen = new Pen(Color.Black, 1);
        Pen DrawPenred = new Pen(Color.Red, 1);
        Pen DrawPenblue = new Pen(Color.Blue, 1);
        Pen DrawPenghost = new Pen(Color.Cyan, 1);
        Point P1, P2, P1v, P2v, tP;
        const int Yemax = 396;
        int indexMaxHeight = -1;
        bool blue = true;
        uint w = 1;
        const int np = 20; // Размер массива
        Point[] mass = new Point[np];
        int type, sw1, sw2, swb = 0, sw3=0 , sw4=0,  Ymax, Xmax ,Yhigh,Ylow,Xmin;
        List<Point>  pntlist2= new List<Point>();
        List<Point> pntlist1 = new List<Point>();
        List<Point> pntlist = new List<Point>();
        List<int> Xb = new List<int>();
        Boolean ghoste ;
        public Form1()
        {
            InitializeComponent();
            g = pb.CreateGraphics();
            ghoste = checkBox1.Checked;
            Ymax = pb.Size.Height;
            Xmax = pb.Size.Width;
            Yhigh = 0;
            Xmin = 0;
            Ylow = Ymax;
        }

        private void btclear_Click(object sender, EventArgs e)
        {
            g.Clear(Color.White);
            
            swb = 0;
            sw1 = 0;
            sw2 = 0;
            sw4 = 0;
            pntlist1.Clear();
            pntlist2.Clear();
            label2.Text = "Создайте фигуру 1 (ЛКМ - точка ПКМ - завершение)";

            linechoose.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (sw4 == 2)
            {
                FigureMath(pntlist1, pntlist2, new[] { 1,2,3 });
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (sw4 == 2)
            {
                FigureMath(pntlist1, pntlist2, new[] {  3 });
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (sw4 == 2)
            {
                FigureMath(pntlist1, pntlist2, new[] { 1, 2 });
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (sw4 == 2)
            {
                FigureMath(pntlist1, pntlist2, new[] {  2 });
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (sw4 == 2)
            {
                blue = false;
                FigureMath(pntlist1, pntlist2, new[] { 1 });
                blue = true;
            }
        }

        private long fack(int a)
        {
            int b = 1;
            long result = 1;
            while (b < a) {
                b++;
                result = result * b;

            }
            return result;
            
        }

 

        public void Draw(Pen Brush, int x, int y)
        { g.DrawRectangle(Brush, x, y, w, w); }

        private void colorchoose_SelectionChangeCommitted(object sender, EventArgs e)
        {
            switch (colorchoose.SelectedIndex)
            {
                case 0:
                    DrawPen.Color = Color.Black;
                    break;
                case 1:
                    DrawPen.Color = Color.Red;
                    break;
                case 2:
                    DrawPen.Color = Color.Green;
                    break;
                case 3:
                    DrawPen.Color = Color.Blue;
                    break;
            }
        }

        private void brushchoose_SelectionChangeCommitted(object sender, EventArgs e)
        {
            switch (brushchoose.SelectedIndex)
            {
                case 0:
                    w = 1;
                    break;
                case 1:
                    w = 3;
                    break;
                case 2:
                    w = 5;
                    break;
                case 3:
                    w = 10;
                    break;
                case 4:
                    w = 50;
                    break;
            }

        }

        private void linechoose_SelectionChangeCommitted(object sender, EventArgs e)
        {

            type = linechoose.SelectedIndex;
            if (type==5)
            {
                label2.Text = "Создайте фигуру 1 (ЛКМ - точка ПКМ - завершение)";
            }

        }

        private void pb_MouseClick(object sender, MouseEventArgs e)
        {
            switch (type)
            {
                
                case 2:
                    switch (e.Button)
                    {
                        case MouseButtons.Left:
                            label1.Text = e.Location.ToString();
                            linechoose.Enabled = false;
                            if (swb != 20)
                            {
                                Draw(DrawPen, e.X, e.Y);
                                mass[swb] = e.Location;
                                if (swb != 0) {
                                    if (ghoste)
                                    ProcDrawLine(DrawPenghost, mass[swb-1].X, mass[swb-1].Y, mass[swb].X, mass[swb].Y);
                                }
                                swb++;
                            }
                            else { 
                                ProcDrawBel(mass,swb-1);
                                swb = 0;
                                linechoose.Enabled = true;
                            }
                            break;
                        case MouseButtons.Right:
                            label1.Text = e.Location.ToString();
                            if (swb >= 3)
                            {
                                ProcDrawBel(mass,swb-1);
                                Ymax = 0;
                                Ylow = Ymax;
                                linechoose.Enabled = true;
                                swb = 0;
                            }
                            else {
                                swb = 0;
                                linechoose.Enabled = true;
                            }
                            break;
                    }
                    break;
                case 3:
                    switch (e.Button)
                    {
                        case MouseButtons.Left:
                            Ymax = pb.Size.Height;
                            Ylow = Ymax;
                            linechoose.Enabled = false;
                            tP = e.Location;
                            if (e.Location.X > Xmax)
                                tP.X = Xmax;
                            if (e.Location.Y > Ymax)
                                tP.Y = Ymax;
                            pntlist.Add(tP);
                            label1.Text = e.Location.ToString();
                            sw3++;
                            break;
                        case MouseButtons.Right:
                            label1.Text = e.Location.ToString();
                            if (sw3 < 3)
                            {
                                pntlist.Clear();
                            }
                            else {
                                Ymax = pb.Size.Height;
                                Ylow = Ymax;
                                ProcDrawNOP(pntlist);
                                pntlist.Clear();
                            }
                            sw3 = 0;
                            linechoose.Enabled = true;
                            break;
                    }
                    break;
                case 4:
                    switch (e.Button)
                    {
                        case MouseButtons.Left:
                            Ymax = pb.Size.Height;
                            if (ghoste)
                                Draw(DrawPen, e.X, e.Y);
                            Ylow = Ymax;
                            linechoose.Enabled = false;
                            tP = e.Location;
                            if (e.Location.X > Xmax)
                                tP.X = Xmax;
                            if (e.Location.Y > Ymax)
                                tP.Y = Ymax;
                            pntlist.Add(tP);
                            label1.Text = e.Location.ToString();
                            sw3++;
                            break;
                        case MouseButtons.Right:
                            label1.Text = e.Location.ToString();
                            if (sw3 < 3)
                            {
                                pntlist.Clear();
                            }
                            else
                            {
                                OrientedFill(pntlist);
                                pntlist.Clear();
                            }
                            sw3 = 0;
                            linechoose.Enabled = true;
                            break;
                    }
                    break;
                case 5:
                    switch (e.Button)
                    {
                        case MouseButtons.Left:
                            if (sw4 == 0)
                            { 
                                 Ymax = pb.Size.Height;
                                if (ghoste)
                                    Draw(DrawPen, e.X, e.Y);
                                Ylow = Ymax;
                                linechoose.Enabled = false;
                                tP = e.Location;
                                if (e.Location.X > Xmax)
                                    tP.X = Xmax;
                                if (e.Location.Y > Ymax)
                                    tP.Y = Ymax;
                                pntlist1.Add(tP);
                                label1.Text = e.Location.ToString();
                                sw3++;
                            }
                            else if (sw4 == 1)
                            {

                                Ymax = pb.Size.Height;
                                if (ghoste)
                                    Draw(DrawPen, e.X, e.Y);
                                Ylow = Ymax;
                                linechoose.Enabled = false;
                                tP = e.Location;
                                if (e.Location.X > Xmax)
                                    tP.X = Xmax;
                                if (e.Location.Y > Ymax)
                                    tP.Y = Ymax;
                                pntlist2.Add(tP);
                                label1.Text = e.Location.ToString();
                                sw3++;
                            }
                                break;
                            
                        case MouseButtons.Right:
                            if (sw4 == 2)
                            {
                                pntlist2.Clear();
                                pntlist1.Clear();
                                linechoose.Enabled = true;
                                label2.Text = "Создайте фигуру 1 (ЛКМ - точка ПКМ - завершение)";
                                sw4 = 0;
                            }
                            if (sw4 == 0)
                            {
                                label1.Text = e.Location.ToString();
                                if (sw3 < 3)
                                {
                                    pntlist1.Clear();
                                }
                                else
                                {
                                    label2.Text = "Создайте фигуру 2 (ЛКМ - точка ПКМ - завершение)";
                                    ProcDrawNOP(pntlist1);
                                    sw4 = 1;
                                }
                                sw3 = 0;
                            }
                            else if (sw4==1)
                            {
                                label1.Text = e.Location.ToString();
                                if (sw3 < 3)
                                {
                                    pntlist1.Clear();
                                }
                                else
                                {
                                    ProcDrawNOP(pntlist2);
                                    sw4 = 2;
                                    label2.Text = "Выберите действие (ПКМ - завершения операций)";
                                }
                                sw3 = 0;
                            }
                            break;
                    }
                    break;
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (type)
            {
                case 0:
                    label1.Text = e.Location.ToString();
                    g.DrawEllipse(DrawPen, e.X, e.Y, w, w);
                    P1.X = e.X; P1.Y = e.Y;
                    break;
                case 1:
                    switch (sw1)
                    {
                        case 0:
                            label1.Text = e.Location.ToString();
                            P1.X = e.X; P1.Y = e.Y;
                            Draw(DrawPen, e.X, e.Y);
                            linechoose.Enabled = false;
                            sw1 = 1;
                            break;
                        case 1:
                            label1.Text = e.Location.ToString();
                            P2.X = e.X; P2.Y = e.Y;
                            Draw(DrawPen, e.X, e.Y);
                            sw1 = 0;
                            break;
                    }
                    break;
                
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            switch (type)
            {
                case 0:
                    label1.Text = e.Location.ToString();
                    ProcDrawLine(DrawPen, P1.X, P1.Y, e.X, e.Y);
                    P1.X = e.X; P1.Y = e.Y;
                    break;
                case 1:
                    switch (sw2)
                    {
                        case 0:
                            label1.Text = e.Location.ToString();
                            P1v.X = e.X; P1v.Y = e.Y;
                            Draw(DrawPen, e.X, e.Y);
                            if (ghoste)
                            ProcDrawLine(DrawPenghost, P1.X, P1.Y, P1v.X, P1v.Y);
                            sw2 = 1;
                            break;
                        case 1:
                            label1.Text = e.Location.ToString();
                            P2v.X = e.X; P2v.Y = e.Y;
                            Draw(DrawPen, e.X, e.Y);
                            if(ghoste)
                            ProcDrawLine(DrawPenghost, P2.X, P2.Y, P2v.X, P2v.Y);
                            ProcDrawcube(P1, P2, P1v, P2v);
                            linechoose.Enabled = true;
                            sw2 = 0;
                            break;
                    }
                    break;
                case 2:
                    break;
            }
        }

        void FigureMath(List<Point> lstp1, List<Point> lstp2, int[] mass) {
            List<int> xal = new List<int>();
            List<int> xar = new List<int>();
            List<int> xbl = new List<int>();
            List<int> xbr = new List<int>();
            List<int> xrl = new List<int>();
            List<int> xrr = new List<int>();
            int count1 = lstp1.Count;
            int count2 = lstp2.Count;
            int n, m;
            int k;
            for (int i = 0; i < count1; i++)
            {
                if (lstp1[i].Y < Ylow)
                {
                    Ylow = lstp1[i].Y;
                }
                if (lstp1[i].Y > Yhigh)
                {
                    Yhigh = lstp1[i].Y;
                }
            }
            for (int i = 0; i < count2; i++)
            {
                if (lstp2[i].Y < Ylow)
                {
                    Ylow = lstp2[i].Y;
                }
                if (lstp2[i].Y > Yhigh)
                {
                    Yhigh = lstp2[i].Y;
                }
            }
            g.Clear(Color.White);
            for (int i = Ylow; i < Yhigh; i++)
            {
                xal.Clear();
                xar.Clear();
                xbl.Clear();
                xbr.Clear();
                Xb.Clear();
                xrr.Clear();
                xrl.Clear();
                for (int c = 0; c < count1; c++)
                {
                    if (c < count1 - 1)
                    {
                        k = c + 1;
                    }
                    else
                    {
                        k = 0;
                    }
                    if ((lstp1[c].Y >= i && lstp1[k].Y < i) || (lstp1[c].Y < i && lstp1[k].Y >= i))
                    {
                        Xb.Add((lstp1[k].X - lstp1[c].X) * (i - lstp1[c].Y) / (lstp1[k].Y - lstp1[c].Y) + lstp1[c].X);
                    }
                }
                Xb.Sort();
                for (int u = 0; u < Xb.Count - 1; u = u + 2)
                {
                    xal.Add(Xb[u]);xar.Add(Xb[u + 1]);
                }
                Xb.Clear();
                for (int c = 0; c < count2; c++)
                {
                    if (c < count2 - 1)
                    {
                        k = c + 1;
                    }
                    else
                    {
                        k = 0;
                    }
                    if ((lstp2[c].Y >= i && lstp2[k].Y < i) || (lstp2[c].Y < i && lstp2[k].Y >= i))
                    {
                        Xb.Add((lstp2[k].X - lstp2[c].X) * (i - lstp2[c].Y) / (lstp2[k].Y - lstp2[c].Y) + lstp2[c].X);
                    }
                }
                Xb.Sort();

                for (int u = 0; u < Xb.Count - 1; u = u + 2)
                {
                    xbl.Add(Xb[u]); xbr.Add(Xb[u + 1]);
                }

                    for (int w = 0; w < xal.Count; w++)
                    {
                        g.DrawLine(DrawPenred, xal[w], i, xar[w], i);
                    }

                if (blue)
                {
                    for (int w = 0; w < xbl.Count; w++)
                    {
                        g.DrawLine(DrawPenblue, xbl[w], i, xbr[w], i);
                    }
                }
                

                int de = xal.Count + xar.Count + xbl.Count + xbr.Count;
                int[] Mx = new int[de];
                int[] Mq = new int[de];
                n = xal.Count;
                Console.WriteLine("M Count = " + de);
                for (int u = 0; u < n; u++)
                {
                    Mx[u] = xal[u];
                    Mq[u] = 2;
                    Console.WriteLine("xalu = " + xal[u]);

                }
                m = n;
                n = xar.Count;
                for (int u = 0; u < n; u++)
                {
                    Mx[m+u] = xar[u];
                    Mq[m+u] = -2;
                    Console.WriteLine("xaru = " + xar[u]);
                }
                m = m + n;
                n = xbl.Count;
                for (int u = 0; u < n; u++)
                {
                    Mx[m + u] = xbl[u];
                    Mq[m + u] = 1;
                    Console.WriteLine("xblu = " + xbl[u]);
                }
                m = m + n;
                n = xbr.Count;
                for (int u = 0; u < n; u++)
                {
                    Mx[m + u] = xbr[u];
                    Mq[m + u] = -1;
                    Console.WriteLine("xbru = " + xbr[u]);
                }
                m = m +n;
                int save0;
                int save1;
                for (int p = 0; p < m - 1; p++)
                {
                    for (int w = 0; w < m - 1 - p; w++)
                    {
                        if (Mx[w] > Mx[w+1])
                        {
                            save0 = Mx[w];
                            save1 = Mq[w];
                            Mx[w] = Mx[w + 1];
                            Mq[w] = Mq[w + 1];
                            Mx[w + 1] = save0;
                            Mq[w + 1] = save1;
                        }
                    }
                }
                for (int u = 0; u < Mx.Length; u++)
                {
                    Console.WriteLine("sort M = " + Mx[u]);
                }
                int q = 0;
                int x;
                int qnew;
                if (Mx.Length != 0)
                {
                    if (Mx[0] >= 0 && Mq[0] < 0)
                    {
                        xrl.Add(0); q = Mx[0] * (-1);
                        Console.WriteLine("!xrl - " + 0 + " y = " + i);
                    }
                    for (int w = 0; w < m; w++)
                    {
                        x = Mx[w]; qnew = q + Mq[w];
                        Console.WriteLine("q = " + q);
                        Console.WriteLine("qnew = " + qnew);

                        for (int v = 0; v < mass.Length; v++)
                        {
                            if (q != mass[v] && qnew == mass[v])
                            {
                                xrl.Add(x);
                                Console.WriteLine("xrl - " + x + " y = " + i);
                            }
                            if (q == mass[v] && qnew != mass[v])
                            {
                                xrr.Add(x);
                                Console.WriteLine("xrr - " + x + " y = " + i);
                            }
                        }
                        q = qnew;
                    }
                    for (int w = 0; w < xrl.Count; w++)
                    {
                        g.DrawLine(DrawPen, xrl[w], i, xrr[w], i);
                    }

                    Console.WriteLine("end of current y = " + i);
                }
            }




            Ylow = Ymax;
            Yhigh = 0;

        }
        void OrientedFill(List<Point> lstp)
        {
            int n = lstp.Count;
            int t = 0;
            bool CW = false;
            for (int i = 0; i < n; i++)
            {
                if (lstp[i].Y < Ylow)
                {
                    Ylow = lstp[i].Y;
                    indexMaxHeight = i;
                }
                if (lstp[i].Y > Yhigh)
                {
                    Yhigh = lstp[i].Y;
                }
            }

            int k1 = (indexMaxHeight == 0) ? n - 1 : indexMaxHeight - 1;
            int k2 = (indexMaxHeight == n - 1) ? 0 : indexMaxHeight + 1;

            CW = lstp[k1].X < lstp[k2].X;
            if (CW)
            {
                for (int y = 0; y < Ylow; y++)
                {
                    g.DrawLine(DrawPen, Xmin, y, Xmax, y);
                }
            }


            for (int y = Ylow; y < Ymax; y++)
            {
                List<Point> Xl = new List<Point>();
                List<Point> Xr = new List<Point>();

                if (CW) Xl.Add(new Point() { X = Xmin, Y = y });


                for (int i = 0; i < n; i++)
                {
                    t = i + 1 < n ? i + 1 : 0;
                    if ((lstp[i].Y < y && lstp[t].Y >= y) || (lstp[i].Y >= y && lstp[t].Y < y))
                    {
                        int x = (lstp[t].X - lstp[i].X) * (y - lstp[i].Y) / (lstp[t].Y - lstp[i].Y) + lstp[i].X;

                        if (lstp[t].Y > lstp[i].Y)
                        {
                            Xl.Add(new Point() { X = x, Y = y }); //xr
                            g.DrawEllipse(DrawPen, x, y, 1, 1);
                        }
                        else
                        {
                            Xr.Add(new Point() { X = x, Y = y }); //xl
                            g.DrawEllipse(DrawPen, x, y, 1, 1);
                        }
                    }
                }
                if (CW) Xr.Add(new Point() { X = Xmax, Y = y });
                Point save = new Point();
                for (int p = 0; p < Xl.Count-1; p++)
                {
                    for (int i = 0; i < Xl.Count - 1-p; i++)
                    {
                        if (Xl[i].X > Xl[i + 1].X)
                        {
                            save = Xl[i];
                            Xl[i] = Xl[i + 1];
                            Xl[i + 1] = save;

                        }
                    }
                }
                for (int p = 0; p < Xr.Count - 1; p++)
                {
                    for (int i = 0; i < Xr.Count - 1-p; i++)
                    {
                        if (Xr[i].X > Xr[i + 1].X)
                        {
                            save = Xr[i];
                            Xr[i] = Xr[i + 1];
                            Xr[i + 1] = save;

                        }
                    }
                }
                for (int i = 0; i < Xl.Count; i++)
                {
                    if (Xl[i].X < Xr[i].X)
                    {
                        g.DrawLine(DrawPen, Xl[i], Xr[i]);
                    }
                }
            }
            if (CW)
            {
                for (int y = Ymax; y < Yemax; y++)
                {
                    g.DrawLine(DrawPen, Xmin, y, Xmax, y);
                }
            }
            Ylow = Ymax;
            Yhigh = 0;


        }
        private void ProcDrawNOP(List<Point> lstp)
        {
            int count = lstp.Count;
            int k;
            for (int i = 0; i < count; i++)
            {
                if (lstp[i].Y<Ylow)
                {
                    Ylow = lstp[i].Y;
                }
                if (lstp[i].Y>Yhigh)
                {
                    Yhigh = lstp[i].Y;
                }
            }
            for (int i = Ylow; i < Yhigh; i++)
            {
                Xb.Clear();
                for (int c = 0; c < count; c++)
                {
                    if (c < count-1 )
                    {
                        k = c + 1;
                    }
                    else {
                        k = 0;
                    }
                    if ((lstp[c].Y >= i && lstp[k].Y < i)|| (lstp[c].Y < i && lstp[k].Y >= i))
                    {
                        Xb.Add((lstp[k].X - lstp[c].X) * (i - lstp[c].Y) / (lstp[k].Y - lstp[c].Y) + lstp[c].X);
                    }
                }
                Xb.Sort();
                for (int u = 0; u < Xb.Count-1; u = u + 2)
                {
                    g.DrawLine(DrawPen,Xb[u], i, Xb[u+1],i);
                }

            }
            Ylow = Ymax;
            Yhigh = 0;
        }


        private void ProcDrawBel(Point[] mass,int n)
        {
           
            long fuct = fack(n);
            double dt = 0.0001;
            double t = dt;
            double J;
            int xt,yt,i;
            Draw(DrawPen, mass[0].X, mass[0].Y);
            while (t < 1 + dt / 2) {
                xt = 0;yt = 0;
                i = 0;
                while (i <= n) {
                    J = Math.Pow(t, i) * Math.Pow((1 - t), (n - i)) * fuct / (fack(i) * fack(n - i));
                    xt = xt + Convert.ToInt32(mass[i].X * J);
                    yt = yt + Convert.ToInt32(mass[i].Y * J);
                    i = i + 1;
                }
                t = t + dt;
                Draw(DrawPen, xt, yt);
            }

        }
        private void ProcDrawcube(Point p1, Point p2, Point p1v, Point p2v)
        {
             int[,] Mh = { { 2, -2, 1, 1}, { -3, 3, -2, -1 } , { 0, 0, 1, 0 } , { 1, 0, 0, 0 } };
             int[,] Gh = { { p1.X, p1.Y }, { p2.X, p2.Y } , { p1v.X-p1.X, p1v.Y-p1.Y } , { p2v.X-p2.X, p2v.Y-p2.Y } };
             int[,] res = new int[4, 2];

             int summ = 0;
             for (int vus = 0; vus < 4; vus++)
             {
                  for (int  kolvo = 0; kolvo < 2; kolvo++)
                 {   
                     for (int dlina = 0; dlina < 4; dlina++)
                    {
                      summ = summ + Mh[vus, dlina] * Gh[dlina, kolvo];
                  }
                    res[vus, kolvo] = summ;
                    summ = 0;
                 }
                }
           
           
                 for (float t = 0;t<=1;t=t + 0.001f)
                  {
                  Draw(DrawPen, Convert.ToInt32(Math.Pow(t, 3) * res[0, 0] + Math.Pow(t, 2) * res[1, 0] + t * res[2, 0] + res[3, 0]),
                      Convert.ToInt32(Math.Pow(t, 3) * res[0, 1] + Math.Pow(t, 2) * res[1, 1] + t * res[2, 1] + res[3, 1]));
             }
        }
        private void ProcDrawLine(Pen DW, int x1, int y1, int x2, int y2)
        {
            int x, y, dx, dy, Sx = 0, Sy = 0;
            int F = 0, Fx = 0, dFx = 0, Fy = 0, dFy = 0;
            Decimal fx, fy;
            fx = x1;
            fy = y1;
            dx = x2 - x1;
            dy = y2 - y1;
            Sx = Math.Sign(dx);
            Sy = Math.Sign(dy);
            if (Sx > 0) dFx = dy;
            else dFx = -dy;
            if (Sy > 0) dFy = dx;
            else dFy = -dx;
            x = x1; y = y1;
            F = 0;
            if (Math.Abs(dx) >= Math.Abs(dy)) // угол наклона <= 45 градусов
            {
                do
                { //Вывести пиксель с координатами х, у
                    Draw(DW, x, y);
                    if (x == x2) break;
                    Fx = F + dFx;
                    F = Fx - dFy;
                    x = x + Sx;
                    if (Math.Abs(Fx) < Math.Abs(F)) F = Fx;
                    else y = y + Sy;
                } while (true);
            }
            else // угол наклона > 45 градусов
            {
                do
                { //Вывести пиксель с координатами х, у
                    Draw(DW, x, y);
                    if (y == y2) break;
                    Fy = F - dFy;
                    F = Fy + dFx;
                    y = y + Sy;
                    if (Math.Abs(Fy) < Math.Abs(F)) F = Fy;
                    else x = x + Sx;
                } while (true);
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            ghoste = checkBox1.Checked;
        }
    }
}
