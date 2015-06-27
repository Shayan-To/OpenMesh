using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace OpenMesh
{

    public class LinearFormatter : IFormatter
    {
        public SerializationBinder Binder
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public StreamingContext Context
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public object Deserialize(System.IO.Stream serializationStream)
        {
            throw new NotImplementedException();
        }

        public void Serialize(System.IO.Stream serializationStream, object graph)
        {
            throw new NotImplementedException();
        }

        public ISurrogateSelector SurrogateSelector
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }

}
