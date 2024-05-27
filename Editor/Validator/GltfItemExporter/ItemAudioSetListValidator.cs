using System.Collections.Generic;
using System.Text.RegularExpressions;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Translation;
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
                        messages.Add(new ValidationMessage(TranslationTable.cck_itemaudiosetlist_empty_id, ValidationMessage.MessageType.Error));
                        emptyIdReported = true;
                    }
                    continue;
                }

                if (ids.Add(id))
                {
                    if (!Constants.Component.ValidIdCharactersRegex.IsMatch(id))
                    {
                        messages.Add(new ValidationMessage(TranslationUtility.GetMessage(TranslationTable.cck_itemaudiosetlist_invalid_characters, id), ValidationMessage.MessageType.Error));
                    }

                    if (id.Length > Constants.Component.MaxIdLength)
                    {
                        messages.Add(new ValidationMessage(TranslationUtility.GetMessage(TranslationTable.cck_itemaudiosetlist_id_length, id, Constants.Component.MaxIdLength), ValidationMessage.MessageType.Error));
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
                        messages.Add(new ValidationMessage(TranslationUtility.GetMessage(TranslationTable.cck_audioclip_not_found, id), ValidationMessage.MessageType.Error));
                    }
                }
                else if (audios.Add(audio))
                {
                    if (audio.channels > MaxChannels)
                    {
                        messages.Add(new ValidationMessage(TranslationUtility.GetMessage(TranslationTable.cck_audioclip_channels_limit, audio.channels, MaxChannels, audio.name), ValidationMessage.MessageType.Error));
                    }

                    if (audio.frequency > MaxSampleRates)
                    {
                        messages.Add(new ValidationMessage(TranslationUtility.GetMessage(TranslationTable.cck_audioclip_samplerate_high, audio.frequency, MaxSampleRates, audio.name), ValidationMessage.MessageType.Error));
                    }

                    if (audio.length > MaxLength)
                    {
                        messages.Add(new ValidationMessage(TranslationUtility.GetMessage(TranslationTable.cck_audioclip_length_limit, audio.length, MaxLength, audio.name), ValidationMessage.MessageType.Error));
                    }
                }
            }

            if (count > MaxCount)
            {
                messages.Add(new ValidationMessage(TranslationUtility.GetMessage(TranslationTable.cck_too_many_itemaudiosets, count, MaxCount), ValidationMessage.MessageType.Error));
            }

            if (collidedIds.Count > 0)
            {
                messages.Add(new ValidationMessage(TranslationUtility.GetMessage(TranslationTable.cck_itemaudiosetlist_duplicate_id, string.Join(", ", collidedIds)), ValidationMessage.MessageType.Error));
            }

            return messages;
        }
    }
}
