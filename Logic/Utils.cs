using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using IO = System.IO;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Reflection;

namespace OpenMesh
{
    public static class Utils
    {

        internal static void Deserializing()
        {
            //Console.WriteLine(CompactStackTrace(2));
        }

        internal static void Serializing()
        {
            //Console.WriteLine(CompactStackTrace(2));
        }

        private static readonly PointF[] PointFSingleArray = new PointF[1];

        public static PointF MatrixByPoint(Matrix M, PointF P)
        {
            PointFSingleArray[0] = P;
            M.TransformPoints(PointFSingleArray);
            return PointFSingleArray[0];
        }

        public static PointF MatrixByVector(Matrix M, PointF P)
        {
            PointFSingleArray[0] = P;
            M.TransformVectors(PointFSingleArray);
            return PointFSingleArray[0];
        }

        public static PointF Multiply(this PointF A, Single B)
        {
            return new PointF(A.X * B, A.Y * B);
        }

        public static PointF Divide(this PointF A, Single B)
        {
            return new PointF(A.X / B, A.Y / B);
        }

        public static PointF CoordByCoordMultiply(this PointF A, PointF B)
        {
            return new PointF(A.X * B.X, A.Y * B.Y);
        }

        public static PointF CoordByCoordDivide(this PointF A, PointF B)
        {
            return new PointF(A.X / B.X, A.Y / B.Y);
        }

        public static PointF Add(this PointF A, PointF B)
        {
            return new PointF(A.X + B.X, A.Y + B.Y);
        }

        public static PointF Subtract(this PointF A, PointF B)
        {
            return new PointF(A.X - B.X, A.Y - B.Y);
        }

        public static SizeF ToSizeF(this PointF A)
        {
            return new SizeF(A);
        }

        public static PointF ToPointF(this SizeF A)
        {
            return new PointF(A.Width, A.Height);
        }

        public static PointF ToPointF(this Size A)
        {
            return new PointF(A.Width, A.Height);
        }

        public static void AddValueWithType(this SerializationInfo Info, String Name, Object Object)
        {
            if (Object == null)
            {
                Info.AddValue(Name + ".Type", "Nothing");
            }
            else
            {
                Info.AddValue(Name + ".Type", Object.GetType().FullName);
                Info.AddValue(Name, Object, Object.GetType());
            }
        }

        public static Object GetValueWithType(this SerializationInfo Info, String Name)
        {
            var T = Info.GetString(Name + ".Type");
            if (T == "Nothing")
            {
                return null;
            }
            return Info.GetValue(Name, Assembly.GetExecutingAssembly().GetType(T));
        }

        public static void AddColor(this SerializationInfo Info, String Name, Color Color)
        {
            Info.AddValue(Name, Convert.ToString(Color.ToArgb(), 16));
        }

        public static Color GetColor(this SerializationInfo Info, String Name)
        {
            return Color.FromArgb(Convert.ToInt32(Info.GetString(Name), 16));
        }

        public static String CompactStackTrace(int n)
        {
            var R = new StringBuilder();

            var S = new StackTrace(true);
            if (n >= S.FrameCount)
                n = S.FrameCount - 1;

            for (int i = 1; i <= n; i++)
            {
                var F = S.GetFrame(i);
                var M = F.GetMethod();
                var TN = M.DeclaringType.Name;

                if (i > 1)
                    R.Append(">");
                R.Append(TN + ".").Append(M.Name + M.GetParameters().Length + ":" + F.GetFileLineNumber());
            }

            return R.ToString();
        }

        public static Boolean HasInvalidPathChars(String Path)
        {
            var InvalidChars = IO.Path.GetInvalidPathChars();

            for (int i = 0; i < Path.Length; i++)
            {
                if (InvalidChars.Contains(Path[i]))
                    return true;

                //if (checkAdditional && (c == '?' || c == '*'))
                //    return true;
            }

            return false;
        }

