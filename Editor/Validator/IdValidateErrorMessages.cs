using ClusterVR.CreatorKit.Translation;

namespace ClusterVR.CreatorKit.Editor.Validator
{
    public static class IdValidateErrorMessages
    {
        public static string LengthErrorMessage(string componentName, string id)
        {
            return TranslationUtility.GetMessage(TranslationTable.cck_component_id_too_long, componentName, id, Constants.Component.MaxIdLength);
        }

        public static string RegexErrorMessage(string componentName, string id)
        {
            return TranslationUtility.GetMessage(TranslationTable.cck_invalid_component_id_characters, componentName, id);
        }
    }
}
