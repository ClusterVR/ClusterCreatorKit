using System;
using System.Security.Cryptography;
using UnityEngine;

namespace ClusterVR.CreatorKit.Item
{
    [Serializable]
    public struct ItemTemplateId : IEquatable<ItemTemplateId>
    {
        [SerializeField] ulong value;
        public ulong Value => value;

        static RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();

        public ItemTemplateId(ulong value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public bool Equals(ItemTemplateId other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is ItemTemplateId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static ItemTemplateId Create()
        {
            var bs = new byte[sizeof(ulong)];
            rngCryptoServiceProvider.GetBytes(bs);
            var value = BitConverter.ToUInt64(bs, 0);
            return new ItemTemplateId(value);
        }
    }
}