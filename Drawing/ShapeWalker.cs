//#define DebugInfo

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;

namespace OpenMesh
{

    public class ShapeWalker
    {

        private static readonly Dictionary<Thread, ShapeWalker> Instances = new Dictionary<Thread, ShapeWalker>();

        public static ShapeWalker Instance
        {
            get
            {
                var C = Thread.CurrentThread;
                ShapeWalker R;
                if (!Instances.TryGetValue(C, out R))
                {
                    R = new ShapeWalker();
                    Instances[C] = R;
                }

                return R;
            }
        }

        public void TypedWalk<T>(ShapeBase Shape, Action<T> Act) where T : ShapeBase
        {
            this.Walk(Shape,
                S =>
                {
                    var Tmp = S as T;
                    if (Tmp != null)
                    {
                        Act.Invoke(Tmp);
                    }
                });
        }

        public void AddHandler(ShapeBase Shape,
                               EventHandler<CollectionChangedEventArgs<ShapeBase>> CollectionChangedEventHandler,
                               PropertyChangedEventHandler PropertyChangedEventHandler)
        {
            this.Walk(Shape,
                S =>
                {
                    S.PropertyChanged -= PropertyChangedEventHandler;
                    S.PropertyChanged += PropertyChangedEventHandler;

                    var Coll = S as ShapeCollection;
                    if (Coll != null)
                    {
                        Coll.Shapes.CollectionChanged -= CollectionChangedEventHandler;
                        Coll.Shapes.CollectionChanged += CollectionChangedEventHandler;
                    }
                });
        }

        public void RemoveHandler(ShapeBase Shape,
                                  EventHandler<CollectionChangedEventArgs<ShapeBase>> CollectionChangedEventHandler,
                                  PropertyChangedEventHandler PropertyChangedEventHandler)
        {
            this.Walk(Shape,
                S =>
                {
                    S.PropertyChanged -= PropertyChangedEventHandler;

                    var Coll = S as ShapeCollection;
                    if (Coll != null)
                    {
                        Coll.Shapes.CollectionChanged -= CollectionChangedEventHandler;
                    }
                });
        }

        public T Walk<T>(ShapeBase Shape, Func<ShapeBase, T, T> Func)
        {
            return this.Walk(Shape, default(T), Func);
        }

        public T NonNullWalk<T>(ShapeBase Shape, Func<ShapeBase, T, T> Func) where T : class
        {
            return this.NonNullWalk(Shape, null, Func);
        }

        public T Walk<T>(ShapeBase Shape, T ParentOut, Func<ShapeBase, T, T> Func)
        {
            this.DebugInfoStart(Shape);

            var Res = Func.Invoke(Shape, ParentOut);

            var Coll = Shape as ShapeCollection;
            if (Coll != null)
            {
                for (int i = 0; i < Coll.Shapes.Count; i++)
                {
                    this.Walk(Coll.Shapes[i], Res, Func);
                }
            }

            var Bag = Shape as ShapeBag;
            if (Bag != null)
            {
                foreach (var i in Bag.Shapes)
                {
                    this.Walk(i, Res, Func);
                }
            }

            this.DebugInfoEnd();

            return Res;
        }

        public T NonNullWalk<T>(ShapeBase Shape, T ParentOut, Func<ShapeBase, T, T> Func) where T : class
        {
            this.DebugInfoStart(Shape);

            var Res = Func.Invoke(Shape, ParentOut);

            if (Res == null)
            {
                this.DebugInfoEnd();
                return null;
            }

            var Coll = Shape as ShapeCollection;
            if (Coll != null)
            {
                for (int i = 0; i < Coll.Shapes.Count; i++)
                {
                    this.NonNullWalk(Coll.Shapes[i], Res, Func);
                }
            }

            var Bag = Shape as ShapeBag;
            if (Bag != null)
            {
                foreach (var i in Bag.Shapes)
                {
                    this.Walk(i, Res, Func);
                }
            }

            this.DebugInfoEnd();

            return Res;
        }

        public void Walk(ShapeBase Shape, Action<ShapeBase> Act)
        {
            this.DebugInfoStart(Shape);

            Act.Invoke(Shape);

            var Coll = Shape as ShapeCollection;
            if (Coll != null)
            {
                for (int i = 0; i < Coll.Shapes.Count; i++)
                {
                    this.Walk(Coll.Shapes[i], Act);
                }
            }

            var Bag = Shape as ShapeBag;
            if (Bag != null)
            {
                foreach (var i in Bag.Shapes)
                {
                    this.Walk(i, Act);
                }
            }

            this.DebugInfoEnd();
        }


        private void DebugInfoStart(ShapeBase Shape)
        {
#if DebugInfo
            if (this.Depth == 0)
                Console.Write(Utils.CompactStackTrace(4) + "--");
            Console.Write(@"{0}""{1}""[", Shape.GetType().Name, Shape.Name);
            this.Depth++;
#endif
        }

        private void DebugInfoEnd()
        {
#if DebugInfo
            this.Depth--;
            Console.Write("]");
            if (this.Depth == 0)
                Console.WriteLine();
#endif
        }

#if DebugInfo
        private int Depth;
#endif

    }

}
