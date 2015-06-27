using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO = System.IO;

namespace OpenMesh
{

    public abstract class ShapeFileFormat
    {

        public abstract ShapeBase ReadShape(IO.TextReader Reader);
        public abstract void WriteShape(ShapeBase Shape, IO.TextWriter Writer);

        public abstract String Name { get; }

    }

}
