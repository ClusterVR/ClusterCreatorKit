//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace VJson
{
    /// <summary>
    /// Write JSON data to streams as UTF-8.
    /// </summary>
    // TODO: Add [Preserve] in Unity
    public sealed class JsonWriter : IDisposable
    {
        struct State
        {
            public StateKind Kind;
            public int Depth;
        }

        enum StateKind
        {
            ObjectKeyHead,
            ObjectKeyOther,
            ObjectValue,
            ArrayHead,
            ArrayOther,
            None,
        }

        private StreamWriter _writer;
        private int _indent;
        private string _indentStr = null;

        private Stack<State> _states = new Stack<State>();

        public JsonWriter(Stream s, int indent = 0)
        {
            _writer = new StreamWriter(s); // UTF-8 by default
            _indent = indent;
            if (_indent > 0) {
                _indentStr = new String(' ', _indent);
            }

            _states.Push(new State
            {
                Kind = StateKind.None,
                Depth = 0,
            });
        }

        public void Dispose()
        {
            if (_writer != null)
            {
                ((IDisposable)_writer).Dispose();
            }
        }

        public void WriteObjectStart()
        {
            var state = _states.Peek();
            if (state.Kind == StateKind.ObjectKeyHead || state.Kind == StateKind.ObjectKeyOther)
            {
                throw new Exception("");
            }

            WriteDelimiter();
            _writer.Write("{");

            _states.Push(new State
            {
                Kind = StateKind.ObjectKeyHead,
                Depth = state.Depth + 1,
            });
        }

        public void WriteObjectKey(string key)
        {
            var state = _states.Peek();
            if (state.Kind != StateKind.ObjectKeyHead && state.Kind != StateKind.ObjectKeyOther)
            {
                throw new Exception("");
            }

            WriteValue(key);
            _writer.Write(":");

            _states.Pop();
            _states.Push(new State
            {
                Kind = StateKind.ObjectValue,
                Depth = state.Depth,
            });
        }

        public void WriteObjectEnd()
        {
            var state = _states.Peek();
            if (state.Kind != StateKind.ObjectKeyHead && state.Kind != StateKind.ObjectKeyOther)
            {
                throw new Exception("");
            }

            _states.Pop();

            if (state.Kind == StateKind.ObjectKeyOther)
            {
                WriteIndentBreakForHuman(_states.Peek().Depth);
            }
            _writer.Write("}");
        }

        public void WriteArrayStart()
        {
            var state = _states.Peek();
            if (state.Kind == StateKind.ObjectKeyHead || state.Kind == StateKind.ObjectKeyOther)
            {
                throw new Exception("");
            }

            WriteDelimiter();
            _writer.Write("[");

            _states.Push(new State
            {
                Kind = StateKind.ArrayHead,
                Depth = state.Depth + 1,
            });
        }

        public void WriteArrayEnd()
        {
            var state = _states.Peek();
            if (state.Kind != StateKind.ArrayHead && state.Kind != StateKind.ArrayOther)
            {
                throw new Exception("");
            }

            _states.Pop();

            if (state.Kind == StateKind.ArrayOther)
            {
                WriteIndentBreakForHuman(_states.Peek().Depth);
            }
            _writer.Write("]");
        }

        public void WriteValue(bool v)
        {
            WriteDelimiter();

            _writer.Write(v ? "true" : "false");
        }

        public void WriteValue(byte v)
        {
            WritePrimitive(v);
        }

        public void WriteValue(sbyte v)
        {
            WritePrimitive(v);
        }

        public void WriteValue(char v)
        {
            WritePrimitive(v);
        }

        public void WriteValue(decimal v)
        {
            WritePrimitive(v);
        }

        public void WriteValue(double v)
        {
            WritePrimitive(v);
        }

        public void WriteValue(float v)
        {
            WritePrimitive(v);
        }

        public void WriteValue(int v)
        {
            WritePrimitive(v);
        }

        public void WriteValue(uint v)
        {
            WritePrimitive(v);
        }

        public void WriteValue(long v)
        {
            WritePrimitive(v);
        }

        public void WriteValue(ulong v)
        {
            WritePrimitive(v);
        }

        public void WriteValue(short v)
        {
            WritePrimitive(v);
        }

        public void WriteValue(ushort v)
        {
            WritePrimitive(v);
        }

        public void WriteValue(string v)
        {
            WriteDelimiter();

            _writer.Write('\"');
            _writer.Write(Escape(v).ToArray());
            _writer.Write('\"');
        }

        public void WriteValueNull()
        {
            WriteDelimiter();

            _writer.Write("null");
        }

        void WritePrimitive(char v)
        {
            WritePrimitive<int>((int)v);
        }

        void WritePrimitive(float v)
        {
            WritePrimitive<string>(string.Format("{0:G9}", v));
        }

        void WritePrimitive(double v)
        {
            WritePrimitive<string>(string.Format("{0:G17}", v));
        }

        void WritePrimitive<T>(T v)
        {
            WriteDelimiter();

            _writer.Write(v);
        }

        void WriteIndentBreakForHuman(int depth)
        {
            if (_indent > 0)
            {
                _writer.Write('\n');

                for (int i = 0; i < depth; ++i)
                {
                    _writer.Write(_indentStr);
                }
            }
        }

        void WriteSpaceForHuman()
        {
            if (_indent > 0)
            {
                _writer.Write(' ');
            }
        }

        void WriteDelimiter()
        {
            var state = _states.Peek();
            if (state.Kind == StateKind.ArrayHead)
            {
                WriteIndentBreakForHuman(state.Depth);

                _states.Pop();
                _states.Push(new State
                {
                    Kind = StateKind.ArrayOther,
                    Depth = state.Depth
                });
                return;
            }

            if (state.Kind == StateKind.ObjectKeyHead)
            {
                WriteIndentBreakForHuman(state.Depth);
            }

            if (state.Kind == StateKind.ArrayOther || state.Kind == StateKind.ObjectKeyOther)
            {
                _writer.Write(",");

                WriteIndentBreakForHuman(state.Depth);
            }

            if (state.Kind == StateKind.ObjectValue)
            {
                WriteSpaceForHuman();

                _states.Pop();
                _states.Push(new State
                {
                    Kind = StateKind.ObjectKeyOther,
                    Depth = state.Depth
                });
            }
        }

        IEnumerable<char> Escape(string s)
        {
            foreach(var c in s)
            {
                char modified = default(char);
                if (c <= 0x20 || c == '\"' || c == '\\')
                {
                    switch(c)
                    {
                        case '\"':
                            modified = '\"';
                            break;

                        case '\\':
                            modified = '\\';
                            break;

                        case '\b':
                            modified = 'b';
                            break;

                        case '\n':
                            modified = 'n';
                            break;

                        case '\r':
                            modified = 'r';
                            break;

                        case '\t':
                            modified = 't';
                            break;
                    }
                }

                if (modified != default(char))
                {
                    yield return '\\';
                    yield return modified;
                }
                else
                {
                    yield return c;
                }
            }
        }
    }
}
