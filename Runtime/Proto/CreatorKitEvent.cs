#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace ClusterVR.CreatorKit.Proto {

  public static partial class CreatorKitEventReflection {

    #region Descriptor
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static CreatorKitEventReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CiZhcGkvY3JlYXRvcmtpdC9jcmVhdG9yX2tpdF9ldmVudC5wcm90bxISY2x1",
            "c3Rlci5jcmVhdG9ya2l0IpACChZDcmVhdG9yS2l0RXZlbnRQYXlsb2FkEh4K",
            "C3RtcF91c2VyX2lkGAEgASgJUgl0bXBVc2VySUQSHQoKc2Vzc2lvbl9pZBgC",
            "IAEoCVIJc2Vzc2lvbklEEksKC2Vudmlyb25tZW50GAMgASgLMikuY2x1c3Rl",
            "ci5jcmVhdG9ya2l0LkNyZWF0b3JLaXRFbnZpcm9ubWVudFILZW52aXJvbm1l",
            "bnQSUwoKZXZlbnRfdHlwZRgEIAEoDjI0LmNsdXN0ZXIuY3JlYXRvcmtpdC5D",
            "cmVhdG9yS2l0RXZlbnRQYXlsb2FkLkV2ZW50VHlwZVIJZXZlbnRUeXBlIhUK",
            "CUV2ZW50VHlwZRIICgRQSU5HEAAivwgKFUNyZWF0b3JLaXRFbnZpcm9ubWVu",
            "dBIlCg5iYXR0ZXJ5X3N0YXR1cxgBIAEoCVINYmF0dGVyeVN0YXR1cxIhCgxk",
            "ZXZpY2VfbW9kZWwYAiABKAlSC2RldmljZU1vZGVsEh8KC2RldmljZV90eXBl",
            "GAMgASgJUgpkZXZpY2VUeXBlEjgKGGRldmljZV91bmlxdWVfaWRlbnRpZmll",
            "chgEIAEoCVIWZGV2aWNlVW5pcXVlSWRlbnRpZmllchIwChRncmFwaGljc19k",
            "ZXZpY2VfbmFtZRgFIAEoCVISZ3JhcGhpY3NEZXZpY2VOYW1lEjAKFGdyYXBo",
            "aWNzX2RldmljZV90eXBlGAYgASgJUhJncmFwaGljc0RldmljZVR5cGUSNAoW",
            "Z3JhcGhpY3NfZGV2aWNlX3ZlbmRvchgHIAEoCVIUZ3JhcGhpY3NEZXZpY2VW",
            "ZW5kb3ISNgoXZ3JhcGhpY3NfZGV2aWNlX3ZlcnNpb24YCCABKAlSFWdyYXBo",
            "aWNzRGV2aWNlVmVyc2lvbhIwChRncmFwaGljc19tZW1vcnlfc2l6ZRgJIAEo",
            "BVISZ3JhcGhpY3NNZW1vcnlTaXplEjYKF2dyYXBoaWNzX211bHRpX3RocmVh",
            "ZGVkGAogASgIUhVncmFwaGljc011bHRpVGhyZWFkZWQSMgoVZ3JhcGhpY3Nf",
            "c2hhZGVyX2xldmVsGAsgASgFUhNncmFwaGljc1NoYWRlckxldmVsEikKEG9w",
            "ZXJhdGluZ19zeXN0ZW0YDCABKAlSD29wZXJhdGluZ1N5c3RlbRI2ChdvcGVy",
            "YXRpbmdfc3lzdGVtX2ZhbWlseRgNIAEoCVIVb3BlcmF0aW5nU3lzdGVtRmFt",
            "aWx5EicKD3Byb2Nlc3Nvcl9jb3VudBgOIAEoBVIOcHJvY2Vzc29yQ291bnQS",
            "LwoTcHJvY2Vzc29yX2ZyZXF1ZW5jeRgPIAEoBVIScHJvY2Vzc29yRnJlcXVl",
            "bmN5EiUKDnByb2Nlc3Nvcl90eXBlGBAgASgJUg1wcm9jZXNzb3JUeXBlEiwK",
            "EnN5c3RlbV9tZW1vcnlfc2l6ZRgRIAEoBVIQc3lzdGVtTWVtb3J5U2l6ZRId",
            "Cgppc19mb2N1c2VkGBIgASgIUglpc0ZvY3VzZWQSHQoKaXNfcGxheWluZxgT",
            "IAEoCFIJaXNQbGF5aW5nEhoKCHBsYXRmb3JtGBQgASgJUghwbGF0Zm9ybRIn",
            "Cg9zeXN0ZW1fbGFuZ3VhZ2UYFSABKAlSDnN5c3RlbUxhbmd1YWdlEiMKDXVu",
            "aXR5X3ZlcnNpb24YFiABKAlSDHVuaXR5VmVyc2lvbhIvChR4cl9kZXZpY2Vf",
            "aXNfcHJlc2VudBgXIAEoCFIReHJEZXZpY2VJc1ByZXNlbnQSJgoPeHJfZGV2",
            "aWNlX21vZGVsGBggASgJUg14ckRldmljZU1vZGVsQi1aDmNsdXN0ZXIubXUv",
            "cnBjqgIaQ2x1c3RlclZSLkNyZWF0b3JLaXQuUHJvdG9iBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.CreatorKitEventPayload), global::ClusterVR.CreatorKit.Proto.CreatorKitEventPayload.Parser, new[]{ "TmpUserId", "SessionId", "Environment", "EventType" }, null, new[]{ typeof(global::ClusterVR.CreatorKit.Proto.CreatorKitEventPayload.Types.EventType) }, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.CreatorKitEnvironment), global::ClusterVR.CreatorKit.Proto.CreatorKitEnvironment.Parser, new[]{ "BatteryStatus", "DeviceModel", "DeviceType", "DeviceUniqueIdentifier", "GraphicsDeviceName", "GraphicsDeviceType", "GraphicsDeviceVendor", "GraphicsDeviceVersion", "GraphicsMemorySize", "GraphicsMultiThreaded", "GraphicsShaderLevel", "OperatingSystem", "OperatingSystemFamily", "ProcessorCount", "ProcessorFrequency", "ProcessorType", "SystemMemorySize", "IsFocused", "IsPlaying", "Platform", "SystemLanguage", "UnityVersion", "XrDeviceIsPresent", "XrDeviceModel" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class CreatorKitEventPayload : pb::IMessage<CreatorKitEventPayload>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<CreatorKitEventPayload> _parser = new pb::MessageParser<CreatorKitEventPayload>(() => new CreatorKitEventPayload());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<CreatorKitEventPayload> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.CreatorKitEventReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CreatorKitEventPayload() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CreatorKitEventPayload(CreatorKitEventPayload other) : this() {
      tmpUserId_ = other.tmpUserId_;
      sessionId_ = other.sessionId_;
      environment_ = other.environment_ != null ? other.environment_.Clone() : null;
      eventType_ = other.eventType_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CreatorKitEventPayload Clone() {
      return new CreatorKitEventPayload(this);
    }

    public const int TmpUserIdFieldNumber = 1;
    private string tmpUserId_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string TmpUserId {
      get { return tmpUserId_; }
      set {
        tmpUserId_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
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
    private global::ClusterVR.CreatorKit.Proto.CreatorKitEnvironment environment_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.CreatorKitEnvironment Environment {
      get { return environment_; }
      set {
        environment_ = value;
      }
    }

    public const int EventTypeFieldNumber = 4;
    private global::ClusterVR.CreatorKit.Proto.CreatorKitEventPayload.Types.EventType eventType_ = global::ClusterVR.CreatorKit.Proto.CreatorKitEventPayload.Types.EventType.Ping;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.CreatorKitEventPayload.Types.EventType EventType {
      get { return eventType_; }
      set {
        eventType_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as CreatorKitEventPayload);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(CreatorKitEventPayload other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (TmpUserId != other.TmpUserId) return false;
      if (SessionId != other.SessionId) return false;
      if (!object.Equals(Environment, other.Environment)) return false;
      if (EventType != other.EventType) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (TmpUserId.Length != 0) hash ^= TmpUserId.GetHashCode();
      if (SessionId.Length != 0) hash ^= SessionId.GetHashCode();
      if (environment_ != null) hash ^= Environment.GetHashCode();
      if (EventType != global::ClusterVR.CreatorKit.Proto.CreatorKitEventPayload.Types.EventType.Ping) hash ^= EventType.GetHashCode();
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
      if (TmpUserId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(TmpUserId);
      }
      if (SessionId.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(SessionId);
      }
      if (environment_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(Environment);
      }
      if (EventType != global::ClusterVR.CreatorKit.Proto.CreatorKitEventPayload.Types.EventType.Ping) {
        output.WriteRawTag(32);
        output.WriteEnum((int) EventType);
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
      if (TmpUserId.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(TmpUserId);
      }
      if (SessionId.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(SessionId);
      }
      if (environment_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(Environment);
      }
      if (EventType != global::ClusterVR.CreatorKit.Proto.CreatorKitEventPayload.Types.EventType.Ping) {
        output.WriteRawTag(32);
        output.WriteEnum((int) EventType);
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
      if (TmpUserId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(TmpUserId);
      }
      if (SessionId.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(SessionId);
      }
      if (environment_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Environment);
      }
      if (EventType != global::ClusterVR.CreatorKit.Proto.CreatorKitEventPayload.Types.EventType.Ping) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) EventType);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(CreatorKitEventPayload other) {
      if (other == null) {
        return;
      }
      if (other.TmpUserId.Length != 0) {
        TmpUserId = other.TmpUserId;
      }
      if (other.SessionId.Length != 0) {
        SessionId = other.SessionId;
      }
      if (other.environment_ != null) {
        if (environment_ == null) {
          Environment = new global::ClusterVR.CreatorKit.Proto.CreatorKitEnvironment();
        }
        Environment.MergeFrom(other.Environment);
      }
      if (other.EventType != global::ClusterVR.CreatorKit.Proto.CreatorKitEventPayload.Types.EventType.Ping) {
        EventType = other.EventType;
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
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            TmpUserId = input.ReadString();
            break;
          }
          case 18: {
            SessionId = input.ReadString();
            break;
          }
          case 26: {
            if (environment_ == null) {
              Environment = new global::ClusterVR.CreatorKit.Proto.CreatorKitEnvironment();
            }
            input.ReadMessage(Environment);
            break;
          }
          case 32: {
            EventType = (global::ClusterVR.CreatorKit.Proto.CreatorKitEventPayload.Types.EventType) input.ReadEnum();
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
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 10: {
            TmpUserId = input.ReadString();
            break;
          }
          case 18: {
            SessionId = input.ReadString();
            break;
          }
          case 26: {
            if (environment_ == null) {
              Environment = new global::ClusterVR.CreatorKit.Proto.CreatorKitEnvironment();
            }
            input.ReadMessage(Environment);
            break;
          }
          case 32: {
            EventType = (global::ClusterVR.CreatorKit.Proto.CreatorKitEventPayload.Types.EventType) input.ReadEnum();
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
        [pbr::OriginalName("PING")] Ping = 0,
      }

    }
    #endregion

  }

  public sealed partial class CreatorKitEnvironment : pb::IMessage<CreatorKitEnvironment>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<CreatorKitEnvironment> _parser = new pb::MessageParser<CreatorKitEnvironment>(() => new CreatorKitEnvironment());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<CreatorKitEnvironment> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.CreatorKitEventReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CreatorKitEnvironment() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public CreatorKitEnvironment(CreatorKitEnvironment other) : this() {
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
    public CreatorKitEnvironment Clone() {
      return new CreatorKitEnvironment(this);
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
      return Equals(other as CreatorKitEnvironment);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(CreatorKitEnvironment other) {
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
    public void MergeFrom(CreatorKitEnvironment other) {
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

  #endregion

}

#endregion Designer generated code
