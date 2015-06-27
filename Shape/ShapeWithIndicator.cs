using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OpenMesh
{

    [Serializable()]
    public class ShapeWithIndicator : ShapeCollection
    {

        public ShapeWithIndicator(Shape Shape)
        {
            this.Name = Shape.Name;

            var IS = new IndicatorShape(Shape) { Name = Shape.Name + " Indicator" };
            this.Shapes.Add(Shape);
            this.Shapes.Add(IS);

            var PS = Shape as PenShape;
            if (PS != null)
            {
                IS.Color = PS.Color;
            }
        }

        #region Serialization Logic
        protected ShapeWithIndicator(SerializationInfo Info, StreamingContext Context)
            : base(Info, Context)
        {
            Utils.Deserializing();
        }

        protected override void GetObjectData(SerializationInfo Info, StreamingContext Context)
        {
            Utils.Serializing();
            base.GetObjectData(Info, Context);
        }
        #endregion

    }

}
