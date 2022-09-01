using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ClusterVR.CreatorKit.ProductUgc
{
    [Serializable]
    public struct ProductId : IEquatable<ProductId>
    {
        static Regex validationRegex = new Regex(@"^[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12}$");
        [SerializeField] string value;
        public string Value => value;

        public ProductId(string value)
        {
            this.value = value;
        }

        public override string ToString() => value;

        public bool Equals(ProductId other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is ProductId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public bool IsValid() => ProductId.IsValid(value);

        public static bool IsValid(string value) => validationRegex.Match(value).Success;
    }
}
