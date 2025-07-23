using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using ClusterVR.CreatorKit.Editor.RPC.Client;
using ClusterVR.CreatorKit.Proto;

namespace ClusterVR.CreatorKit.Editor.Analytics
{
    public static class PanamaLogger
    {
        static readonly Queue<PanamaEvent> PanamaEvents = new();
        static string UserId;
        static string TmpUserId;
        static string CreatorKitVersion;

        public static void SetUserId(string userId)
        {
            UserId = userId;
        }

        public static void SetTmpUserId(string tmpUserId)
        {
            TmpUserId = tmpUserId;
        }

        public static void SetCreatorKitVersion(string creatorKitVersion)
        {
            CreatorKitVersion = creatorKitVersion;
        }

        public static void SendEvents()
        {
            if (UserId == null || TmpUserId == null)
            {
                return;
            }
            while (PanamaEvents.TryDequeue(out var panamaEvent))
            {
                panamaEvent.EventSource = PanamaEvent.Types.EventSource.CreatorKit;

                panamaEvent.UserId = UserId;
                panamaEvent.UserPseudoId = TmpUserId;

#if UNITY_EDITOR_WIN
                panamaEvent.Platform = PanamaEvent.Types.Platform.Win;
#elif UNITY_EDITOR_OSX
                panamaEvent.Platform = PanamaEvent.Types.Platform.Mac;
#endif

                panamaEvent.DeviceType = "ClusterCreatorKit";
                panamaEvent.Ts = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                panamaEvent.AppVersion = CreatorKitVersion;

                PanamaApiClient.PostEventAsync(panamaEvent);
            }
        }

        public static void LogCckPing(CckPing cckPing)
        {
            LogEvent(new PanamaEvent
            {
                EventType = PanamaEvent.Types.EventType.CckPing,
                CckPingObject = cckPing
            });
        }

        public static void LogCckOpenUnity()
        {
            LogEvent(new PanamaEvent
            {
                EventType = PanamaEvent.Types.EventType.CckOpenUnity,
            });
        }

        public static void LogCckInstall(string version, string prevVersion)
        {
            LogEvent(new PanamaEvent
            {
                EventType = PanamaEvent.Types.EventType.CckInstall,
                CckInstallObject = new CckInstall
                {
                    Version = version,
                    PrevVersion = prevVersion
                }
            });
        }

        public static void LogCckMenuItem(MenuItemType menuItemType)
        {
            LogEvent(new PanamaEvent
            {
                EventType = PanamaEvent.Types.EventType.CckMenuItem,
                CckMenuItemObject = new CckMenuItem
                {
                    MenuItemType = menuItemType.ToString()
                }
            });
        }

        public static void LogCckOpenLink(string linkUrl, string from)
        {
            LogEvent(new PanamaEvent
            {
                EventType = PanamaEvent.Types.EventType.CckOpenLink,
                CckOpenLinkObject = new CckOpenLink
                {
                    LinkUrl = linkUrl,
                    From = from
                }
            });
        }

        public static void LogCckEditorPreviewStart()
        {
            LogEvent(new PanamaEvent
            {
                EventType = PanamaEvent.Types.EventType.CckEditorPreviewStart
            });
        }

        public static void LogCckEditorPreviewStop(ulong durationMs)
        {
            LogEvent(new PanamaEvent
            {
                EventType = PanamaEvent.Types.EventType.CckEditorPreviewStop,
                CckEditorPreviewStopObject = new CckEditorPreviewStop
                {
                    DurationMs = durationMs
                }
            });
        }

        public static void LogCckWorldUploadStart(CckWorldUploadStart cckWorldUploadStart)
        {
            LogEvent(new PanamaEvent
            {
                EventType = PanamaEvent.Types.EventType.CckWorldUploadStart,
                CckWorldUploadStartObject = cckWorldUploadStart
            });
        }

        public static void LogCckWorldUploadComplete(CckWorldUploadComplete cckWorldUploadComplete)
        {
            LogEvent(new PanamaEvent
            {
                EventType = PanamaEvent.Types.EventType.CckWorldUploadComplete,
                CckWorldUploadCompleteObject = cckWorldUploadComplete
            });
        }

        public static void LogCckNewInstall()
        {
            LogEvent(new PanamaEvent
            {
                EventType = PanamaEvent.Types.EventType.CckNewInstall
            });
        }

        public static void LogCckWorldUploadFailed(CckWorldUploadFailed cckWorldUploadFailed)
        {
            LogEvent(new PanamaEvent
            {
                EventType = PanamaEvent.Types.EventType.CckWorldUploadFailed,
                CckWorldUploadFailedObject = cckWorldUploadFailed
            });
        }

        static void LogEvent(PanamaEvent panamaEvent)
        {
            if (Api.RPC.Constants.IsOverridingHost)
            {
                return;
            }
            PanamaEvents.Enqueue(panamaEvent);
        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        public enum MenuItemType
        {
            Assets_ClusterScript,
            ClusterPreview_ControlWindow,
            ClusterPreview_DisableVR,
            ClusterPreview_EnableVR,
            ClusterPreview_PackageInstaller,
            ClusterPreview_Settings,
            ClusterPreview_WebTriggerWindow,
            Cluster_About,
            Cluster_ExternalCallURL,
            Cluster_ExternalEndpoint,
            Cluster_FetchCraftItemInfo,
            Cluster_ScriptLogConsole,
            Cluster_Settings,
            Cluster_UploadAccessory,
            Cluster_UploadCraftItem,
            Cluster_UploadWorld,
            GameObject_Mirror,
            GameObject_PlayerLocalUI,
        }
    }
}
