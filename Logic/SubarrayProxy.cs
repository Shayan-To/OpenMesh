using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenMesh
{

    public struct SubarrayProxy<T>
    {

        public SubarrayProxy(T[] Arr, Int32 Start, Int32 End)
        {
            this.Arr = Arr;
            this.Start = Start;
            this.End = End;
        }

        public T this[int Index]
        {
            get
            {
                Index += this.Start;
                if (Index < this.Start | Index >= this.End)
                    throw new IndexOutOfRangeException();
                return this.Arr[Index];
            }
        }

        public Int32 Count
        {
            get
            {
                return this.End - this.Start;
            }
        }

        private T[] Arr;
        private Int32 Start, End;

    }

}
