using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenMesh
{

    public class ShapeDrawingPane : Control
    {

        public ShapeDrawingPane()
        {
            this._ShapeDrawer = new ShapeDrawer();
            this.ResizeRedraw = true;
            this._ShapeDrawer.Changed += this.OnShapeDrawerChanged;
            this.IsInteractive = true;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            this._ShapeDrawer.BoardBounds = Rectangle.FromCornerSize(new PointF(), this.Size.ToPointF());
            this._ShapeDrawer.ResetView();
            base.OnHandleCreated(e);
        }

        protected override void OnResize(EventArgs e)
        {
            this._ShapeDrawer.BoardBounds = Rectangle.FromCornerSize(new PointF(), this.Size.ToPointF());
            base.OnResize(e);
        }

        private void OnShapeDrawerChanged(Object Sender, EventArgs E)
        {
            this.Invalidate();
        }

        private PointF PanningOrigin;

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!this.IsInteractive)
            {
                base.OnMouseDown(e);
                return;
            }

            if (e.Button == System.Windows.Forms.MouseButtons.Left)
                this.PanningOrigin = e.Location;
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            this.DoMouseEvent(e);
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.DoMouseEvent(e);
            base.OnMouseUp(e);
        }

        private void DoMouseEvent(MouseEventArgs e)
        {
            if (!this.IsInteractive)
                return;

            PointF V = e.Location, D = this.PanningOrigin.Subtract(V);
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this._ShapeDrawer.PanView(D);
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Middle)
            {
                this._ShapeDrawer.RealScale *= (Single)Math.Pow(1.5, D.Y / 10);
            }
            this.PanningOrigin = V;
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (!this.IsInteractive)
            {
                base.OnMouseWheel(e);
                return;
            }

            this._ShapeDrawer.RealScale *= (Single)Math.Pow(1.5, e.Delta / (Double)SystemInformation.MouseWheelScrollDelta);
            base.OnMouseWheel(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            e.Graphics.Clear(this.BackColor);
            this._ShapeDrawer.Draw(e.Graphics);
            e.Graphics.DrawRectangle(new Pen(this.ForeColor, 1), new System.Drawing.Rectangle(new Point(), this.Size - new Size(1, 1)));
        }

        public Boolean IsInteractive { get; set; }

        #region ShapeDrawer Property
        private readonly ShapeDrawer _ShapeDrawer;

        public ShapeDrawer ShapeDrawer
        {
            get
            {
                return this._ShapeDrawer;
            }
        }
        #endregion

    }

}
