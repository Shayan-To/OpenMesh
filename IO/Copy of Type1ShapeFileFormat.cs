using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO = System.IO;
using System.Drawing;

namespace OpenMesh
{

    public class Type2ShapeFileFormat : ShapeFileFormat
    {

        public override ShapeBase ReadShape(IO.TextReader TReader)
        {
            var Reader = new IO.TextReaderWE(TReader);
            var Shapes = new ShapeCollection() { Name = "Parts" };

            var Lines = new List<Line>();

            while (!Reader.IsFinished)
            {
                Line L;
                PointF P1, P2;

                if (Reader.ReadLine().Trim() != "zone")
                {
                    throw new Exception("Invalid output");
                }

                var t = new String[] { " " };
                var PS = Reader.ReadLine().Split(t, StringSplitOptions.RemoveEmptyEntries);
                P1 = new PointF(Single.Parse(PS[0]), Single.Parse(PS[1]));
                PS = Reader.ReadLine().Split(t, StringSplitOptions.RemoveEmptyEntries);
                P2 = new PointF(Single.Parse(PS[0]), Single.Parse(PS[1]));
                L = new Line(P1, P2);
                Lines.Add(L);
            }

            Shapes.Shapes.Add(new LinesShape(Lines) { Name = "Part 1" });

            return Shapes;
        }

        public override void WriteShape(ShapeBase Shape, IO.TextWriter Writer)
        {
            ShapeWalker.Instance.TypedWalk<LinesShape>(Shape,
                S =>
                {
                    foreach (var l in S.Lines)
                    {
                        Writer.WriteLine(" zone");

                        Writer.Write(l.P1.X);
                        Writer.Write(" ");
                        Writer.Write(l.P1.Y);
                        Writer.WriteLine();

                        Writer.Write(l.P2.X);
                        Writer.Write(" ");
                        Writer.Write(l.P2.Y);
                        Writer.WriteLine();
                    }
                });
        }

        public override string Name
        {
            get
            {
                return "Type 2";
            }
        }

    }

}
