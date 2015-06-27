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

    public partial class IntegerTrackBarEditor : IntegerEditorBase
    {

        public IntegerTrackBarEditor()
        {
            this.InitializeComponent();
        }

        public IntegerTrackBarEditor(Object Obj, PropertyInfo Property)
            : base(Obj, Property)
        {
            this.InitializeComponent();

            var AttrArr = Property.GetCustomAttributes(typeof(EditableIntegerTrackBarPropertyAttribute), true);
            if (AttrArr.Length != 0)
            {
                var Attr = (EditableIntegerTrackBarPropertyAttribute)AttrArr[0];
                this.trackBar1.Maximum = Attr.MaxValue;
                this.trackBar1.Minimum = Attr.MinValue;
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
            this.trackBar1.Value = this.PropertyValue;
            base.OnPropertyChanged();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.PropertyValue = this.trackBar1.Value;
            this.OnPropertyChanged();
        }

    }

}
