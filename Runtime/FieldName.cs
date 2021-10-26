using System;
using System.Linq;

namespace ClusterVR.CreatorKit
{
    public readonly struct FieldName : IEquatable<FieldName>
    {
        const char Delimiter = '.';

        public static readonly FieldName None = new FieldName();

        string Value { get; }
        string Suffix => Delimiter + Value;

        public FieldName(string key)
        {
            Value = key.Split(Delimiter).Last();
        }

        public string BuildKey(string rawKey)
        {
            if (string.IsNullOrEmpty(Value)) return rawKey;
            return rawKey + Suffix;
        }

        public bool Equals(FieldName other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is FieldName other && Equals(other);
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }
    }
}
