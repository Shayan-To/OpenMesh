using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace OpenMesh
{

    [Serializable()]
    public abstract class PenShape : Shape
    {

        public PenShape()
        {
            this._Pen = new Pen(Color.Black, 1.0f);
            this._ThickPen = new Pen(Color.Black, 2.0f);
            this.Color = Color.Black;
        }
        
        #region Serialization Logic
        protected PenShape(SerializationInfo Info, StreamingContext Context)
            : base(Info, Context)
        {
            Utils.Deserializing();
            this._Pen = new Pen(Color.Black, 1.0f);
            this._ThickPen = new Pen(Color.Black, 2.0f);
            this.Color = Info.GetColor("Color");
        }

        protected override void GetObjectData(SerializationInfo Info, StreamingContext Context)
        {
            Utils.Serializing();
            base.GetObjectData(Info, Context);

            Info.AddColor("Color", this.Color);
        }
        #endregion

        #region Pen Property
        private Pen _Pen, _ThickPen;

        public Pen Pen
        {
            get
            {
                if (this.IsSelected)
                {
                    return this._ThickPen;
                }
                return this._Pen;
            }
            //set
            //{
            //    if (value == null)
            //        throw new ArgumentNullException();
            //    this._Pen = value;
            //    this._ThickPen = new Pen(value.Brush, 2.0f);
            //    this.NotifyPropertyChanged("Pen");
            //}
        }
        #endregion

        #region Color Property
        [EditableProperty()]
        public Color Color
        {
            get
            {
                return this._Pen.Color;
            }
            set
            {
                //this.Pen = new Pen(new SolidBrush(value));
                this._Pen.Color = value;
                this._ThickPen.Color = value;
                this.NotifyPropertyChanged("Color");
                this.NotifyPropertyChanged("Pen");
            }
        }
        #endregion

    }
}
