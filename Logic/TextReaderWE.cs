using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IO = System.IO;

namespace System.IO
{

    public class TextReaderWE : IO.TextReader
    {

        public TextReaderWE(TextReader Reader)
        {
            this.Reader = Reader;
            this.PeekedChar = new Int32?();
        }
        
        public override void Close()
        {
            this.Reader.Close();
        }

        public override Runtime.Remoting.ObjRef CreateObjRef(Type requestedType)
        {
            return this.Reader.CreateObjRef(requestedType);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                this.Reader.Dispose();
        }

        public override bool Equals(object obj)
        {
            return this.Reader.Equals(obj);
        }

        public override int GetHashCode()
        {
            return this.Reader.GetHashCode();
        }

        public override object InitializeLifetimeService()
        {
            return this.Reader.InitializeLifetimeService();
        }

        public override int Peek()
        {
            if (!this.PeekedChar.HasValue)
                this.PeekedChar = new Int32?(this.Reader.Read());
            return this.PeekedChar.Value;
        }

        public override int Read()
        {
            if (this.PeekedChar.HasValue)
            {
                Int32 R = this.PeekedChar.Value;
                this.PeekedChar = new Int32?();
                return R;
            }
            return this.Reader.Read();
        }

        public override int Read(char[] buffer, int index, int count)
        {
            if (this.PeekedChar.HasValue)
            {
                Int32 T = this.PeekedChar.Value;
                this.PeekedChar = new Int32?();

                if (T == -1)
                    return 0;

                buffer[index++] = (Char)T;
                count--;

                if (count == 0)
                    return 1;

                return this.Reader.Read(buffer, index, count) + 1;
            }
            return this.Reader.Read(buffer, index, count);
        }

        public override int ReadBlock(char[] buffer, int index, int count)
        {
            if (this.PeekedChar.HasValue)
            {
                Int32 T = this.PeekedChar.Value;
                this.PeekedChar = new Int32?();

                if (T == -1)
                    return 0;

                buffer[index++] = (Char)T;
                count--;

                if (count == 0)
                    return 1;

                return this.Reader.ReadBlock(buffer, index, count) + 1;
            }
            return this.Reader.ReadBlock(buffer, index, count);
        }

        public override string ReadLine()
        {
            if (this.PeekedChar.HasValue)
            {
                Int32 T = this.PeekedChar.Value;
                this.PeekedChar = new Int32?();

                if (T == -1)
                    return null;

                Char C = (Char)T;

                if (C == '\n' || C == '\r')
                {
                    if (C == '\r' && this.Peek() == '\n')
                        this.Read();
                    return "";
                }

                return C + this.Reader.ReadLine();
            }
            return this.Reader.ReadLine();
        }

        public override string ReadToEnd()
        {
            if (this.PeekedChar.HasValue)
            {
                Int32 T = this.PeekedChar.Value;
                this.PeekedChar = new Int32?();

                if (T == -1)
                    return "";

                return (Char)T + this.Reader.ReadToEnd();
            }
            return this.Reader.ReadToEnd();
        }

        public override string ToString()
        {
            return this.Reader.ToString();
        }

        public Boolean IsFinished
        {
            get
            {
                return this.Peek() == -1;
            }
        }

        private Int32? PeekedChar;
        private TextReader Reader;

    }

}
