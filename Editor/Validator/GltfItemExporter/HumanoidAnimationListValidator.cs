using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Item;

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
                        messages.Add(new ValidationMessage($"HumanoidAnimationListのIdが空です。有効なIdを設定してください。", ValidationMessage.MessageType.Error));
                        emptyIdReported = true;
                    }
                }
                else
                {
                    if (ids.Add(id))
                    {
                        if (!Constants.Component.ValidIdCharactersRegex.IsMatch(id))
                        {
                            messages.Add(new ValidationMessage($"HumanoidAnimationListのIdに使用できない文字が含まれています。Idには英数字とアポストロフィ・カンマ・ハイフン・ピリオド・アンダースコアのみが使用可能です。 Id: {id}", ValidationMessage.MessageType.Error));
                        }

                        if (id.Length > Constants.Component.MaxIdLength)
                        {
                            messages.Add(new ValidationMessage($"HumanoidAnimationListのIdが長すぎます。 Id: {id} 最大値: {Constants.Component.MaxIdLength}", ValidationMessage.MessageType.Error));
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
                        messages.Add(new ValidationMessage($"Animationがみつかりませんでした。HumanoidAnimationにAnimation Clipを設定してください。(Id: {id})", ValidationMessage.MessageType.Error));
                    }
                }
                else
                {
                    totalKeyFrames += humanoidAnimation.Curves.Select(curve => curve.Curve.keys.Length).Sum();
                }
            }

            if (count > MaxCount)
            {
                messages.Add(new ValidationMessage($"HumanoidAnimationの数が多すぎます。 現在値: {count} 最大値: {MaxCount}", ValidationMessage.MessageType.Error));
            }

            if (totalKeyFrames > MaxKeyFrames)
            {
                messages.Add(new ValidationMessage($"HumanoidAnimationListのキーフレームが多すぎます。 現在値: {totalKeyFrames} 最大値: {MaxKeyFrames})", ValidationMessage.MessageType.Error));
            }

            if (collidedIds.Count > 0)
            {
                messages.Add(new ValidationMessage($"HumanoidAnimationListのIdが重複しています。 Id: {string.Join(", ", collidedIds)}", ValidationMessage.MessageType.Error));
            }

            return messages;
        }
    }
}