        public static String GetParentPath(String Path)
        {
            if (Path == null || Path.Length == 0)
            {
                return null;
            }

            if (HasInvalidPathChars(Path))
                throw new ArgumentException();

            Char ch = Path[Path.Length - 1];
            if (Path.Length == 1 && (ch == IO.Path.DirectorySeparatorChar || ch == IO.Path.AltDirectorySeparatorChar))
                return Path;
            if (ch == IO.Path.VolumeSeparatorChar)
                return Path;

            for (int i = Path.Length - 2; i >= 0; i--)
            {
                ch = Path[i];
                if (ch == IO.Path.DirectorySeparatorChar ||
                    ch == IO.Path.AltDirectorySeparatorChar ||
                    ch == IO.Path.VolumeSeparatorChar)
                {
                    if (i == 0)
                    {
                        return Path.Substring(0, 1);
                    }
                    return Path.Substring(0, i);
                }
            }
            return "";
        }

        //        void BrowseButton_Click(Object sender, EventArgs e)
        //        {
        //            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
        //            {
        //                this.textBox1.Text = this.openFileDialog1.FileName;
        //            }
        //        }

        //        void RedrawButton_Click(System.Object sender, System.EventArgs e)
        //        {
        //            //this.makeImage();
        //        }

        //        void SaveImageButton_Click(System.Object sender, System.EventArgs e)
        //        {
        //            if (this.saveFileDialog1.ShowDialog() == DialogResult.OK)
        //            {
        //                var bmp = new Bitmap(this.control1.Width, this.control1.Height);
        //                var g = Graphics.FromImage(bmp);

        //                g.Clear(Color.White);
        //                if (this.LinesDrawer != null)
        //                {
        //                    this.LinesDrawer.Draw(g);
        //                }

        //                bmp.Save(this.saveFileDialog1.FileName, System.Drawing.Imaging.ImageFormat.Png);

        //                g.Dispose();
        //                bmp.Dispose();
        //            }
        //        }

        //        void RunButton_Click(System.Object sender, System.EventArgs e)
        //        {
        //            //this.Lines = new array<Line>(5);
        //            //this.Lines[0] = Line(-10, 20, 7, 10);
        //            //this.Lines[1] = Line(8, -20, 42, -8);
        //            //this.Lines[2] = Line(-50, 24, -78, 45);
        //            //this.Lines[3] = Line(27, -42, 11, 20);
        //            //this.Lines[4] = Line(0, 37, 0, -80);

        //            //////////////////////////////////////////////////////////////////////////
        //            //var Asm = Reflection.Assembly.GetExecutingAssembly();
        //            //var App = Asm.GetManifestResourceStream("My2DUnstructuremesh.OpenMesh2D.exe");
        //            //if (!IO.Directory.Exists(".\\Temp"))
        //            //{
        //            //	IO.Directory.CreateDirectory(".\\Temp");
        //            //}
        //            //var File = IO.File.Open(".\\Temp\\App.exe", IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.None);
        //            //
        //            //File.Close();
        //            //App.Close();

        //            try
        //            {
        //                var InFile = IO.File.Open(this.textBox1.Text, IO.FileMode.Open, IO.FileAccess.Read);
        //                var InLFile = IO.File.Open(".\\input.txt", IO.FileMode.Create, IO.FileAccess.Write);
        //                Utils.copyStream(InFile, InLFile);
        //                InFile.Close();
        //                InLFile.Close();

        //                var Prc = new Process();
        //                Prc.StartInfo = new ProcessStartInfo(".\\OpenMesh2D.exe");
        //                Prc.StartInfo.CreateNoWindow = true;
        //                Prc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        //                Prc.StartInfo.UseShellExecute = true;
        //                Prc.Start();
        //                Prc.WaitForExit();

        //                var OutFile = IO.File.Open(".\\output.txt", IO.FileMode.Open, IO.FileAccess.Read);
        //                var OutText = new IO.StreamReader(OutFile, true);
        //                var Lines = new List<Line>();

        //                while (!OutText.EndOfStream)
        //                {
        //                    Line L;
        //                    PointF P1, P2;

        //                    if (OutText.ReadLine().Trim() != "zone")
        //                    {
        //                        throw new Exception("Invalid output");
        //                    }

        //                    var t = new String[1];
        //                    t[0] = " ";
        //                    var PS = OutText.ReadLine().Split(t, StringSplitOptions.RemoveEmptyEntries);
        //                    P1 = new PointF(Convert.ToDouble(PS[0]), Convert.ToDouble(PS[1]));
        //                    PS = OutText.ReadLine().Split(t, StringSplitOptions.RemoveEmptyEntries);
        //                    P2 = new PointF(Convert.ToDouble(PS[0]), Convert.ToDouble(PS[1]));
        //                    L = new Line(P1, P2);
        //                    Lines.Add(L);
        //                }

