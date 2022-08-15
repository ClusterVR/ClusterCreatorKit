//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Linq;

namespace VJson.Internal
{
    internal struct State
    {
        string _elemName;
        string ElemName
        {
            get
            {
                return !string.IsNullOrEmpty(_elemName) ? _elemName : "(root)";
            }
        }

        internal State NestAsElem(int elem)
        {
            return new State()
            {
                _elemName = String.Format("{0}[{1}]", ElemName, elem),
            };
        }

        internal State NestAsElem(string elem)
        {
            return new State()
            {
                _elemName = String.Format("{0}[\"{1}\"]", ElemName, elem),
            };
        }

        internal string CreateMessage(string format, params object[] args)
        {
            return String.Format("{0}: {1}.", ElemName, String.Format(format, args));
        }
    }

    internal static class StateExtension
    {
        public static string CreateNodeConversionFailureMessage(this State s, INode fromNode, IEnumerable<Type> toTypes)
        {
            var toTypesStr = string.Format("one of [{0}]",
                                           string.Join(", ", toTypes.Select(t => t.ToString()).ToArray()));
            return CreateNodeConversionFailureMessage(s, fromNode, toTypesStr);
        }

        public static string CreateNodeConversionFailureMessage(this State s, INode fromNode, Type toType)
        {
            var toTypeStr = toType.ToString();

            switch (fromNode.Kind)
            {
                case NodeKind.Null:
                    if (!TypeHelper.IsBoxed(toType))
                    {
                        toTypeStr = string.Format("non-boxed value({0})", toTypeStr);
                    }
                    break;
            }

            return CreateNodeConversionFailureMessage(s, fromNode, toTypeStr);
        }

        private static string CreateNodeConversionFailureMessage(this State s, INode fromNode, string toDetail)
        {
            var fromNodeStr = string.Format("{0} node", fromNode.Kind);
            switch (fromNode.Kind)
            {
                case NodeKind.Boolean:
                    fromNodeStr = string.Format("{0} ({1})", fromNodeStr, ((BooleanNode)fromNode).Value);
                    break;

                case NodeKind.Integer:
                    fromNodeStr = string.Format("{0} ({1})", fromNodeStr, ((IntegerNode)fromNode).Value);
                    break;

                case NodeKind.Float:
                    fromNodeStr = string.Format("{0} ({1})", fromNodeStr, ((FloatNode)fromNode).Value);
                    break;
            }

            return s.CreateMessage("{0} cannot convert to {1}", fromNodeStr, toDetail);
        }

        public static string CreateTypeConversionFailureMessage<T>(this State s,
                                                                   T fromValue,
                                                                   Type toType,
                                                                   string reason = null)
        {
            var introStr = string.Format("{0} value", typeof(T).ToString());

            var kind = Node.KindOfType(typeof(T));
            switch (kind)
            {
                case NodeKind.Boolean:
                case NodeKind.Integer:
                case NodeKind.Float:
                    introStr = string.Format("{0} ({1})", introStr, fromValue);
                    break;
            }

            var msgBase = string.Format("{0} cannot convert to {1}", introStr, toType);
            if (reason != null)
            {
                msgBase = string.Format("{0} (Reason: {1})", msgBase, reason);
            }
            return s.CreateMessage(msgBase);
        }
    }
}
