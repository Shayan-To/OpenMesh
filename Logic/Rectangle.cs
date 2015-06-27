using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenMesh
{

    public struct Rectangle
    {

        private Rectangle(Line Line)
        {
            this.Line = Line;
        }

        public Rectangle(RectangleF Rect)
        {
            Single X, Y;

            X = Rect.Top;
            Y = Rect.Left;

            this.Line = new Line(X, Y, X + Rect.Width, Y + Rect.Height);
        }

        public Rectangle(System.Drawing.Rectangle Rect)
        {
            Single X, Y;

            X = Rect.Top;
            Y = Rect.Left;

            this.Line = new Line(X, Y, X + Rect.Width, Y + Rect.Height);
        }

        public static Rectangle FromCorners(PointF P1, PointF P2)
        {
            return FromCorners(P1.X, P1.Y, P2.X, P2.Y);
        }

        public static Rectangle FromCorners(Single X1, Single Y1, Single X2, Single Y2)
        {
            Single c;

            if (X1 > X2)
            {
                c = X1;
                X1 = X2;
                X2 = c;
            }
            if (Y1 > Y2)
            {
                c = Y1;
                Y1 = Y2;
                Y2 = c;
            }

            return new Rectangle(new Line(X1, Y1, X2, Y2));
        }

        public static Rectangle FromCornerSize(PointF TopLeftCorner, PointF Size)
        {
            return new Rectangle(new Line(TopLeftCorner, TopLeftCorner.Add(Size)));
        }

        public static Rectangle FromCenterSize(PointF Center, PointF Size)
        {
            Size = Size.Divide(2);
            return new Rectangle(new Line(Center.Subtract(Size), Center.Add(Size)));
        }

        public static Rectangle BoundingRectangle(params Rectangle[] Rectangles)
        {
            return BoundingRectangle(Rectangles);
        }

        public static Rectangle BoundingRectangle(IEnumerable<Rectangle> Rectangles)
        {
            Single MinX, MinY, MaxX, MaxY;
            Boolean bl;

            MinX = 0;
            MinY = 0;
            MaxX = 0;
            MaxY = 0;
            bl = true;

            foreach (Rectangle R in Rectangles)
            {
                if (R.Size == new PointF())
                {
                    continue;
                }
                if (bl)
                {
                    MinX = R.Line.P1.X;
                    MinY = R.Line.P1.Y;
                    MaxX = MinX;
                    MaxY = MinY;

                    bl = false;
                }
                if (R.Line.P1.X < R.Line.P2.X)
                {
                    MinX = Math.Min(R.Line.P1.X, MinX);
                    MaxX = Math.Max(R.Line.P2.X, MaxX);
                }
                else
                {
                    MinX = Math.Min(R.Line.P2.X, MinX);
                    MaxX = Math.Max(R.Line.P1.X, MaxX);
                }
                if (R.Line.P1.Y < R.Line.P2.Y)
                {
                    MinY = Math.Min(R.Line.P1.Y, MinY);
                    MaxY = Math.Max(R.Line.P2.Y, MaxY);
                }
                else
                {
                    MinY = Math.Min(R.Line.P2.Y, MinY);
                    MaxY = Math.Max(R.Line.P1.Y, MaxY);
                }
            }

            if (bl)
                return new Rectangle();
            return Rectangle.FromCorners(MinX, MinY, MaxX, MaxY);
        }

        public Rectangle InnerBoundingSquare()
        {
            PointF Size, Corner;

            Size = this.Size;

            if (Size.X == Size.Y)
                return this;

            Corner = this.TopLeftCorner;

            if (Size.X < Size.Y)
            {
                Corner.Y += (Size.Y - Size.X) / 2;
                Size = new PointF(Size.X, Size.X);
            }
            else
            {
                Corner.X += (Size.X - Size.Y) / 2;
                Size = new PointF(Size.Y, Size.Y);
            }

            return Rectangle.FromCornerSize(Corner, Size);
        }

        public Rectangle OuterBoundingSquare()
        {
            PointF Size, Corner;

            Size = this.Size;

            if (Size.X == Size.Y)
                return this;

            Corner = this.TopLeftCorner;

            if (Size.X > Size.Y)
            {
                Corner.Y += (Size.Y - Size.X) / 2;
                Size = new PointF(Size.X, Size.X);
            }
            else
            {
                Corner.X += (Size.X - Size.Y) / 2;
                Size = new PointF(Size.Y, Size.Y);
            }

            return Rectangle.FromCornerSize(Corner, Size);
        }

        public PointF[] ToThreePoints()
        {
            return new PointF[] { this.TopLeftCorner,
                                  this.TopRightCorner,
                                  this.BottomLeftCorner };
        }

        public RectangleF ToRectangleF()
        {
            return new RectangleF(this.TopLeftCorner, this.Size.ToSizeF());
        }

        public Boolean IsInside(PointF P)
        {
            return this.Line.P1.X < P.X & P.X < this.Line.P2.X &
                   this.Line.P1.Y < P.Y & P.Y < this.Line.P2.Y;
        }

        public static Rectangle operator +(Rectangle A, PointF B)
        {
            return new Rectangle(A.Line + B);
        }

        public static Rectangle operator -(Rectangle A, PointF B)
        {
            return new Rectangle(A.Line - B);
        }

        public override string ToString()
        {
            return String.Concat("Rectangle{(", this.Line.P1.X, ", ", this.Line.P1.Y, ")-(",
                                 this.Line.P2.X, ", ", this.Line.P2.Y, ")}");
        }

        public PointF TopLeftCorner
        {
            get
            {
                return this.Line.P1;
            }
        }

        public PointF BottomRightCorner
        {
            get
            {
                return this.Line.P2;
            }
        }

        public PointF TopRightCorner
        {
            get
            {
                return this.Line.P1.Add(new PointF(this.Size.X, 0));
            }
        }

        public PointF BottomLeftCorner
        {
            get
            {
                return this.Line.P1.Add(new PointF(0, this.Size.Y));
            }
        }

        public PointF Center
        {
            get
            {
                return (this.Line.P1.Add(this.Line.P2)).Divide(2);
            }
        }

        public PointF Size
        {
            get
            {
                return this.Line.P2.Subtract(this.Line.P1);
            }
        }

        private readonly Line Line;

    }

}
