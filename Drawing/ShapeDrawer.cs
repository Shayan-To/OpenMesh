using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;

namespace OpenMesh
{

    public class ShapeDrawer
    {

        public ShapeDrawer()
        {
            this.TransformMatrix = new Matrix();
            this.InverseTransformMatrix = new Matrix();
            this._IsSuspended = false;

            this.ShapeCollection_CollectionChangedEventHandler = new EventHandler<CollectionChangedEventArgs<ShapeBase>>(this.ShapeCollection_CollectionChanged);
            this.Shape_PropertyChangedEventHandler = new PropertyChangedEventHandler(this.Shape_PropertyChanged);
        }

        #region EventHandlers
        private readonly EventHandler<CollectionChangedEventArgs<ShapeBase>> ShapeCollection_CollectionChangedEventHandler;
        private void ShapeCollection_CollectionChanged(Object Sender, CollectionChangedEventArgs<ShapeBase> E)
        {
            //Console.WriteLine("*ShapeDrawer.CollectionChanged");
            foreach (var i in E.RemovedOnes)
            {
                ShapeWalker.Instance.RemoveHandler(i.Item, this.ShapeCollection_CollectionChangedEventHandler, this.Shape_PropertyChangedEventHandler);
            }
            foreach (var i in E.AddedOnes)
            {
                ShapeWalker.Instance.AddHandler(i.Item, this.ShapeCollection_CollectionChangedEventHandler, this.Shape_PropertyChangedEventHandler);
            }

            this.Shape_Changed();
        }

        private static readonly String[] NonEffectiveProperties = { "Name", "Parent" };

        private readonly PropertyChangedEventHandler Shape_PropertyChangedEventHandler;
        private void Shape_PropertyChanged(Object Sender, PropertyChangedEventArgs E)
        {
            //Console.WriteLine("*ShapeDrawer.PropertyChanged");
            if (!NonEffectiveProperties.Contains(E.PropertyName))
                this.OnChanged();
        }

        private void Shape_Changed()
        {
            this.ConstructBounds();
            this.OnChanged();
        }
        #endregion

        private void ConstructBounds()
        {
            if (this._IsSuspended)
                return;

            var Rects = new List<Rectangle>();
            ShapeWalker.Instance.TypedWalk<Shape>(this._Shape, S => Rects.Add(S.OccupyingRectangle));
            this.ShapeBounds = Rectangle.BoundingRectangle(Rects);
            this.ShapeBoundsSquare = this.ShapeBounds.OuterBoundingSquare();

            this.ConstructMatrix();
        }

        private void ConstructMatrix()
        {
            if (this._IsSuspended)
                return;

            var S = this.VisibleShapeBounds.Size;
            if (S.X == 0 || S.Y == 0)
            {
                this.TransformMatrix.Reset();
                this.InverseTransformMatrix.Reset();
                return;
            }

            var M = new Matrix(this.VisibleShapeBounds.ToRectangleF(),
                               this.BoardBoundsSquare.ToThreePoints());
            this.TransformMatrix.Reset();
            this.TransformMatrix.Multiply(M);
            this.InverseTransformMatrix.Reset();
            this.InverseTransformMatrix.Multiply(M);
            this.InverseTransformMatrix.Invert();
        }

        #region View Logic
        public void ResetView()
        {
            var D = this.BoardBoundsSquare.Size.X / 96 * 0.25f;
            var S = new PointF(D, D);
            this.VisibleShapeBounds = Rectangle.FromCenterSize(new PointF(), S);

            this.ConstructMatrix();
            this.OnChanged();
        }

        public void FillView()
        {
            this.VisibleShapeBounds = this.ShapeBoundsSquare;

            this.ConstructMatrix();
            this.OnChanged();
        }

        public void PanView(PointF Amount)
        {
            this.VisibleShapeBounds += Utils.MatrixByVector(this.InverseTransformMatrix, Amount);

            this.ConstructMatrix();
            this.OnChanged();
        }
        #endregion

        public void Suspend()
        {
            this._IsSuspended = true;
        }

        public void Resume()
        {
            this._IsSuspended = false;

            this.ConstructBounds();
            this.OnChanged();
        }

        public void Refresh()
        {
            this.Shape_Changed();
        }

        private readonly List<Shape> ShapesCollection = new List<Shape>();

