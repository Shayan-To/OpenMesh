using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenMesh
{

    public partial class ShapeTreeViewer : UserControl
    {

        public ShapeTreeViewer()
        {
            this.InitializeComponent();

            this.CheckBoxSetter = (S, V) => { S.IsVisible = V; };
            this.CheckBoxGetter = (S) => S.IsVisible;

            this.CollectionChangedEventHandler = new EventHandler<CollectionChangedEventArgs<ShapeBase>>(this.ShapeCollection_CollectionChanged);
            this.PropertyChangedEventHandler = new PropertyChangedEventHandler(this.Shape_PropertyChanged);
        }

        #region EventHandlers
        private readonly EventHandler<CollectionChangedEventArgs<ShapeBase>> CollectionChangedEventHandler;
        private void ShapeCollection_CollectionChanged(Object Sender, CollectionChangedEventArgs<ShapeBase> E)
        {
            //Console.WriteLine("*ShapeTreeViewer.CollectionChanged");
            var Shape = this.GetShape(Sender as NotifyingCollection<ShapeBase>);
            var TN = this.GetNode(Shape);
            if (TN == null)
            {
                return;
            }

            for (int i = 0; i < E.RemovedOnes.Length; i++)
            {
                var T = E.RemovedOnes[i];
                this.RemoveNode(T.Item);
                TN.Nodes.RemoveAt(T.Position);
            }

            for (int i = 0; i < E.AddedOnes.Length; i++)
            {
                var T = E.AddedOnes[i];
                TN.Nodes.Insert(T.Position, this.CreateNode(T.Item));
            }

            this.treeView1.ExpandAll();
        }

        private readonly PropertyChangedEventHandler PropertyChangedEventHandler;
        private void Shape_PropertyChanged(Object Sender, PropertyChangedEventArgs E)
        {
            //Console.WriteLine("*ShapeTreeViewer.PropertyChanged");
            var Shape = Sender as ShapeBase;
            var TN = this.GetNode(Shape);
            if (TN == null)
            {
                return;
            }

            switch (E.PropertyName)
            {
                case "Name":
                    TN.Text = Shape.Name;
                    break;
                case "Pen":
                    var PS = Shape as PenShape;
                    if (PS != null)
                    {
                        TN.ForeColor = PS.Color;
                    }
                    break;
                default:
                    TN.Checked = this.CheckBoxGetter.Invoke(Shape);
                    break;
            }
        }

        public void ShapeCheckBoxStateChanged(ShapeBase Shape)
        {
            var TN = this.GetNode(Shape);
            if (TN == null)
            {
                return;
            }
            TN.Checked = this.CheckBoxGetter.Invoke(Shape);
        }
        #endregion

        #region Walking Logic
        private TreeNode CreateNode(ShapeBase Shape)
        {
            #region ...
            //if (this.NodesDic.TryGetValue(Shape, out Node))
            //{
            //    return false;
            //}

            //Node = ShapeWalker.Instance.NonNullWalk<TreeNode>(Shape,
            //    (S, P) =>
            //    {
            //        var Bl = true;

            //        TreeNode N;
            //        if (!this.NodesDic.TryGetValue(Shape, out N))
            //        {
            //            N = new TreeNode(Shape.Name);
            //            Bl = false;
            //        }

            //        if (P != null)
            //        {
            //            P.Nodes.Add(N);
            //        }

            //        if (Bl)
            //        {
            //            return N;
            //        }

            //        //Console.WriteLine(Shape.Name);

            //        this.NodesDic.Add(Shape, N);
            //        var Coll = Shape as ShapeCollection;
            //        if (Coll != null)
            //        {
            //            this.CollsDic.Add(Coll.Shapes, Coll);
            //        }

            //        return N;
            //    });
            #endregion

            var R = ShapeWalker.Instance.Walk<TreeNode>(Shape,
                (S, P) =>
                {
                    TreeNode N = new TreeNode(S.Name) { Tag = S, Checked = this.CheckBoxGetter.Invoke(S) };

                    var PS = S as PenShape;
                    if (PS != null)
                    {
                        N.ForeColor = PS.Color;
                    }
                    //Console.Write("'{0}:{1}'", S.GetType().Name, S.Name);

                    if (P != null)
                    {
                        P.Nodes.Add(N);
                    }

                    //Console.WriteLine(Shape.Name);

                    this.NodesDic.Add(S, N);
                    var Coll = S as ShapeCollection;
                    if (Coll != null)
                    {
                        this.CollsDic.Add(Coll.Shapes, Coll);
                    }

                    return N;
                });
            ShapeWalker.Instance.AddHandler(Shape, this.CollectionChangedEventHandler, this.PropertyChangedEventHandler);

            return R;
        }

        private void RemoveNode(ShapeBase Shape)
        {
            ShapeWalker.Instance.Walk(Shape,
                S =>
                {
                    this.NodesDic.Remove(S);
                    var Coll = S as ShapeCollection;
                    if (Coll != null)
                    {
                        this.CollsDic.Remove(Coll.Shapes);
                    }
                });
            ShapeWalker.Instance.RemoveHandler(Shape, this.CollectionChangedEventHandler, this.PropertyChangedEventHandler);
        }
        #endregion

        #region UI EventHandlers
        private Boolean DoEvents = true;

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (!DoEvents)
            {
                return;
            }

            var T = this._SelectedShape;
            if (this.treeView1.SelectedNode == null)
            {
                this._SelectedShape = null;
            }
            else
            {
                this._SelectedShape = (ShapeBase)this.treeView1.SelectedNode.Tag;
            }

            this.OnSelectedShapeChanged(T);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var S = this._SelectedShape;
            if (S == null)
                return;

            var P = S.Parent as ShapeCollection;
            if (P == null)
                return;

            var I = P.Shapes.IndexOf(S);
            if (I > 0)
            {
                P.Shapes.Move(I, I - 1);
            }

            this.SelectedShape = S;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var S = this._SelectedShape;
            if (S == null)
                return;

            var P = S.Parent as ShapeCollection;
            if (P == null)
                return;

            var I = P.Shapes.IndexOf(S);
            if (I < P.Shapes.Count - 1)
            {
                P.Shapes.Move(I, I + 1);
            }

            this.SelectedShape = S;
        }

        private void treeView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                var S = this.treeView1.SelectedNode;
                if (S.Parent != null)
                    this.treeView1.SelectedNode = S.Parent;
                e.Handled = true;
            }
        }

        private void treeView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                var N = this.treeView1.HitTest(e.Location);
                //Console.WriteLine(N.Location);
                if ((N.Location & (TreeViewHitTestLocations.None |
                                   TreeViewHitTestLocations.Indent |
                                   TreeViewHitTestLocations.RightOfLabel |
                                   TreeViewHitTestLocations.AboveClientArea |
                                   TreeViewHitTestLocations.BelowClientArea |
                                   TreeViewHitTestLocations.RightOfClientArea |
                                   TreeViewHitTestLocations.LeftOfClientArea)) != 0)
                {
                    //Console.Write("aa");
                    this.SelectedShape = null;
                }
                if (N.Location == TreeViewHitTestLocations.StateImage)
                {
                    var S = N.Node.Tag as ShapeBase;
                    if (S != null)
                    {
                        this.CheckBoxSetter.Invoke(S, !this.CheckBoxGetter.Invoke(S));
                    }
                }
            }
        }

        private void ShapeTreeViewer_MouseUp(object sender, MouseEventArgs e)
        {
            this.SelectedShape = null;
        }
        #endregion

        #region SelectedShapeChanged Event
        public event EventHandler SelectedShapeChanged;

        protected virtual void OnSelectedShapeChanged(ShapeBase PreviousShape)
        {
            if (PreviousShape != null)
            {
                PreviousShape.IsSelected = false;
            }

            if (this._SelectedShape != null)
            {
                this._SelectedShape.IsSelected = true;
            }

            if (this.SelectedShapeChanged != null)
            {
                this.SelectedShapeChanged.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion

        public Boolean ShowMoveButtons
        {
            set
            {
                this.tableLayoutPanel1.Visible = value;
                this.treeView1.Dock = (value ? DockStyle.None : DockStyle.Fill);
            }
        }

        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Action<ShapeBase, Boolean> CheckBoxSetter { get; set; }

        [Browsable(false),
         DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<ShapeBase, Boolean> CheckBoxGetter { get; set; }

        #region Shape Property
        private ShapeBase _Shape = null;

        public ShapeBase Shape
        {
            get
            {
                return this._Shape;
            }
            set
            {
                if (this._Shape != null)
                {
                    this.RemoveNode(this._Shape);
                }
                this.treeView1.Nodes.Clear();

                this._Shape = value;

                if (value != null)
                {
                    this.treeView1.Nodes.Add(this.CreateNode(value));
                }
                this.treeView1.ExpandAll();
            }
        }
        #endregion

        #region SelectedShape Property
        private ShapeBase _SelectedShape;

        public ShapeBase SelectedShape
        {
            get
            {
                return this._SelectedShape;
            }
            set
            {
                var T = this._SelectedShape;

                this._SelectedShape = value;

                this.DoEvents = false;
                this.treeView1.SelectedNode = this.GetNode(value);
                this.DoEvents = true;

                this.OnSelectedShapeChanged(T);
            }
        }
        #endregion

        #region NodesDic Logic
        private readonly Dictionary<ShapeBase, TreeNode> NodesDic = new Dictionary<ShapeBase, TreeNode>();
        private readonly Dictionary<NotifyingCollection<ShapeBase>, ShapeCollection> CollsDic = new Dictionary<NotifyingCollection<ShapeBase>, ShapeCollection>();

        private TreeNode GetNode(ShapeBase Shape)
        {
            if (Shape == null)
                return null;
            return this.NodesDic[Shape];
        }

        private ShapeCollection GetShape(NotifyingCollection<ShapeBase> Coll)
        {
            if (Coll == null)
                return null;
            return this.CollsDic[Coll];
        }

        private TreeNode GetNode(NotifyingCollection<ShapeBase> Coll)
        {
            return this.GetNode(this.GetShape(Coll));
        }
        #endregion

    }

}
