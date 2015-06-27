using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Runtime.Serialization;

namespace OpenMesh
{

    [Serializable()]
    public class LinesShape : PenShape
    {

        public LinesShape(List<Line> Lines)
        {
            this._Lines = Lines;
            this._OccupyingRectangle = Rectangle.BoundingRectangle(Lines.Select(L => L.BoundingRectangle()));
        }

        public LinesShape(params Line[] Lines)
            : this(new List<Line>(Lines))
        {
        }

        public override void Measure(Rectangle Bounds, float Scale)
        {
            this.TransformLines.Clear();
            foreach (var L in this.Lines)
            {
                if (Bounds.IsInside(L.P1) || Bounds.IsInside(L.P2))
                {
                    this.TransformLines.Add(L);
                }
            }
        }

        private readonly List<Line> TransformLines = new List<Line>();

        public override int TransformPointsCount
        {
            get
            {
                return this.TransformLines.Count * 2;
            }
        }

        public override PointF GetTransformPoints(int Index)
        {
            var L = this.TransformLines[Index / 2];

            if (Index % 2 == 0)
                return L.P1;
            return L.P2;
        }

        public override void Draw(Graphics G, SubarrayProxy<PointF> Points, float Scale)
        {
            PointF T = new PointF();
            var Bl = false;
            var Path = new GraphicsPath();

            for (int i = 0; i < Points.Count; i++)
            {
                if (Bl)
                {
                    Path.StartFigure();
                    Path.AddLine(Points[i], T);
                    Path.CloseFigure();
                }
                else
                    T = Points[i];
                Bl = !Bl;
            }

            G.DrawPath(this.Pen, Path);
        }

        #region Serialization Logic
        protected LinesShape(SerializationInfo Info, StreamingContext Context)
            : base(Info, Context)
        {
            Utils.Deserializing();
            var N = Info.GetInt32("Lines.Count");
            this._Lines = new List<Line>(N);
            for (int i = 0; i < N; i++)
            {
                this._Lines.Add((Line)Info.GetValue("Lines[" + i + "]", typeof(Line)));
            }
            this._OccupyingRectangle = Rectangle.BoundingRectangle(Lines.Select(L => L.BoundingRectangle()));
        }

        protected override void GetObjectData(SerializationInfo Info, StreamingContext Context)
        {
            Utils.Serializing();
            base.GetObjectData(Info, Context);

            Info.AddValue("Lines.Count", this._Lines.Count);
            for (int i = 0; i < this._Lines.Count; i++)
            {
                Info.AddValue("Lines[" + i + "]", this._Lines[i], typeof(Line));
            }
        }
        #endregion

        #region OccupyingRectangle Property
        private readonly Rectangle _OccupyingRectangle;

        public override Rectangle OccupyingRectangle
        {
            get
            {
                return this._OccupyingRectangle;
            }
        }
        #endregion

        #region Lines Property
        private readonly List<Line> _Lines;

        public List<Line> Lines
        {
            get
            {
                return this._Lines;
            }
        }
        #endregion

    }
}
