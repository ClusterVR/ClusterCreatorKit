namespace ClusterVR.CreatorKit.Editor.Validator
{
    public static class IdValidateErrorMessages
    {
        public static string LengthErrorMessage(string componentName, string id)
        {
            return $"{componentName}のIdが長すぎます。 Id: {id} 最大値: {Constants.Component.MaxIdLength}";
        }

        public static string RegexErrorMessage(string componentName, string id)
        {
            return $"{componentName}のIdに使用できない文字が含まれています。Idには英数字とアポストロフィ・カンマ・ハイフン・ピリオド・アンダースコアのみが使用可能です。 Id: {id}";
        }
    }
}