        public void Draw(Graphics G)
        {
            //Console.WriteLine("Draw started.");
            if (this._Shape == null)
                return;

            //Console.WriteLine("Drawing...");

            var Scale = this.Scale;
            var Bounds = Rectangle.FromCenterSize(this.VisibleShapeBounds.Center, this.BoardBounds.Size.Divide(this.Scale));
            var Count = 0;
            this.ShapesCollection.Clear();

            ShapeWalker.Instance.TypedWalk<Shape>(this._Shape,
                S =>
                {
                    //Console.WriteLine(Environment.StackTrace);
                    if (!S.IsVisible)
                        return;

                    S.Measure(Bounds, Scale);
                    Count += S.TransformPointsCount;
                    this.ShapesCollection.Add(S);
                });

            var Points = new PointF[Count];
            var I = 0;

            if (Count != 0)
            {
                foreach (var S in this.ShapesCollection)
                {
                    for (int i = 0; i < S.TransformPointsCount; i++)
                    {
                        Points[I++] = S.GetTransformPoints(i);
                    }
                }

                this.TransformMatrix.TransformPoints(Points);
            }

            I = 0;
            foreach (var S in this.ShapesCollection)
            {
                S.Draw(G, new SubarrayProxy<PointF>(Points, I, I += S.TransformPointsCount), Scale);
            }
        }

        #region Changed Event
        internal event EventHandler Changed;

        private void OnChanged()
        {
            //Console.WriteLine("*ShapeDrawer.Changed--" + Utils.CompactStackTrace(4));
            if (this._IsSuspended | !this.IsWorking)
                return;

            if (this.Changed != null)
                this.Changed(this, EventArgs.Empty);
        }
        #endregion

        #region IsWorking Property
        public Boolean IsWorking
        {
            get
            {
                if (this._Shape == null)
                    return false;

                var T = this.BoardBoundsSquare.Size;
                //Console.WriteLine(T);
                if (T.X <= 0 || Math.Abs(T.Y - T.X) > 1e-5)
                    return false;

                T = this.VisibleShapeBounds.Size;
                //Console.WriteLine(T);
                if (T.X <= 0 || Math.Abs(T.Y - T.X) > 1e-5)
                    return false;

                return true;
            }
        }
        #endregion

        #region Scale Properties
        public Single Scale
        {
            get
            {
                return this.BoardBoundsSquare.Size.X / this.VisibleShapeBounds.Size.X;
            }
            private set
            {
                value = this.BoardBoundsSquare.Size.X / value;
                var S = new PointF(value, value);
                this.VisibleShapeBounds = Rectangle.FromCenterSize(this.VisibleShapeBounds.Center, S);

                this.ConstructMatrix();
                this.OnChanged();
            }
        }

        public Single RealScale
        {
            get
            {
                return this.Scale / 96 * 0.25f;
            }
            set
            {
                if (value < MinZoomFactor)
                    value = MinZoomFactor;
                if (value > MaxZoomFactor)
                    value = MaxZoomFactor;
                if (Single.IsNaN(value))
                    value = 1;

                this.Scale = value * 96 / 0.25f;
            }
        }
        #endregion

        #region Shape Property
        private ShapeBase _Shape;

        public ShapeBase Shape
        {
            get
            {
                return this._Shape;
            }
            set
            {
                if (this._Shape != null)
                    ShapeWalker.Instance.RemoveHandler(this._Shape, this.ShapeCollection_CollectionChangedEventHandler, this.Shape_PropertyChangedEventHandler);
                this._Shape = value;
                if (this._Shape != null)
                    ShapeWalker.Instance.AddHandler(this._Shape, this.ShapeCollection_CollectionChangedEventHandler, this.Shape_PropertyChangedEventHandler);

                this.Shape_Changed();
            }
        }
        #endregion

        #region BoardBounds Property
        private Rectangle _BoardBounds;

        public Rectangle BoardBounds
        {
            get
            {
                return this._BoardBounds;
            }
            set
            {
                this._BoardBounds = value;
                this.BoardBoundsSquare = value.InnerBoundingSquare();

                this.ConstructMatrix();
                this.OnChanged();
            }
        }
        #endregion

        #region IsSuspended Property
        private Boolean _IsSuspended;

        public Boolean IsSuspended
        {
            get
            {
                return this._IsSuspended;
            }
        }
        #endregion

        //private Rectangle VisibleShapeBounds
        //{
        //    get
        //    {
        //        return this._VisibleShapeBounds;
        //    }
        //    set
        //    {
        //        Console.Write("{0} {1} ", this.Scale, this.RealScale);
        //        this._VisibleShapeBounds = value;
        //        Console.Write("{0} {1} ", this.RealScale, value);
        //        Console.WriteLine(Utils.CompactStackTrace(5));
        //    }
        //}

        private Rectangle VisibleShapeBounds, ShapeBounds, ShapeBoundsSquare, BoardBoundsSquare;
        private readonly Matrix TransformMatrix, InverseTransformMatrix;

        private const Single MinZoomFactor = 1e-4f;
        private const Single MaxZoomFactor = 1e+4f;

    }

}
