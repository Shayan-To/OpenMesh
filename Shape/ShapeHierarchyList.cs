using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OpenMesh
{

    [Serializable()]
    public class ShapeHierarchyList : IEnumerable<Shape>, ISerializable
    {

        public ShapeHierarchyList(ShapeBase Shape)
        {
            this._Shape = Shape;
        }

        Enumerator GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator<Shape> IEnumerable<Shape>.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #region Serialization Logic
        protected ShapeHierarchyList(SerializationInfo Info, StreamingContext Context)
        {
            Utils.Deserializing();
            this._Shape = (ShapeBase)Info.GetValueWithType("Shape");

            var N = Info.GetInt32("Item.Count");
            for (int i = 0; i < N; i++)
            {
                this.ShapesSet.Add((ShapeBase)Info.GetValueWithType("Item[" + i + "]"));
            }
        }

        protected virtual void GetObjectData(SerializationInfo Info, StreamingContext Context)
        {
            Utils.Serializing();
            Info.AddValueWithType("Shape", this._Shape);

            Info.AddValue("Item.Count", this.ShapesSet.Count);
            var i = 0;
            foreach (var S in this.ShapesSet)
            {
                Info.AddValueWithType("Item[" + i + "]", S);
                i++;
            }
        }

        void ISerializable.GetObjectData(SerializationInfo Info, StreamingContext Context)
        {
            this.GetObjectData(Info, Context);
        }
        #endregion

        #region ShapeStateChanged Event
        public event EventHandler<ShapeStateChangedEventArgs> ShapeStateChanged;

        protected virtual void OnShapeStateChanged(ShapeStateChangedEventArgs E)
        {
            if (this.ShapeStateChanged != null)
            {
                this.ShapeStateChanged.Invoke(this, E);
            }
        }

        public class ShapeStateChangedEventArgs : EventArgs
        {

            public ShapeStateChangedEventArgs(ShapeBase Shape)
            {
                this._Shape = Shape;
            }

            private readonly ShapeBase _Shape;

            public ShapeBase Shape
            {
                get
                {
                    return this._Shape;
                }
            }

        }
        #endregion

        #region Changed Event
        public event EventHandler Changed;

        protected virtual void OnChanged()
        {
            if (this.Changed != null)
            {
                this.Changed.Invoke(this, EventArgs.Empty);
            }
        }
        #endregion

        #region Item Property
        private readonly HashSet<ShapeBase> ShapesSet = new HashSet<ShapeBase>();

        public Boolean this[ShapeBase Shape]
        {
            get
            {
                return this.ShapesSet.Contains(Shape);
            }
            set
            {
                this.SetItemRecurce(Shape, value);
                this.OnChanged();
            }
        }

        private void SetItemRecurce(ShapeBase Shape, Boolean value)
        {
            if (!this.SetItem(Shape, value))
                return;

            if (value)
            {
                var Coll = Shape as ShapeCollection;
                if (Coll != null)
                {
                    for (int i = 0; i < Coll.Shapes.Count; i++)
                    {
                        this.SetItemRecurce(Coll.Shapes[i], value);
                    }
                }
            }
        }

        private void CheckChildren(ShapeCollection Shape)
        {
            var Bl = false;

            for (int i = 0; i < Shape.Shapes.Count; i++)
            {
                if (this[Shape.Shapes[i]])
                {
                    Bl = true;
                    break;
                }
            }

            if (!Bl)
            {
                this.SetItem(Shape, false);
            }
        }

        public Boolean SetItem(ShapeBase Shape, Boolean value)
        {
            if (this[Shape] == value)
                return false;

            if (value)
            {
                this.ShapesSet.Add(Shape);
                if (Shape.Parent != null)
                {
                    this.SetItem(Shape.Parent, value);
                }
            }
            else
            {
                this.ShapesSet.Remove(Shape);

                var Coll = Shape as ShapeCollection;
                if (Coll != null)
                {
                    for (int i = 0; i < Coll.Shapes.Count; i++)
                    {
                        this.SetItem(Coll.Shapes[i], value);
                    }
                }

                var P = Shape.Parent as ShapeCollection;
                if (P != null)
                {
                    this.CheckChildren(P);
                }
            }

            this.OnShapeStateChanged(new ShapeStateChangedEventArgs(Shape));

            return true;
        }
        #endregion

        #region Shape Property
        private readonly ShapeBase _Shape;

        public ShapeBase Shape
        {
            get
            {
                return this._Shape;
            }
        }
        #endregion

        public struct Enumerator : IEnumerator<Shape>
        {

            public Enumerator(ShapeHierarchyList P)
            {
                this._Current = null;
                this.CurrentBase = null;
                this.Stack = new Stack<KeyValuePair<ShapeCollection, Int32>>();

                this.P = P;
                this.Reset();
            }

            private Shape _Current;

            public Shape Current
            {
                get
                {
                    if (this._Current == null)
                        throw new InvalidOperationException();
                    return this._Current;
                }
            }

            private Boolean CheckShape(ShapeBase Shape)
            {
                var S = Shape as Shape;
                if (S != null)
                {
                    if (!this.P[S])
                    {
                        return false;
                    }
                    this._Current = S;
                    return true;
                }

                var C = Shape as ShapeCollection;
                if (C != null)
                {
                    if (C.Shapes.Count == 0)
                        return false;
                    this.Stack.Push(new KeyValuePair<ShapeCollection, int>(C, 0));
                    return this.CheckShape(C.Shapes[0]);
                }

                return false;
            }

            public bool MoveNext()
            {
                if (this.CurrentBase != null)
                {
                    var TT = this.CurrentBase;
                    this.CurrentBase = null;
                    if (this.CheckShape(TT))
                    {
                        return true;
                    }
                }

                do
                {
                    if (this.Stack.Count == 0)
                    {
                        this._Current = null;
                        return false;
                    }

                    var T = this.Stack.Pop();
                    T = new KeyValuePair<ShapeCollection, int>(T.Key, T.Value + 1);

                    if (T.Value < T.Key.Shapes.Count)
                    {
                        this.Stack.Push(T);
                        if (this.CheckShape(T.Key.Shapes[T.Value]))
                        {
                            return true;
                        }
                    }
                } while (true);
            }

            public void Reset()
            {
                this._Current = null;
                this.CurrentBase = this.P._Shape;
                this.Stack.Clear();
            }

            Object System.Collections.IEnumerator.Current
            {
                get
                {
                    return this.Current;
                }
            }

            public void Dispose()
            {
            }

            private readonly Stack<KeyValuePair<ShapeCollection, Int32>> Stack;
            private readonly ShapeHierarchyList P;
            private ShapeBase CurrentBase;

        }

    }

}
