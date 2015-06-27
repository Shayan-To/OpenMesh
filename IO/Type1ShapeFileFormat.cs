using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO = System.IO;
using System.Drawing;

namespace OpenMesh
{

    public class Type1ShapeFileFormat : ShapeFileFormat
    {

        public override ShapeBase ReadShape(IO.TextReader Reader)
        {
            int n = int.Parse(Reader.ReadLine().Trim());
            var ar = new int[n];
            for (int i = 0; i < n; i++)
            {
                ar[i] = int.Parse(Reader.ReadLine().Trim());
            }
            int m = int.Parse(Reader.ReadLine().Trim());

            var Points = new PointF[m];

            for (int i = 0; i < m; i++)
            {
                var L = Reader.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Points[i] = new PointF(Single.Parse(L[0]), Single.Parse(L[1]));
            }

            var Collection = new ShapeCollection() { Name = "Parts" };

            var MyColors = new Color[] { Color.Red, Color.Blue, Color.Magenta, Color.Green, Color.Teal };

            for (int i = 0; i < n; i++)
            {
                m = ar[i];
                var S = new List<Line>();

                for (int j = 0; j < m; j++)
                {
                    var L = Reader.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    S.Add(new Line(Points[int.Parse(L[0]) - 1], Points[int.Parse(L[1]) - 1]));
                }

                Collection.Shapes.Add(new LinesShape(S) { Color = MyColors[i], Name = "Part " + (i + 1).ToString() });
            }

            return Collection;
        }

        public override void WriteShape(ShapeBase Shape, IO.TextWriter Writer)
        {
            var Dic = new Dictionary<PointF, Int32>();
            var Sz = 0;

            ShapeWalker.Instance.TypedWalk<LinesShape>(Shape, S => { Sz++; });
            Writer.WriteLine(Sz);
            ShapeWalker.Instance.TypedWalk<LinesShape>(Shape, S => { Writer.WriteLine(S.Lines.Count); });

            Sz = 0;
            ShapeWalker.Instance.TypedWalk<LinesShape>(Shape,
                S =>
                {
                    foreach (var L in S.Lines)
                    {
                        if (!Dic.ContainsKey(L.P1))
                        {
                            Dic.Add(L.P1, Sz++);
                        }
                        if (!Dic.ContainsKey(L.P2))
                        {
                            Dic.Add(L.P2, Sz++);
                        }
                    }
                });

            Writer.WriteLine(Sz);
            foreach (var KV in Dic.OrderBy(KV => KV.Value))
            {
                Writer.Write(KV.Key.X);
                Writer.Write(" ");
                Writer.Write(KV.Key.Y);
                Writer.WriteLine();
            }

            ShapeWalker.Instance.TypedWalk<LinesShape>(Shape,
                S =>
                {
                    Writer.WriteLine();
                    foreach (var l in S.Lines)
                    {
                        Writer.Write(Dic[l.P1]);
                        Writer.Write(" ");
                        Writer.Write(Dic[l.P2]);
                        Writer.WriteLine();
                    }
                });
        }

        public override string Name
        {
            get
            {
                return "Type 1";
            }
        }

    }

}
