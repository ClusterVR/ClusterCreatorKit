namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public sealed class AuthenticationInfo
    {
        public string RawValue { get; }
        public string Host { get; }
        public string Token { get; }
        public bool IsValid { get; }
        public string ValidationError { get; }

        public AuthenticationInfo(string raw)
        {
            RawValue = raw;
            var split = raw.Split(':');
            if (split.Length > 1)
            {
                Host = split[0];
                Token = split[1];
            }
            else
            {
                Token = raw;
            }

            var validationError = "";
            IsValid = IsValidToken(Token, ref validationError);
            ValidationError = validationError;
        }

        static bool IsValidToken(string token, ref string errorMessage)
        {
            if (string.IsNullOrEmpty(token))
            {
                return false;
            }
            if (token.Length != 64)
            {
                errorMessage = "不正なアクセストークンです";
                return false;
            }

            return true;
        }
    }
}
