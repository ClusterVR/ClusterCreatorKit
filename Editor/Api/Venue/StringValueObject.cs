namespace ClusterVR.CreatorKit.Editor.Api.Venue
{
    public abstract class StringValueObject
    {
        bool Equals(StringValueObject other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != GetType())
            {
                return false;
            }
            return Equals((StringValueObject) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(StringValueObject left, StringValueObject right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(StringValueObject left, StringValueObject right)
        {
            return !Equals(left, right);
        }

        public string Value { get; }

        protected StringValueObject(string value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
