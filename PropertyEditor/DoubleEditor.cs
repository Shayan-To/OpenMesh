using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace OpenMesh
{

    public partial class DoubleEditor : DoubleEditorBase
    {

        public DoubleEditor()
        {
            this.InitializeComponent();
        }

        public DoubleEditor(Object Obj, PropertyInfo Property)
            : base(Obj, Property)
        {
            this.InitializeComponent();

            var AttrArr = Property.GetCustomAttributes(typeof(EditableDoublePropertyAttribute), true);
            if (AttrArr.Length != 0)
            {
                var Attr = (EditableDoublePropertyAttribute)AttrArr[0];
                if (Attr.Scale == 0)
                {
                    this.ValueScale = 10;
                }
                else
                {
                    this.ValueScale = Attr.Scale;
                }
                this.trackBar1.Maximum = Attr.MaxValue * this.ValueScale;
                this.trackBar1.Minimum = Attr.MinValue * this.ValueScale;
                if (Attr.Step != 0)
                {
                    this.trackBar1.SmallChange = Attr.Step;
                }
            }

            this.label1.Text = this.PropertyName;
            this.OnPropertyChanged();
        }

        protected override void OnPropertyChanged()
        {
            this.TrackBarUpdates = false;
            this.trackBar1.Value = (Int32)(this.PropertyValue * this.ValueScale);
            this.TrackBarUpdates = true;

            base.OnPropertyChanged();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (TrackBarUpdates)
            {
                this.PropertyValue = (Double)this.trackBar1.Value / this.ValueScale;
                this.OnPropertyChanged();
            }
        }

        private Boolean TrackBarUpdates = true;
        private Int32 ValueScale;

    }

    public class DoubleEditorBase : ValueEditorBase<Double>
    {

        public DoubleEditorBase()
            : base()
        {
        }

        public DoubleEditorBase(Object Obj, PropertyInfo Property)
            : base(Obj, Property)
        {
        }

    }

}
