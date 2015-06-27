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

    public partial class StringEditor : StringEditorBase
    {

        public StringEditor()
        {
            this.InitializeComponent();
        }

        public StringEditor(Object Obj, PropertyInfo Property)
            : base(Obj, Property)
        {
            this.InitializeComponent();
            this.label1.Text = this.PropertyName;
            this.OnPropertyChanged();
        }

        protected override void OnPropertyChanged()
        {
            this.textBox1.Text = this.PropertyValue;
            base.OnPropertyChanged();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.textBox1_Leave(sender, e);
                e.Handled = true;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            this.PropertyValue = this.textBox1.Text;
            this.OnPropertyChanged();
        }

    }

    public class StringEditorBase : ValueEditorBase<String>
    {

        public StringEditorBase()
            : base()
        {
        }

        public StringEditorBase(Object Obj, PropertyInfo Property)
            : base(Obj, Property)
        {
        }

    }

}
