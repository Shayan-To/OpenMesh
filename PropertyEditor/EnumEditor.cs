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

    public partial class EnumEditor : EnumEditorBase
    {

        public EnumEditor()
        {
            this.InitializeComponent();
        }

        public EnumEditor(Object Obj, PropertyInfo Property)
            : base(Obj, Property)
        {
            this.InitializeComponent();
            this.label1.Text = this.PropertyName;

            foreach (var N in Enum.GetNames(Property.PropertyType))
            {
                var EP = new EnumPair() { Name = N, Value = (Enum)Enum.Parse(Property.PropertyType, N) };
                this.Dic.Add(EP.Value, EP);
                this.comboBox1.Items.Add(EP);
            }

            this.OnPropertyChanged();
        }

        protected override void OnPropertyChanged()
        {
            this.comboBox1.SelectedItem = this.Dic[this.PropertyValue];
            base.OnPropertyChanged();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PropertyValue = ((EnumPair)this.comboBox1.SelectedItem).Value;
            this.OnPropertyChanged();
        }

        private Dictionary<Enum, EnumPair> Dic = new Dictionary<Enum, EnumPair>();

        private class EnumPair
        {

            public String Name { get; set; }
            public Enum Value { get; set; }

            public override string ToString()
            {
                return this.Name;
            }

        }

    }

    public class EnumEditorBase : ValueEditorBase<Enum>
    {

        public EnumEditorBase()
            : base()
        {
        }

        public EnumEditorBase(Object Obj, PropertyInfo Property)
            : base(Obj, Property)
        {
        }

    }

}
