using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Editor.Api.Analytics;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Proto;
using UnityEngine;
using UnityEngine.XR;

namespace ClusterVR.CreatorKit.Editor.Analytics
{
    public static class EventSender
    {
        public static void Ping(string accessToken, string sessionId, string tmpUserId)
        {
            var payload = new CreatorKitEventPayload
            {
                TmpUserId = tmpUserId,
                SessionId = sessionId,
                Environment = CreateEnvironment(),
                EventType = CreatorKitEventPayload.Types.EventType.Ping
            };

            _ = APIServiceClient.PostAnalyticsEvent(new EventPayload(payload), accessToken,
                default);
            PanamaLogger.LogCckPing(Convert(payload));
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

        static CckPing Convert(CreatorKitEventPayload payload)
        {
            var environment = payload.Environment;
            return new CckPing
            {
                SessionId = payload.SessionId,
                Environment = new CckPing.Types.CckPingCreatorKitEnvironment
                {
                    BatteryStatus = environment.BatteryStatus,
                    DeviceModel = environment.DeviceModel,
                    DeviceType = environment.DeviceType,
                    DeviceUniqueIdentifier = environment.DeviceUniqueIdentifier,
                    GraphicsDeviceName = environment.GraphicsDeviceName,
                    GraphicsDeviceType = environment.GraphicsDeviceType,
                    GraphicsDeviceVendor = environment.GraphicsDeviceVendor,
                    GraphicsDeviceVersion = environment.GraphicsDeviceVersion,
                    GraphicsMemorySize = environment.GraphicsMemorySize,
                    GraphicsMultiThreaded = environment.GraphicsMultiThreaded,
                    GraphicsShaderLevel = environment.GraphicsShaderLevel,
                    OperatingSystem = environment.OperatingSystem,
                    OperatingSystemFamily = environment.OperatingSystemFamily,
                    ProcessorCount = environment.ProcessorCount,
                    ProcessorFrequency = environment.ProcessorFrequency,
                    ProcessorType = environment.ProcessorType,
                    SystemMemorySize = environment.SystemMemorySize,
                    IsFocused = environment.IsFocused,
                    IsPlaying = environment.IsPlaying,
                    Platform = environment.Platform,
                    SystemLanguage = environment.SystemLanguage,
                    UnityVersion = environment.UnityVersion,
                    XrDeviceIsPresent = environment.XrDeviceIsPresent,
                    XrDeviceModel = environment.XrDeviceModel
                }
            };
        }
    }
}
