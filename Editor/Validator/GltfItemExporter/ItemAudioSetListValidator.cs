using System.Collections.Generic;
using System.Text.RegularExpressions;
using ClusterVR.CreatorKit.Item;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Validator.GltfItemExporter
{
    public static class ItemAudioSetListValidator
    {
        const int MaxChannels = 2;
        const int MaxSampleRates = 48000;
        const float MaxLength = 5f;
        const int MaxCount = 5;

        public static IEnumerable<ValidationMessage> Validate(IItemAudioSetList itemAudioSetList)
        {
            var messages = new List<ValidationMessage>();
            var emptyIdReported = false;
            var ids = new HashSet<string>();
            var collidedIds = new HashSet<string>();
            var emptyAudioReportedIds = new HashSet<string>();
            var audios = new HashSet<AudioClip>();
            var count = 0;
            foreach (var set in itemAudioSetList.ItemAudioSets)
            {
                count++;
                var id = set.Id;

                if (string.IsNullOrEmpty(id))
                {
                    if (!emptyIdReported)
                    {
                        messages.Add(new ValidationMessage($"ItemAudioSetListのIdが空です。有効なIdを設定してください。", ValidationMessage.MessageType.Error));
                        emptyIdReported = true;
                    }
                    continue;
                }

                if (ids.Add(id))
                {
                    if (!Constants.Component.ValidIdCharactersRegex.IsMatch(id))
                    {
                        messages.Add(new ValidationMessage($"ItemAudioSetListのIdに使用できない文字が含まれています。Idには英数字とアポストロフィ・カンマ・ハイフン・ピリオド・アンダースコアのみが使用可能です。 Id: {id}", ValidationMessage.MessageType.Error));
                    }

                    if (id.Length > Constants.Component.MaxIdLength)
                    {
                        messages.Add(new ValidationMessage($"ItemAudioSetListのIdが長すぎます。 Id: {id} 最大値: {Constants.Component.MaxIdLength}", ValidationMessage.MessageType.Error));
                    }
                }
                else
                {
                    collidedIds.Add(id);
                }

                var audio = set.AudioClip;
                if (audio == null)
                {
                    if (emptyAudioReportedIds.Add(id))
                    {
                        messages.Add(new ValidationMessage($"AudioClipがみつかりませんでした。ItemAudioSetにAudioClipを設定してください。(Id: {id})", ValidationMessage.MessageType.Error));
                    }
                }
                else if (audios.Add(audio))
                {
                    if (audio.channels > MaxChannels)
                    {
                        messages.Add(new ValidationMessage($"AudioClipのChannelが多すぎます。 現在値: {audio.channels} 最大値: {MaxChannels} (Name: {audio.name})", ValidationMessage.MessageType.Error));
                    }

                    if (audio.frequency > MaxSampleRates)
                    {
                        messages.Add(new ValidationMessage($"AudioClipのSample Rateが高すぎます。 現在値: {audio.frequency} 最大値: {MaxSampleRates} (Name: {audio.name}) (Sample RateはAudio ClipのImport SettingsのSample Rate SettingをOverride Sample Rateにすることで変更が可能です。)", ValidationMessage.MessageType.Error));
                    }

                    if (audio.length > MaxLength)
                    {
                        messages.Add(new ValidationMessage($"AudioClipが長すぎます。 現在値: {audio.length: 0.00}秒 最大値: {MaxLength}秒 (Name: {audio.name})", ValidationMessage.MessageType.Error));
                    }
                }
            }

            if (count > MaxCount)
            {
                messages.Add(new ValidationMessage($"ItemAudioSetの数が多すぎます。 現在値: {count} 最大値: {MaxCount}", ValidationMessage.MessageType.Error));
            }

            if (collidedIds.Count > 0)
            {
                messages.Add(new ValidationMessage($"ItemAudioSetListのIdが重複しています。 Id: {string.Join(", ", collidedIds)}", ValidationMessage.MessageType.Error));
            }

            return messages;
        }
    }
}