        //                OutText.Close();
        //                OutFile.Close();

        //                this.LinesDrawer = new LinesDrawer(Lines.ToArray());
        //                this.LinesDrawer.SetBoardSize(new PointF(this.control1.Size));
        //            }
        //            catch (Exception ex)
        //            {
        //                MessageBox.Show(ex.Message);
        //            }

        //            this.InvalidateControl1();
        //        }

        //        public static void copyStream(IO.Stream inStream, IO.Stream outStream)
        //        {
        //            /*static*/
        //            var Buffer = new Byte[1024];
        //            for (; ; )
        //            {
        //                int n = inStream.Read(Buffer, 0, Buffer.Length);
        //                if (n == 0)
        //                    break;
        //                outStream.Write(Buffer, 0, n);
        //            }
        //        }

        //        //void makeImage()
        //        //{
        //        //	Double x1, x2, y1, y2, w, h, dw, dh, bw, bh;
        //        //	Graphics g;
        //        //
        //        //	this.OriginalImage = null;
        //        //	this.pictureBox1.Image = this.OriginalImage;
        //        //	
        //        //	if (this.Lines == null || this.Lines.Length == 0)
        //        //		return;
        //        //
        //        //	x1 = this.Lines[0].x1;
        //        //	y1 = this.Lines[0].y1;
        //        //	x2 = x1;
        //        //	y2 = y1;
        //        //
        //        //	for each (Line l in this.Lines)
        //        //	{
        //        //		x1 = Math.Min(l.x1, x1);
        //        //		x1 = Math.Min(l.x2, x1);
        //        //		y1 = Math.Min(l.y1, y1);
        //        //		y1 = Math.Min(l.y2, y1);
        //        //		x2 = Math.Max(l.x1, x2);
        //        //		x2 = Math.Max(l.x2, x2);
        //        //		y2 = Math.Max(l.y1, y2);
        //        //		y2 = Math.Max(l.y2, y2);
        //        //	}
        //        //	
        //        //	w = x2 - x1;
        //        //	h = y2 - y1;
        //        //
        //        //	bw = (Double)this.numericUpDown1.Value;
        //        //	bh = bw;
        //        //
        //        //	if (w < h)
        //        //	{
        //        //		bw = bh / h * w;
        //        //	}
        //        //	else
        //        //	{
        //        //		bh = bw / w * h;
        //        //	}
        //        //
        //        //	bw += 20;
        //        //	bh += 20;
        //        //
        //        //	dw = 20 / bw * w;
        //        //	dh = 20 / bh * h;
        //        //
        //        //	x1 -= dw;
        //        //	y1 -= dh;
        //        //	w += dw * 2;
        //        //	h += dh * 2;
        //        //
        //        //	this.OriginalImage = new Bitmap((Int32)bw, (Int32)bh);
        //        //	g = Graphics.FromImage(this.OriginalImage);
        //        //
        //        //	//var R = new Random();
        //        //
        //        //	var Path = new Drawing2D.GraphicsPath();
        //        //	for each (Line l in this.Lines)
        //        //	{
        //        //		//g.DrawLine(new Pen(Color.FromArgb(R.Next(256), R.Next(256), R.Next(256))),
        //        //		Path.AddLine((Single)((l.x1 - x1) / w * bw),
        //        //					(Single)((l.y1 - y1) / h * bh),
        //        //					(Single)((l.x2 - x1) / w * bw),
        //        //					(Single)((l.y2 - y1) / h * bh));
        //        //	}
        //        //	
        //        //	g.Clear(Color.White);
        //        //	g.ResetTransform();
        //        //	g.TranslateTransform(x1, y1);
        //        //	g.ScaleTransform(1 / (w * bw), 1 / (h * bh));
        //        //	g.TranslateTransform(this.OffsetX, this.OffsetY);
        //        //	g.ScaleTransform(this.ZoomFactor, this.ZoomFactor);
        //        //	g.DrawPath(Pens.Black, Path);
        //        //
        //        //	this.pictureBox1.Image = this.OriginalImage;
        //        //}

        //        void control1_Paint(Object sender, PaintEventArgs e)
        //        {
        //            e.Graphics.Clear(Color.White);
        //            if (this.LinesDrawer == null)
        //            {
        //                return;
        //            }
        //            this.LinesDrawer.Draw(e.Graphics);
        //            this.Control1InvalidatePending = false;
        //        }

