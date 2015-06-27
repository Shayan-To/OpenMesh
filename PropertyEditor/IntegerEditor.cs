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

    public partial class IntegerEditor : IntegerEditorBase
    {
       
        public IntegerEditor()
        {
            this.InitializeComponent();
        }

        public IntegerEditor(Object Obj, PropertyInfo Property)
            : base(Obj, Property)
        {
            this.InitializeComponent();

            var AttrArr = Property.GetCustomAttributes(typeof(EditableIntegerPropertyAttribute), true);
            if (AttrArr.Length != 0)
            {
                var Attr = (EditableIntegerPropertyAttribute)AttrArr[0];
                this.numericUpDown1.Maximum = Attr.MaxValue;
                this.numericUpDown1.Minimum = Attr.MinValue;
            }

            this.label1.Text = this.PropertyName;
            this.OnPropertyChanged();
        }

        protected override void OnPropertyChanged()
        {
            this.numericUpDown1.Value = this.PropertyValue;
            base.OnPropertyChanged();
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            this.PropertyValue = (Int32)this.numericUpDown1.Value;
            this.OnPropertyChanged();
        }

    }

    public class IntegerEditorBase : ValueEditorBase<Int32>
    {

        public IntegerEditorBase()
            : base()
        {
        }

        public IntegerEditorBase(Object Obj, PropertyInfo Property)
            : base(Obj, Property)
        {
        }

    }

}
