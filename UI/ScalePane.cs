using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace OpenMesh
{
    public class ScalePane : Control
    {

        public ScalePane()
        {
            this.ForeBrush = new SolidBrush(this.ForeColor);
            this.ForePen = new Pen(this.ForeBrush, 1);
        }

        private void OnShapeDrawerChanged(Object Sender, EventArgs E)
        {
            this.CalculateScale();
            this.Invalidate();
        }

        private void CalculateScale()
        {
            if (this._ShapeDrawer == null || !this._ShapeDrawer.IsWorking)
            {
                return;
            }

            var Width = this.Width - 10;

            this.ScaleUnit = (Single)this._ShapeDrawer.Scale;

            this.ScalePower = 0;
            while (this.ScaleUnit < Width)
            {
                this.ScalePower += 1;
                this.ScaleUnit *= 10;
            }
            while (this.ScaleUnit > Width)
            {
                this.ScalePower -= 1;
                this.ScaleUnit /= 10;
            }

            //Console.Write("this.ScalePower");
            //Console.WriteLine(this.ScalePower);
            //Console.Write("this.ScaleUnit");
            //Console.WriteLine(this.ScaleUnit);

            if (this.ScaleUnit * 5 < Width)
            {
                this.ScaleCoeff = 5;
            }
            else if (this.ScaleUnit * 2 < Width)
            {
                this.ScaleCoeff = 2;
            }
            else
            {
                this.ScaleCoeff = 1;
            }

            //Console.Write("this.ScaleCoeff");
            //Console.WriteLine(this.ScaleCoeff);

            this.ScaleMaxCoeff = this.ScaleCoeff;
            while (this.ScaleUnit * (this.ScaleMaxCoeff + 1) < Width)
            {
                this.ScaleMaxCoeff += 1;
            }

            //Console.Write("this.ScaleMaxCoeff");
            //Console.WriteLine(this.ScaleMaxCoeff);
        }

        private static String[] PosPowers = { "da", "h", "k", "M", "G", "T", "P", "E", "Z", "Y" };
        private static String[] NegPowers = { "d", "c", "m", "μ", "n", "p", "f", "a", "z", "y" };

        private String GetText()
        {
            var Power = this.ScalePower;
            var Powers = PosPowers;
            var Prefix = "";

            if (Power < 0)
            {
                Power = -Power;
                Powers = NegPowers;
            }

            if (Power == 1)
            {
                Prefix = Powers[0];
                Power = 0;
            }
            else if (Power == 2)
            {
                Prefix = Powers[1];
                Power = 0;
            }
            else if (Power != 0)
            {
                if (this.ScalePower < 0 && Power % 3 != 0)
                {
                    Power += 3;
                }
                Prefix = Powers[Power / 3 + 1];
                Power = Power % 3;
                if (this.ScalePower < 0 && Power != 0)
                {
                    Power = 3 - Power;
                }
            }

            var Num = this.ScaleCoeff;
            if (Power != 0)
            {
                Num *= 10;
                Power -= 1;
            }
            if (Power != 0)
            {
                Num *= 10;
                Power -= 1;
            }

            return String.Concat(Num, Prefix, "m");
        }

        private Pen ForePen;
        private Brush ForeBrush;

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;

                this.ForePen.Dispose();
                this.ForeBrush.Dispose();
                this.ForeBrush = new SolidBrush(value);
                this.ForePen = new Pen(this.ForeBrush, 1);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);

            if (this._ShapeDrawer == null || !this._ShapeDrawer.IsWorking)
            {
                return;
            }

            var Height = (this.Height - 15) / 4.0f;
            var H = 10 + 3 * Height;

            e.Graphics.DrawRectangle(this.ForePen, 5, H, this.ScaleMaxCoeff * this.ScaleUnit, Height);

            for (int i = 0; i < this.ScaleMaxCoeff; i += 2)
            {
                e.Graphics.FillRectangle(this.ForeBrush, 5 + i * this.ScaleUnit, H, this.ScaleUnit, Height);
            }

            if (this.ScaleMaxCoeff <= 2)
            {
                var T = this.ScaleUnit / 5;
                for (int i = 0; i <= this.ScaleMaxCoeff * 5; i++)
                {
                    if (i % 5 == 0)
                    {
                        e.Graphics.DrawLine(this.ForePen, 5 + i * T, H, 5 + i * T, H - 3);
                    }
                    else
                        e.Graphics.DrawLine(this.ForePen, 5 + i * T, H, 5 + i * T, H - 2);
                }
            }
            else
            {
                e.Graphics.DrawLine(this.ForePen, 5 + this.ScaleCoeff * this.ScaleUnit, H,
                                                  5 + this.ScaleCoeff * this.ScaleUnit, H - 3);
                e.Graphics.DrawLine(this.ForePen, 5, H,
                                                  5, H - 3);
            }

            var Text = this.GetText();
            var TextSize = e.Graphics.MeasureString(Text, this.Font);

            e.Graphics.DrawString(Text, this.Font, this.ForeBrush, this.ScaleCoeff * this.ScaleUnit - TextSize.Width / 2, H - TextSize.Height - 5);
        }

        #region ShapeDrawer Property
        private ShapeDrawer _ShapeDrawer = null;

        public ShapeDrawer ShapeDrawer
        {
            get
            {
                return this._ShapeDrawer;
            }
            set
            {
                if (this._ShapeDrawer != null)
                    throw new NotSupportedException();
                this._ShapeDrawer = value;
                if (value != null)
                {
                    value.Changed += new EventHandler(this.OnShapeDrawerChanged);
                }
            }
        }
        #endregion

        private int ScaleCoeff, ScaleMaxCoeff, ScalePower;
        private Single ScaleUnit;

    }
}
