namespace ClusterVR.CreatorKit.Editor.Api.User
{
    public readonly struct UserInfo
    {
        public readonly string Username;
        public readonly string VerifiedToken;

        public UserInfo(string username, string verifiedToken)
        {
            Username = username;
            VerifiedToken = verifiedToken;
        }
    }
}
