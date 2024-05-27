using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Translation;

namespace ClusterVR.CreatorKit.Editor.Validator.GltfItemExporter
{
    public static class HumanoidAnimationListValidator
    {
        const int MaxCount = 10;
        const float MaxKeyFrames = 50000;

        public static IEnumerable<ValidationMessage> Validate(IHumanoidAnimationList humanoidAnimationList)
        {
            var messages = new List<ValidationMessage>();
            var emptyIdReported = false;
            var ids = new HashSet<string>();
            var collidedIds = new HashSet<string>();
            var emptyHumanoidAnimationReportedIds = new HashSet<string>();
            var count = 0;
            var totalKeyFrames = 0;
            foreach (var entry in humanoidAnimationList.HumanoidAnimations)
            {
                count++;
                var id = entry.Id;

                if (string.IsNullOrEmpty(id))
                {
                    if (!emptyIdReported)
                    {
                        messages.Add(new ValidationMessage(TranslationTable.cck_humanoidanimationlist_empty_id, ValidationMessage.MessageType.Error));
                        emptyIdReported = true;
                    }
                }
                else
                {
                    if (ids.Add(id))
                    {
                        if (!Constants.Component.ValidIdCharactersRegex.IsMatch(id))
                        {
                            messages.Add(new ValidationMessage(TranslationUtility.GetMessage(TranslationTable.cck_humanoidanimationlist_invalid_characters, id), ValidationMessage.MessageType.Error));
                        }

                        if (id.Length > Constants.Component.MaxIdLength)
                        {
                            messages.Add(new ValidationMessage(TranslationUtility.GetMessage(TranslationTable.cck_humanoidanimationlist_id_length, id, Constants.Component.MaxIdLength), ValidationMessage.MessageType.Error));
                        }
                    }
                    else
                    {
                        collidedIds.Add(id);
                    }
                }

                var humanoidAnimation = entry.HumanoidAnimation;
                if (humanoidAnimation == null)
                {
                    if (emptyHumanoidAnimationReportedIds.Add(id))
                    {
                        messages.Add(new ValidationMessage(TranslationUtility.GetMessage(TranslationTable.cck_animation_not_found, id), ValidationMessage.MessageType.Error));
                    }
                }
                else
                {
                    totalKeyFrames += humanoidAnimation.Curves.Select(curve => curve.Curve.keys.Length).Sum();
                }
            }

            if (count > MaxCount)
            {
                messages.Add(new ValidationMessage(TranslationUtility.GetMessage(TranslationTable.cck_too_many_humanoidanimations, count, MaxCount), ValidationMessage.MessageType.Error));
            }

            if (totalKeyFrames > MaxKeyFrames)
            {
                messages.Add(new ValidationMessage(TranslationUtility.GetMessage(TranslationTable.cck_humanoidanimationlist_keyframes_limit, totalKeyFrames, MaxKeyFrames), ValidationMessage.MessageType.Error));
            }

            if (collidedIds.Count > 0)
            {
                messages.Add(new ValidationMessage(TranslationUtility.GetMessage(TranslationTable.cck_humanoidanimationlist_duplicate_id, string.Join(", ", collidedIds)), ValidationMessage.MessageType.Error));
            }

            return messages;
        }
    }
}
