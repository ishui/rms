namespace TiannuoPM.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class StringTokenizer : IEnumerable
    {
        public const string DefaultDelimiters = " \t\n\r\f";
        private readonly string delims;
        private string empty;
        private int index;
        private string[] tokens;

        public StringTokenizer(string str)
        {
            this.delims = " \t\n\r\f";
            this.tokens = null;
            this.index = 0;
            this.empty = string.Empty;
            this.Tokenize(str, false, false);
        }

        public StringTokenizer(string str, string delims)
        {
            this.delims = " \t\n\r\f";
            this.tokens = null;
            this.index = 0;
            this.empty = string.Empty;
            if (delims != null)
            {
                this.delims = delims;
            }
            this.Tokenize(str, false, false);
        }

        public StringTokenizer(string str, params char[] delims)
        {
            this.delims = " \t\n\r\f";
            this.tokens = null;
            this.index = 0;
            this.empty = string.Empty;
            if (delims != null)
            {
                this.delims = new string(delims);
            }
            this.Tokenize(str, false, false);
        }

        public StringTokenizer(string str, string delims, bool returnDelims)
        {
            this.delims = " \t\n\r\f";
            this.tokens = null;
            this.index = 0;
            this.empty = string.Empty;
            if (delims != null)
            {
                this.delims = delims;
            }
            this.Tokenize(str, returnDelims, false);
        }

        public StringTokenizer(string str, string delims, bool returnDelims, bool returnEmpty)
        {
            this.delims = " \t\n\r\f";
            this.tokens = null;
            this.index = 0;
            this.empty = string.Empty;
            if (delims != null)
            {
                this.delims = delims;
            }
            this.Tokenize(str, returnDelims, returnEmpty);
        }

        public StringTokenizer(string str, string delims, bool returnDelims, bool returnEmpty, string empty)
        {
            this.delims = " \t\n\r\f";
            this.tokens = null;
            this.index = 0;
            this.empty = string.Empty;
            if (delims != null)
            {
                this.delims = delims;
            }
            this.empty = empty;
            this.Tokenize(str, returnDelims, returnEmpty);
        }

       

        public void Reset()
        {
            this.index = 0;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return null;
        }

        private void Tokenize(string str, bool returnDelims, bool returnEmpty)
        {
            if (returnDelims)
            {
                this.tokens = str.Split(this.delims.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                List<string> list = new List<string>(this.tokens.Length << 1);
                int startIndex = str.IndexOfAny(this.delims.ToCharArray());
                int num2 = 0;
                int num3 = startIndex - 1;
                if (startIndex == 0)
                {
                    do
                    {
                        list.Add(new string(str[startIndex], 1));
                        num3 = startIndex++;
                        startIndex = str.IndexOfAny(this.delims.ToCharArray(), startIndex);
                        if (returnEmpty && (startIndex == (num3 + 1)))
                        {
                            list.Add(this.empty);
                        }
                    }
                    while (startIndex == (num3 + 1));
                }
                while (startIndex > -1)
                {
                    list.Add(this.tokens[num2++]);
                    do
                    {
                        list.Add(new string(str[startIndex], 1));
                        num3 = startIndex++;
                        startIndex = str.IndexOfAny(this.delims.ToCharArray(), startIndex);
                        if (returnEmpty && (startIndex == (num3 + 1)))
                        {
                            list.Add(this.empty);
                        }
                    }
                    while (startIndex == (num3 + 1));
                }
                if (num2 < this.tokens.Length)
                {
                    list.Add(this.tokens[num2++]);
                }
                this.tokens = list.ToArray();
                list = null;
            }
            else if (returnEmpty)
            {
                this.tokens = str.Split(this.delims.ToCharArray(), StringSplitOptions.None);
                if (this.empty != string.Empty)
                {
                    for (int i = 0; i < this.tokens.Length; i++)
                    {
                        if (this.tokens[i] == string.Empty)
                        {
                            this.tokens[i] = this.empty;
                        }
                    }
                }
            }
            else
            {
                this.tokens = str.Split(this.delims.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            }
        }

        public int Count
        {
            get
            {
                return this.tokens.Length;
            }
        }

        public int CountTokens
        {
            get
            {
                return (this.tokens.Length - this.index);
            }
        }

        public string EmptyString
        {
            get
            {
                return this.empty;
            }
        }

        public bool HasMoreTokens
        {
            get
            {
                return (this.index < this.tokens.Length);
            }
        }

        public string this[int index]
        {
            get
            {
                return this.tokens[index];
            }
        }

        public string NextToken
        {
            get
            {
                return this.tokens[this.index++];
            }
        }

        /*[CompilerGenerated]
        private sealed class GetEnumerator : IEnumerator<string>, IEnumerator, IDisposable
        {
            private int _state;
            private string _current;
            public StringTokenizer _this;

            [DebuggerHidden]
            public GetEnumerator(int _state)
            {
                this._state = _state;
            }

            private bool MoveNext()
            {
                switch (this._state)
                {
                    case 0:
                        this._state = -1;
                        while (this._this.HasMoreTokens)
                        {
                            this._current = this._this.NextToken;
                            this._state = 1;
                            return true;
                        Label_0043:
                            this._state = -1;
                        }
                        break;

                    case 1:
                        goto Label_0043;
                }
                return false;
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            void IDisposable.Dispose()
            {
            }

            string IEnumerator<string>.Current
            {
                [DebuggerHidden]
                get
                {
                    return this._current;
                }
            }

            object IEnumerator.Current
            {
                [DebuggerHidden]
                get
                {
                    return this._current;
                }
            }
        }*/
    }
}