        //        void InvalidateControl1()
        //        {
        //            if (!this.Control1InvalidatePending)
        //            {
        //                this.control1.Invalidate();
        //                this.Control1InvalidatePending = true;
        //            }
        //        }

        //        void control1_Resize(Object sender, EventArgs e)
        //        {
        //            this.InvalidateControl1();
        //            this.LinesDrawer.SetBoardSize(new PointF(this.control1.Size));
        //        }

        //        void ZoomOutButton_Click(Object sender, EventArgs e)
        //        {
        //            if (this.LinesDrawer == null)
        //            {
        //                return;
        //            }
        //            this.LinesDrawer.Zoom(2.0 / 3.0);
        //            this.InvalidateControl1();
        //        }

        //        void ZoomInButton_Click(Object sender, EventArgs e)
        //        {
        //            if (this.LinesDrawer == null)
        //            {
        //                return;
        //            }
        //            this.LinesDrawer.Zoom(3.0 / 2.0);
        //            this.InvalidateControl1();
        //        }

        //        void control1_MouseUp(System.Object sender, System.Windows.Forms.MouseEventArgs e)
        //        {
        //            this.DragStartPoint = null;
        //        }

        //        void control1_MouseMove(System.Object sender, System.Windows.Forms.MouseEventArgs e)
        //        {
        //            PointF T;

        //            if (this.LinesDrawer == null)
        //            {
        //                return;
        //            }

        //            if (e.Button != MouseButtons.Left)
        //            {
        //                return;
        //            }

        //            T = new PointF(e.Location);

        //            if (this.DragStartPoint == null)
        //            {
        //                this.DragStartPoint = T;
        //                return;
        //            }

        //            this.LinesDrawer.Pan(T - this.DragStartPoint);

        //            this.DragStartPoint = T;

        //            this.InvalidateControl1();
        //        }

        //        //Bitmap MainForm.makeImage(Int32 bw, Int32 bh)
        //        //{
        //        //	Bitmap bmp;
        //        //	Graphics g;
        //        //
        //        //	bmp = new Bitmap(bw, bh);
        //        //	g = Graphics.FromImage(bmp);
        //        //
        //        //	this.drawImage(g, bw, bh);
        //        //
        //        //	return bmp;
        //        //}
        //        //
        //        //void drawImage(Graphics g, Double bw, Double bh)
        //        //{
        //        //	Double x1, x2, y1, y2, w, h, dw, dh;
        //        //
        //        //	g.Clear(Color.White);
        //        //	
        //        //	if (this.Lines == null || this.Lines.Length == 0)
        //        //		return;
        //        //
        //        //	x1 = this.Lines[0].x1;
        //        //	y1 = this.Lines[0].y1;
        //        //	x2 = x1;
        //        //	y2 = y1;
        //        //
        //        //	for each (Line l in this.Lines)
        //        //	{
        //        //		x1 = Math.Min(l.x1, x1);
        //        //		x1 = Math.Min(l.x2, x1);
        //        //		y1 = Math.Min(l.y1, y1);
        //        //		y1 = Math.Min(l.y2, y1);
        //        //		x2 = Math.Max(l.x1, x2);
        //        //		x2 = Math.Max(l.x2, x2);
        //        //		y2 = Math.Max(l.y1, y2);
        //        //		y2 = Math.Max(l.y2, y2);
        //        //	}
        //        //	
        //        //	w = x2 - x1;
        //        //	h = y2 - y1;
        //        //
        //        //	dw = 20 / bw * w;
        //        //	dh = 20 / bh * h;
        //        //
        //        //	x1 -= dw;
        //        //	y1 -= dh;
        //        //	w += dw * 2;
        //        //	h += dh * 2;
        //        //
        //        //	var R = new Random();
        //        //
        //        //	for each (Line l in this.Lines)
        //        //	{
        //        //		g.DrawLine(new Pen(Color.FromArgb(R.Next(256), R.Next(256), R.Next(256))),
        //        //					(Single)((l.x1 - x1) / w * bw),
        //        //					(Single)((l.y1 - y1) / h * bh),
        //        //					(Single)((l.x2 - x1) / w * bw),
        //        //					(Single)((l.y2 - y1) / h * bh));
        //        //	}
        //        //}
    }
}
