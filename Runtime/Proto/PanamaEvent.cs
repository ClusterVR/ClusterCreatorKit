#pragma warning disable 1591, 0612, 3021, 8981
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace ClusterVR.CreatorKit.Proto {

  public static partial class PanamaEventReflection {

    #region Descriptor
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static PanamaEventReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CiRwYW5hbWEvY3JlYXRvcmtpdC9wYW5hbWFfZXZlbnQucHJvdG8SEXBhbmFt",
            "YS5jcmVhdG9ya2l0IuQLCgtQYW5hbWFFdmVudBIOCgJ0cxgBIAEoA1ICdHMS",
            "SAoKZXZlbnRfdHlwZRgCIAEoDjIoLnBhbmFtYS5jcmVhdG9ya2l0LlBhbmFt",
            "YUV2ZW50LkV2ZW50VHlwZVIKZXZlbnRfdHlwZRIYCgd1c2VyX2lkGAMgASgJ",
            "Ugd1c2VyX2lkEiYKDnVzZXJfcHNldWRvX2lkGAggASgJUg51c2VyX3BzZXVk",
            "b19pZBJDCghwbGF0Zm9ybRgEIAEoDjInLnBhbmFtYS5jcmVhdG9ya2l0LlBh",
            "bmFtYUV2ZW50LlBsYXRmb3JtUghwbGF0Zm9ybRIgCgtkZXZpY2VfdHlwZRgF",
            "IAEoCVILZGV2aWNlX3R5cGUSIAoLYXBwX3ZlcnNpb24YBiABKAlSC2FwcF92",
            "ZXJzaW9uEiQKDWJ1aWxkX3ZlcnNpb24YByABKAlSDWJ1aWxkX3ZlcnNpb24S",
            "TgoMZXZlbnRfc291cmNlGAkgASgOMioucGFuYW1hLmNyZWF0b3JraXQuUGFu",
            "YW1hRXZlbnQuRXZlbnRTb3VyY2VSDGV2ZW50X3NvdXJjZRIaCghpc191bml0",
            "eRgKIAEoCFIIaXNfdW5pdHkSHgoKdXNlcl9hZ2VudBgLIAEoCVIKdXNlcl9h",
            "Z2VudBI2Cg9jY2tfcGluZ19vYmplY3QYsgEgASgLMhoucGFuYW1hLmNyZWF0",
            "b3JraXQuQ2NrUGluZ0gAEjwKEmNja19pbnN0YWxsX29iamVjdBizASABKAsy",
            "HS5wYW5hbWEuY3JlYXRvcmtpdC5DY2tJbnN0YWxsSAASPwoUY2NrX21lbnVf",
            "aXRlbV9vYmplY3QYtAEgASgLMh4ucGFuYW1hLmNyZWF0b3JraXQuQ2NrTWVu",
            "dUl0ZW1IABI/ChRjY2tfb3Blbl9saW5rX29iamVjdBi1ASABKAsyHi5wYW5h",
            "bWEuY3JlYXRvcmtpdC5DY2tPcGVuTGlua0gAElIKHmNja19lZGl0b3JfcHJl",
            "dmlld19zdG9wX29iamVjdBi2ASABKAsyJy5wYW5hbWEuY3JlYXRvcmtpdC5D",
            "Y2tFZGl0b3JQcmV2aWV3U3RvcEgAElAKHWNja193b3JsZF91cGxvYWRfc3Rh",
            "cnRfb2JqZWN0GLcBIAEoCzImLnBhbmFtYS5jcmVhdG9ya2l0LkNja1dvcmxk",
            "VXBsb2FkU3RhcnRIABJWCiBjY2tfd29ybGRfdXBsb2FkX2NvbXBsZXRlX29i",
            "amVjdBi4ASABKAsyKS5wYW5hbWEuY3JlYXRvcmtpdC5DY2tXb3JsZFVwbG9h",
            "ZENvbXBsZXRlSAASUgoeY2NrX3dvcmxkX3VwbG9hZF9mYWlsZWRfb2JqZWN0",
            "GLwBIAEoCzInLnBhbmFtYS5jcmVhdG9ya2l0LkNja1dvcmxkVXBsb2FkRmFp",
            "bGVkSAAiswIKCUV2ZW50VHlwZRIaChZFVkVOVF9UWVBFX1VOU1BFQ0lGSUVE",
            "EAASDQoIQ0NLX1BJTkcQxgESEwoOQ0NLX09QRU5fVU5JVFkQxwESEAoLQ0NL",
            "X0lOU1RBTEwQyAESEgoNQ0NLX01FTlVfSVRFTRDJARISCg1DQ0tfT1BFTl9M",
            "SU5LEMoBEh0KGENDS19FRElUT1JfUFJFVklFV19TVEFSVBDLARIcChdDQ0tf",
            "RURJVE9SX1BSRVZJRVdfU1RPUBDMARIbChZDQ0tfV09STERfVVBMT0FEX1NU",
            "QVJUEM0BEh4KGUNDS19XT1JMRF9VUExPQURfQ09NUExFVEUQzgESFAoPQ0NL",
            "X05FV19JTlNUQUxMENIBEhwKF0NDS19XT1JMRF9VUExPQURfRkFJTEVEENMB",
            "IjYKCFBsYXRmb3JtEhgKFFBMQVRGT1JNX1VOU1BFQ0lGSUVEEAASBwoDV0lO",
            "EAMSBwoDTUFDEAQiPAoLRXZlbnRTb3VyY2USHAoYRVZFTlRfU09VUkNFX1VO",
            "U1BFQ0lGSUVEEAASDwoLQ1JFQVRPUl9LSVQQBEIICgZvYmplY3Qi8QkKB0Nj",
            "a1BpbmcSHQoKc2Vzc2lvbl9pZBgCIAEoCVIJc2Vzc2lvbklEElkKC2Vudmly",
            "b25tZW50GAMgASgLMjcucGFuYW1hLmNyZWF0b3JraXQuQ2NrUGluZy5DY2tQ",
            "aW5nQ3JlYXRvcktpdEVudmlyb25tZW50UgtlbnZpcm9ubWVudBrGCAocQ2Nr",
            "UGluZ0NyZWF0b3JLaXRFbnZpcm9ubWVudBIlCg5iYXR0ZXJ5X3N0YXR1cxgB",
            "IAEoCVINYmF0dGVyeVN0YXR1cxIhCgxkZXZpY2VfbW9kZWwYAiABKAlSC2Rl",
            "dmljZU1vZGVsEh8KC2RldmljZV90eXBlGAMgASgJUgpkZXZpY2VUeXBlEjgK",
            "GGRldmljZV91bmlxdWVfaWRlbnRpZmllchgEIAEoCVIWZGV2aWNlVW5pcXVl",
            "SWRlbnRpZmllchIwChRncmFwaGljc19kZXZpY2VfbmFtZRgFIAEoCVISZ3Jh",
            "cGhpY3NEZXZpY2VOYW1lEjAKFGdyYXBoaWNzX2RldmljZV90eXBlGAYgASgJ",
            "UhJncmFwaGljc0RldmljZVR5cGUSNAoWZ3JhcGhpY3NfZGV2aWNlX3ZlbmRv",
            "chgHIAEoCVIUZ3JhcGhpY3NEZXZpY2VWZW5kb3ISNgoXZ3JhcGhpY3NfZGV2",
            "aWNlX3ZlcnNpb24YCCABKAlSFWdyYXBoaWNzRGV2aWNlVmVyc2lvbhIwChRn",
            "cmFwaGljc19tZW1vcnlfc2l6ZRgJIAEoBVISZ3JhcGhpY3NNZW1vcnlTaXpl",
            "EjYKF2dyYXBoaWNzX211bHRpX3RocmVhZGVkGAogASgIUhVncmFwaGljc011",
            "bHRpVGhyZWFkZWQSMgoVZ3JhcGhpY3Nfc2hhZGVyX2xldmVsGAsgASgFUhNn",
            "cmFwaGljc1NoYWRlckxldmVsEikKEG9wZXJhdGluZ19zeXN0ZW0YDCABKAlS",
            "D29wZXJhdGluZ1N5c3RlbRI2ChdvcGVyYXRpbmdfc3lzdGVtX2ZhbWlseRgN",
            "IAEoCVIVb3BlcmF0aW5nU3lzdGVtRmFtaWx5EicKD3Byb2Nlc3Nvcl9jb3Vu",
            "dBgOIAEoBVIOcHJvY2Vzc29yQ291bnQSLwoTcHJvY2Vzc29yX2ZyZXF1ZW5j",
            "eRgPIAEoBVIScHJvY2Vzc29yRnJlcXVlbmN5EiUKDnByb2Nlc3Nvcl90eXBl",
            "GBAgASgJUg1wcm9jZXNzb3JUeXBlEiwKEnN5c3RlbV9tZW1vcnlfc2l6ZRgR",
            "IAEoBVIQc3lzdGVtTWVtb3J5U2l6ZRIdCgppc19mb2N1c2VkGBIgASgIUglp",
            "c0ZvY3VzZWQSHQoKaXNfcGxheWluZxgTIAEoCFIJaXNQbGF5aW5nEhoKCHBs",
            "YXRmb3JtGBQgASgJUghwbGF0Zm9ybRInCg9zeXN0ZW1fbGFuZ3VhZ2UYFSAB",
            "KAlSDnN5c3RlbUxhbmd1YWdlEiMKDXVuaXR5X3ZlcnNpb24YFiABKAlSDHVu",
            "aXR5VmVyc2lvbhIvChR4cl9kZXZpY2VfaXNfcHJlc2VudBgXIAEoCFIReHJE",
            "ZXZpY2VJc1ByZXNlbnQSJgoPeHJfZGV2aWNlX21vZGVsGBggASgJUg14ckRl",
            "dmljZU1vZGVsSgQIARACSgQIBBAFUgt0bXBfdXNlcl9pZFIKZXZlbnRfdHlw",
            "ZSJKCgpDY2tJbnN0YWxsEhgKB3ZlcnNpb24YASABKAlSB3ZlcnNpb24SIgoM",
            "cHJldl92ZXJzaW9uGAIgASgJUgxwcmV2X3ZlcnNpb24iNQoLQ2NrTWVudUl0",
            "ZW0SJgoObWVudV9pdGVtX3R5cGUYASABKAlSDm1lbnVfaXRlbV90eXBlIj0K",
            "C0Nja09wZW5MaW5rEhoKCGxpbmtfdXJsGAEgASgJUghsaW5rX3VybBISCgRm",
            "cm9tGAIgASgJUgRmcm9tIjgKFENja0VkaXRvclByZXZpZXdTdG9wEiAKC2R1",
            "cmF0aW9uX21zGAEgASgEUgtkdXJhdGlvbl9tcyLfAQoTQ2NrV29ybGRVcGxv",
            "YWRTdGFydBIYCgdpc19iZXRhGAEgASgIUgdpc19iZXRhEh4KCmlzX3ByZXZp",
            "ZXcYAiABKAhSCmlzX3ByZXZpZXcSIAoLcHJldmlld193aW4YAyABKAhSC3By",
            "ZXZpZXdfd2luEiAKC3ByZXZpZXdfbWFjGAQgASgIUgtwcmV2aWV3X21hYxIo",
            "Cg9wcmV2aWV3X2FuZHJvaWQYBSABKAhSD3ByZXZpZXdfYW5kcm9pZBIgCgtw",
            "cmV2aWV3X2lvcxgGIAEoCFILcHJldmlld19pb3MiuAYKFkNja1dvcmxkVXBs",
            "b2FkQ29tcGxldGUSGAoHaXNfYmV0YRgBIAEoCFIHaXNfYmV0YRIeCgppc19w",
            "cmV2aWV3GAIgASgIUgppc19wcmV2aWV3EiAKC3ByZXZpZXdfd2luGAMgASgI",
            "UgtwcmV2aWV3X3dpbhIgCgtwcmV2aWV3X21hYxgEIAEoCFILcHJldmlld19t",
            "YWMSKAoPcHJldmlld19hbmRyb2lkGAUgASgIUg9wcmV2aWV3X2FuZHJvaWQS",
            "IAoLcHJldmlld19pb3MYBiABKAhSC3ByZXZpZXdfaW9zEiAKC2R1cmF0aW9u",
            "X21zGAcgASgEUgtkdXJhdGlvbl9tcxJjCgtidWlsZF9zdGF0cxgIIAMoCzJB",
            "LnBhbmFtYS5jcmVhdG9ya2l0LkNja1dvcmxkVXBsb2FkQ29tcGxldGUuQ2Nr",
            "V29ybGRCdWlsZFN0YXRzVmFsdWVSC2J1aWxkX3N0YXRzEmMKC3NjZW5lX3N0",
            "YXRzGAkgASgLMkEucGFuYW1hLmNyZWF0b3JraXQuQ2NrV29ybGRVcGxvYWRD",
            "b21wbGV0ZS5DY2tXb3JsZFNjZW5lU3RhdHNWYWx1ZVILc2NlbmVfc3RhdHMa",
            "dwoXQ2NrV29ybGRCdWlsZFN0YXRzVmFsdWUSGgoIcGxhdGZvcm0YASABKAlS",
            "CHBsYXRmb3JtEiAKC3NjZW5lX2luZGV4GAIgASgFUgtzY2VuZV9pbmRleBIe",
            "CgpidWlsZF9zaXplGAMgASgEUgpidWlsZF9zaXplGu4BChdDY2tXb3JsZFNj",
            "ZW5lU3RhdHNWYWx1ZRKDAQoKY29tcG9uZW50cxgBIAMoCzJjLnBhbmFtYS5j",
            "cmVhdG9ya2l0LkNja1dvcmxkVXBsb2FkQ29tcGxldGUuQ2NrV29ybGRTY2Vu",
            "ZVN0YXRzVmFsdWUuQ2NrV29ybGRTY2VuZVN0YXRzQ29tcG9uZW50c1ZhbHVl",
            "Ugpjb21wb25lbnRzGk0KIUNja1dvcmxkU2NlbmVTdGF0c0NvbXBvbmVudHNW",
            "YWx1ZRISCgRuYW1lGAEgASgJUgRuYW1lEhQKBWNvdW50GAIgASgNUgVjb3Vu",
            "dCKCAgoUQ2NrV29ybGRVcGxvYWRGYWlsZWQSGAoHaXNfYmV0YRgBIAEoCFIH",
            "aXNfYmV0YRIeCgppc19wcmV2aWV3GAIgASgIUgppc19wcmV2aWV3EiAKC3By",
            "ZXZpZXdfd2luGAMgASgIUgtwcmV2aWV3X3dpbhIgCgtwcmV2aWV3X21hYxgE",
            "IAEoCFILcHJldmlld19tYWMSKAoPcHJldmlld19hbmRyb2lkGAUgASgIUg9w",
            "cmV2aWV3X2FuZHJvaWQSIAoLcHJldmlld19pb3MYBiABKAhSC3ByZXZpZXdf",
            "aW9zEiAKC2R1cmF0aW9uX21zGAcgASgEUgtkdXJhdGlvbl9tc0IdqgIaQ2x1",
            "c3RlclZSLkNyZWF0b3JLaXQuUHJvdG9iBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.PanamaEvent), global::ClusterVR.CreatorKit.Proto.PanamaEvent.Parser, new[]{ "Ts", "EventType", "UserId", "UserPseudoId", "Platform", "DeviceType", "AppVersion", "BuildVersion", "EventSource", "IsUnity", "UserAgent", "CckPingObject", "CckInstallObject", "CckMenuItemObject", "CckOpenLinkObject", "CckEditorPreviewStopObject", "CckWorldUploadStartObject", "CckWorldUploadCompleteObject", "CckWorldUploadFailedObject" }, new[]{ "Object" }, new[]{ typeof(global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.EventType), typeof(global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.Platform), typeof(global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.EventSource) }, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.CckPing), global::ClusterVR.CreatorKit.Proto.CckPing.Parser, new[]{ "SessionId", "Environment" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.CckPing.Types.CckPingCreatorKitEnvironment), global::ClusterVR.CreatorKit.Proto.CckPing.Types.CckPingCreatorKitEnvironment.Parser, new[]{ "BatteryStatus", "DeviceModel", "DeviceType", "DeviceUniqueIdentifier", "GraphicsDeviceName", "GraphicsDeviceType", "GraphicsDeviceVendor", "GraphicsDeviceVersion", "GraphicsMemorySize", "GraphicsMultiThreaded", "GraphicsShaderLevel", "OperatingSystem", "OperatingSystemFamily", "ProcessorCount", "ProcessorFrequency", "ProcessorType", "SystemMemorySize", "IsFocused", "IsPlaying", "Platform", "SystemLanguage", "UnityVersion", "XrDeviceIsPresent", "XrDeviceModel" }, null, null, null, null)}),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.CckInstall), global::ClusterVR.CreatorKit.Proto.CckInstall.Parser, new[]{ "Version", "PrevVersion" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.CckMenuItem), global::ClusterVR.CreatorKit.Proto.CckMenuItem.Parser, new[]{ "MenuItemType" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.CckOpenLink), global::ClusterVR.CreatorKit.Proto.CckOpenLink.Parser, new[]{ "LinkUrl", "From" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.CckEditorPreviewStop), global::ClusterVR.CreatorKit.Proto.CckEditorPreviewStop.Parser, new[]{ "DurationMs" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.CckWorldUploadStart), global::ClusterVR.CreatorKit.Proto.CckWorldUploadStart.Parser, new[]{ "IsBeta", "IsPreview", "PreviewWin", "PreviewMac", "PreviewAndroid", "PreviewIos" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete), global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete.Parser, new[]{ "IsBeta", "IsPreview", "PreviewWin", "PreviewMac", "PreviewAndroid", "PreviewIos", "DurationMs", "BuildStats", "SceneStats" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete.Types.CckWorldBuildStatsValue), global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete.Types.CckWorldBuildStatsValue.Parser, new[]{ "Platform", "SceneIndex", "BuildSize" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete.Types.CckWorldSceneStatsValue), global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete.Types.CckWorldSceneStatsValue.Parser, new[]{ "Components" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete.Types.CckWorldSceneStatsValue.Types.CckWorldSceneStatsComponentsValue), global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete.Types.CckWorldSceneStatsValue.Types.CckWorldSceneStatsComponentsValue.Parser, new[]{ "Name", "Count" }, null, null, null, null)})}),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.CckWorldUploadFailed), global::ClusterVR.CreatorKit.Proto.CckWorldUploadFailed.Parser, new[]{ "IsBeta", "IsPreview", "PreviewWin", "PreviewMac", "PreviewAndroid", "PreviewIos", "DurationMs" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
  public sealed partial class PanamaEvent : pb::IMessage<PanamaEvent>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<PanamaEvent> _parser = new pb::MessageParser<PanamaEvent>(() => new PanamaEvent());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<PanamaEvent> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.PanamaEventReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public PanamaEvent() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public PanamaEvent(PanamaEvent other) : this() {
      ts_ = other.ts_;
      eventType_ = other.eventType_;
      userId_ = other.userId_;
      userPseudoId_ = other.userPseudoId_;
      platform_ = other.platform_;
      deviceType_ = other.deviceType_;
      appVersion_ = other.appVersion_;
      buildVersion_ = other.buildVersion_;
      eventSource_ = other.eventSource_;
      isUnity_ = other.isUnity_;
      userAgent_ = other.userAgent_;
      switch (other.ObjectCase) {
        case ObjectOneofCase.CckPingObject:
          CckPingObject = other.CckPingObject.Clone();
          break;
        case ObjectOneofCase.CckInstallObject:
          CckInstallObject = other.CckInstallObject.Clone();
          break;
        case ObjectOneofCase.CckMenuItemObject:
          CckMenuItemObject = other.CckMenuItemObject.Clone();
          break;
        case ObjectOneofCase.CckOpenLinkObject:
          CckOpenLinkObject = other.CckOpenLinkObject.Clone();
          break;
        case ObjectOneofCase.CckEditorPreviewStopObject:
          CckEditorPreviewStopObject = other.CckEditorPreviewStopObject.Clone();
          break;
        case ObjectOneofCase.CckWorldUploadStartObject:
          CckWorldUploadStartObject = other.CckWorldUploadStartObject.Clone();
          break;
        case ObjectOneofCase.CckWorldUploadCompleteObject:
          CckWorldUploadCompleteObject = other.CckWorldUploadCompleteObject.Clone();
          break;
        case ObjectOneofCase.CckWorldUploadFailedObject:
          CckWorldUploadFailedObject = other.CckWorldUploadFailedObject.Clone();
          break;
      }

      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public PanamaEvent Clone() {
      return new PanamaEvent(this);
    }

    public const int TsFieldNumber = 1;
    private long ts_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public long Ts {
      get { return ts_; }
      set {
        ts_ = value;
      }
    }

    public const int EventTypeFieldNumber = 2;
    private global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.EventType eventType_ = global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.EventType.Unspecified;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.EventType EventType {
      get { return eventType_; }
      set {
        eventType_ = value;
      }
    }

    public const int UserIdFieldNumber = 3;
    private string userId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string UserId {
      get { return userId_; }
      set {
        userId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    public const int UserPseudoIdFieldNumber = 8;
    private string userPseudoId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string UserPseudoId {
      get { return userPseudoId_; }
      set {
        userPseudoId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    public const int PlatformFieldNumber = 4;
    private global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.Platform platform_ = global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.Platform.Unspecified;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.Platform Platform {
      get { return platform_; }
      set {
        platform_ = value;
      }
    }

    public const int DeviceTypeFieldNumber = 5;
    private string deviceType_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string DeviceType {
      get { return deviceType_; }
      set {
        deviceType_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    public const int AppVersionFieldNumber = 6;
    private string appVersion_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string AppVersion {
      get { return appVersion_; }
      set {
        appVersion_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    public const int BuildVersionFieldNumber = 7;
    private string buildVersion_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string BuildVersion {
      get { return buildVersion_; }
      set {
        buildVersion_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    public const int EventSourceFieldNumber = 9;
    private global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.EventSource eventSource_ = global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.EventSource.Unspecified;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.EventSource EventSource {
      get { return eventSource_; }
      set {
        eventSource_ = value;
      }
    }

    public const int IsUnityFieldNumber = 10;
    private bool isUnity_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool IsUnity {
      get { return isUnity_; }
      set {
        isUnity_ = value;
      }
    }

    public const int UserAgentFieldNumber = 11;
    private string userAgent_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string UserAgent {
      get { return userAgent_; }
      set {
        userAgent_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    public const int CckPingObjectFieldNumber = 178;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.CckPing CckPingObject {
      get { return objectCase_ == ObjectOneofCase.CckPingObject ? (global::ClusterVR.CreatorKit.Proto.CckPing) object_ : null; }
      set {
        object_ = value;
        objectCase_ = value == null ? ObjectOneofCase.None : ObjectOneofCase.CckPingObject;
      }
    }

    public const int CckInstallObjectFieldNumber = 179;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.CckInstall CckInstallObject {
      get { return objectCase_ == ObjectOneofCase.CckInstallObject ? (global::ClusterVR.CreatorKit.Proto.CckInstall) object_ : null; }
      set {
        object_ = value;
        objectCase_ = value == null ? ObjectOneofCase.None : ObjectOneofCase.CckInstallObject;
      }
    }

    public const int CckMenuItemObjectFieldNumber = 180;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.CckMenuItem CckMenuItemObject {
      get { return objectCase_ == ObjectOneofCase.CckMenuItemObject ? (global::ClusterVR.CreatorKit.Proto.CckMenuItem) object_ : null; }
      set {
        object_ = value;
        objectCase_ = value == null ? ObjectOneofCase.None : ObjectOneofCase.CckMenuItemObject;
      }
    }

    public const int CckOpenLinkObjectFieldNumber = 181;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.CckOpenLink CckOpenLinkObject {
      get { return objectCase_ == ObjectOneofCase.CckOpenLinkObject ? (global::ClusterVR.CreatorKit.Proto.CckOpenLink) object_ : null; }
      set {
        object_ = value;
        objectCase_ = value == null ? ObjectOneofCase.None : ObjectOneofCase.CckOpenLinkObject;
      }
    }

    public const int CckEditorPreviewStopObjectFieldNumber = 182;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.CckEditorPreviewStop CckEditorPreviewStopObject {
      get { return objectCase_ == ObjectOneofCase.CckEditorPreviewStopObject ? (global::ClusterVR.CreatorKit.Proto.CckEditorPreviewStop) object_ : null; }
      set {
        object_ = value;
        objectCase_ = value == null ? ObjectOneofCase.None : ObjectOneofCase.CckEditorPreviewStopObject;
      }
    }

    public const int CckWorldUploadStartObjectFieldNumber = 183;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.CckWorldUploadStart CckWorldUploadStartObject {
      get { return objectCase_ == ObjectOneofCase.CckWorldUploadStartObject ? (global::ClusterVR.CreatorKit.Proto.CckWorldUploadStart) object_ : null; }
      set {
        object_ = value;
        objectCase_ = value == null ? ObjectOneofCase.None : ObjectOneofCase.CckWorldUploadStartObject;
      }
    }

    public const int CckWorldUploadCompleteObjectFieldNumber = 184;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete CckWorldUploadCompleteObject {
      get { return objectCase_ == ObjectOneofCase.CckWorldUploadCompleteObject ? (global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete) object_ : null; }
      set {
        object_ = value;
        objectCase_ = value == null ? ObjectOneofCase.None : ObjectOneofCase.CckWorldUploadCompleteObject;
      }
    }

    public const int CckWorldUploadFailedObjectFieldNumber = 188;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.CckWorldUploadFailed CckWorldUploadFailedObject {
      get { return objectCase_ == ObjectOneofCase.CckWorldUploadFailedObject ? (global::ClusterVR.CreatorKit.Proto.CckWorldUploadFailed) object_ : null; }
      set {
        object_ = value;
        objectCase_ = value == null ? ObjectOneofCase.None : ObjectOneofCase.CckWorldUploadFailedObject;
      }
    }

    private object object_;
    public enum ObjectOneofCase {
      None = 0,
      CckPingObject = 178,
      CckInstallObject = 179,
      CckMenuItemObject = 180,
      CckOpenLinkObject = 181,
      CckEditorPreviewStopObject = 182,
      CckWorldUploadStartObject = 183,
      CckWorldUploadCompleteObject = 184,
      CckWorldUploadFailedObject = 188,
    }
    private ObjectOneofCase objectCase_ = ObjectOneofCase.None;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ObjectOneofCase ObjectCase {
      get { return objectCase_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearObject() {
      objectCase_ = ObjectOneofCase.None;
      object_ = null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as PanamaEvent);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(PanamaEvent other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Ts != other.Ts) return false;
      if (EventType != other.EventType) return false;
      if (UserId != other.UserId) return false;
      if (UserPseudoId != other.UserPseudoId) return false;
      if (Platform != other.Platform) return false;
      if (DeviceType != other.DeviceType) return false;
      if (AppVersion != other.AppVersion) return false;
      if (BuildVersion != other.BuildVersion) return false;
      if (EventSource != other.EventSource) return false;
      if (IsUnity != other.IsUnity) return false;
      if (UserAgent != other.UserAgent) return false;
      if (!object.Equals(CckPingObject, other.CckPingObject)) return false;
      if (!object.Equals(CckInstallObject, other.CckInstallObject)) return false;
      if (!object.Equals(CckMenuItemObject, other.CckMenuItemObject)) return false;
      if (!object.Equals(CckOpenLinkObject, other.CckOpenLinkObject)) return false;
      if (!object.Equals(CckEditorPreviewStopObject, other.CckEditorPreviewStopObject)) return false;
      if (!object.Equals(CckWorldUploadStartObject, other.CckWorldUploadStartObject)) return false;
      if (!object.Equals(CckWorldUploadCompleteObject, other.CckWorldUploadCompleteObject)) return false;
      if (!object.Equals(CckWorldUploadFailedObject, other.CckWorldUploadFailedObject)) return false;
      if (ObjectCase != other.ObjectCase) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (Ts != 0L) hash ^= Ts.GetHashCode();
      if (EventType != global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.EventType.Unspecified) hash ^= EventType.GetHashCode();
      if (UserId.Length != 0) hash ^= UserId.GetHashCode();
      if (UserPseudoId.Length != 0) hash ^= UserPseudoId.GetHashCode();
      if (Platform != global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.Platform.Unspecified) hash ^= Platform.GetHashCode();
      if (DeviceType.Length != 0) hash ^= DeviceType.GetHashCode();
      if (AppVersion.Length != 0) hash ^= AppVersion.GetHashCode();
      if (BuildVersion.Length != 0) hash ^= BuildVersion.GetHashCode();
      if (EventSource != global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.EventSource.Unspecified) hash ^= EventSource.GetHashCode();
      if (IsUnity != false) hash ^= IsUnity.GetHashCode();
      if (UserAgent.Length != 0) hash ^= UserAgent.GetHashCode();
      if (objectCase_ == ObjectOneofCase.CckPingObject) hash ^= CckPingObject.GetHashCode();
      if (objectCase_ == ObjectOneofCase.CckInstallObject) hash ^= CckInstallObject.GetHashCode();
      if (objectCase_ == ObjectOneofCase.CckMenuItemObject) hash ^= CckMenuItemObject.GetHashCode();
      if (objectCase_ == ObjectOneofCase.CckOpenLinkObject) hash ^= CckOpenLinkObject.GetHashCode();
      if (objectCase_ == ObjectOneofCase.CckEditorPreviewStopObject) hash ^= CckEditorPreviewStopObject.GetHashCode();
      if (objectCase_ == ObjectOneofCase.CckWorldUploadStartObject) hash ^= CckWorldUploadStartObject.GetHashCode();
      if (objectCase_ == ObjectOneofCase.CckWorldUploadCompleteObject) hash ^= CckWorldUploadCompleteObject.GetHashCode();
      if (objectCase_ == ObjectOneofCase.CckWorldUploadFailedObject) hash ^= CckWorldUploadFailedObject.GetHashCode();
      hash ^= (int) objectCase_;
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (Ts != 0L) {
        output.WriteRawTag(8);
        output.WriteInt64(Ts);
      }
      if (EventType != global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.EventType.Unspecified) {
        output.WriteRawTag(16);
        output.WriteEnum((int) EventType);
      }
      if (UserId.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(UserId);
      }
      if (Platform != global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.Platform.Unspecified) {
        output.WriteRawTag(32);
        output.WriteEnum((int) Platform);
      }
      if (DeviceType.Length != 0) {
        output.WriteRawTag(42);
        output.WriteString(DeviceType);
      }
      if (AppVersion.Length != 0) {
        output.WriteRawTag(50);
        output.WriteString(AppVersion);
      }
      if (BuildVersion.Length != 0) {
        output.WriteRawTag(58);
        output.WriteString(BuildVersion);
      }
      if (UserPseudoId.Length != 0) {
        output.WriteRawTag(66);
        output.WriteString(UserPseudoId);
      }
      if (EventSource != global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.EventSource.Unspecified) {
        output.WriteRawTag(72);
        output.WriteEnum((int) EventSource);
      }
      if (IsUnity != false) {
        output.WriteRawTag(80);
        output.WriteBool(IsUnity);
      }
      if (UserAgent.Length != 0) {
        output.WriteRawTag(90);
        output.WriteString(UserAgent);
      }
      if (objectCase_ == ObjectOneofCase.CckPingObject) {
        output.WriteRawTag(146, 11);
        output.WriteMessage(CckPingObject);
      }
      if (objectCase_ == ObjectOneofCase.CckInstallObject) {
        output.WriteRawTag(154, 11);
        output.WriteMessage(CckInstallObject);
      }
      if (objectCase_ == ObjectOneofCase.CckMenuItemObject) {
        output.WriteRawTag(162, 11);
        output.WriteMessage(CckMenuItemObject);
      }
      if (objectCase_ == ObjectOneofCase.CckOpenLinkObject) {
        output.WriteRawTag(170, 11);
        output.WriteMessage(CckOpenLinkObject);
      }
      if (objectCase_ == ObjectOneofCase.CckEditorPreviewStopObject) {
        output.WriteRawTag(178, 11);
        output.WriteMessage(CckEditorPreviewStopObject);
      }
      if (objectCase_ == ObjectOneofCase.CckWorldUploadStartObject) {
        output.WriteRawTag(186, 11);
        output.WriteMessage(CckWorldUploadStartObject);
      }
      if (objectCase_ == ObjectOneofCase.CckWorldUploadCompleteObject) {
        output.WriteRawTag(194, 11);
        output.WriteMessage(CckWorldUploadCompleteObject);
      }
      if (objectCase_ == ObjectOneofCase.CckWorldUploadFailedObject) {
        output.WriteRawTag(226, 11);
        output.WriteMessage(CckWorldUploadFailedObject);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (Ts != 0L) {
        output.WriteRawTag(8);
        output.WriteInt64(Ts);
      }
      if (EventType != global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.EventType.Unspecified) {
        output.WriteRawTag(16);
        output.WriteEnum((int) EventType);
      }
      if (UserId.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(UserId);
      }
      if (Platform != global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.Platform.Unspecified) {
        output.WriteRawTag(32);
        output.WriteEnum((int) Platform);
      }
      if (DeviceType.Length != 0) {
        output.WriteRawTag(42);
        output.WriteString(DeviceType);
      }
      if (AppVersion.Length != 0) {
        output.WriteRawTag(50);
        output.WriteString(AppVersion);
      }
      if (BuildVersion.Length != 0) {
        output.WriteRawTag(58);
        output.WriteString(BuildVersion);
      }
      if (UserPseudoId.Length != 0) {
        output.WriteRawTag(66);
        output.WriteString(UserPseudoId);
      }
      if (EventSource != global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.EventSource.Unspecified) {
        output.WriteRawTag(72);
        output.WriteEnum((int) EventSource);
      }
      if (IsUnity != false) {
        output.WriteRawTag(80);
        output.WriteBool(IsUnity);
      }
      if (UserAgent.Length != 0) {
        output.WriteRawTag(90);
        output.WriteString(UserAgent);
      }
      if (objectCase_ == ObjectOneofCase.CckPingObject) {
        output.WriteRawTag(146, 11);
        output.WriteMessage(CckPingObject);
      }
      if (objectCase_ == ObjectOneofCase.CckInstallObject) {
        output.WriteRawTag(154, 11);
        output.WriteMessage(CckInstallObject);
      }
      if (objectCase_ == ObjectOneofCase.CckMenuItemObject) {
        output.WriteRawTag(162, 11);
        output.WriteMessage(CckMenuItemObject);
      }
      if (objectCase_ == ObjectOneofCase.CckOpenLinkObject) {
        output.WriteRawTag(170, 11);
        output.WriteMessage(CckOpenLinkObject);
      }
      if (objectCase_ == ObjectOneofCase.CckEditorPreviewStopObject) {
        output.WriteRawTag(178, 11);
        output.WriteMessage(CckEditorPreviewStopObject);
      }
      if (objectCase_ == ObjectOneofCase.CckWorldUploadStartObject) {
        output.WriteRawTag(186, 11);
        output.WriteMessage(CckWorldUploadStartObject);
      }
      if (objectCase_ == ObjectOneofCase.CckWorldUploadCompleteObject) {
        output.WriteRawTag(194, 11);
        output.WriteMessage(CckWorldUploadCompleteObject);
      }
      if (objectCase_ == ObjectOneofCase.CckWorldUploadFailedObject) {
        output.WriteRawTag(226, 11);
        output.WriteMessage(CckWorldUploadFailedObject);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (Ts != 0L) {
        size += 1 + pb::CodedOutputStream.ComputeInt64Size(Ts);
      }
      if (EventType != global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.EventType.Unspecified) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) EventType);
      }
      if (UserId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(UserId);
      }
      if (UserPseudoId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(UserPseudoId);
      }
      if (Platform != global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.Platform.Unspecified) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Platform);
      }
      if (DeviceType.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(DeviceType);
      }
      if (AppVersion.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(AppVersion);
      }
      if (BuildVersion.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(BuildVersion);
      }
      if (EventSource != global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.EventSource.Unspecified) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) EventSource);
      }
      if (IsUnity != false) {
        size += 1 + 1;
      }
      if (UserAgent.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(UserAgent);
      }
      if (objectCase_ == ObjectOneofCase.CckPingObject) {
        size += 2 + pb::CodedOutputStream.ComputeMessageSize(CckPingObject);
      }
      if (objectCase_ == ObjectOneofCase.CckInstallObject) {
        size += 2 + pb::CodedOutputStream.ComputeMessageSize(CckInstallObject);
      }
      if (objectCase_ == ObjectOneofCase.CckMenuItemObject) {
        size += 2 + pb::CodedOutputStream.ComputeMessageSize(CckMenuItemObject);
      }
      if (objectCase_ == ObjectOneofCase.CckOpenLinkObject) {
        size += 2 + pb::CodedOutputStream.ComputeMessageSize(CckOpenLinkObject);
      }
      if (objectCase_ == ObjectOneofCase.CckEditorPreviewStopObject) {
        size += 2 + pb::CodedOutputStream.ComputeMessageSize(CckEditorPreviewStopObject);
      }
      if (objectCase_ == ObjectOneofCase.CckWorldUploadStartObject) {
        size += 2 + pb::CodedOutputStream.ComputeMessageSize(CckWorldUploadStartObject);
      }
      if (objectCase_ == ObjectOneofCase.CckWorldUploadCompleteObject) {
        size += 2 + pb::CodedOutputStream.ComputeMessageSize(CckWorldUploadCompleteObject);
      }
      if (objectCase_ == ObjectOneofCase.CckWorldUploadFailedObject) {
        size += 2 + pb::CodedOutputStream.ComputeMessageSize(CckWorldUploadFailedObject);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(PanamaEvent other) {
      if (other == null) {
        return;
      }
      if (other.Ts != 0L) {
        Ts = other.Ts;
      }
      if (other.EventType != global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.EventType.Unspecified) {
        EventType = other.EventType;
      }
      if (other.UserId.Length != 0) {
        UserId = other.UserId;
      }
      if (other.UserPseudoId.Length != 0) {
        UserPseudoId = other.UserPseudoId;
      }
      if (other.Platform != global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.Platform.Unspecified) {
        Platform = other.Platform;
      }
      if (other.DeviceType.Length != 0) {
        DeviceType = other.DeviceType;
      }
      if (other.AppVersion.Length != 0) {
        AppVersion = other.AppVersion;
      }
      if (other.BuildVersion.Length != 0) {
        BuildVersion = other.BuildVersion;
      }
      if (other.EventSource != global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.EventSource.Unspecified) {
        EventSource = other.EventSource;
      }
      if (other.IsUnity != false) {
        IsUnity = other.IsUnity;
      }
      if (other.UserAgent.Length != 0) {
        UserAgent = other.UserAgent;
      }
      switch (other.ObjectCase) {
        case ObjectOneofCase.CckPingObject:
          if (CckPingObject == null) {
            CckPingObject = new global::ClusterVR.CreatorKit.Proto.CckPing();
          }
          CckPingObject.MergeFrom(other.CckPingObject);
          break;
        case ObjectOneofCase.CckInstallObject:
          if (CckInstallObject == null) {
            CckInstallObject = new global::ClusterVR.CreatorKit.Proto.CckInstall();
          }
          CckInstallObject.MergeFrom(other.CckInstallObject);
          break;
        case ObjectOneofCase.CckMenuItemObject:
          if (CckMenuItemObject == null) {
            CckMenuItemObject = new global::ClusterVR.CreatorKit.Proto.CckMenuItem();
          }
          CckMenuItemObject.MergeFrom(other.CckMenuItemObject);
          break;
        case ObjectOneofCase.CckOpenLinkObject:
          if (CckOpenLinkObject == null) {
            CckOpenLinkObject = new global::ClusterVR.CreatorKit.Proto.CckOpenLink();
          }
          CckOpenLinkObject.MergeFrom(other.CckOpenLinkObject);
          break;
        case ObjectOneofCase.CckEditorPreviewStopObject:
          if (CckEditorPreviewStopObject == null) {
            CckEditorPreviewStopObject = new global::ClusterVR.CreatorKit.Proto.CckEditorPreviewStop();
          }
          CckEditorPreviewStopObject.MergeFrom(other.CckEditorPreviewStopObject);
          break;
        case ObjectOneofCase.CckWorldUploadStartObject:
          if (CckWorldUploadStartObject == null) {
            CckWorldUploadStartObject = new global::ClusterVR.CreatorKit.Proto.CckWorldUploadStart();
          }
          CckWorldUploadStartObject.MergeFrom(other.CckWorldUploadStartObject);
          break;
        case ObjectOneofCase.CckWorldUploadCompleteObject:
          if (CckWorldUploadCompleteObject == null) {
            CckWorldUploadCompleteObject = new global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete();
          }
          CckWorldUploadCompleteObject.MergeFrom(other.CckWorldUploadCompleteObject);
          break;
        case ObjectOneofCase.CckWorldUploadFailedObject:
          if (CckWorldUploadFailedObject == null) {
            CckWorldUploadFailedObject = new global::ClusterVR.CreatorKit.Proto.CckWorldUploadFailed();
          }
          CckWorldUploadFailedObject.MergeFrom(other.CckWorldUploadFailedObject);
          break;
      }

      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Ts = input.ReadInt64();
            break;
          }
          case 16: {
            EventType = (global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.EventType) input.ReadEnum();
            break;
          }
          case 26: {
            UserId = input.ReadString();
            break;
          }
          case 32: {
            Platform = (global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.Platform) input.ReadEnum();
            break;
          }
          case 42: {
            DeviceType = input.ReadString();
            break;
          }
          case 50: {
            AppVersion = input.ReadString();
            break;
          }
          case 58: {
            BuildVersion = input.ReadString();
            break;
          }
          case 66: {
            UserPseudoId = input.ReadString();
            break;
          }
          case 72: {
            EventSource = (global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.EventSource) input.ReadEnum();
            break;
          }
          case 80: {
            IsUnity = input.ReadBool();
            break;
          }
          case 90: {
            UserAgent = input.ReadString();
            break;
          }
          case 1426: {
            global::ClusterVR.CreatorKit.Proto.CckPing subBuilder = new global::ClusterVR.CreatorKit.Proto.CckPing();
            if (objectCase_ == ObjectOneofCase.CckPingObject) {
              subBuilder.MergeFrom(CckPingObject);
            }
            input.ReadMessage(subBuilder);
            CckPingObject = subBuilder;
            break;
          }
          case 1434: {
            global::ClusterVR.CreatorKit.Proto.CckInstall subBuilder = new global::ClusterVR.CreatorKit.Proto.CckInstall();
            if (objectCase_ == ObjectOneofCase.CckInstallObject) {
              subBuilder.MergeFrom(CckInstallObject);
            }
            input.ReadMessage(subBuilder);
            CckInstallObject = subBuilder;
            break;
          }
          case 1442: {
            global::ClusterVR.CreatorKit.Proto.CckMenuItem subBuilder = new global::ClusterVR.CreatorKit.Proto.CckMenuItem();
            if (objectCase_ == ObjectOneofCase.CckMenuItemObject) {
              subBuilder.MergeFrom(CckMenuItemObject);
            }
            input.ReadMessage(subBuilder);
            CckMenuItemObject = subBuilder;
            break;
          }
          case 1450: {
            global::ClusterVR.CreatorKit.Proto.CckOpenLink subBuilder = new global::ClusterVR.CreatorKit.Proto.CckOpenLink();
            if (objectCase_ == ObjectOneofCase.CckOpenLinkObject) {
              subBuilder.MergeFrom(CckOpenLinkObject);
            }
            input.ReadMessage(subBuilder);
            CckOpenLinkObject = subBuilder;
            break;
          }
          case 1458: {
            global::ClusterVR.CreatorKit.Proto.CckEditorPreviewStop subBuilder = new global::ClusterVR.CreatorKit.Proto.CckEditorPreviewStop();
            if (objectCase_ == ObjectOneofCase.CckEditorPreviewStopObject) {
              subBuilder.MergeFrom(CckEditorPreviewStopObject);
            }
            input.ReadMessage(subBuilder);
            CckEditorPreviewStopObject = subBuilder;
            break;
          }
          case 1466: {
            global::ClusterVR.CreatorKit.Proto.CckWorldUploadStart subBuilder = new global::ClusterVR.CreatorKit.Proto.CckWorldUploadStart();
            if (objectCase_ == ObjectOneofCase.CckWorldUploadStartObject) {
              subBuilder.MergeFrom(CckWorldUploadStartObject);
            }
            input.ReadMessage(subBuilder);
            CckWorldUploadStartObject = subBuilder;
            break;
          }
          case 1474: {
            global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete subBuilder = new global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete();
            if (objectCase_ == ObjectOneofCase.CckWorldUploadCompleteObject) {
              subBuilder.MergeFrom(CckWorldUploadCompleteObject);
            }
            input.ReadMessage(subBuilder);
            CckWorldUploadCompleteObject = subBuilder;
            break;
          }
          case 1506: {
            global::ClusterVR.CreatorKit.Proto.CckWorldUploadFailed subBuilder = new global::ClusterVR.CreatorKit.Proto.CckWorldUploadFailed();
            if (objectCase_ == ObjectOneofCase.CckWorldUploadFailedObject) {
              subBuilder.MergeFrom(CckWorldUploadFailedObject);
            }
            input.ReadMessage(subBuilder);
            CckWorldUploadFailedObject = subBuilder;
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            Ts = input.ReadInt64();
            break;
          }
          case 16: {
            EventType = (global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.EventType) input.ReadEnum();
            break;
          }
          case 26: {
            UserId = input.ReadString();
            break;
          }
          case 32: {
            Platform = (global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.Platform) input.ReadEnum();
            break;
          }
          case 42: {
            DeviceType = input.ReadString();
            break;
          }
          case 50: {
            AppVersion = input.ReadString();
            break;
          }
          case 58: {
            BuildVersion = input.ReadString();
            break;
          }
          case 66: {
            UserPseudoId = input.ReadString();
            break;
          }
          case 72: {
            EventSource = (global::ClusterVR.CreatorKit.Proto.PanamaEvent.Types.EventSource) input.ReadEnum();
            break;
          }
          case 80: {
            IsUnity = input.ReadBool();
            break;
          }
          case 90: {
            UserAgent = input.ReadString();
            break;
          }
          case 1426: {
            global::ClusterVR.CreatorKit.Proto.CckPing subBuilder = new global::ClusterVR.CreatorKit.Proto.CckPing();
            if (objectCase_ == ObjectOneofCase.CckPingObject) {
              subBuilder.MergeFrom(CckPingObject);
            }
            input.ReadMessage(subBuilder);
            CckPingObject = subBuilder;
            break;
          }
          case 1434: {
            global::ClusterVR.CreatorKit.Proto.CckInstall subBuilder = new global::ClusterVR.CreatorKit.Proto.CckInstall();
            if (objectCase_ == ObjectOneofCase.CckInstallObject) {
              subBuilder.MergeFrom(CckInstallObject);
            }
            input.ReadMessage(subBuilder);
            CckInstallObject = subBuilder;
            break;
          }
          case 1442: {
            global::ClusterVR.CreatorKit.Proto.CckMenuItem subBuilder = new global::ClusterVR.CreatorKit.Proto.CckMenuItem();
            if (objectCase_ == ObjectOneofCase.CckMenuItemObject) {
              subBuilder.MergeFrom(CckMenuItemObject);
            }
            input.ReadMessage(subBuilder);
            CckMenuItemObject = subBuilder;
            break;
          }
          case 1450: {
            global::ClusterVR.CreatorKit.Proto.CckOpenLink subBuilder = new global::ClusterVR.CreatorKit.Proto.CckOpenLink();
            if (objectCase_ == ObjectOneofCase.CckOpenLinkObject) {
              subBuilder.MergeFrom(CckOpenLinkObject);
            }
            input.ReadMessage(subBuilder);
            CckOpenLinkObject = subBuilder;
            break;
          }
          case 1458: {
            global::ClusterVR.CreatorKit.Proto.CckEditorPreviewStop subBuilder = new global::ClusterVR.CreatorKit.Proto.CckEditorPreviewStop();
            if (objectCase_ == ObjectOneofCase.CckEditorPreviewStopObject) {
              subBuilder.MergeFrom(CckEditorPreviewStopObject);
            }
            input.ReadMessage(subBuilder);
            CckEditorPreviewStopObject = subBuilder;
            break;
          }
          case 1466: {
            global::ClusterVR.CreatorKit.Proto.CckWorldUploadStart subBuilder = new global::ClusterVR.CreatorKit.Proto.CckWorldUploadStart();
            if (objectCase_ == ObjectOneofCase.CckWorldUploadStartObject) {
              subBuilder.MergeFrom(CckWorldUploadStartObject);
            }
            input.ReadMessage(subBuilder);
            CckWorldUploadStartObject = subBuilder;
            break;
          }
          case 1474: {
            global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete subBuilder = new global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete();
            if (objectCase_ == ObjectOneofCase.CckWorldUploadCompleteObject) {
              subBuilder.MergeFrom(CckWorldUploadCompleteObject);
            }
            input.ReadMessage(subBuilder);
            CckWorldUploadCompleteObject = subBuilder;
            break;
          }
          case 1506: {
            global::ClusterVR.CreatorKit.Proto.CckWorldUploadFailed subBuilder = new global::ClusterVR.CreatorKit.Proto.CckWorldUploadFailed();
            if (objectCase_ == ObjectOneofCase.CckWorldUploadFailedObject) {
              subBuilder.MergeFrom(CckWorldUploadFailedObject);
            }
            input.ReadMessage(subBuilder);
            CckWorldUploadFailedObject = subBuilder;
            break;
          }
        }
      }
    }
    #endif

    #region Nested types
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static partial class Types {
      public enum EventType {
        [pbr::OriginalName("EVENT_TYPE_UNSPECIFIED")] Unspecified = 0,
        [pbr::OriginalName("CCK_PING")] CckPing = 198,
        [pbr::OriginalName("CCK_OPEN_UNITY")] CckOpenUnity = 199,
        [pbr::OriginalName("CCK_INSTALL")] CckInstall = 200,
        [pbr::OriginalName("CCK_MENU_ITEM")] CckMenuItem = 201,
        [pbr::OriginalName("CCK_OPEN_LINK")] CckOpenLink = 202,
        [pbr::OriginalName("CCK_EDITOR_PREVIEW_START")] CckEditorPreviewStart = 203,
        [pbr::OriginalName("CCK_EDITOR_PREVIEW_STOP")] CckEditorPreviewStop = 204,
        [pbr::OriginalName("CCK_WORLD_UPLOAD_START")] CckWorldUploadStart = 205,
        [pbr::OriginalName("CCK_WORLD_UPLOAD_COMPLETE")] CckWorldUploadComplete = 206,
        [pbr::OriginalName("CCK_NEW_INSTALL")] CckNewInstall = 210,
        [pbr::OriginalName("CCK_WORLD_UPLOAD_FAILED")] CckWorldUploadFailed = 211,
      }

      public enum Platform {
        [pbr::OriginalName("PLATFORM_UNSPECIFIED")] Unspecified = 0,
        [pbr::OriginalName("WIN")] Win = 3,
        [pbr::OriginalName("MAC")] Mac = 4,
      }

      public enum EventSource {
        [pbr::OriginalName("EVENT_SOURCE_UNSPECIFIED")] Unspecified = 0,
        [pbr::OriginalName("CREATOR_KIT")] CreatorKit = 4,
      }

    }
    #endregion

  }

  [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
  public sealed partial class CckPing : pb::IMessage<CckPing>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<CckPing> _parser = new pb::MessageParser<CckPing>(() => new CckPing());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<CckPing> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.PanamaEventReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CckPing() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CckPing(CckPing other) : this() {
      sessionId_ = other.sessionId_;
      environment_ = other.environment_ != null ? other.environment_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CckPing Clone() {
      return new CckPing(this);
    }

    public const int SessionIdFieldNumber = 2;
    private string sessionId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string SessionId {
      get { return sessionId_; }
      set {
        sessionId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    public const int EnvironmentFieldNumber = 3;
    private global::ClusterVR.CreatorKit.Proto.CckPing.Types.CckPingCreatorKitEnvironment environment_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.CckPing.Types.CckPingCreatorKitEnvironment Environment {
      get { return environment_; }
      set {
        environment_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as CckPing);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(CckPing other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (SessionId != other.SessionId) return false;
      if (!object.Equals(Environment, other.Environment)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (SessionId.Length != 0) hash ^= SessionId.GetHashCode();
      if (environment_ != null) hash ^= Environment.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (SessionId.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(SessionId);
      }
      if (environment_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(Environment);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (SessionId.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(SessionId);
      }
      if (environment_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(Environment);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (SessionId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(SessionId);
      }
      if (environment_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Environment);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(CckPing other) {
      if (other == null) {
        return;
      }
      if (other.SessionId.Length != 0) {
        SessionId = other.SessionId;
      }
      if (other.environment_ != null) {
        if (environment_ == null) {
          Environment = new global::ClusterVR.CreatorKit.Proto.CckPing.Types.CckPingCreatorKitEnvironment();
        }
        Environment.MergeFrom(other.Environment);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 18: {
            SessionId = input.ReadString();
            break;
          }
          case 26: {
            if (environment_ == null) {
              Environment = new global::ClusterVR.CreatorKit.Proto.CckPing.Types.CckPingCreatorKitEnvironment();
            }
            input.ReadMessage(Environment);
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 18: {
            SessionId = input.ReadString();
            break;
          }
          case 26: {
            if (environment_ == null) {
              Environment = new global::ClusterVR.CreatorKit.Proto.CckPing.Types.CckPingCreatorKitEnvironment();
            }
            input.ReadMessage(Environment);
            break;
          }
        }
      }
    }
    #endif

    #region Nested types
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static partial class Types {
      [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
      public sealed partial class CckPingCreatorKitEnvironment : pb::IMessage<CckPingCreatorKitEnvironment>
      #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
          , pb::IBufferMessage
      #endif
      {
        private static readonly pb::MessageParser<CckPingCreatorKitEnvironment> _parser = new pb::MessageParser<CckPingCreatorKitEnvironment>(() => new CckPingCreatorKitEnvironment());
        private pb::UnknownFieldSet _unknownFields;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public static pb::MessageParser<CckPingCreatorKitEnvironment> Parser { get { return _parser; } }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public static pbr::MessageDescriptor Descriptor {
          get { return global::ClusterVR.CreatorKit.Proto.CckPing.Descriptor.NestedTypes[0]; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        pbr::MessageDescriptor pb::IMessage.Descriptor {
          get { return Descriptor; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public CckPingCreatorKitEnvironment() {
          OnConstruction();
        }

        partial void OnConstruction();

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public CckPingCreatorKitEnvironment(CckPingCreatorKitEnvironment other) : this() {
          batteryStatus_ = other.batteryStatus_;
          deviceModel_ = other.deviceModel_;
          deviceType_ = other.deviceType_;
          deviceUniqueIdentifier_ = other.deviceUniqueIdentifier_;
          graphicsDeviceName_ = other.graphicsDeviceName_;
          graphicsDeviceType_ = other.graphicsDeviceType_;
          graphicsDeviceVendor_ = other.graphicsDeviceVendor_;
          graphicsDeviceVersion_ = other.graphicsDeviceVersion_;
          graphicsMemorySize_ = other.graphicsMemorySize_;
          graphicsMultiThreaded_ = other.graphicsMultiThreaded_;
          graphicsShaderLevel_ = other.graphicsShaderLevel_;
          operatingSystem_ = other.operatingSystem_;
          operatingSystemFamily_ = other.operatingSystemFamily_;
          processorCount_ = other.processorCount_;
          processorFrequency_ = other.processorFrequency_;
          processorType_ = other.processorType_;
          systemMemorySize_ = other.systemMemorySize_;
          isFocused_ = other.isFocused_;
          isPlaying_ = other.isPlaying_;
          platform_ = other.platform_;
          systemLanguage_ = other.systemLanguage_;
          unityVersion_ = other.unityVersion_;
          xrDeviceIsPresent_ = other.xrDeviceIsPresent_;
          xrDeviceModel_ = other.xrDeviceModel_;
          _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public CckPingCreatorKitEnvironment Clone() {
          return new CckPingCreatorKitEnvironment(this);
        }

        public const int BatteryStatusFieldNumber = 1;
        private string batteryStatus_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public string BatteryStatus {
          get { return batteryStatus_; }
          set {
            batteryStatus_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        public const int DeviceModelFieldNumber = 2;
        private string deviceModel_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public string DeviceModel {
          get { return deviceModel_; }
          set {
            deviceModel_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        public const int DeviceTypeFieldNumber = 3;
        private string deviceType_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public string DeviceType {
          get { return deviceType_; }
          set {
            deviceType_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        public const int DeviceUniqueIdentifierFieldNumber = 4;
        private string deviceUniqueIdentifier_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public string DeviceUniqueIdentifier {
          get { return deviceUniqueIdentifier_; }
          set {
            deviceUniqueIdentifier_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        public const int GraphicsDeviceNameFieldNumber = 5;
        private string graphicsDeviceName_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public string GraphicsDeviceName {
          get { return graphicsDeviceName_; }
          set {
            graphicsDeviceName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        public const int GraphicsDeviceTypeFieldNumber = 6;
        private string graphicsDeviceType_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public string GraphicsDeviceType {
          get { return graphicsDeviceType_; }
          set {
            graphicsDeviceType_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        public const int GraphicsDeviceVendorFieldNumber = 7;
        private string graphicsDeviceVendor_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public string GraphicsDeviceVendor {
          get { return graphicsDeviceVendor_; }
          set {
            graphicsDeviceVendor_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        public const int GraphicsDeviceVersionFieldNumber = 8;
        private string graphicsDeviceVersion_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public string GraphicsDeviceVersion {
          get { return graphicsDeviceVersion_; }
          set {
            graphicsDeviceVersion_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        public const int GraphicsMemorySizeFieldNumber = 9;
        private int graphicsMemorySize_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public int GraphicsMemorySize {
          get { return graphicsMemorySize_; }
          set {
            graphicsMemorySize_ = value;
          }
        }

        public const int GraphicsMultiThreadedFieldNumber = 10;
        private bool graphicsMultiThreaded_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public bool GraphicsMultiThreaded {
          get { return graphicsMultiThreaded_; }
          set {
            graphicsMultiThreaded_ = value;
          }
        }

        public const int GraphicsShaderLevelFieldNumber = 11;
        private int graphicsShaderLevel_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public int GraphicsShaderLevel {
          get { return graphicsShaderLevel_; }
          set {
            graphicsShaderLevel_ = value;
          }
        }

        public const int OperatingSystemFieldNumber = 12;
        private string operatingSystem_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public string OperatingSystem {
          get { return operatingSystem_; }
          set {
            operatingSystem_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        public const int OperatingSystemFamilyFieldNumber = 13;
        private string operatingSystemFamily_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public string OperatingSystemFamily {
          get { return operatingSystemFamily_; }
          set {
            operatingSystemFamily_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        public const int ProcessorCountFieldNumber = 14;
        private int processorCount_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public int ProcessorCount {
          get { return processorCount_; }
          set {
            processorCount_ = value;
          }
        }

        public const int ProcessorFrequencyFieldNumber = 15;
        private int processorFrequency_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public int ProcessorFrequency {
          get { return processorFrequency_; }
          set {
            processorFrequency_ = value;
          }
        }

        public const int ProcessorTypeFieldNumber = 16;
        private string processorType_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public string ProcessorType {
          get { return processorType_; }
          set {
            processorType_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        public const int SystemMemorySizeFieldNumber = 17;
        private int systemMemorySize_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public int SystemMemorySize {
          get { return systemMemorySize_; }
          set {
            systemMemorySize_ = value;
          }
        }

        public const int IsFocusedFieldNumber = 18;
        private bool isFocused_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public bool IsFocused {
          get { return isFocused_; }
          set {
            isFocused_ = value;
          }
        }

        public const int IsPlayingFieldNumber = 19;
        private bool isPlaying_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public bool IsPlaying {
          get { return isPlaying_; }
          set {
            isPlaying_ = value;
          }
        }

        public const int PlatformFieldNumber = 20;
        private string platform_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public string Platform {
          get { return platform_; }
          set {
            platform_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        public const int SystemLanguageFieldNumber = 21;
        private string systemLanguage_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public string SystemLanguage {
          get { return systemLanguage_; }
          set {
            systemLanguage_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        public const int UnityVersionFieldNumber = 22;
        private string unityVersion_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public string UnityVersion {
          get { return unityVersion_; }
          set {
            unityVersion_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        public const int XrDeviceIsPresentFieldNumber = 23;
        private bool xrDeviceIsPresent_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public bool XrDeviceIsPresent {
          get { return xrDeviceIsPresent_; }
          set {
            xrDeviceIsPresent_ = value;
          }
        }

        public const int XrDeviceModelFieldNumber = 24;
        private string xrDeviceModel_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public string XrDeviceModel {
          get { return xrDeviceModel_; }
          set {
            xrDeviceModel_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public override bool Equals(object other) {
          return Equals(other as CckPingCreatorKitEnvironment);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public bool Equals(CckPingCreatorKitEnvironment other) {
          if (ReferenceEquals(other, null)) {
            return false;
          }
          if (ReferenceEquals(other, this)) {
            return true;
          }
          if (BatteryStatus != other.BatteryStatus) return false;
          if (DeviceModel != other.DeviceModel) return false;
          if (DeviceType != other.DeviceType) return false;
          if (DeviceUniqueIdentifier != other.DeviceUniqueIdentifier) return false;
          if (GraphicsDeviceName != other.GraphicsDeviceName) return false;
          if (GraphicsDeviceType != other.GraphicsDeviceType) return false;
          if (GraphicsDeviceVendor != other.GraphicsDeviceVendor) return false;
          if (GraphicsDeviceVersion != other.GraphicsDeviceVersion) return false;
          if (GraphicsMemorySize != other.GraphicsMemorySize) return false;
          if (GraphicsMultiThreaded != other.GraphicsMultiThreaded) return false;
          if (GraphicsShaderLevel != other.GraphicsShaderLevel) return false;
          if (OperatingSystem != other.OperatingSystem) return false;
          if (OperatingSystemFamily != other.OperatingSystemFamily) return false;
          if (ProcessorCount != other.ProcessorCount) return false;
          if (ProcessorFrequency != other.ProcessorFrequency) return false;
          if (ProcessorType != other.ProcessorType) return false;
          if (SystemMemorySize != other.SystemMemorySize) return false;
          if (IsFocused != other.IsFocused) return false;
          if (IsPlaying != other.IsPlaying) return false;
          if (Platform != other.Platform) return false;
          if (SystemLanguage != other.SystemLanguage) return false;
          if (UnityVersion != other.UnityVersion) return false;
          if (XrDeviceIsPresent != other.XrDeviceIsPresent) return false;
          if (XrDeviceModel != other.XrDeviceModel) return false;
          return Equals(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public override int GetHashCode() {
          int hash = 1;
          if (BatteryStatus.Length != 0) hash ^= BatteryStatus.GetHashCode();
          if (DeviceModel.Length != 0) hash ^= DeviceModel.GetHashCode();
          if (DeviceType.Length != 0) hash ^= DeviceType.GetHashCode();
          if (DeviceUniqueIdentifier.Length != 0) hash ^= DeviceUniqueIdentifier.GetHashCode();
          if (GraphicsDeviceName.Length != 0) hash ^= GraphicsDeviceName.GetHashCode();
          if (GraphicsDeviceType.Length != 0) hash ^= GraphicsDeviceType.GetHashCode();
          if (GraphicsDeviceVendor.Length != 0) hash ^= GraphicsDeviceVendor.GetHashCode();
          if (GraphicsDeviceVersion.Length != 0) hash ^= GraphicsDeviceVersion.GetHashCode();
          if (GraphicsMemorySize != 0) hash ^= GraphicsMemorySize.GetHashCode();
          if (GraphicsMultiThreaded != false) hash ^= GraphicsMultiThreaded.GetHashCode();
          if (GraphicsShaderLevel != 0) hash ^= GraphicsShaderLevel.GetHashCode();
          if (OperatingSystem.Length != 0) hash ^= OperatingSystem.GetHashCode();
          if (OperatingSystemFamily.Length != 0) hash ^= OperatingSystemFamily.GetHashCode();
          if (ProcessorCount != 0) hash ^= ProcessorCount.GetHashCode();
          if (ProcessorFrequency != 0) hash ^= ProcessorFrequency.GetHashCode();
          if (ProcessorType.Length != 0) hash ^= ProcessorType.GetHashCode();
          if (SystemMemorySize != 0) hash ^= SystemMemorySize.GetHashCode();
          if (IsFocused != false) hash ^= IsFocused.GetHashCode();
          if (IsPlaying != false) hash ^= IsPlaying.GetHashCode();
          if (Platform.Length != 0) hash ^= Platform.GetHashCode();
          if (SystemLanguage.Length != 0) hash ^= SystemLanguage.GetHashCode();
          if (UnityVersion.Length != 0) hash ^= UnityVersion.GetHashCode();
          if (XrDeviceIsPresent != false) hash ^= XrDeviceIsPresent.GetHashCode();
          if (XrDeviceModel.Length != 0) hash ^= XrDeviceModel.GetHashCode();
          if (_unknownFields != null) {
            hash ^= _unknownFields.GetHashCode();
          }
          return hash;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public override string ToString() {
          return pb::JsonFormatter.ToDiagnosticString(this);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public void WriteTo(pb::CodedOutputStream output) {
        #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
          output.WriteRawMessage(this);
        #else
          if (BatteryStatus.Length != 0) {
            output.WriteRawTag(10);
            output.WriteString(BatteryStatus);
          }
          if (DeviceModel.Length != 0) {
            output.WriteRawTag(18);
            output.WriteString(DeviceModel);
          }
          if (DeviceType.Length != 0) {
            output.WriteRawTag(26);
            output.WriteString(DeviceType);
          }
          if (DeviceUniqueIdentifier.Length != 0) {
            output.WriteRawTag(34);
            output.WriteString(DeviceUniqueIdentifier);
          }
          if (GraphicsDeviceName.Length != 0) {
            output.WriteRawTag(42);
            output.WriteString(GraphicsDeviceName);
          }
          if (GraphicsDeviceType.Length != 0) {
            output.WriteRawTag(50);
            output.WriteString(GraphicsDeviceType);
          }
          if (GraphicsDeviceVendor.Length != 0) {
            output.WriteRawTag(58);
            output.WriteString(GraphicsDeviceVendor);
          }
          if (GraphicsDeviceVersion.Length != 0) {
            output.WriteRawTag(66);
            output.WriteString(GraphicsDeviceVersion);
          }
          if (GraphicsMemorySize != 0) {
            output.WriteRawTag(72);
            output.WriteInt32(GraphicsMemorySize);
          }
          if (GraphicsMultiThreaded != false) {
            output.WriteRawTag(80);
            output.WriteBool(GraphicsMultiThreaded);
          }
          if (GraphicsShaderLevel != 0) {
            output.WriteRawTag(88);
            output.WriteInt32(GraphicsShaderLevel);
          }
          if (OperatingSystem.Length != 0) {
            output.WriteRawTag(98);
            output.WriteString(OperatingSystem);
          }
          if (OperatingSystemFamily.Length != 0) {
            output.WriteRawTag(106);
            output.WriteString(OperatingSystemFamily);
          }
          if (ProcessorCount != 0) {
            output.WriteRawTag(112);
            output.WriteInt32(ProcessorCount);
          }
          if (ProcessorFrequency != 0) {
            output.WriteRawTag(120);
            output.WriteInt32(ProcessorFrequency);
          }
          if (ProcessorType.Length != 0) {
            output.WriteRawTag(130, 1);
            output.WriteString(ProcessorType);
          }
          if (SystemMemorySize != 0) {
            output.WriteRawTag(136, 1);
            output.WriteInt32(SystemMemorySize);
          }
          if (IsFocused != false) {
            output.WriteRawTag(144, 1);
            output.WriteBool(IsFocused);
          }
          if (IsPlaying != false) {
            output.WriteRawTag(152, 1);
            output.WriteBool(IsPlaying);
          }
          if (Platform.Length != 0) {
            output.WriteRawTag(162, 1);
            output.WriteString(Platform);
          }
          if (SystemLanguage.Length != 0) {
            output.WriteRawTag(170, 1);
            output.WriteString(SystemLanguage);
          }
          if (UnityVersion.Length != 0) {
            output.WriteRawTag(178, 1);
            output.WriteString(UnityVersion);
          }
          if (XrDeviceIsPresent != false) {
            output.WriteRawTag(184, 1);
            output.WriteBool(XrDeviceIsPresent);
          }
          if (XrDeviceModel.Length != 0) {
            output.WriteRawTag(194, 1);
            output.WriteString(XrDeviceModel);
          }
          if (_unknownFields != null) {
            _unknownFields.WriteTo(output);
          }
        #endif
        }

        #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
          if (BatteryStatus.Length != 0) {
            output.WriteRawTag(10);
            output.WriteString(BatteryStatus);
          }
          if (DeviceModel.Length != 0) {
            output.WriteRawTag(18);
            output.WriteString(DeviceModel);
          }
          if (DeviceType.Length != 0) {
            output.WriteRawTag(26);
            output.WriteString(DeviceType);
          }
          if (DeviceUniqueIdentifier.Length != 0) {
            output.WriteRawTag(34);
            output.WriteString(DeviceUniqueIdentifier);
          }
          if (GraphicsDeviceName.Length != 0) {
            output.WriteRawTag(42);
            output.WriteString(GraphicsDeviceName);
          }
          if (GraphicsDeviceType.Length != 0) {
            output.WriteRawTag(50);
            output.WriteString(GraphicsDeviceType);
          }
          if (GraphicsDeviceVendor.Length != 0) {
            output.WriteRawTag(58);
            output.WriteString(GraphicsDeviceVendor);
          }
          if (GraphicsDeviceVersion.Length != 0) {
            output.WriteRawTag(66);
            output.WriteString(GraphicsDeviceVersion);
          }
          if (GraphicsMemorySize != 0) {
            output.WriteRawTag(72);
            output.WriteInt32(GraphicsMemorySize);
          }
          if (GraphicsMultiThreaded != false) {
            output.WriteRawTag(80);
            output.WriteBool(GraphicsMultiThreaded);
          }
          if (GraphicsShaderLevel != 0) {
            output.WriteRawTag(88);
            output.WriteInt32(GraphicsShaderLevel);
          }
          if (OperatingSystem.Length != 0) {
            output.WriteRawTag(98);
            output.WriteString(OperatingSystem);
          }
          if (OperatingSystemFamily.Length != 0) {
            output.WriteRawTag(106);
            output.WriteString(OperatingSystemFamily);
          }
          if (ProcessorCount != 0) {
            output.WriteRawTag(112);
            output.WriteInt32(ProcessorCount);
          }
          if (ProcessorFrequency != 0) {
            output.WriteRawTag(120);
            output.WriteInt32(ProcessorFrequency);
          }
          if (ProcessorType.Length != 0) {
            output.WriteRawTag(130, 1);
            output.WriteString(ProcessorType);
          }
          if (SystemMemorySize != 0) {
            output.WriteRawTag(136, 1);
            output.WriteInt32(SystemMemorySize);
          }
          if (IsFocused != false) {
            output.WriteRawTag(144, 1);
            output.WriteBool(IsFocused);
          }
          if (IsPlaying != false) {
            output.WriteRawTag(152, 1);
            output.WriteBool(IsPlaying);
          }
          if (Platform.Length != 0) {
            output.WriteRawTag(162, 1);
            output.WriteString(Platform);
          }
          if (SystemLanguage.Length != 0) {
            output.WriteRawTag(170, 1);
            output.WriteString(SystemLanguage);
          }
          if (UnityVersion.Length != 0) {
            output.WriteRawTag(178, 1);
            output.WriteString(UnityVersion);
          }
          if (XrDeviceIsPresent != false) {
            output.WriteRawTag(184, 1);
            output.WriteBool(XrDeviceIsPresent);
          }
          if (XrDeviceModel.Length != 0) {
            output.WriteRawTag(194, 1);
            output.WriteString(XrDeviceModel);
          }
          if (_unknownFields != null) {
            _unknownFields.WriteTo(ref output);
          }
        }
        #endif

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public int CalculateSize() {
          int size = 0;
          if (BatteryStatus.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(BatteryStatus);
          }
          if (DeviceModel.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(DeviceModel);
          }
          if (DeviceType.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(DeviceType);
          }
          if (DeviceUniqueIdentifier.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(DeviceUniqueIdentifier);
          }
          if (GraphicsDeviceName.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(GraphicsDeviceName);
          }
          if (GraphicsDeviceType.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(GraphicsDeviceType);
          }
          if (GraphicsDeviceVendor.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(GraphicsDeviceVendor);
          }
          if (GraphicsDeviceVersion.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(GraphicsDeviceVersion);
          }
          if (GraphicsMemorySize != 0) {
            size += 1 + pb::CodedOutputStream.ComputeInt32Size(GraphicsMemorySize);
          }
          if (GraphicsMultiThreaded != false) {
            size += 1 + 1;
          }
          if (GraphicsShaderLevel != 0) {
            size += 1 + pb::CodedOutputStream.ComputeInt32Size(GraphicsShaderLevel);
          }
          if (OperatingSystem.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(OperatingSystem);
          }
          if (OperatingSystemFamily.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(OperatingSystemFamily);
          }
          if (ProcessorCount != 0) {
            size += 1 + pb::CodedOutputStream.ComputeInt32Size(ProcessorCount);
          }
          if (ProcessorFrequency != 0) {
            size += 1 + pb::CodedOutputStream.ComputeInt32Size(ProcessorFrequency);
          }
          if (ProcessorType.Length != 0) {
            size += 2 + pb::CodedOutputStream.ComputeStringSize(ProcessorType);
          }
          if (SystemMemorySize != 0) {
            size += 2 + pb::CodedOutputStream.ComputeInt32Size(SystemMemorySize);
          }
          if (IsFocused != false) {
            size += 2 + 1;
          }
          if (IsPlaying != false) {
            size += 2 + 1;
          }
          if (Platform.Length != 0) {
            size += 2 + pb::CodedOutputStream.ComputeStringSize(Platform);
          }
          if (SystemLanguage.Length != 0) {
            size += 2 + pb::CodedOutputStream.ComputeStringSize(SystemLanguage);
          }
          if (UnityVersion.Length != 0) {
            size += 2 + pb::CodedOutputStream.ComputeStringSize(UnityVersion);
          }
          if (XrDeviceIsPresent != false) {
            size += 2 + 1;
          }
          if (XrDeviceModel.Length != 0) {
            size += 2 + pb::CodedOutputStream.ComputeStringSize(XrDeviceModel);
          }
          if (_unknownFields != null) {
            size += _unknownFields.CalculateSize();
          }
          return size;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public void MergeFrom(CckPingCreatorKitEnvironment other) {
          if (other == null) {
            return;
          }
          if (other.BatteryStatus.Length != 0) {
            BatteryStatus = other.BatteryStatus;
          }
          if (other.DeviceModel.Length != 0) {
            DeviceModel = other.DeviceModel;
          }
          if (other.DeviceType.Length != 0) {
            DeviceType = other.DeviceType;
          }
          if (other.DeviceUniqueIdentifier.Length != 0) {
            DeviceUniqueIdentifier = other.DeviceUniqueIdentifier;
          }
          if (other.GraphicsDeviceName.Length != 0) {
            GraphicsDeviceName = other.GraphicsDeviceName;
          }
          if (other.GraphicsDeviceType.Length != 0) {
            GraphicsDeviceType = other.GraphicsDeviceType;
          }
          if (other.GraphicsDeviceVendor.Length != 0) {
            GraphicsDeviceVendor = other.GraphicsDeviceVendor;
          }
          if (other.GraphicsDeviceVersion.Length != 0) {
            GraphicsDeviceVersion = other.GraphicsDeviceVersion;
          }
          if (other.GraphicsMemorySize != 0) {
            GraphicsMemorySize = other.GraphicsMemorySize;
          }
          if (other.GraphicsMultiThreaded != false) {
            GraphicsMultiThreaded = other.GraphicsMultiThreaded;
          }
          if (other.GraphicsShaderLevel != 0) {
            GraphicsShaderLevel = other.GraphicsShaderLevel;
          }
          if (other.OperatingSystem.Length != 0) {
            OperatingSystem = other.OperatingSystem;
          }
          if (other.OperatingSystemFamily.Length != 0) {
            OperatingSystemFamily = other.OperatingSystemFamily;
          }
          if (other.ProcessorCount != 0) {
            ProcessorCount = other.ProcessorCount;
          }
          if (other.ProcessorFrequency != 0) {
            ProcessorFrequency = other.ProcessorFrequency;
          }
          if (other.ProcessorType.Length != 0) {
            ProcessorType = other.ProcessorType;
          }
          if (other.SystemMemorySize != 0) {
            SystemMemorySize = other.SystemMemorySize;
          }
          if (other.IsFocused != false) {
            IsFocused = other.IsFocused;
          }
          if (other.IsPlaying != false) {
            IsPlaying = other.IsPlaying;
          }
          if (other.Platform.Length != 0) {
            Platform = other.Platform;
          }
          if (other.SystemLanguage.Length != 0) {
            SystemLanguage = other.SystemLanguage;
          }
          if (other.UnityVersion.Length != 0) {
            UnityVersion = other.UnityVersion;
          }
          if (other.XrDeviceIsPresent != false) {
            XrDeviceIsPresent = other.XrDeviceIsPresent;
          }
          if (other.XrDeviceModel.Length != 0) {
            XrDeviceModel = other.XrDeviceModel;
          }
          _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public void MergeFrom(pb::CodedInputStream input) {
        #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
          input.ReadRawMessage(this);
        #else
          uint tag;
          while ((tag = input.ReadTag()) != 0) {
          if ((tag & 7) == 4) {
            return;
          }
          switch(tag) {
              default:
                _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
                break;
              case 10: {
                BatteryStatus = input.ReadString();
                break;
              }
              case 18: {
                DeviceModel = input.ReadString();
                break;
              }
              case 26: {
                DeviceType = input.ReadString();
                break;
              }
              case 34: {
                DeviceUniqueIdentifier = input.ReadString();
                break;
              }
              case 42: {
                GraphicsDeviceName = input.ReadString();
                break;
              }
              case 50: {
                GraphicsDeviceType = input.ReadString();
                break;
              }
              case 58: {
                GraphicsDeviceVendor = input.ReadString();
                break;
              }
              case 66: {
                GraphicsDeviceVersion = input.ReadString();
                break;
              }
              case 72: {
                GraphicsMemorySize = input.ReadInt32();
                break;
              }
              case 80: {
                GraphicsMultiThreaded = input.ReadBool();
                break;
              }
              case 88: {
                GraphicsShaderLevel = input.ReadInt32();
                break;
              }
              case 98: {
                OperatingSystem = input.ReadString();
                break;
              }
              case 106: {
                OperatingSystemFamily = input.ReadString();
                break;
              }
              case 112: {
                ProcessorCount = input.ReadInt32();
                break;
              }
              case 120: {
                ProcessorFrequency = input.ReadInt32();
                break;
              }
              case 130: {
                ProcessorType = input.ReadString();
                break;
              }
              case 136: {
                SystemMemorySize = input.ReadInt32();
                break;
              }
              case 144: {
                IsFocused = input.ReadBool();
                break;
              }
              case 152: {
                IsPlaying = input.ReadBool();
                break;
              }
              case 162: {
                Platform = input.ReadString();
                break;
              }
              case 170: {
                SystemLanguage = input.ReadString();
                break;
              }
              case 178: {
                UnityVersion = input.ReadString();
                break;
              }
              case 184: {
                XrDeviceIsPresent = input.ReadBool();
                break;
              }
              case 194: {
                XrDeviceModel = input.ReadString();
                break;
              }
            }
          }
        #endif
        }

        #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
          uint tag;
          while ((tag = input.ReadTag()) != 0) {
          if ((tag & 7) == 4) {
            return;
          }
          switch(tag) {
              default:
                _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
                break;
              case 10: {
                BatteryStatus = input.ReadString();
                break;
              }
              case 18: {
                DeviceModel = input.ReadString();
                break;
              }
              case 26: {
                DeviceType = input.ReadString();
                break;
              }
              case 34: {
                DeviceUniqueIdentifier = input.ReadString();
                break;
              }
              case 42: {
                GraphicsDeviceName = input.ReadString();
                break;
              }
              case 50: {
                GraphicsDeviceType = input.ReadString();
                break;
              }
              case 58: {
                GraphicsDeviceVendor = input.ReadString();
                break;
              }
              case 66: {
                GraphicsDeviceVersion = input.ReadString();
                break;
              }
              case 72: {
                GraphicsMemorySize = input.ReadInt32();
                break;
              }
              case 80: {
                GraphicsMultiThreaded = input.ReadBool();
                break;
              }
              case 88: {
                GraphicsShaderLevel = input.ReadInt32();
                break;
              }
              case 98: {
                OperatingSystem = input.ReadString();
                break;
              }
              case 106: {
                OperatingSystemFamily = input.ReadString();
                break;
              }
              case 112: {
                ProcessorCount = input.ReadInt32();
                break;
              }
              case 120: {
                ProcessorFrequency = input.ReadInt32();
                break;
              }
              case 130: {
                ProcessorType = input.ReadString();
                break;
              }
              case 136: {
                SystemMemorySize = input.ReadInt32();
                break;
              }
              case 144: {
                IsFocused = input.ReadBool();
                break;
              }
              case 152: {
                IsPlaying = input.ReadBool();
                break;
              }
              case 162: {
                Platform = input.ReadString();
                break;
              }
              case 170: {
                SystemLanguage = input.ReadString();
                break;
              }
              case 178: {
                UnityVersion = input.ReadString();
                break;
              }
              case 184: {
                XrDeviceIsPresent = input.ReadBool();
                break;
              }
              case 194: {
                XrDeviceModel = input.ReadString();
                break;
              }
            }
          }
        }
        #endif

      }

    }
    #endregion

  }

  [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
  public sealed partial class CckInstall : pb::IMessage<CckInstall>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<CckInstall> _parser = new pb::MessageParser<CckInstall>(() => new CckInstall());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<CckInstall> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.PanamaEventReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CckInstall() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CckInstall(CckInstall other) : this() {
      version_ = other.version_;
      prevVersion_ = other.prevVersion_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CckInstall Clone() {
      return new CckInstall(this);
    }

    public const int VersionFieldNumber = 1;
    private string version_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Version {
      get { return version_; }
      set {
        version_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    public const int PrevVersionFieldNumber = 2;
    private string prevVersion_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string PrevVersion {
      get { return prevVersion_; }
      set {
        prevVersion_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as CckInstall);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(CckInstall other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Version != other.Version) return false;
      if (PrevVersion != other.PrevVersion) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (Version.Length != 0) hash ^= Version.GetHashCode();
      if (PrevVersion.Length != 0) hash ^= PrevVersion.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (Version.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Version);
      }
      if (PrevVersion.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(PrevVersion);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (Version.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Version);
      }
      if (PrevVersion.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(PrevVersion);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (Version.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Version);
      }
      if (PrevVersion.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(PrevVersion);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(CckInstall other) {
      if (other == null) {
        return;
      }
      if (other.Version.Length != 0) {
        Version = other.Version;
      }
      if (other.PrevVersion.Length != 0) {
        PrevVersion = other.PrevVersion;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Version = input.ReadString();
            break;
          }
          case 18: {
            PrevVersion = input.ReadString();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            Version = input.ReadString();
            break;
          }
          case 18: {
            PrevVersion = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

  }

  [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
  public sealed partial class CckMenuItem : pb::IMessage<CckMenuItem>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<CckMenuItem> _parser = new pb::MessageParser<CckMenuItem>(() => new CckMenuItem());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<CckMenuItem> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.PanamaEventReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CckMenuItem() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CckMenuItem(CckMenuItem other) : this() {
      menuItemType_ = other.menuItemType_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CckMenuItem Clone() {
      return new CckMenuItem(this);
    }

    public const int MenuItemTypeFieldNumber = 1;
    private string menuItemType_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string MenuItemType {
      get { return menuItemType_; }
      set {
        menuItemType_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as CckMenuItem);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(CckMenuItem other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (MenuItemType != other.MenuItemType) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (MenuItemType.Length != 0) hash ^= MenuItemType.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (MenuItemType.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(MenuItemType);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (MenuItemType.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(MenuItemType);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (MenuItemType.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(MenuItemType);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(CckMenuItem other) {
      if (other == null) {
        return;
      }
      if (other.MenuItemType.Length != 0) {
        MenuItemType = other.MenuItemType;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            MenuItemType = input.ReadString();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            MenuItemType = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

  }

  [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
  public sealed partial class CckOpenLink : pb::IMessage<CckOpenLink>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<CckOpenLink> _parser = new pb::MessageParser<CckOpenLink>(() => new CckOpenLink());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<CckOpenLink> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.PanamaEventReflection.Descriptor.MessageTypes[4]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CckOpenLink() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CckOpenLink(CckOpenLink other) : this() {
      linkUrl_ = other.linkUrl_;
      from_ = other.from_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CckOpenLink Clone() {
      return new CckOpenLink(this);
    }

    public const int LinkUrlFieldNumber = 1;
    private string linkUrl_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string LinkUrl {
      get { return linkUrl_; }
      set {
        linkUrl_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    public const int FromFieldNumber = 2;
    private string from_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string From {
      get { return from_; }
      set {
        from_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as CckOpenLink);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(CckOpenLink other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (LinkUrl != other.LinkUrl) return false;
      if (From != other.From) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (LinkUrl.Length != 0) hash ^= LinkUrl.GetHashCode();
      if (From.Length != 0) hash ^= From.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (LinkUrl.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(LinkUrl);
      }
      if (From.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(From);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (LinkUrl.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(LinkUrl);
      }
      if (From.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(From);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (LinkUrl.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(LinkUrl);
      }
      if (From.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(From);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(CckOpenLink other) {
      if (other == null) {
        return;
      }
      if (other.LinkUrl.Length != 0) {
        LinkUrl = other.LinkUrl;
      }
      if (other.From.Length != 0) {
        From = other.From;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            LinkUrl = input.ReadString();
            break;
          }
          case 18: {
            From = input.ReadString();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            LinkUrl = input.ReadString();
            break;
          }
          case 18: {
            From = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

  }

  [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
  public sealed partial class CckEditorPreviewStop : pb::IMessage<CckEditorPreviewStop>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<CckEditorPreviewStop> _parser = new pb::MessageParser<CckEditorPreviewStop>(() => new CckEditorPreviewStop());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<CckEditorPreviewStop> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.PanamaEventReflection.Descriptor.MessageTypes[5]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CckEditorPreviewStop() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CckEditorPreviewStop(CckEditorPreviewStop other) : this() {
      durationMs_ = other.durationMs_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CckEditorPreviewStop Clone() {
      return new CckEditorPreviewStop(this);
    }

    public const int DurationMsFieldNumber = 1;
    private ulong durationMs_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ulong DurationMs {
      get { return durationMs_; }
      set {
        durationMs_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as CckEditorPreviewStop);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(CckEditorPreviewStop other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (DurationMs != other.DurationMs) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (DurationMs != 0UL) hash ^= DurationMs.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (DurationMs != 0UL) {
        output.WriteRawTag(8);
        output.WriteUInt64(DurationMs);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (DurationMs != 0UL) {
        output.WriteRawTag(8);
        output.WriteUInt64(DurationMs);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (DurationMs != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(DurationMs);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(CckEditorPreviewStop other) {
      if (other == null) {
        return;
      }
      if (other.DurationMs != 0UL) {
        DurationMs = other.DurationMs;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            DurationMs = input.ReadUInt64();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            DurationMs = input.ReadUInt64();
            break;
          }
        }
      }
    }
    #endif

  }

  [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
  public sealed partial class CckWorldUploadStart : pb::IMessage<CckWorldUploadStart>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<CckWorldUploadStart> _parser = new pb::MessageParser<CckWorldUploadStart>(() => new CckWorldUploadStart());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<CckWorldUploadStart> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.PanamaEventReflection.Descriptor.MessageTypes[6]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CckWorldUploadStart() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CckWorldUploadStart(CckWorldUploadStart other) : this() {
      isBeta_ = other.isBeta_;
      isPreview_ = other.isPreview_;
      previewWin_ = other.previewWin_;
      previewMac_ = other.previewMac_;
      previewAndroid_ = other.previewAndroid_;
      previewIos_ = other.previewIos_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CckWorldUploadStart Clone() {
      return new CckWorldUploadStart(this);
    }

    public const int IsBetaFieldNumber = 1;
    private bool isBeta_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool IsBeta {
      get { return isBeta_; }
      set {
        isBeta_ = value;
      }
    }

    public const int IsPreviewFieldNumber = 2;
    private bool isPreview_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool IsPreview {
      get { return isPreview_; }
      set {
        isPreview_ = value;
      }
    }

    public const int PreviewWinFieldNumber = 3;
    private bool previewWin_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool PreviewWin {
      get { return previewWin_; }
      set {
        previewWin_ = value;
      }
    }

    public const int PreviewMacFieldNumber = 4;
    private bool previewMac_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool PreviewMac {
      get { return previewMac_; }
      set {
        previewMac_ = value;
      }
    }

    public const int PreviewAndroidFieldNumber = 5;
    private bool previewAndroid_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool PreviewAndroid {
      get { return previewAndroid_; }
      set {
        previewAndroid_ = value;
      }
    }

    public const int PreviewIosFieldNumber = 6;
    private bool previewIos_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool PreviewIos {
      get { return previewIos_; }
      set {
        previewIos_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as CckWorldUploadStart);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(CckWorldUploadStart other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (IsBeta != other.IsBeta) return false;
      if (IsPreview != other.IsPreview) return false;
      if (PreviewWin != other.PreviewWin) return false;
      if (PreviewMac != other.PreviewMac) return false;
      if (PreviewAndroid != other.PreviewAndroid) return false;
      if (PreviewIos != other.PreviewIos) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (IsBeta != false) hash ^= IsBeta.GetHashCode();
      if (IsPreview != false) hash ^= IsPreview.GetHashCode();
      if (PreviewWin != false) hash ^= PreviewWin.GetHashCode();
      if (PreviewMac != false) hash ^= PreviewMac.GetHashCode();
      if (PreviewAndroid != false) hash ^= PreviewAndroid.GetHashCode();
      if (PreviewIos != false) hash ^= PreviewIos.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (IsBeta != false) {
        output.WriteRawTag(8);
        output.WriteBool(IsBeta);
      }
      if (IsPreview != false) {
        output.WriteRawTag(16);
        output.WriteBool(IsPreview);
      }
      if (PreviewWin != false) {
        output.WriteRawTag(24);
        output.WriteBool(PreviewWin);
      }
      if (PreviewMac != false) {
        output.WriteRawTag(32);
        output.WriteBool(PreviewMac);
      }
      if (PreviewAndroid != false) {
        output.WriteRawTag(40);
        output.WriteBool(PreviewAndroid);
      }
      if (PreviewIos != false) {
        output.WriteRawTag(48);
        output.WriteBool(PreviewIos);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (IsBeta != false) {
        output.WriteRawTag(8);
        output.WriteBool(IsBeta);
      }
      if (IsPreview != false) {
        output.WriteRawTag(16);
        output.WriteBool(IsPreview);
      }
      if (PreviewWin != false) {
        output.WriteRawTag(24);
        output.WriteBool(PreviewWin);
      }
      if (PreviewMac != false) {
        output.WriteRawTag(32);
        output.WriteBool(PreviewMac);
      }
      if (PreviewAndroid != false) {
        output.WriteRawTag(40);
        output.WriteBool(PreviewAndroid);
      }
      if (PreviewIos != false) {
        output.WriteRawTag(48);
        output.WriteBool(PreviewIos);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (IsBeta != false) {
        size += 1 + 1;
      }
      if (IsPreview != false) {
        size += 1 + 1;
      }
      if (PreviewWin != false) {
        size += 1 + 1;
      }
      if (PreviewMac != false) {
        size += 1 + 1;
      }
      if (PreviewAndroid != false) {
        size += 1 + 1;
      }
      if (PreviewIos != false) {
        size += 1 + 1;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(CckWorldUploadStart other) {
      if (other == null) {
        return;
      }
      if (other.IsBeta != false) {
        IsBeta = other.IsBeta;
      }
      if (other.IsPreview != false) {
        IsPreview = other.IsPreview;
      }
      if (other.PreviewWin != false) {
        PreviewWin = other.PreviewWin;
      }
      if (other.PreviewMac != false) {
        PreviewMac = other.PreviewMac;
      }
      if (other.PreviewAndroid != false) {
        PreviewAndroid = other.PreviewAndroid;
      }
      if (other.PreviewIos != false) {
        PreviewIos = other.PreviewIos;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            IsBeta = input.ReadBool();
            break;
          }
          case 16: {
            IsPreview = input.ReadBool();
            break;
          }
          case 24: {
            PreviewWin = input.ReadBool();
            break;
          }
          case 32: {
            PreviewMac = input.ReadBool();
            break;
          }
          case 40: {
            PreviewAndroid = input.ReadBool();
            break;
          }
          case 48: {
            PreviewIos = input.ReadBool();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            IsBeta = input.ReadBool();
            break;
          }
          case 16: {
            IsPreview = input.ReadBool();
            break;
          }
          case 24: {
            PreviewWin = input.ReadBool();
            break;
          }
          case 32: {
            PreviewMac = input.ReadBool();
            break;
          }
          case 40: {
            PreviewAndroid = input.ReadBool();
            break;
          }
          case 48: {
            PreviewIos = input.ReadBool();
            break;
          }
        }
      }
    }
    #endif

  }

  [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
  public sealed partial class CckWorldUploadComplete : pb::IMessage<CckWorldUploadComplete>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<CckWorldUploadComplete> _parser = new pb::MessageParser<CckWorldUploadComplete>(() => new CckWorldUploadComplete());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<CckWorldUploadComplete> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.PanamaEventReflection.Descriptor.MessageTypes[7]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CckWorldUploadComplete() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CckWorldUploadComplete(CckWorldUploadComplete other) : this() {
      isBeta_ = other.isBeta_;
      isPreview_ = other.isPreview_;
      previewWin_ = other.previewWin_;
      previewMac_ = other.previewMac_;
      previewAndroid_ = other.previewAndroid_;
      previewIos_ = other.previewIos_;
      durationMs_ = other.durationMs_;
      buildStats_ = other.buildStats_.Clone();
      sceneStats_ = other.sceneStats_ != null ? other.sceneStats_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CckWorldUploadComplete Clone() {
      return new CckWorldUploadComplete(this);
    }

    public const int IsBetaFieldNumber = 1;
    private bool isBeta_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool IsBeta {
      get { return isBeta_; }
      set {
        isBeta_ = value;
      }
    }

    public const int IsPreviewFieldNumber = 2;
    private bool isPreview_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool IsPreview {
      get { return isPreview_; }
      set {
        isPreview_ = value;
      }
    }

    public const int PreviewWinFieldNumber = 3;
    private bool previewWin_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool PreviewWin {
      get { return previewWin_; }
      set {
        previewWin_ = value;
      }
    }

    public const int PreviewMacFieldNumber = 4;
    private bool previewMac_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool PreviewMac {
      get { return previewMac_; }
      set {
        previewMac_ = value;
      }
    }

    public const int PreviewAndroidFieldNumber = 5;
    private bool previewAndroid_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool PreviewAndroid {
      get { return previewAndroid_; }
      set {
        previewAndroid_ = value;
      }
    }

    public const int PreviewIosFieldNumber = 6;
    private bool previewIos_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool PreviewIos {
      get { return previewIos_; }
      set {
        previewIos_ = value;
      }
    }

    public const int DurationMsFieldNumber = 7;
    private ulong durationMs_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ulong DurationMs {
      get { return durationMs_; }
      set {
        durationMs_ = value;
      }
    }

    public const int BuildStatsFieldNumber = 8;
    private static readonly pb::FieldCodec<global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete.Types.CckWorldBuildStatsValue> _repeated_buildStats_codec
        = pb::FieldCodec.ForMessage(66, global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete.Types.CckWorldBuildStatsValue.Parser);
    private readonly pbc::RepeatedField<global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete.Types.CckWorldBuildStatsValue> buildStats_ = new pbc::RepeatedField<global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete.Types.CckWorldBuildStatsValue>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete.Types.CckWorldBuildStatsValue> BuildStats {
      get { return buildStats_; }
    }

    public const int SceneStatsFieldNumber = 9;
    private global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete.Types.CckWorldSceneStatsValue sceneStats_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete.Types.CckWorldSceneStatsValue SceneStats {
      get { return sceneStats_; }
      set {
        sceneStats_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as CckWorldUploadComplete);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(CckWorldUploadComplete other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (IsBeta != other.IsBeta) return false;
      if (IsPreview != other.IsPreview) return false;
      if (PreviewWin != other.PreviewWin) return false;
      if (PreviewMac != other.PreviewMac) return false;
      if (PreviewAndroid != other.PreviewAndroid) return false;
      if (PreviewIos != other.PreviewIos) return false;
      if (DurationMs != other.DurationMs) return false;
      if(!buildStats_.Equals(other.buildStats_)) return false;
      if (!object.Equals(SceneStats, other.SceneStats)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (IsBeta != false) hash ^= IsBeta.GetHashCode();
      if (IsPreview != false) hash ^= IsPreview.GetHashCode();
      if (PreviewWin != false) hash ^= PreviewWin.GetHashCode();
      if (PreviewMac != false) hash ^= PreviewMac.GetHashCode();
      if (PreviewAndroid != false) hash ^= PreviewAndroid.GetHashCode();
      if (PreviewIos != false) hash ^= PreviewIos.GetHashCode();
      if (DurationMs != 0UL) hash ^= DurationMs.GetHashCode();
      hash ^= buildStats_.GetHashCode();
      if (sceneStats_ != null) hash ^= SceneStats.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (IsBeta != false) {
        output.WriteRawTag(8);
        output.WriteBool(IsBeta);
      }
      if (IsPreview != false) {
        output.WriteRawTag(16);
        output.WriteBool(IsPreview);
      }
      if (PreviewWin != false) {
        output.WriteRawTag(24);
        output.WriteBool(PreviewWin);
      }
      if (PreviewMac != false) {
        output.WriteRawTag(32);
        output.WriteBool(PreviewMac);
      }
      if (PreviewAndroid != false) {
        output.WriteRawTag(40);
        output.WriteBool(PreviewAndroid);
      }
      if (PreviewIos != false) {
        output.WriteRawTag(48);
        output.WriteBool(PreviewIos);
      }
      if (DurationMs != 0UL) {
        output.WriteRawTag(56);
        output.WriteUInt64(DurationMs);
      }
      buildStats_.WriteTo(output, _repeated_buildStats_codec);
      if (sceneStats_ != null) {
        output.WriteRawTag(74);
        output.WriteMessage(SceneStats);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (IsBeta != false) {
        output.WriteRawTag(8);
        output.WriteBool(IsBeta);
      }
      if (IsPreview != false) {
        output.WriteRawTag(16);
        output.WriteBool(IsPreview);
      }
      if (PreviewWin != false) {
        output.WriteRawTag(24);
        output.WriteBool(PreviewWin);
      }
      if (PreviewMac != false) {
        output.WriteRawTag(32);
        output.WriteBool(PreviewMac);
      }
      if (PreviewAndroid != false) {
        output.WriteRawTag(40);
        output.WriteBool(PreviewAndroid);
      }
      if (PreviewIos != false) {
        output.WriteRawTag(48);
        output.WriteBool(PreviewIos);
      }
      if (DurationMs != 0UL) {
        output.WriteRawTag(56);
        output.WriteUInt64(DurationMs);
      }
      buildStats_.WriteTo(ref output, _repeated_buildStats_codec);
      if (sceneStats_ != null) {
        output.WriteRawTag(74);
        output.WriteMessage(SceneStats);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (IsBeta != false) {
        size += 1 + 1;
      }
      if (IsPreview != false) {
        size += 1 + 1;
      }
      if (PreviewWin != false) {
        size += 1 + 1;
      }
      if (PreviewMac != false) {
        size += 1 + 1;
      }
      if (PreviewAndroid != false) {
        size += 1 + 1;
      }
      if (PreviewIos != false) {
        size += 1 + 1;
      }
      if (DurationMs != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(DurationMs);
      }
      size += buildStats_.CalculateSize(_repeated_buildStats_codec);
      if (sceneStats_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(SceneStats);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(CckWorldUploadComplete other) {
      if (other == null) {
        return;
      }
      if (other.IsBeta != false) {
        IsBeta = other.IsBeta;
      }
      if (other.IsPreview != false) {
        IsPreview = other.IsPreview;
      }
      if (other.PreviewWin != false) {
        PreviewWin = other.PreviewWin;
      }
      if (other.PreviewMac != false) {
        PreviewMac = other.PreviewMac;
      }
      if (other.PreviewAndroid != false) {
        PreviewAndroid = other.PreviewAndroid;
      }
      if (other.PreviewIos != false) {
        PreviewIos = other.PreviewIos;
      }
      if (other.DurationMs != 0UL) {
        DurationMs = other.DurationMs;
      }
      buildStats_.Add(other.buildStats_);
      if (other.sceneStats_ != null) {
        if (sceneStats_ == null) {
          SceneStats = new global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete.Types.CckWorldSceneStatsValue();
        }
        SceneStats.MergeFrom(other.SceneStats);
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            IsBeta = input.ReadBool();
            break;
          }
          case 16: {
            IsPreview = input.ReadBool();
            break;
          }
          case 24: {
            PreviewWin = input.ReadBool();
            break;
          }
          case 32: {
            PreviewMac = input.ReadBool();
            break;
          }
          case 40: {
            PreviewAndroid = input.ReadBool();
            break;
          }
          case 48: {
            PreviewIos = input.ReadBool();
            break;
          }
          case 56: {
            DurationMs = input.ReadUInt64();
            break;
          }
          case 66: {
            buildStats_.AddEntriesFrom(input, _repeated_buildStats_codec);
            break;
          }
          case 74: {
            if (sceneStats_ == null) {
              SceneStats = new global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete.Types.CckWorldSceneStatsValue();
            }
            input.ReadMessage(SceneStats);
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            IsBeta = input.ReadBool();
            break;
          }
          case 16: {
            IsPreview = input.ReadBool();
            break;
          }
          case 24: {
            PreviewWin = input.ReadBool();
            break;
          }
          case 32: {
            PreviewMac = input.ReadBool();
            break;
          }
          case 40: {
            PreviewAndroid = input.ReadBool();
            break;
          }
          case 48: {
            PreviewIos = input.ReadBool();
            break;
          }
          case 56: {
            DurationMs = input.ReadUInt64();
            break;
          }
          case 66: {
            buildStats_.AddEntriesFrom(ref input, _repeated_buildStats_codec);
            break;
          }
          case 74: {
            if (sceneStats_ == null) {
              SceneStats = new global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete.Types.CckWorldSceneStatsValue();
            }
            input.ReadMessage(SceneStats);
            break;
          }
        }
      }
    }
    #endif

    #region Nested types
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static partial class Types {
      [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
      public sealed partial class CckWorldBuildStatsValue : pb::IMessage<CckWorldBuildStatsValue>
      #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
          , pb::IBufferMessage
      #endif
      {
        private static readonly pb::MessageParser<CckWorldBuildStatsValue> _parser = new pb::MessageParser<CckWorldBuildStatsValue>(() => new CckWorldBuildStatsValue());
        private pb::UnknownFieldSet _unknownFields;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public static pb::MessageParser<CckWorldBuildStatsValue> Parser { get { return _parser; } }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public static pbr::MessageDescriptor Descriptor {
          get { return global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete.Descriptor.NestedTypes[0]; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        pbr::MessageDescriptor pb::IMessage.Descriptor {
          get { return Descriptor; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public CckWorldBuildStatsValue() {
          OnConstruction();
        }

        partial void OnConstruction();

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public CckWorldBuildStatsValue(CckWorldBuildStatsValue other) : this() {
          platform_ = other.platform_;
          sceneIndex_ = other.sceneIndex_;
          buildSize_ = other.buildSize_;
          _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public CckWorldBuildStatsValue Clone() {
          return new CckWorldBuildStatsValue(this);
        }

        public const int PlatformFieldNumber = 1;
        private string platform_ = "";
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public string Platform {
          get { return platform_; }
          set {
            platform_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
          }
        }

        public const int SceneIndexFieldNumber = 2;
        private int sceneIndex_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public int SceneIndex {
          get { return sceneIndex_; }
          set {
            sceneIndex_ = value;
          }
        }

        public const int BuildSizeFieldNumber = 3;
        private ulong buildSize_;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public ulong BuildSize {
          get { return buildSize_; }
          set {
            buildSize_ = value;
          }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public override bool Equals(object other) {
          return Equals(other as CckWorldBuildStatsValue);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public bool Equals(CckWorldBuildStatsValue other) {
          if (ReferenceEquals(other, null)) {
            return false;
          }
          if (ReferenceEquals(other, this)) {
            return true;
          }
          if (Platform != other.Platform) return false;
          if (SceneIndex != other.SceneIndex) return false;
          if (BuildSize != other.BuildSize) return false;
          return Equals(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public override int GetHashCode() {
          int hash = 1;
          if (Platform.Length != 0) hash ^= Platform.GetHashCode();
          if (SceneIndex != 0) hash ^= SceneIndex.GetHashCode();
          if (BuildSize != 0UL) hash ^= BuildSize.GetHashCode();
          if (_unknownFields != null) {
            hash ^= _unknownFields.GetHashCode();
          }
          return hash;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public override string ToString() {
          return pb::JsonFormatter.ToDiagnosticString(this);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public void WriteTo(pb::CodedOutputStream output) {
        #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
          output.WriteRawMessage(this);
        #else
          if (Platform.Length != 0) {
            output.WriteRawTag(10);
            output.WriteString(Platform);
          }
          if (SceneIndex != 0) {
            output.WriteRawTag(16);
            output.WriteInt32(SceneIndex);
          }
          if (BuildSize != 0UL) {
            output.WriteRawTag(24);
            output.WriteUInt64(BuildSize);
          }
          if (_unknownFields != null) {
            _unknownFields.WriteTo(output);
          }
        #endif
        }

        #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
          if (Platform.Length != 0) {
            output.WriteRawTag(10);
            output.WriteString(Platform);
          }
          if (SceneIndex != 0) {
            output.WriteRawTag(16);
            output.WriteInt32(SceneIndex);
          }
          if (BuildSize != 0UL) {
            output.WriteRawTag(24);
            output.WriteUInt64(BuildSize);
          }
          if (_unknownFields != null) {
            _unknownFields.WriteTo(ref output);
          }
        }
        #endif

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public int CalculateSize() {
          int size = 0;
          if (Platform.Length != 0) {
            size += 1 + pb::CodedOutputStream.ComputeStringSize(Platform);
          }
          if (SceneIndex != 0) {
            size += 1 + pb::CodedOutputStream.ComputeInt32Size(SceneIndex);
          }
          if (BuildSize != 0UL) {
            size += 1 + pb::CodedOutputStream.ComputeUInt64Size(BuildSize);
          }
          if (_unknownFields != null) {
            size += _unknownFields.CalculateSize();
          }
          return size;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public void MergeFrom(CckWorldBuildStatsValue other) {
          if (other == null) {
            return;
          }
          if (other.Platform.Length != 0) {
            Platform = other.Platform;
          }
          if (other.SceneIndex != 0) {
            SceneIndex = other.SceneIndex;
          }
          if (other.BuildSize != 0UL) {
            BuildSize = other.BuildSize;
          }
          _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public void MergeFrom(pb::CodedInputStream input) {
        #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
          input.ReadRawMessage(this);
        #else
          uint tag;
          while ((tag = input.ReadTag()) != 0) {
          if ((tag & 7) == 4) {
            return;
          }
          switch(tag) {
              default:
                _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
                break;
              case 10: {
                Platform = input.ReadString();
                break;
              }
              case 16: {
                SceneIndex = input.ReadInt32();
                break;
              }
              case 24: {
                BuildSize = input.ReadUInt64();
                break;
              }
            }
          }
        #endif
        }

        #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
          uint tag;
          while ((tag = input.ReadTag()) != 0) {
          if ((tag & 7) == 4) {
            return;
          }
          switch(tag) {
              default:
                _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
                break;
              case 10: {
                Platform = input.ReadString();
                break;
              }
              case 16: {
                SceneIndex = input.ReadInt32();
                break;
              }
              case 24: {
                BuildSize = input.ReadUInt64();
                break;
              }
            }
          }
        }
        #endif

      }

      [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
      public sealed partial class CckWorldSceneStatsValue : pb::IMessage<CckWorldSceneStatsValue>
      #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
          , pb::IBufferMessage
      #endif
      {
        private static readonly pb::MessageParser<CckWorldSceneStatsValue> _parser = new pb::MessageParser<CckWorldSceneStatsValue>(() => new CckWorldSceneStatsValue());
        private pb::UnknownFieldSet _unknownFields;
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public static pb::MessageParser<CckWorldSceneStatsValue> Parser { get { return _parser; } }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public static pbr::MessageDescriptor Descriptor {
          get { return global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete.Descriptor.NestedTypes[1]; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        pbr::MessageDescriptor pb::IMessage.Descriptor {
          get { return Descriptor; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public CckWorldSceneStatsValue() {
          OnConstruction();
        }

        partial void OnConstruction();

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public CckWorldSceneStatsValue(CckWorldSceneStatsValue other) : this() {
          components_ = other.components_.Clone();
          _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public CckWorldSceneStatsValue Clone() {
          return new CckWorldSceneStatsValue(this);
        }

        public const int ComponentsFieldNumber = 1;
        private static readonly pb::FieldCodec<global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete.Types.CckWorldSceneStatsValue.Types.CckWorldSceneStatsComponentsValue> _repeated_components_codec
            = pb::FieldCodec.ForMessage(10, global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete.Types.CckWorldSceneStatsValue.Types.CckWorldSceneStatsComponentsValue.Parser);
        private readonly pbc::RepeatedField<global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete.Types.CckWorldSceneStatsValue.Types.CckWorldSceneStatsComponentsValue> components_ = new pbc::RepeatedField<global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete.Types.CckWorldSceneStatsValue.Types.CckWorldSceneStatsComponentsValue>();
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public pbc::RepeatedField<global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete.Types.CckWorldSceneStatsValue.Types.CckWorldSceneStatsComponentsValue> Components {
          get { return components_; }
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public override bool Equals(object other) {
          return Equals(other as CckWorldSceneStatsValue);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public bool Equals(CckWorldSceneStatsValue other) {
          if (ReferenceEquals(other, null)) {
            return false;
          }
          if (ReferenceEquals(other, this)) {
            return true;
          }
          if(!components_.Equals(other.components_)) return false;
          return Equals(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public override int GetHashCode() {
          int hash = 1;
          hash ^= components_.GetHashCode();
          if (_unknownFields != null) {
            hash ^= _unknownFields.GetHashCode();
          }
          return hash;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public override string ToString() {
          return pb::JsonFormatter.ToDiagnosticString(this);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public void WriteTo(pb::CodedOutputStream output) {
        #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
          output.WriteRawMessage(this);
        #else
          components_.WriteTo(output, _repeated_components_codec);
          if (_unknownFields != null) {
            _unknownFields.WriteTo(output);
          }
        #endif
        }

        #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
          components_.WriteTo(ref output, _repeated_components_codec);
          if (_unknownFields != null) {
            _unknownFields.WriteTo(ref output);
          }
        }
        #endif

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public int CalculateSize() {
          int size = 0;
          size += components_.CalculateSize(_repeated_components_codec);
          if (_unknownFields != null) {
            size += _unknownFields.CalculateSize();
          }
          return size;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public void MergeFrom(CckWorldSceneStatsValue other) {
          if (other == null) {
            return;
          }
          components_.Add(other.components_);
          _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public void MergeFrom(pb::CodedInputStream input) {
        #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
          input.ReadRawMessage(this);
        #else
          uint tag;
          while ((tag = input.ReadTag()) != 0) {
          if ((tag & 7) == 4) {
            return;
          }
          switch(tag) {
              default:
                _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
                break;
              case 10: {
                components_.AddEntriesFrom(input, _repeated_components_codec);
                break;
              }
            }
          }
        #endif
        }

        #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
          uint tag;
          while ((tag = input.ReadTag()) != 0) {
          if ((tag & 7) == 4) {
            return;
          }
          switch(tag) {
              default:
                _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
                break;
              case 10: {
                components_.AddEntriesFrom(ref input, _repeated_components_codec);
                break;
              }
            }
          }
        }
        #endif

        #region Nested types
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
        [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
        public static partial class Types {
          [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
          public sealed partial class CckWorldSceneStatsComponentsValue : pb::IMessage<CckWorldSceneStatsComponentsValue>
          #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
              , pb::IBufferMessage
          #endif
          {
            private static readonly pb::MessageParser<CckWorldSceneStatsComponentsValue> _parser = new pb::MessageParser<CckWorldSceneStatsComponentsValue>(() => new CckWorldSceneStatsComponentsValue());
            private pb::UnknownFieldSet _unknownFields;
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
            public static pb::MessageParser<CckWorldSceneStatsComponentsValue> Parser { get { return _parser; } }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
            public static pbr::MessageDescriptor Descriptor {
              get { return global::ClusterVR.CreatorKit.Proto.CckWorldUploadComplete.Types.CckWorldSceneStatsValue.Descriptor.NestedTypes[0]; }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
            pbr::MessageDescriptor pb::IMessage.Descriptor {
              get { return Descriptor; }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
            public CckWorldSceneStatsComponentsValue() {
              OnConstruction();
            }

            partial void OnConstruction();

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
            public CckWorldSceneStatsComponentsValue(CckWorldSceneStatsComponentsValue other) : this() {
              name_ = other.name_;
              count_ = other.count_;
              _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
            public CckWorldSceneStatsComponentsValue Clone() {
              return new CckWorldSceneStatsComponentsValue(this);
            }

            public const int NameFieldNumber = 1;
            private string name_ = "";
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
            public string Name {
              get { return name_; }
              set {
                name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
              }
            }

            public const int CountFieldNumber = 2;
            private uint count_;
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
            public uint Count {
              get { return count_; }
              set {
                count_ = value;
              }
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
            public override bool Equals(object other) {
              return Equals(other as CckWorldSceneStatsComponentsValue);
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
            public bool Equals(CckWorldSceneStatsComponentsValue other) {
              if (ReferenceEquals(other, null)) {
                return false;
              }
              if (ReferenceEquals(other, this)) {
                return true;
              }
              if (Name != other.Name) return false;
              if (Count != other.Count) return false;
              return Equals(_unknownFields, other._unknownFields);
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
            public override int GetHashCode() {
              int hash = 1;
              if (Name.Length != 0) hash ^= Name.GetHashCode();
              if (Count != 0) hash ^= Count.GetHashCode();
              if (_unknownFields != null) {
                hash ^= _unknownFields.GetHashCode();
              }
              return hash;
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
            public override string ToString() {
              return pb::JsonFormatter.ToDiagnosticString(this);
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
            public void WriteTo(pb::CodedOutputStream output) {
            #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
              output.WriteRawMessage(this);
            #else
              if (Name.Length != 0) {
                output.WriteRawTag(10);
                output.WriteString(Name);
              }
              if (Count != 0) {
                output.WriteRawTag(16);
                output.WriteUInt32(Count);
              }
              if (_unknownFields != null) {
                _unknownFields.WriteTo(output);
              }
            #endif
            }

            #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
            void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
              if (Name.Length != 0) {
                output.WriteRawTag(10);
                output.WriteString(Name);
              }
              if (Count != 0) {
                output.WriteRawTag(16);
                output.WriteUInt32(Count);
              }
              if (_unknownFields != null) {
                _unknownFields.WriteTo(ref output);
              }
            }
            #endif

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
            public int CalculateSize() {
              int size = 0;
              if (Name.Length != 0) {
                size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
              }
              if (Count != 0) {
                size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Count);
              }
              if (_unknownFields != null) {
                size += _unknownFields.CalculateSize();
              }
              return size;
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
            public void MergeFrom(CckWorldSceneStatsComponentsValue other) {
              if (other == null) {
                return;
              }
              if (other.Name.Length != 0) {
                Name = other.Name;
              }
              if (other.Count != 0) {
                Count = other.Count;
              }
              _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
            }

            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
            public void MergeFrom(pb::CodedInputStream input) {
            #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
              input.ReadRawMessage(this);
            #else
              uint tag;
              while ((tag = input.ReadTag()) != 0) {
              if ((tag & 7) == 4) {
                return;
              }
              switch(tag) {
                  default:
                    _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
                    break;
                  case 10: {
                    Name = input.ReadString();
                    break;
                  }
                  case 16: {
                    Count = input.ReadUInt32();
                    break;
                  }
                }
              }
            #endif
            }

            #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
            [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
            [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
            void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
              uint tag;
              while ((tag = input.ReadTag()) != 0) {
              if ((tag & 7) == 4) {
                return;
              }
              switch(tag) {
                  default:
                    _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
                    break;
                  case 10: {
                    Name = input.ReadString();
                    break;
                  }
                  case 16: {
                    Count = input.ReadUInt32();
                    break;
                  }
                }
              }
            }
            #endif

          }

        }
        #endregion

      }

    }
    #endregion

  }

  [global::System.Diagnostics.DebuggerDisplayAttribute("{ToString(),nq}")]
  public sealed partial class CckWorldUploadFailed : pb::IMessage<CckWorldUploadFailed>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<CckWorldUploadFailed> _parser = new pb::MessageParser<CckWorldUploadFailed>(() => new CckWorldUploadFailed());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<CckWorldUploadFailed> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.PanamaEventReflection.Descriptor.MessageTypes[8]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CckWorldUploadFailed() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CckWorldUploadFailed(CckWorldUploadFailed other) : this() {
      isBeta_ = other.isBeta_;
      isPreview_ = other.isPreview_;
      previewWin_ = other.previewWin_;
      previewMac_ = other.previewMac_;
      previewAndroid_ = other.previewAndroid_;
      previewIos_ = other.previewIos_;
      durationMs_ = other.durationMs_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CckWorldUploadFailed Clone() {
      return new CckWorldUploadFailed(this);
    }

    public const int IsBetaFieldNumber = 1;
    private bool isBeta_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool IsBeta {
      get { return isBeta_; }
      set {
        isBeta_ = value;
      }
    }

    public const int IsPreviewFieldNumber = 2;
    private bool isPreview_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool IsPreview {
      get { return isPreview_; }
      set {
        isPreview_ = value;
      }
    }

    public const int PreviewWinFieldNumber = 3;
    private bool previewWin_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool PreviewWin {
      get { return previewWin_; }
      set {
        previewWin_ = value;
      }
    }

    public const int PreviewMacFieldNumber = 4;
    private bool previewMac_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool PreviewMac {
      get { return previewMac_; }
      set {
        previewMac_ = value;
      }
    }

    public const int PreviewAndroidFieldNumber = 5;
    private bool previewAndroid_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool PreviewAndroid {
      get { return previewAndroid_; }
      set {
        previewAndroid_ = value;
      }
    }

    public const int PreviewIosFieldNumber = 6;
    private bool previewIos_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool PreviewIos {
      get { return previewIos_; }
      set {
        previewIos_ = value;
      }
    }

    public const int DurationMsFieldNumber = 7;
    private ulong durationMs_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ulong DurationMs {
      get { return durationMs_; }
      set {
        durationMs_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as CckWorldUploadFailed);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(CckWorldUploadFailed other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (IsBeta != other.IsBeta) return false;
      if (IsPreview != other.IsPreview) return false;
      if (PreviewWin != other.PreviewWin) return false;
      if (PreviewMac != other.PreviewMac) return false;
      if (PreviewAndroid != other.PreviewAndroid) return false;
      if (PreviewIos != other.PreviewIos) return false;
      if (DurationMs != other.DurationMs) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (IsBeta != false) hash ^= IsBeta.GetHashCode();
      if (IsPreview != false) hash ^= IsPreview.GetHashCode();
      if (PreviewWin != false) hash ^= PreviewWin.GetHashCode();
      if (PreviewMac != false) hash ^= PreviewMac.GetHashCode();
      if (PreviewAndroid != false) hash ^= PreviewAndroid.GetHashCode();
      if (PreviewIos != false) hash ^= PreviewIos.GetHashCode();
      if (DurationMs != 0UL) hash ^= DurationMs.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (IsBeta != false) {
        output.WriteRawTag(8);
        output.WriteBool(IsBeta);
      }
      if (IsPreview != false) {
        output.WriteRawTag(16);
        output.WriteBool(IsPreview);
      }
      if (PreviewWin != false) {
        output.WriteRawTag(24);
        output.WriteBool(PreviewWin);
      }
      if (PreviewMac != false) {
        output.WriteRawTag(32);
        output.WriteBool(PreviewMac);
      }
      if (PreviewAndroid != false) {
        output.WriteRawTag(40);
        output.WriteBool(PreviewAndroid);
      }
      if (PreviewIos != false) {
        output.WriteRawTag(48);
        output.WriteBool(PreviewIos);
      }
      if (DurationMs != 0UL) {
        output.WriteRawTag(56);
        output.WriteUInt64(DurationMs);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (IsBeta != false) {
        output.WriteRawTag(8);
        output.WriteBool(IsBeta);
      }
      if (IsPreview != false) {
        output.WriteRawTag(16);
        output.WriteBool(IsPreview);
      }
      if (PreviewWin != false) {
        output.WriteRawTag(24);
        output.WriteBool(PreviewWin);
      }
      if (PreviewMac != false) {
        output.WriteRawTag(32);
        output.WriteBool(PreviewMac);
      }
      if (PreviewAndroid != false) {
        output.WriteRawTag(40);
        output.WriteBool(PreviewAndroid);
      }
      if (PreviewIos != false) {
        output.WriteRawTag(48);
        output.WriteBool(PreviewIos);
      }
      if (DurationMs != 0UL) {
        output.WriteRawTag(56);
        output.WriteUInt64(DurationMs);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (IsBeta != false) {
        size += 1 + 1;
      }
      if (IsPreview != false) {
        size += 1 + 1;
      }
      if (PreviewWin != false) {
        size += 1 + 1;
      }
      if (PreviewMac != false) {
        size += 1 + 1;
      }
      if (PreviewAndroid != false) {
        size += 1 + 1;
      }
      if (PreviewIos != false) {
        size += 1 + 1;
      }
      if (DurationMs != 0UL) {
        size += 1 + pb::CodedOutputStream.ComputeUInt64Size(DurationMs);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(CckWorldUploadFailed other) {
      if (other == null) {
        return;
      }
      if (other.IsBeta != false) {
        IsBeta = other.IsBeta;
      }
      if (other.IsPreview != false) {
        IsPreview = other.IsPreview;
      }
      if (other.PreviewWin != false) {
        PreviewWin = other.PreviewWin;
      }
      if (other.PreviewMac != false) {
        PreviewMac = other.PreviewMac;
      }
      if (other.PreviewAndroid != false) {
        PreviewAndroid = other.PreviewAndroid;
      }
      if (other.PreviewIos != false) {
        PreviewIos = other.PreviewIos;
      }
      if (other.DurationMs != 0UL) {
        DurationMs = other.DurationMs;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            IsBeta = input.ReadBool();
            break;
          }
          case 16: {
            IsPreview = input.ReadBool();
            break;
          }
          case 24: {
            PreviewWin = input.ReadBool();
            break;
          }
          case 32: {
            PreviewMac = input.ReadBool();
            break;
          }
          case 40: {
            PreviewAndroid = input.ReadBool();
            break;
          }
          case 48: {
            PreviewIos = input.ReadBool();
            break;
          }
          case 56: {
            DurationMs = input.ReadUInt64();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
      if ((tag & 7) == 4) {
        return;
      }
      switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            IsBeta = input.ReadBool();
            break;
          }
          case 16: {
            IsPreview = input.ReadBool();
            break;
          }
          case 24: {
            PreviewWin = input.ReadBool();
            break;
          }
          case 32: {
            PreviewMac = input.ReadBool();
            break;
          }
          case 40: {
            PreviewAndroid = input.ReadBool();
            break;
          }
          case 48: {
            PreviewIos = input.ReadBool();
            break;
          }
          case 56: {
            DurationMs = input.ReadUInt64();
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
