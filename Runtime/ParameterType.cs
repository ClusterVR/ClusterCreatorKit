namespace ClusterVR.CreatorKit
{
    public enum ParameterType
    {
        Signal,
        Bool,
        Float,
        Integer,
        Vector2,
        Vector3,
        Double,
    }

    public static class ParameterTypeExtensions
    {
        public static bool TryGetCommonType(ParameterType type1, ParameterType type2, out ParameterType commonType)
        {
            if (type1 == type2)
            {
                commonType = type1;
                return true;
            }

            if (type1 == ParameterType.Vector2)
            {
                if (type2 == ParameterType.Vector3)
                {
                    commonType = ParameterType.Vector3;
                    return true;
                }
                else
                {
                    commonType = default;
                    return false;
                }
            }
            else if (type1 == ParameterType.Vector3)
            {
                if (type2 == ParameterType.Vector2)
                {
                    commonType = ParameterType.Vector3;
                    return true;
                }
                else
                {
                    commonType = default;
                    return false;
                }
            }
            else if (type2 == ParameterType.Vector2 || type2 == ParameterType.Vector3)
            {
                    commonType = default;
                    return false;
            }

            if (type1 == ParameterType.Signal || type1 == ParameterType.Double || type2 == ParameterType.Signal || type2 == ParameterType.Double)
            {
                commonType = ParameterType.Double;
                return true;
            }
            else if (type1 == ParameterType.Float || type2 == ParameterType.Float)
            {
                commonType = ParameterType.Float;
                return true;
            }
            else
            {
                commonType = ParameterType.Integer;
                return true;
            }
        }

        public static bool CanCastToValue(this ParameterType parameterType)
        {
            return parameterType == ParameterType.Signal ||
                parameterType == ParameterType.Bool || parameterType == ParameterType.Float ||
                parameterType == ParameterType.Integer || parameterType == ParameterType.Double;
        }

        public static bool CanCastToVector(this ParameterType parameterType)
        {
            return parameterType == ParameterType.Vector2 || parameterType == ParameterType.Vector3;
        }
    }
}
