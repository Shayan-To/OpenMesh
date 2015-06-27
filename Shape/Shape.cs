using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace OpenMesh
{

    [Serializable()]
    public abstract class Shape : ShapeBase
    {

        public abstract Rectangle OccupyingRectangle { get; }
        //public abstract void Measure();
        public abstract void Measure(Rectangle Bounds, Single Scale);
        public abstract void Draw(Graphics G, SubarrayProxy<PointF> Points, Single Scale);
        public abstract Int32 TransformPointsCount { get; }
        public abstract PointF GetTransformPoints(Int32 Index);

        public Shape()
        {
        }

        #region Serialization Logic
        protected Shape(SerializationInfo Info, StreamingContext Context)
            : base(Info, Context)
        {
            Utils.Deserializing();
            this._IsSelected = Info.GetBoolean("IsSelected");
            this._IsVisible = Info.GetBoolean("IsVisible");
        }

        protected override void GetObjectData(SerializationInfo Info, StreamingContext Context)
        {
            Utils.Serializing();
            base.GetObjectData(Info, Context);

            Info.AddValue("IsSelected", this._IsSelected);
            Info.AddValue("IsVisible", this._IsVisible);
        }
        #endregion

        #region IsSelected Property
        private Boolean _IsSelected;

        public sealed override Boolean IsSelected
        {
            get
            {
                return this._IsSelected;
            }
            set
            {
                this.SetSelected(value);
                this.NotifyPropertyChanged("IsSelected");
            }
        }

        public override void SetSelected(bool value)
        {
            this._IsSelected = value;
        }
        #endregion

        #region IsVisible Property
        private Boolean _IsVisible = true;

        [EditableProperty()]
        public sealed override Boolean IsVisible
        {
            get
            {
                return this._IsVisible;
            }
            set
            {
                this.SetVisible(value);
            }
        }

        public sealed override void SetVisible(bool value)
        {
            if (this._IsVisible == value)
                return;

            this._IsVisible = value;
            if (value)
            {
                if (this.Parent != null)
                {
                    this.Parent.SetVisible(value);
                }
            }
            else
            {
                var P = this.Parent as ShapeCollection;
                if (P != null)
                {
                    P.CheckChildrenVisibility();
                }
            }
            this.NotifyPropertyChanged("IsVisible");
        }
        #endregion

        //public sealed override int ShapesCount
        //{
        //    get
        //    {
        //        return 1;
        //    }
        //}

        //public sealed override ShapeBase GetShape(int Index)
        //{
        //    if (Index != 0)
        //        throw new IndexOutOfRangeException();
        //    return this;
        //}

    }

}
