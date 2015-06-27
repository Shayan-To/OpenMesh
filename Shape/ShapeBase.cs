using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Reflection;

namespace OpenMesh
{

    [Serializable()]
    public abstract class ShapeBase : NotifyingObject, ISerializable
    {

        [EditableProperty()]
        public abstract Boolean IsVisible { get; set; }
        public abstract void SetVisible(Boolean value);
        public abstract Boolean IsSelected { get; set; }
        public abstract void SetSelected(Boolean value);
        //public abstract Int32 ShapesCount { get; }
        //public abstract ShapeBase GetShape(Int32 Index);

        public ShapeBase()
        {
        }

        #region Serialization Logic
        protected ShapeBase(SerializationInfo Info, StreamingContext Context)
        {
            Utils.Deserializing();
            this._Name = Info.GetString("Name");
            this._Parent = (ShapeBase)Info.GetValueWithType("Parent");
        }

        protected virtual void GetObjectData(SerializationInfo Info, StreamingContext Context)
        {
            Utils.Serializing();
            Info.AddValue("Name", this._Name);
            Info.AddValueWithType("Parent", this._Parent);
        }

        void ISerializable.GetObjectData(SerializationInfo Info, StreamingContext Context)
        {
            this.GetObjectData(Info, Context);
        }
        #endregion

        #region Name Property
        private String _Name;

        [EditableProperty()]
        public String Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException();
                this._Name = value;
                this.NotifyPropertyChanged("Name");
            }
        }
        #endregion

        #region Parent Property
        private ShapeBase _Parent = null;

        public ShapeBase Parent
        {
            get
            {
                return this._Parent;
            }
        }

        internal void SetParent(ShapeBase Shape)
        {
            this._Parent = Shape;

            Debug.Assert(new StackTrace().GetFrame(1).GetMethod().DeclaringType == typeof(ShapeCollection));
        }
        #endregion

    }

}
