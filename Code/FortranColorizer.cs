using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace OpenMesh
{

    public class FortranColorizer
    {

        public FortranColorizer(String FortranCode)
        {
            this._Code = FortranCode;
            this.Index = 0;
            this._ColorizedCode = this.GenerateColorizedCode();
        }

        private String GenerateColorizedCode()
        {
            StringBuilder Res;
            Token T;
            Boolean Finished;

            Res = new StringBuilder(@"{\rtf1\ansi\deff0{\fonttbl{\f0 Courier New;}}").AppendLine()
                    .AppendLine(@"{\colortbl\red0\green0\blue0;\red0\green0\blue255;\red163\green21\blue21;\red0\green128\blue0;}")
                    .Append(@"{\pard ");
            Finished = false;

            do
            {
                T = this.ReadToken();

                switch (T.Type)
                {
                    case "White":
                        if (IsEndLine(T.Text[0]))
                        {
                            Res.AppendLine(@"\par}")
                               .Append(@"{\pard ");
                        }
                        else
                        {
                            Res.Append(T.Text);
                        }
                        break;
                    case "Word":
                        if (Keywords.Contains(T.Text.ToLowerInvariant()))
                        {
                            Res.Append(@"{\cf1 ").Append(T.Text).Append("}");
                        }
                        else
                        {
                            Res.Append(T.Text);
                        }
                        break;
                    case "Number":
                        Res.Append(T.Text);
                        break;
                    case "Comment":
                        Res.Append(@"{\cf3 ").Append(T.Text).Append("}");
                        break;
                    case "Stirng":
                        Res.Append(@"{\cf2 ").Append(T.Text).Append("}");
                        break;
                    case "Operator":
                        Res.Append(T.Text);
                        break;
                    case "End":
                        Finished = true;
                        break;
                    default:
                        Debug.Assert(false);
                        break;
                }
            } while (!Finished);

            return Res.AppendLine(@"\par}").AppendLine(@"}").ToString();
        }

        private Token ReadToken()
        {
            Char ch;
            int I;

            if (this.IsEnd())
                return new Token("End", null);

            I = this.Index;
            ch = this.Read();

            if (Char.IsWhiteSpace(ch))
            {
                if (ch == '\r' && this.Peek() == '\n')
                    return new Token("White", new String(this.Read(), 1));
                return new Token("White", new String(ch, 1));
            }
            else if (IsL_(ch))
            {
                while (!this.IsEnd() && IsLD_(this.Peek()))
                    this.Read();

                return new Token("Word", this._Code.Substring(I, this.Index - I));
            }
            else if (Char.IsDigit(ch) || (ch == '.' && Char.IsDigit(this.Peek())))
            {
                Boolean Dot;

                Dot = ch == '.';

                while (!this.IsEnd())
                {
                    ch = this.Peek();

                    if (Char.IsDigit(this.Peek()))
                    {
                        this.Read();
                    }
                    else if (ch == '.')
                    {
                        if (Dot)
                            break;
                        Dot = true;
                        this.Read();
                    }
                    else if (ch == 'e' || ch == 'E')
                    {
                        this.Read();
                        ch = this.Peek();
                        if (ch == '+' || ch == '-')
                            this.Read();
                        Dot = true;
                    }
                    else
                        break;
                }

                return new Token("Number", this._Code.Substring(I, this.Index - I));
            }
            else if (ch == '!')
            {
                while (!this.IsEnd() && !IsEndLine(this.Peek()))
                {
                    this.Read();
                }

                return new Token("Comment", this._Code.Substring(I, this.Index - I));
            }
            else if (ch == '\'' || ch == '"')
            {
                Char Mk;

                Mk = ch;

                while (!this.IsEnd() && this.Peek() != Mk)
                {
                    this.Read();
                }

                if (!this.IsEnd())
                    this.Read();

                return new Token("Stirng", this._Code.Substring(I, this.Index - I));
            }
            else
            {
                return new Token("Operator", new String(ch, 1));
            }

        }

        private Boolean SkipChar(Char ch)
        {
            if (this.Peek() == ch)
            {
                this.Read();
                return true;
            }
            return false;
        }

        private Char Read()
        {
            return this._Code[this.Index++];
        }

        private Char Peek()
        {
            return this._Code[this.Index];
        }

        private Boolean IsEnd()
        {
            return this.Index >= this._Code.Length;
        }

        private static Boolean IsEndLine(Char c)
        {
            return c == '\r' || c == '\n';
        }

        private static Boolean IsSpace(Char c)
        {
            return !IsEndLine(c) && Char.IsWhiteSpace(c);
        }

        private static Boolean IsL_(Char c)
        {
            return c == '_' || Char.IsLetter(c);
        }

        private static Boolean IsLD_(Char c)
        {
            return c == '_' || Char.IsLetter(c) || Char.IsDigit(c);
        }

        #region Code Property
        private readonly String _Code;
        public String Code
        {
            get
            {
                return this._Code;
            }
        }
        #endregion

        #region ColorizedCode Property
        private readonly String _ColorizedCode = null;
        public String ColorizedCode
        {
            get
            {
                return this._ColorizedCode;
            }
        }
        #endregion

        private static readonly ReadOnlyCollection<String> Keywords = new List<String>(new String[] { "assign", "backspace", "block data", "call", "close", "common", "continue", "data", "dimension", "do", "else", "elseif", "end", "endfile", "endif", "entry", "equivalence", "external", "format", "function", "goto", "if", "implicit", "inquire", "intrinsic", "open", "parameter", "pause", "print", "program", "read", "return", "rewind", "rewrite", "save", "stop", "subroutine", "then", "write", "allocatable", "allocate", "case", "contains", "cycle", "deallocate", "elsewhere", "exit", "include", "interface", "intent", "module", "namelist", "nullify", "only", "operator", "optional", "pointer", "private", "procedure", "public", "recursive", "result", "select", "sequence", "target", "use", "while", "where" }).AsReadOnly();

        private int Index;

        private struct Token
        {

            public Token(String Type, String Text)
            {
                _Type = Type;
                _Text = Text;
            }

            #region Type Property
            private readonly String _Type;
            public String Type
            {
                get
                {
                    return this._Type;
                }
            }
            #endregion

            #region Text Property
            private readonly String _Text;
            public String Text
            {
                get
                {
                    return this._Text;
                }
            }
            #endregion

        }

    }

}
