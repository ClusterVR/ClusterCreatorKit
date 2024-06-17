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
            "U2V0dGluZyLfAgoTV29ybGRSdW50aW1lU2V0dGluZxIbChN1c2VfbW92aW5n",
            "X3BsYXRmb3JtGAEgASgIEi4KJnVzZV9tb3ZpbmdfcGxhdGZvcm1faG9yaXpv",
            "bnRhbF9pbmVydGlhGAIgASgIEiwKJHVzZV9tb3ZpbmdfcGxhdGZvcm1fdmVy",
            "dGljYWxfaW5lcnRpYRgDIAEoCBIYChB1c2Vfd29ybGRfc2hhZG93GAQgASgI",
            "EhQKDHVzZV9tYW50bGluZxgFIAEoCBJBCghodWRfdHlwZRgGIAEoDjIvLmNs",
            "dXN0ZXIuY3JlYXRvcmtpdC5Xb3JsZFJ1bnRpbWVTZXR0aW5nLkhVRFR5cGUS",
            "GgoSZW5hYmxlX2Nyb3VjaF93YWxrGAcgASgIIj4KB0hVRFR5cGUSDwoLVU5L",
            "Tk9XTl9IVUQQABIOCgpMRUdBQ1lfSFVEEAESEgoOQ0xVU1RFUl9IVURfVjIQ",
            "AkItWg5jbHVzdGVyLm11L3JwY6oCGkNsdXN0ZXJWUi5DcmVhdG9yS2l0LlBy",
            "b3RvYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.WorldDescriptor), global::ClusterVR.CreatorKit.Proto.WorldDescriptor.Parser, new[]{ "PersistedPlayerStateKeys", "WorldRuntimeSetting" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.WorldRuntimeSetting), global::ClusterVR.CreatorKit.Proto.WorldRuntimeSetting.Parser, new[]{ "UseMovingPlatform", "UseMovingPlatformHorizontalInertia", "UseMovingPlatformVerticalInertia", "UseWorldShadow", "UseMantling", "HudType", "EnableCrouchWalk" }, null, new[]{ typeof(global::ClusterVR.CreatorKit.Proto.WorldRuntimeSetting.Types.HUDType) }, null, null)
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
      useWorldShadow_ = other.useWorldShadow_;
      useMantling_ = other.useMantling_;
      hudType_ = other.hudType_;
      enableCrouchWalk_ = other.enableCrouchWalk_;
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

    public const int UseWorldShadowFieldNumber = 4;
    private bool useWorldShadow_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool UseWorldShadow {
      get { return useWorldShadow_; }
      set {
        useWorldShadow_ = value;
      }
    }

    public const int UseMantlingFieldNumber = 5;
    private bool useMantling_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool UseMantling {
      get { return useMantling_; }
      set {
        useMantling_ = value;
      }
    }

    public const int HudTypeFieldNumber = 6;
    private global::ClusterVR.CreatorKit.Proto.WorldRuntimeSetting.Types.HUDType hudType_ = global::ClusterVR.CreatorKit.Proto.WorldRuntimeSetting.Types.HUDType.UnknownHud;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.WorldRuntimeSetting.Types.HUDType HudType {
      get { return hudType_; }
      set {
        hudType_ = value;
      }
    }

    public const int EnableCrouchWalkFieldNumber = 7;
    private bool enableCrouchWalk_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool EnableCrouchWalk {
      get { return enableCrouchWalk_; }
      set {
        enableCrouchWalk_ = value;
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
      if (UseWorldShadow != other.UseWorldShadow) return false;
      if (UseMantling != other.UseMantling) return false;
      if (HudType != other.HudType) return false;
      if (EnableCrouchWalk != other.EnableCrouchWalk) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (UseMovingPlatform != false) hash ^= UseMovingPlatform.GetHashCode();
      if (UseMovingPlatformHorizontalInertia != false) hash ^= UseMovingPlatformHorizontalInertia.GetHashCode();
      if (UseMovingPlatformVerticalInertia != false) hash ^= UseMovingPlatformVerticalInertia.GetHashCode();
      if (UseWorldShadow != false) hash ^= UseWorldShadow.GetHashCode();
      if (UseMantling != false) hash ^= UseMantling.GetHashCode();
      if (HudType != global::ClusterVR.CreatorKit.Proto.WorldRuntimeSetting.Types.HUDType.UnknownHud) hash ^= HudType.GetHashCode();
      if (EnableCrouchWalk != false) hash ^= EnableCrouchWalk.GetHashCode();
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
      if (UseWorldShadow != false) {
        output.WriteRawTag(32);
        output.WriteBool(UseWorldShadow);
      }
      if (UseMantling != false) {
        output.WriteRawTag(40);
        output.WriteBool(UseMantling);
      }
      if (HudType != global::ClusterVR.CreatorKit.Proto.WorldRuntimeSetting.Types.HUDType.UnknownHud) {
        output.WriteRawTag(48);
        output.WriteEnum((int) HudType);
      }
      if (EnableCrouchWalk != false) {
        output.WriteRawTag(56);
        output.WriteBool(EnableCrouchWalk);
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
      if (UseWorldShadow != false) {
        output.WriteRawTag(32);
        output.WriteBool(UseWorldShadow);
      }
      if (UseMantling != false) {
        output.WriteRawTag(40);
        output.WriteBool(UseMantling);
      }
      if (HudType != global::ClusterVR.CreatorKit.Proto.WorldRuntimeSetting.Types.HUDType.UnknownHud) {
        output.WriteRawTag(48);
        output.WriteEnum((int) HudType);
      }
      if (EnableCrouchWalk != false) {
        output.WriteRawTag(56);
        output.WriteBool(EnableCrouchWalk);
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
      if (UseWorldShadow != false) {
        size += 1 + 1;
      }
      if (UseMantling != false) {
        size += 1 + 1;
      }
      if (HudType != global::ClusterVR.CreatorKit.Proto.WorldRuntimeSetting.Types.HUDType.UnknownHud) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) HudType);
      }
      if (EnableCrouchWalk != false) {
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
      if (other.UseWorldShadow != false) {
        UseWorldShadow = other.UseWorldShadow;
      }
      if (other.UseMantling != false) {
        UseMantling = other.UseMantling;
      }
      if (other.HudType != global::ClusterVR.CreatorKit.Proto.WorldRuntimeSetting.Types.HUDType.UnknownHud) {
        HudType = other.HudType;
      }
      if (other.EnableCrouchWalk != false) {
        EnableCrouchWalk = other.EnableCrouchWalk;
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
          case 32: {
            UseWorldShadow = input.ReadBool();
            break;
          }
          case 40: {
            UseMantling = input.ReadBool();
            break;
          }
          case 48: {
            HudType = (global::ClusterVR.CreatorKit.Proto.WorldRuntimeSetting.Types.HUDType) input.ReadEnum();
            break;
          }
          case 56: {
            EnableCrouchWalk = input.ReadBool();
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
          case 32: {
            UseWorldShadow = input.ReadBool();
            break;
          }
          case 40: {
            UseMantling = input.ReadBool();
            break;
          }
          case 48: {
            HudType = (global::ClusterVR.CreatorKit.Proto.WorldRuntimeSetting.Types.HUDType) input.ReadEnum();
            break;
          }
          case 56: {
            EnableCrouchWalk = input.ReadBool();
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
      public enum HUDType {
        [pbr::OriginalName("UNKNOWN_HUD")] UnknownHud = 0,
        [pbr::OriginalName("LEGACY_HUD")] LegacyHud = 1,
        [pbr::OriginalName("CLUSTER_HUD_V2")] ClusterHudV2 = 2,
      }

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code
