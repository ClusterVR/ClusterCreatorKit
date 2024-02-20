#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace ClusterVR.CreatorKit.Proto {

  public static partial class WorldDescriptorReflection {

    #region Descriptor
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static WorldDescriptorReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CiVhcGkvY3JlYXRvcmtpdC93b3JsZF9kZXNjcmlwdG9yLnByb3RvEhJjbHVz",
            "dGVyLmNyZWF0b3JraXQifgoPV29ybGREZXNjcmlwdG9yEiMKG3BlcnNpc3Rl",
            "ZF9wbGF5ZXJfc3RhdGVfa2V5cxgBIAMoCRJGChV3b3JsZF9ydW50aW1lX3Nl",
            "dHRpbmcYAiABKAsyJy5jbHVzdGVyLmNyZWF0b3JraXQuV29ybGRSdW50aW1l",
            "U2V0dGluZyKQAQoTV29ybGRSdW50aW1lU2V0dGluZxIbChN1c2VfbW92aW5n",
            "X3BsYXRmb3JtGAEgASgIEi4KJnVzZV9tb3ZpbmdfcGxhdGZvcm1faG9yaXpv",
            "bnRhbF9pbmVydGlhGAIgASgIEiwKJHVzZV9tb3ZpbmdfcGxhdGZvcm1fdmVy",
            "dGljYWxfaW5lcnRpYRgDIAEoCEItWg5jbHVzdGVyLm11L3JwY6oCGkNsdXN0",
            "ZXJWUi5DcmVhdG9yS2l0LlByb3RvYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.WorldDescriptor), global::ClusterVR.CreatorKit.Proto.WorldDescriptor.Parser, new[]{ "PersistedPlayerStateKeys", "WorldRuntimeSetting" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.WorldRuntimeSetting), global::ClusterVR.CreatorKit.Proto.WorldRuntimeSetting.Parser, new[]{ "UseMovingPlatform", "UseMovingPlatformHorizontalInertia", "UseMovingPlatformVerticalInertia" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class WorldDescriptor : pb::IMessage<WorldDescriptor>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<WorldDescriptor> _parser = new pb::MessageParser<WorldDescriptor>(() => new WorldDescriptor());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<WorldDescriptor> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.WorldDescriptorReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public WorldDescriptor() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public WorldDescriptor(WorldDescriptor other) : this() {
      persistedPlayerStateKeys_ = other.persistedPlayerStateKeys_.Clone();
      worldRuntimeSetting_ = other.worldRuntimeSetting_ != null ? other.worldRuntimeSetting_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public WorldDescriptor Clone() {
      return new WorldDescriptor(this);
    }

    public const int PersistedPlayerStateKeysFieldNumber = 1;
    private static readonly pb::FieldCodec<string> _repeated_persistedPlayerStateKeys_codec
        = pb::FieldCodec.ForString(10);
    private readonly pbc::RepeatedField<string> persistedPlayerStateKeys_ = new pbc::RepeatedField<string>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<string> PersistedPlayerStateKeys {
      get { return persistedPlayerStateKeys_; }
    }

    public const int WorldRuntimeSettingFieldNumber = 2;
    private global::ClusterVR.CreatorKit.Proto.WorldRuntimeSetting worldRuntimeSetting_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.WorldRuntimeSetting WorldRuntimeSetting {
      get { return worldRuntimeSetting_; }
      set {
        worldRuntimeSetting_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as WorldDescriptor);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(WorldDescriptor other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!persistedPlayerStateKeys_.Equals(other.persistedPlayerStateKeys_)) return false;
      if (!object.Equals(WorldRuntimeSetting, other.WorldRuntimeSetting)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= persistedPlayerStateKeys_.GetHashCode();
      if (worldRuntimeSetting_ != null) hash ^= WorldRuntimeSetting.GetHashCode();
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
      persistedPlayerStateKeys_.WriteTo(output, _repeated_persistedPlayerStateKeys_codec);
      if (worldRuntimeSetting_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(WorldRuntimeSetting);
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
      persistedPlayerStateKeys_.WriteTo(ref output, _repeated_persistedPlayerStateKeys_codec);
      if (worldRuntimeSetting_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(WorldRuntimeSetting);
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
      size += persistedPlayerStateKeys_.CalculateSize(_repeated_persistedPlayerStateKeys_codec);
      if (worldRuntimeSetting_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(WorldRuntimeSetting);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(WorldDescriptor other) {
      if (other == null) {
        return;
      }
      persistedPlayerStateKeys_.Add(other.persistedPlayerStateKeys_);
      if (other.worldRuntimeSetting_ != null) {
        if (worldRuntimeSetting_ == null) {
          WorldRuntimeSetting = new global::ClusterVR.CreatorKit.Proto.WorldRuntimeSetting();
        }
        WorldRuntimeSetting.MergeFrom(other.WorldRuntimeSetting);
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
            persistedPlayerStateKeys_.AddEntriesFrom(input, _repeated_persistedPlayerStateKeys_codec);
            break;
          }
          case 18: {
            if (worldRuntimeSetting_ == null) {
              WorldRuntimeSetting = new global::ClusterVR.CreatorKit.Proto.WorldRuntimeSetting();
            }
            input.ReadMessage(WorldRuntimeSetting);
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
            persistedPlayerStateKeys_.AddEntriesFrom(ref input, _repeated_persistedPlayerStateKeys_codec);
            break;
          }
          case 18: {
            if (worldRuntimeSetting_ == null) {
              WorldRuntimeSetting = new global::ClusterVR.CreatorKit.Proto.WorldRuntimeSetting();
            }
            input.ReadMessage(WorldRuntimeSetting);
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class WorldRuntimeSetting : pb::IMessage<WorldRuntimeSetting>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<WorldRuntimeSetting> _parser = new pb::MessageParser<WorldRuntimeSetting>(() => new WorldRuntimeSetting());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<WorldRuntimeSetting> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.WorldDescriptorReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public WorldRuntimeSetting() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public WorldRuntimeSetting(WorldRuntimeSetting other) : this() {
      useMovingPlatform_ = other.useMovingPlatform_;
      useMovingPlatformHorizontalInertia_ = other.useMovingPlatformHorizontalInertia_;
      useMovingPlatformVerticalInertia_ = other.useMovingPlatformVerticalInertia_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public WorldRuntimeSetting Clone() {
      return new WorldRuntimeSetting(this);
    }

    public const int UseMovingPlatformFieldNumber = 1;
    private bool useMovingPlatform_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool UseMovingPlatform {
      get { return useMovingPlatform_; }
      set {
        useMovingPlatform_ = value;
      }
    }

    public const int UseMovingPlatformHorizontalInertiaFieldNumber = 2;
    private bool useMovingPlatformHorizontalInertia_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool UseMovingPlatformHorizontalInertia {
      get { return useMovingPlatformHorizontalInertia_; }
      set {
        useMovingPlatformHorizontalInertia_ = value;
      }
    }

    public const int UseMovingPlatformVerticalInertiaFieldNumber = 3;
    private bool useMovingPlatformVerticalInertia_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool UseMovingPlatformVerticalInertia {
      get { return useMovingPlatformVerticalInertia_; }
      set {
        useMovingPlatformVerticalInertia_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as WorldRuntimeSetting);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(WorldRuntimeSetting other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (UseMovingPlatform != other.UseMovingPlatform) return false;
      if (UseMovingPlatformHorizontalInertia != other.UseMovingPlatformHorizontalInertia) return false;
      if (UseMovingPlatformVerticalInertia != other.UseMovingPlatformVerticalInertia) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (UseMovingPlatform != false) hash ^= UseMovingPlatform.GetHashCode();
      if (UseMovingPlatformHorizontalInertia != false) hash ^= UseMovingPlatformHorizontalInertia.GetHashCode();
      if (UseMovingPlatformVerticalInertia != false) hash ^= UseMovingPlatformVerticalInertia.GetHashCode();
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
      if (UseMovingPlatform != false) {
        output.WriteRawTag(8);
        output.WriteBool(UseMovingPlatform);
      }
      if (UseMovingPlatformHorizontalInertia != false) {
        output.WriteRawTag(16);
        output.WriteBool(UseMovingPlatformHorizontalInertia);
      }
      if (UseMovingPlatformVerticalInertia != false) {
        output.WriteRawTag(24);
        output.WriteBool(UseMovingPlatformVerticalInertia);
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
      if (UseMovingPlatform != false) {
        output.WriteRawTag(8);
        output.WriteBool(UseMovingPlatform);
      }
      if (UseMovingPlatformHorizontalInertia != false) {
        output.WriteRawTag(16);
        output.WriteBool(UseMovingPlatformHorizontalInertia);
      }
      if (UseMovingPlatformVerticalInertia != false) {
        output.WriteRawTag(24);
        output.WriteBool(UseMovingPlatformVerticalInertia);
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
      if (UseMovingPlatform != false) {
        size += 1 + 1;
      }
      if (UseMovingPlatformHorizontalInertia != false) {
        size += 1 + 1;
      }
      if (UseMovingPlatformVerticalInertia != false) {
        size += 1 + 1;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(WorldRuntimeSetting other) {
      if (other == null) {
        return;
      }
      if (other.UseMovingPlatform != false) {
        UseMovingPlatform = other.UseMovingPlatform;
      }
      if (other.UseMovingPlatformHorizontalInertia != false) {
        UseMovingPlatformHorizontalInertia = other.UseMovingPlatformHorizontalInertia;
      }
      if (other.UseMovingPlatformVerticalInertia != false) {
        UseMovingPlatformVerticalInertia = other.UseMovingPlatformVerticalInertia;
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
          case 8: {
            UseMovingPlatform = input.ReadBool();
            break;
          }
          case 16: {
            UseMovingPlatformHorizontalInertia = input.ReadBool();
            break;
          }
          case 24: {
            UseMovingPlatformVerticalInertia = input.ReadBool();
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
          case 8: {
            UseMovingPlatform = input.ReadBool();
            break;
          }
          case 16: {
            UseMovingPlatformHorizontalInertia = input.ReadBool();
            break;
          }
          case 24: {
            UseMovingPlatformVerticalInertia = input.ReadBool();
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
