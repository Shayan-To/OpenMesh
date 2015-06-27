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

    public partial class ColorEditor : ColorEditorBase
    {

        public ColorEditor()
        {
            this.InitializeComponent();
        }

        public ColorEditor(Object Obj, PropertyInfo Property)
            : base(Obj, Property)
        {
            this.InitializeComponent();
            this.label1.Text = this.PropertyName;
            this.OnPropertyChanged();
        }

        protected override void OnPropertyChanged()
        {
            this.pictureBox1.BackColor = this.PropertyValue;
            base.OnPropertyChanged();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.colorDialog1.Color = this.PropertyValue;
            if (this.colorDialog1.ShowDialog() == DialogResult.OK)
            {
                this.PropertyValue = this.colorDialog1.Color;
                this.OnPropertyChanged();
            }
        }

    }

    public class ColorEditorBase : ValueEditorBase<Color>
    {

        public ColorEditorBase()
            : base()
        {
        }

        public ColorEditorBase(Object Obj, PropertyInfo Property)
            : base(Obj, Property)
        {
        }

    }

}
