//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace VJson
{
    public sealed class JsonReader : IDisposable
    {
        private ReaderWrapper _reader;

        private StringBuilder _strCache = new StringBuilder();

        public JsonReader(Stream s)
        {
            _reader = new ReaderWrapper(s);
        }

        public void Dispose()
        {
            if (_reader != null)
            {
                ((IDisposable)_reader).Dispose();
            }
        }

        public INode Read()
        {
            var node = ReadElement();

            var next = _reader.Peek();
            if (next != -1)
            {
                throw NodeExpectedError("EOS");
            }

            return node;
        }

        INode ReadElement()
        {
            SkipWS();
            var node = ReadValue();
            SkipWS();

            return node;
        }

        INode ReadValue()
        {
            INode node = null;

            if ((node = ReadObject()) != null)
            {
                return node;
            }

            if ((node = ReadArray()) != null)
            {
                return node;
            }

            if ((node = ReadString()) != null)
            {
                return node;
            }

            if ((node = ReadNumber()) != null)
            {
                return node;
            }

            if ((node = ReadLiteral()) != null)
            {
                return node;
            }

            throw NodeExpectedError("value");
        }

        INode ReadObject()
        {
            var next = _reader.Peek();
            if (next != '{')
            {
                return null;
            }
            _reader.Read(); // Discard

            var node = new ObjectNode();

            for (int i = 0; ; ++i)
            {
                SkipWS();

                next = _reader.Peek();
                if (next == '}')
                {
                    _reader.Read(); // Discard
                    break;
                }

                if (i > 0)
                {
                    if (next != ',')
                    {
                        throw TokenExpectedError(',');
                    }
                    _reader.Read(); // Discard
                }

                SkipWS();
                INode keyNode = ReadString();
                if (keyNode == null)
                {
                    throw NodeExpectedError("string");
                }
                SkipWS();

                next = _reader.Peek();
                if (next != ':')
                {
                    throw TokenExpectedError(':');
                }
                _reader.Read(); // Discard

                INode elemNode = ReadElement();
                if (elemNode == null)
                {
                    throw NodeExpectedError("element");
                }

                node.AddElement(((StringNode)keyNode).Value, elemNode);
            }

            return node;
        }

        INode ReadArray()
        {
            var next = _reader.Peek();
            if (next != '[')
            {
                return null;
            }
            _reader.Read(); // Discard

            var node = new ArrayNode();

            for (int i = 0; ; ++i)
            {
                SkipWS();

                next = _reader.Peek();
                if (next == ']')
                {
                    _reader.Read(); // Discard
                    break;
                }

                if (i > 0)
                {
                    if (next != ',')
                    {
                        throw TokenExpectedError(',');
                    }
                    _reader.Read(); // Discard
                }

                INode elemNode = ReadElement();
                if (elemNode == null)
                {
                    throw NodeExpectedError("element");
                }

                node.AddElement(elemNode);
            }

            return node;
        }

        INode ReadString()
        {
            var next = _reader.Peek();
            if (next != '"')
            {
                return null;
            }
            _reader.Read(); // Discard

            for (; ; )
            {
                next = _reader.Peek();
                switch (next)
                {
                    case '"':
                        _reader.Read(); // Discard

                        var span = CommitBuffer();
                        var str = Regex.Unescape(span);
                        return new StringNode(str);

                    case '\\':
                        _reader.Read(); // Discard

                        if (!ReadEscape())
                        {
                            throw NodeExpectedError("escape");
                        };
                        break;

                    default:
                        var c = _reader.Read(); // Consume
                        var codePoint = c;
                        var isPair = char.IsHighSurrogate((char)c);
                        if (isPair)
                        {
                            next = _reader.Read();  // Consume
                            if (!char.IsLowSurrogate((char)next))
                            {
                                throw NodeExpectedError("low-surrogate");
                            }
                            codePoint = char.ConvertToUtf32((char)c, (char)next);
                        }

                        if (codePoint < 0x20 || codePoint > 0x10ffff)
                        {
                            throw NodeExpectedError("unicode char (0x20 <= char <= 0x10ffff");
                        }

                        SaveToBuffer(c);
                        if (isPair)
                        {
                            SaveToBuffer(next);
                        }

                        break;
                }
            }
        }

        bool ReadEscape()
        {
            var next = _reader.Peek();
            switch (next)
            {
                case '\"':
                    SaveToBuffer('\\');
                    SaveToBuffer(_reader.Read());
                    return true;

                case '\\':
                    SaveToBuffer('\\');
                    SaveToBuffer(_reader.Read());
                    return true;

                case '/':
                    // Escape is not required in C#
                    SaveToBuffer(_reader.Read());
                    return true;

                case 'b':
                    SaveToBuffer('\\');
                    SaveToBuffer(_reader.Read());
                    return true;

                case 'n':
                    SaveToBuffer('\\');
                    SaveToBuffer(_reader.Read());
                    return true;

                case 'r':
                    SaveToBuffer('\\');
                    SaveToBuffer(_reader.Read());
                    return true;

                case 't':
                    SaveToBuffer('\\');
                    SaveToBuffer(_reader.Read());
                    return true;

                case 'u':
                    SaveToBuffer('\\');
                    SaveToBuffer(_reader.Read());
                    for (int i = 0; i < 4; ++i)
                    {
                        if (!ReadHex())
                        {
                            throw NodeExpectedError("hex");
                        }
                    }
                    return true;

                default:
                    return false;
            }
        }

        bool ReadHex()
        {
            if (ReadDigit())
            {
                return true;
            }

            var next = _reader.Peek();
            if (next >= 'A' && next <= 'F')
            {
                SaveToBuffer(_reader.Read());
                return true;
            }

            if (next >= 'a' && next <= 'f')
            {
                SaveToBuffer(_reader.Read());
                return true;
            }

            return false;
        }

        INode ReadNumber()
        {
            if (!ReadInt())
            {
                return null;
            }

            var isFloat = false;
            isFloat |= ReadFrac();
            isFloat |= ReadExp();

            var span = CommitBuffer();
            if (isFloat)
            {
                var v = double.Parse(span, CultureInfo.InvariantCulture); // TODO: Fix for large numbers
                return new FloatNode(v);
            } else {
                var v = long.Parse(span, CultureInfo.InvariantCulture);   // TODO: Fix for large numbers
                return new IntegerNode(v);
            }
        }

        bool ReadInt()
        {
            if (ReadOneNine())
            {
                ReadDigits();
                return true;
            }

            if (ReadDigit())
            {
                return true;
            }

            var next = _reader.Peek();
            if (next != '-')
            {
                return false;
            }

            SaveToBuffer(_reader.Read());

            if (ReadOneNine())
            {
                ReadDigits();
                return true;
            }

            if (ReadDigit())
            {
                return true;
            }

            throw NodeExpectedError("number");
        }

        bool ReadDigits()
        {
            if (!ReadDigit())
            {
                return false;
            }

            while (ReadDigit()) { }
            return true;
        }

        bool ReadDigit()
        {
            var next = _reader.Peek();
            if (next != '0')
            {
                return ReadOneNine();
            }

            SaveToBuffer(_reader.Read());

            return true;
        }

        bool ReadOneNine()
        {
            var next = _reader.Peek();
            if (next < '1' || next > '9')
            {
                return false;
            }

            SaveToBuffer(_reader.Read());

            return true;
        }

        bool ReadFrac()
        {
            var next = _reader.Peek();
            if (next != '.')
            {
                return false;
            }

            SaveToBuffer(_reader.Read());

            if (!ReadDigits())
            {
                throw NodeExpectedError("digits");
            }

            return true;
        }

        bool ReadExp()
        {
            var next = _reader.Peek();
            if (next != 'E' && next != 'e')
            {
                return false;
            }

            SaveToBuffer(_reader.Read());

            ReadSign();

            if (!ReadDigits())
            {
                throw NodeExpectedError("digits");
            }

            return true;
        }

        bool ReadSign()
        {
            var next = _reader.Peek();
            if (next != '+' && next != '-')
            {
                return false;
            }

            SaveToBuffer(_reader.Read());

            return true;
        }

        INode ReadLiteral()
        {
            var s = String.Empty;

            var next = _reader.Peek();
            switch (next)
            {
                case 't':
                    // Maybe true
                    s = ConsumeChars(4);
                    if (s.ToLower() != "true")
                    {
                        throw NodeExpectedError("true");
                    }
                    return new BooleanNode(true);

                case 'f':
                    // Maybe false
                    s = ConsumeChars(5);
                    if (s.ToLower() != "false")
                    {
                        throw NodeExpectedError("false");
                    }
                    return new BooleanNode(false);

                case 'n':
                    // Maybe null
                    s = ConsumeChars(4);
                    if (s.ToLower() != "null")
                    {
                        throw NodeExpectedError("null");
                    }
                    return new NullNode();

                default:
                    return null;
            }
        }

        void SkipWS()
        {
            for (; ; )
            {
                var next = _reader.Peek();
                switch (next)
                {
                    case 0x0009:
                    case 0x000a:
                    case 0x000d:
                    case 0x0020:
                        _reader.Read(); // Discard
                        break;

                    default:
                        return;
                }
            }
        }

        void SaveToBuffer(int c)
        {
            _strCache.Append((char)c);
        }

        string CommitBuffer()
        {
            var span = _strCache.ToString();
            _strCache.Length = 0;

            return span;
        }

        string ConsumeChars(int length)
        {
            for (int i = 0; i < length; ++i)
            {
                var c = _reader.Read();
                SaveToBuffer(c);
            }
            return CommitBuffer();
        }

        ParseFailedException NodeExpectedError(string expected)
        {
            var msg = String.Format("A node \"{0}\" is expected but '{1}' is provided", expected, _reader.LastToken);
            return new ParseFailedException(msg, _reader.Position);
        }

        ParseFailedException TokenExpectedError(char expected)
        {
            var msg = String.Format("A charactor '{0}' is expected but '{1}' is provided", expected, _reader.LastToken);
            return new ParseFailedException(msg, _reader.Position);
        }

        private class ReaderWrapper : IDisposable
        {
            private StreamReader _reader;

            public ulong Position
            {
                get; private set;
            }

            private int _lastToken;
            public string LastToken
            {
                get
                {
                    if (_lastToken == -1) {
                        return "<EOS>";
                    }
                    return ((char)_lastToken).ToString();
                }
            }

            public ReaderWrapper(Stream s)
            {
                _reader = new StreamReader(s); // Encodings will be auto detected
                Position = 0;
            }

            public void Dispose()
            {
                if (_reader != null)
                {
                    ((IDisposable)_reader).Dispose();
                }
            }

            public int Peek()
            {
                _lastToken = _reader.Peek();
                return _lastToken;
            }

            public int Read()
            {
                ++Position;

                _lastToken = _reader.Read();
                return _lastToken;
            }
        }
    }

    public class ParseFailedException : Exception
    {
        public ParseFailedException(string message, ulong pos)
        : base(String.Format("{0} (at position {1})", message, pos))
        {
        }
    }
}
