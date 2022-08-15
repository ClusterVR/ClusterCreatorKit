using System;
using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Editor.Api.Analytics;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Builder;
using ClusterVR.CreatorKit.Proto;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

namespace ClusterVR.CreatorKit.Editor.Analytics
{
    [InitializeOnLoad]
    public static class EventSender
    {
        const int IntervalSec = 60 * 5;

        sealed class SessionInfo : ScriptableSingleton<SessionInfo>
        {
            [SerializeField] string sessionId;
            [SerializeField] double lastSentAt;

            public string SessionId
            {
                get
                {
                    if (string.IsNullOrEmpty(sessionId))
                    {
                        sessionId = Guid.NewGuid().ToString();
                    }

                    return sessionId;
                }
            }

            public double LastSentAt
            {
                get => lastSentAt;
                set => lastSentAt = value;
            }
        }

        static EventSender()
        {
            EditorApplication.update += Update;
        }

        static void Update()
        {
            if (!EditorPrefsUtils.EnableSendingAnalyticsData)
            {
                return;
            }

            var now = EditorApplication.timeSinceStartup;
            if (now - SessionInfo.instance.LastSentAt < IntervalSec)
            {
                return;
            }

            var payload = new CreatorKitEventPayload
            {
                TmpUserId = GetOrCreateTmpUserId(),
                SessionId = SessionInfo.instance.SessionId,
                Environment = CreateEnvironment(),
                EventType = CreatorKitEventPayload.Types.EventType.Ping
            };

            _ = APIServiceClient.PostAnalyticsEvent(new EventPayload(payload), EditorPrefsUtils.SavedAccessToken.Token,
                default);

            SessionInfo.instance.LastSentAt = now;
        }

        static string GetOrCreateTmpUserId()
        {
            var tmpUserId = EditorPrefsUtils.TmpUserId;
            if (!string.IsNullOrEmpty(tmpUserId))
            {
                return tmpUserId;
            }
            tmpUserId = Guid.NewGuid().ToString();
            EditorPrefsUtils.TmpUserId = tmpUserId;
            return tmpUserId;
        }

        static CreatorKitEnvironment CreateEnvironment()
        {
            return new CreatorKitEnvironment
            {
                BatteryStatus = SystemInfo.batteryStatus.ToString(),
                DeviceModel = SystemInfo.deviceModel,
                DeviceType = SystemInfo.deviceType.ToString(),
                DeviceUniqueIdentifier = SystemInfo.deviceUniqueIdentifier,
                GraphicsDeviceName = SystemInfo.graphicsDeviceName,
                GraphicsDeviceType = SystemInfo.graphicsDeviceType.ToString(),
                GraphicsDeviceVendor = SystemInfo.graphicsDeviceVendor,
                GraphicsDeviceVersion = SystemInfo.graphicsDeviceVersion,
                GraphicsMemorySize = SystemInfo.graphicsMemorySize,
                GraphicsMultiThreaded = SystemInfo.graphicsMultiThreaded,
                GraphicsShaderLevel = SystemInfo.graphicsShaderLevel,
                OperatingSystem = SystemInfo.operatingSystem,
                OperatingSystemFamily = SystemInfo.operatingSystemFamily.ToString(),
                ProcessorCount = SystemInfo.processorCount,
                ProcessorFrequency = SystemInfo.processorFrequency,
                ProcessorType = SystemInfo.processorType,
                SystemMemorySize = SystemInfo.systemMemorySize,
                IsFocused = Application.isFocused,
                IsPlaying = Application.isPlaying,
                Platform = Application.platform.ToString(),
                SystemLanguage = Application.systemLanguage.ToString(),
                UnityVersion = Application.unityVersion,
                XrDeviceIsPresent = IsXRDevicePresent(),
                XrDeviceModel = InputDevices.GetDeviceAtXRNode(XRNode.Head).name ?? ""
            };
        }

        static bool IsXRDevicePresent()
        {
            var xrDisplaySubsystems = new List<XRDisplaySubsystem>();
            SubsystemManager.GetInstances(xrDisplaySubsystems);
            return xrDisplaySubsystems.Any(xrDisplay => xrDisplay.running);
        }
    }
}
