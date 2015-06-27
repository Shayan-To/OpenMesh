using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenMesh
{

    public class EditablePropertyAttribute : Attribute
    {

        public Type ValueEditor { get; set; }

    }

    public class EditableIntegerPropertyAttribute : EditablePropertyAttribute
    {

        public Int32 MinValue { get; set; }
        public Int32 MaxValue { get; set; }

    }

    public class EditableDoublePropertyAttribute : EditablePropertyAttribute
    {

        public Int32 MinValue { get; set; }
        public Int32 MaxValue { get; set; }
        public Int32 Scale { get; set; }
        public Int32 Step { get; set; }

    }

    public class EditableIntegerTrackBarPropertyAttribute : EditablePropertyAttribute
    {

        public EditableIntegerTrackBarPropertyAttribute()
        {
            this.ValueEditor = typeof(IntegerTrackBarEditor);
        }

        public Int32 MinValue { get; set; }
        public Int32 MaxValue { get; set; }
        public Int32 Step { get; set; }

    }

}
