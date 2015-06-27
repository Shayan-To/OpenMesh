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

    public partial class BooleanEditor : BooleanEditorBase
    {

        public BooleanEditor()
        {
            this.InitializeComponent();
        }

        public BooleanEditor(Object Obj, PropertyInfo Property)
            : base(Obj, Property)
        {
            this.InitializeComponent();
            this.checkBox1.Text = this.PropertyName;
            this.OnPropertyChanged();
        }

        protected override void OnPropertyChanged()
        {
            this.checkBox1.Checked = this.PropertyValue;
            base.OnPropertyChanged();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.PropertyValue = this.checkBox1.Checked;
        }

    }


    public class BooleanEditorBase : ValueEditorBase<Boolean>
    {

        public BooleanEditorBase()
            : base()
        {
        }

        public BooleanEditorBase(Object Obj, PropertyInfo Property)
            : base(Obj, Property)
        {
        }

    }

}
