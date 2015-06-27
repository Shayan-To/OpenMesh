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

    public partial class PropertyEditor : UserControl
    {

        static PropertyEditor()
        {
            EditorDictionary = new Dictionary<Type, Type>();
            EditorDictionary.Add(typeof(Int32), typeof(IntegerEditor));
            EditorDictionary.Add(typeof(String), typeof(StringEditor));
            EditorDictionary.Add(typeof(Color), typeof(ColorEditor));
            EditorDictionary.Add(typeof(Boolean), typeof(BooleanEditor));
            EditorDictionary.Add(typeof(Enum), typeof(EnumEditor));
        }

        public PropertyEditor()
        {
            this.InitializeComponent();
        }

        private void UpdateEditors()
        {
            this.flowLayoutPanel1.SuspendLayout();

            this.flowLayoutPanel1.Controls.Clear();
            this.Properties.Clear();

            if (this._EditingObject != null)
            {
                foreach (var C in this._BaseControls)
                {
                    C.Margin = new Padding(0, 3, 0, 3);
                    C.Width = this.flowLayoutPanel1.Width - C.Margin.Horizontal;
                    //C.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    this.flowLayoutPanel1.Controls.Add(C);
                }
                foreach (var P in this._EditingObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
                {
                    //Console.WriteLine(P.Name);
                    if (!P.CanRead | !P.CanWrite)
                        continue;
                    //Console.WriteLine("Yes!");
                    //if (P.Name == "IsVisible")
                    //{
                    //    Console.WriteLine(P.GetCustomAttributesData().Count);
                    //    foreach (var i in P.GetCustomAttributesData())
                    //    {
                    //        Console.WriteLine(i);
                    //    }
                    //}
                    var AttrArr = P.GetCustomAttributes(typeof(EditablePropertyAttribute), true);
                    if (AttrArr.Length == 0)
                        continue;
                    var Attr = (EditablePropertyAttribute)AttrArr[0];
                    Type Editor;
                    if (Attr.ValueEditor != null)
                    {
                        Editor = Attr.ValueEditor;
                    }
                    else if (!EditorDictionary.TryGetValue(P.PropertyType, out Editor))
                    {
                        var T = P.PropertyType;
                        Editor = null;
                        foreach (var KV in EditorDictionary)
                        {
                            if (KV.Key.IsAssignableFrom(T))
                            {
                                Editor = KV.Value;
                                break;
                            }
                        }
                        if (Editor == null)
                        {
                            Console.WriteLine("Property ignored: {0}.{1}", P.DeclaringType.FullName, P.Name);
                            continue;
                        }
                    }
                    //Console.WriteLine("Yeah!!");

                    this.Properties.Add(P);

                    var C = (Control)(Editor.GetConstructor(new Type[] { typeof(Object), typeof(PropertyInfo) })
                                            .Invoke(new Object[] { this._EditingObject, P }));
                    //Console.WriteLine(C);
                    C.Margin = new Padding();//0, 3, 0, 3);
                    C.Width = this.flowLayoutPanel1.Width - C.Margin.Horizontal;
                    //C.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
                    this.flowLayoutPanel1.Controls.Add(C);
                }
            }

            this.flowLayoutPanel1.ResumeLayout();
        }

        private Object _EditingObject;

        public Object EditingObject
        {
            get
            {
                return this._EditingObject;
            }
            set
            {
                this._EditingObject = value;
                this.UpdateEditors();
            }
        }

        private readonly List<Control> _BaseControls = new List<Control>();

        public List<Control> BaseControls
        {
            get
            {
                return this._BaseControls;
            }
        }

        private readonly List<PropertyInfo> Properties = new List<PropertyInfo>();
        private static readonly Dictionary<Type, Type> EditorDictionary;

        private void flowLayoutPanel1_Resize(object sender, EventArgs e)
        {
            this.flowLayoutPanel1.SuspendLayout();

            var W = this.flowLayoutPanel1.Width - this.flowLayoutPanel1.Padding.Horizontal;
            foreach (Control C in this.flowLayoutPanel1.Controls)
            {
                C.Width = W - C.Margin.Horizontal;
            }

            this.flowLayoutPanel1.ResumeLayout();
        }
    }

}
