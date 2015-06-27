using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.Serialization;

namespace OpenMesh
{

    [Serializable()]
    public struct Line : ISerializable
    {

        public Line(Single X1, Single Y1, Single X2, Single Y2)
        {
            this._P1 = new PointF(X1, Y1);
            this._P2 = new PointF(X2, Y2);
        }

        public Line(PointF P1, PointF P2)
        {
            this._P1 = P1;
            this._P2 = P2;
        }

        public static Line operator +(Line A, PointF B)
        {
            return new Line(A.P1.Add(B), A.P2.Add(B));
        }

        public static Line operator -(Line A, PointF B)
        {
            return new Line(A.P1.Subtract(B), A.P2.Subtract(B));
        }

        public Rectangle BoundingRectangle()
        {
            return Rectangle.FromCorners(this._P1, this._P2);
        }

        public override string ToString()
        {
            return String.Concat("Line{(", this._P1.X, ", ", this._P1.Y, ")-(", this._P2.X, ", ", this._P2.Y, ")}");
        }

        #region Serialization Logic
        private Line(SerializationInfo Info, StreamingContext Context)
        {
            //Utils.Deserializing();
            this._P1 = new PointF(Info.GetSingle("P1.X"), Info.GetSingle("P1.Y"));
            this._P2 = new PointF(Info.GetSingle("P2.X"), Info.GetSingle("P2.Y"));
        }

        void ISerializable.GetObjectData(SerializationInfo Info, StreamingContext Context)
        {
            //Utils.Serializing();
            Info.AddValue("P1.X", this._P1.X);
            Info.AddValue("P1.Y", this._P1.Y);
            Info.AddValue("P2.X", this._P2.X);
            Info.AddValue("P2.Y", this._P2.Y);
        }
        #endregion

        #region P1 Property
        private readonly PointF _P1;
        public PointF P1
        {
            get
            {
                return this._P1;
            }
        }
        #endregion

        #region P2 Property
        private readonly PointF _P2;
        public PointF P2
        {
            get
            {
                return this._P2;
            }
        }
        #endregion

    };

}
