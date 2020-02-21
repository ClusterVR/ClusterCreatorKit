namespace ClusterVR.CreatorKit.Editor
{
    /// 認証可能なことがAPIで確認済みのユーザー情報
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
