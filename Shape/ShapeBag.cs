using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OpenMesh
{

    [Serializable()]
    public class ShapeBag : ShapeBase
    {

        public ShapeBag(IEnumerable<Shape> Shapes)
        {
            this._Shapes = Shapes;
        }

        #region Serialization Logic
        protected ShapeBag(SerializationInfo Info, StreamingContext Context)
            : base(Info, Context)
        {
            Utils.Deserializing();
            this._Shapes = (IEnumerable<Shape>)Info.GetValueWithType("Shapes");
        }

        protected override void GetObjectData(SerializationInfo Info, StreamingContext Context)
        {
            Utils.Serializing();
            base.GetObjectData(Info, Context);

            Info.AddValueWithType("Shapes", this._Shapes);
        }
        #endregion

        #region IsVisible Property
        public override bool IsVisible
        {
            get
            {
                return true;
            }
            set
            {
                this.SetVisible(value);
            }
        }

        public override void SetVisible(bool value)
        {
            //throw new NotSupportedException();
        }
        #endregion

        #region IsSelected Property
        public override bool IsSelected
        {
            get
            {
                return false;
            }
            set
            {
                this.SetSelected(value);
            }
        }

        public override void SetSelected(bool value)
        {
            //throw new NotSupportedException();
        }
        #endregion

        #region Shapes Property
        private readonly IEnumerable<Shape> _Shapes;

        public IEnumerable<Shape> Shapes
        {
            get
            {
                return this._Shapes;
            }
        }
        #endregion

    }

}
