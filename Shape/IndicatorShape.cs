using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;

namespace OpenMesh
{

    [Serializable()]
    public class IndicatorShape : PenShape
    {

        public IndicatorShape(Shape Shape)
        {
            this.Shape = Shape;
            this.IndicatorsSize = 5;
            this.IndicatorsShape = ShapeOfIndicator.Square;
        }

        public override void Measure(Rectangle Bounds, float Scale)
        {
            //Console.WriteLine(Bounds);
            this.TransformPoints.Clear();

            this.Shape.Measure(Bounds, Scale);
            for (int i = 0; i < this.Shape.TransformPointsCount; i++)
            {
                this.TransformPoints.Add(this.Shape.GetTransformPoints(i));
            }
        }

        private readonly List<PointF> TransformPoints = new List<PointF>();

        public override int TransformPointsCount
        {
            get
            {
                return this.TransformPoints.Count;
            }
        }

        public override PointF GetTransformPoints(int Index)
        {
            return this.TransformPoints[Index];
        }

        public override void Draw(Graphics G, SubarrayProxy<PointF> Points, float Scale)
        {
            var Path = new GraphicsPath();

            var Sz = this.IndicatorsSize;
            var Sz2 = Sz * 2;

            switch (this.IndicatorsShape)
            {
                case ShapeOfIndicator.Square:
                    for (int i = 0; i < Points.Count; i++)
                    {
                        var T = Points[i];
                        Path.AddRectangle(new RectangleF((Single)T.X - Sz, (Single)T.Y - Sz, Sz2, Sz2));
                    }
                    break;
                case ShapeOfIndicator.Circle:
                    for (int i = 0; i < Points.Count; i++)
                    {
                        var T = Points[i];
                        Path.AddEllipse(new RectangleF((Single)T.X - Sz, (Single)T.Y - Sz, Sz2, Sz2));
                    }
                    break;
                case ShapeOfIndicator.SquareRotated:
                    var D = Sz * (Single)Math.Sqrt(2);
                    for (int i = 0; i < Points.Count; i++)
                    {
                        var T = Points[i];
                        Path.StartFigure();
                        Path.AddPolygon(new[] { new PointF(T.X - D, T.Y), new PointF(T.X, T.Y - D), new PointF(T.X + D, T.Y), new PointF(T.X, T.Y + D) });
                        Path.CloseFigure();
                    }
                    break;
                case ShapeOfIndicator.Triangle:
                    var H = 2 * Sz * (Single)Math.Cos(Math.PI / 6);
                    var C = Sz * (Single)Math.Tan(Math.PI / 6);
                    for (int i = 0; i < Points.Count; i++)
                    {
                        var T = Points[i];
                        var Y = T.Y + C;
                        Path.StartFigure();
                        Path.AddPolygon(new[] { new PointF(T.X, Y - H), new PointF(T.X - Sz, Y), new PointF(T.X + Sz, Y) });
                        Path.CloseFigure();
                    }
                    break;
                default:
                    break;
            }

            G.DrawPath(this.Pen, Path);
        }

        #region Serialization Logic
        protected IndicatorShape(SerializationInfo Info, StreamingContext Context)
            : base(Info, Context)
        {
            Utils.Deserializing();
            this.Shape = (Shape)Info.GetValueWithType("Shape");
            this._IndicatorsShape = (ShapeOfIndicator)Info.GetInt32("IndicatorsShape");
            this._IndicatorsSize = Info.GetInt32("IndicatorsSize");
        }

        protected override void GetObjectData(SerializationInfo Info, StreamingContext Context)
        {
            Utils.Serializing();
            base.GetObjectData(Info, Context);

            Info.AddValueWithType("Shape", this.Shape);
            Info.AddValue("IndicatorsShape", (Int32)this._IndicatorsShape);
            Info.AddValue("IndicatorsSize", this._IndicatorsSize);
        }
        #endregion

        #region IndicatorsSize Property
        private Int32 _IndicatorsSize;

        [EditableIntegerTrackBarProperty(MinValue = 1, MaxValue = 15)]
        public Int32 IndicatorsSize
        {
            get
            {
                return this._IndicatorsSize;
            }
            set
            {
                this._IndicatorsSize = value;
                this.NotifyPropertyChanged("IndicatorsSize");
            }
        }
        #endregion

        #region IndicatorsShape Property
        private ShapeOfIndicator _IndicatorsShape;

        [EditableProperty()]
        public ShapeOfIndicator IndicatorsShape
        {
            get
            {
                return this._IndicatorsShape;
            }
            set
            {
                this._IndicatorsShape = value;
                this.NotifyPropertyChanged("IndicatorsShape");
            }
        }
        #endregion

        #region OccupyingRectangle Property
        public override Rectangle OccupyingRectangle
        {
            get
            {
                return new Rectangle();
            }
        }
        #endregion

        private readonly Shape Shape;

        public enum ShapeOfIndicator
        {

            Square,
            SquareRotated,
            Circle,
            Triangle

        }

    }

}
