using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Reflection;

namespace OpenMesh
{

    [Serializable()]
    public class ShapeCollection : ShapeBase
    {

        public ShapeCollection()
        {
            this._Shapes.ItemCameIn += this.Collection_ItemCameIn;
            this._Shapes.ItemWentOut += this.Collection_ItemWentOut;
        }

        //public sealed override int ShapesCount
        //{
        //    get
        //    {
        //        return this._Shapes.Count;
        //    }
        //}

        //public sealed override ShapeBase GetShape(int Index)
        //{
        //    return this._Shapes[Index];
        //}

        #region EventHandlers Logic
        private void Collection_ItemCameIn(Object Sender, ItemTransferedEventArgs<ShapeBase> E)
        {
            //if (E.Item.Parent != null)
            //    throw new NotSupportedException("Multiple parents not supported.");
            E.Item.SetParent(this);
        }

        private void Collection_ItemWentOut(Object Sender, ItemTransferedEventArgs<ShapeBase> E)
        {
            //Debug.Assert(E.Item.Parent != null, "Non-null parent assertion failed.");
            if (Object.ReferenceEquals(E.Item.Parent, this))
                E.Item.SetParent(null);
        }
        #endregion

        #region Serialization Logic
        protected ShapeCollection(SerializationInfo Info, StreamingContext Context)
            : base(Info, Context)
        {
            Utils.Deserializing();
            this._IsSelected = Info.GetBoolean("IsSelected");
            this._IsVisible = Info.GetBoolean("IsVisible");

            var N = Info.GetInt32("Shapes.Count");
            for (int i = 0; i < N; i++)
            {
                this._Shapes.Add((ShapeBase)Info.GetValueWithType("Shapes[" + i + "]"));
            }
        }

        protected override void GetObjectData(SerializationInfo Info, StreamingContext Context)
        {
            Utils.Serializing();
            base.GetObjectData(Info, Context);

            Info.AddValue("IsSelected", this._IsSelected);
            Info.AddValue("IsVisible", this._IsVisible);

            Info.AddValue("Shapes.Count", this._Shapes.Count);
            for (int i = 0; i < this._Shapes.Count; i++)
            {
                Info.AddValueWithType("Shapes[" + i + "]", this._Shapes[i]);
            }

            this._Shapes.ItemCameIn += this.Collection_ItemCameIn;
            this._Shapes.ItemWentOut += this.Collection_ItemWentOut;
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
                if (this._IsSelected == value)
                    return;
                this.SetSelected(value);
                this.NotifyPropertyChanged("IsSelected");
            }
        }

        public override void SetSelected(bool value)
        {
            if (this._IsSelected == value)
                return;

            this._IsSelected = value;
            for (int i = 0; i < this.Shapes.Count; i++)
            {
                this.Shapes[i].SetSelected(value);
            }
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
                if (this._IsVisible == value)
                    return;

                this.SetVisible(value);
                if (value)
                {
                    for (int i = 0; i < this.Shapes.Count; i++)
                    {
                        this.Shapes[i].IsVisible = value;
                    }
                }
            }
        }

        public sealed override void SetVisible(Boolean value)
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
                for (int i = 0; i < this.Shapes.Count; i++)
                {
                    this.Shapes[i].SetVisible(value);
                }

                var P = this.Parent as ShapeCollection;
                if (P != null)
                {
                    P.CheckChildrenVisibility();
                }
            }
            this.NotifyPropertyChanged("IsVisible");
        }

        internal void CheckChildrenVisibility()
        {
            var Bl = false;

            for (int i = 0; i < this.Shapes.Count; i++)
            {
                if (this.Shapes[i].IsVisible)
                {
                    Bl = true;
                    break;
                }
            }

            if (!Bl)
            {
                this.IsVisible = false;
            }
        }
        #endregion

        #region Shapes Property
        private readonly NotifyingCollection<ShapeBase> _Shapes = new NotifyingCollection<ShapeBase>();

        public NotifyingCollection<ShapeBase> Shapes
        {
            get
            {
                return this._Shapes;
            }
        }
        #endregion

    }

}
