#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace ClusterVR.CreatorKit.Proto {

  public static partial class ItemTemplateReflection {

    #region Descriptor
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ItemTemplateReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CiJhcGkvY3JlYXRvcmtpdC9pdGVtX3RlbXBsYXRlLnByb3RvEhJjbHVzdGVy",
            "LmNyZWF0b3JraXQikgMKBEl0ZW0SKgoEbWV0YRgBIAEoCzIcLmNsdXN0ZXIu",
            "Y3JlYXRvcmtpdC5JdGVtTWV0YRI1Cgxtb3ZhYmxlX2l0ZW0YAiABKAsyHy5j",
            "bHVzdGVyLmNyZWF0b3JraXQuTW92YWJsZUl0ZW0SNQoMcmlkYWJsZV9pdGVt",
            "GAMgASgLMh8uY2x1c3Rlci5jcmVhdG9ya2l0LlJpZGFibGVJdGVtEjkKDmdy",
            "YWJiYWJsZV9pdGVtGAQgASgLMiEuY2x1c3Rlci5jcmVhdG9ya2l0LkdyYWJi",
            "YWJsZUl0ZW0SOwoPc2NyaXB0YWJsZV9pdGVtGAUgASgLMiIuY2x1c3Rlci5j",
            "cmVhdG9ya2l0LlNjcmlwdGFibGVJdGVtEjkKDmFjY2Vzc29yeV9pdGVtGAYg",
            "ASgLMiEuY2x1c3Rlci5jcmVhdG9ya2l0LkFjY2Vzc29yeUl0ZW0SPQoTaXRl",
            "bV9hdWRpb19zZXRfbGlzdBgHIAMoCzIgLmNsdXN0ZXIuY3JlYXRvcmtpdC5J",
            "dGVtQXVkaW9TZXQiSQoISXRlbU1ldGESLwoEbmFtZRgBIAMoCzIhLmNsdXN0",
            "ZXIuY3JlYXRvcmtpdC5Mb2NhbGl6ZWRUZXh0EgwKBHNpemUYAiADKA0iMAoN",
            "TG9jYWxpemVkVGV4dBIRCglsYW5nX2NvZGUYASABKAkSDAoEdGV4dBgCIAEo",
            "CSINCgtNb3ZhYmxlSXRlbSKlAQoLUmlkYWJsZUl0ZW0SDAoEc2VhdBgBIAEo",
            "DRIaChJoYXNfZXhpdF90cmFuc2Zvcm0YAiABKAgSFgoOZXhpdF90cmFuc2Zv",
            "cm0YAyABKA0SFQoNaGFzX2xlZnRfZ3JpcBgEIAEoCBIRCglsZWZ0X2dyaXAY",
            "BSABKA0SFgoOaGFzX3JpZ2h0X2dyaXAYBiABKAgSEgoKcmlnaHRfZ3JpcBgH",
            "IAEoDSIvCg1HcmFiYmFibGVJdGVtEhAKCGhhc19ncmlwGAEgASgIEgwKBGdy",
            "aXAYAiABKA0iJQoOU2NyaXB0YWJsZUl0ZW0SEwoLc291cmNlX2NvZGUYASAB",
            "KAkiNQoPT2Zmc2V0VHJhbnNmb3JtEiIKGnRyYW5zbGF0aW9uX3JvdGF0aW9u",
            "X3NjYWxlGAEgAygCIjoKEkF0dGFjaENhc2VUb0F2YXRhchIkChxkZWZhdWx0",
            "X2h1bWFuX2JvZHlfYm9uZV9uYW1lGAEgASgJIq4BCg1BY2Nlc3NvcnlJdGVt",
            "EkUKGGRlZmF1bHRfb2Zmc2V0X3RyYW5zZm9ybRgDIAEoCzIjLmNsdXN0ZXIu",
            "Y3JlYXRvcmtpdC5PZmZzZXRUcmFuc2Zvcm0SRwoVYXR0YWNoX2Nhc2VfdG9f",
            "YXZhdGFyGAIgASgLMiYuY2x1c3Rlci5jcmVhdG9ya2l0LkF0dGFjaENhc2VU",
            "b0F2YXRhckgAQg0KC2F0dGFjaF9jYXNlIlIKDEl0ZW1BdWRpb1NldBIKCgJp",
            "ZBgBIAEoCRIoCgVhdWRpbxgCIAEoCzIZLmNsdXN0ZXIuY3JlYXRvcmtpdC5B",
            "dWRpbxIMCgRsb29wGAMgASgIIj0KBUF1ZGlvEiYKA3BjbRgBIAEoCzIXLmNs",
            "dXN0ZXIuY3JlYXRvcmtpdC5QY21IAEIMCgphdWRpb19jYXNlIjoKA1BjbRIQ",
            "CghjaGFubmVscxgBIAEoDRITCgtzYW1wbGVfcmF0ZRgCIAEoDRIMCgRkYXRh",
            "GAMgAygCQi1aDmNsdXN0ZXIubXUvcnBjqgIaQ2x1c3RlclZSLkNyZWF0b3JL",
            "aXQuUHJvdG9iBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.Item), global::ClusterVR.CreatorKit.Proto.Item.Parser, new[]{ "Meta", "MovableItem", "RidableItem", "GrabbableItem", "ScriptableItem", "AccessoryItem", "ItemAudioSetList" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.ItemMeta), global::ClusterVR.CreatorKit.Proto.ItemMeta.Parser, new[]{ "Name", "Size" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.LocalizedText), global::ClusterVR.CreatorKit.Proto.LocalizedText.Parser, new[]{ "LangCode", "Text" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.MovableItem), global::ClusterVR.CreatorKit.Proto.MovableItem.Parser, null, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.RidableItem), global::ClusterVR.CreatorKit.Proto.RidableItem.Parser, new[]{ "Seat", "HasExitTransform", "ExitTransform", "HasLeftGrip", "LeftGrip", "HasRightGrip", "RightGrip" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.GrabbableItem), global::ClusterVR.CreatorKit.Proto.GrabbableItem.Parser, new[]{ "HasGrip", "Grip" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.ScriptableItem), global::ClusterVR.CreatorKit.Proto.ScriptableItem.Parser, new[]{ "SourceCode" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.OffsetTransform), global::ClusterVR.CreatorKit.Proto.OffsetTransform.Parser, new[]{ "TranslationRotationScale" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.AttachCaseToAvatar), global::ClusterVR.CreatorKit.Proto.AttachCaseToAvatar.Parser, new[]{ "DefaultHumanBodyBoneName" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.AccessoryItem), global::ClusterVR.CreatorKit.Proto.AccessoryItem.Parser, new[]{ "DefaultOffsetTransform", "AttachCaseToAvatar" }, new[]{ "AttachCase" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.ItemAudioSet), global::ClusterVR.CreatorKit.Proto.ItemAudioSet.Parser, new[]{ "Id", "Audio", "Loop" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.Audio), global::ClusterVR.CreatorKit.Proto.Audio.Parser, new[]{ "Pcm" }, new[]{ "AudioCase" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.Pcm), global::ClusterVR.CreatorKit.Proto.Pcm.Parser, new[]{ "Channels", "SampleRate", "Data" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class Item : pb::IMessage<Item>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Item> _parser = new pb::MessageParser<Item>(() => new Item());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<Item> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.ItemTemplateReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Item() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Item(Item other) : this() {
      meta_ = other.meta_ != null ? other.meta_.Clone() : null;
      movableItem_ = other.movableItem_ != null ? other.movableItem_.Clone() : null;
      ridableItem_ = other.ridableItem_ != null ? other.ridableItem_.Clone() : null;
      grabbableItem_ = other.grabbableItem_ != null ? other.grabbableItem_.Clone() : null;
      scriptableItem_ = other.scriptableItem_ != null ? other.scriptableItem_.Clone() : null;
      accessoryItem_ = other.accessoryItem_ != null ? other.accessoryItem_.Clone() : null;
      itemAudioSetList_ = other.itemAudioSetList_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Item Clone() {
      return new Item(this);
    }

    public const int MetaFieldNumber = 1;
    private global::ClusterVR.CreatorKit.Proto.ItemMeta meta_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.ItemMeta Meta {
      get { return meta_; }
      set {
        meta_ = value;
      }
    }

    public const int MovableItemFieldNumber = 2;
    private global::ClusterVR.CreatorKit.Proto.MovableItem movableItem_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.MovableItem MovableItem {
      get { return movableItem_; }
      set {
        movableItem_ = value;
      }
    }

    public const int RidableItemFieldNumber = 3;
    private global::ClusterVR.CreatorKit.Proto.RidableItem ridableItem_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.RidableItem RidableItem {
      get { return ridableItem_; }
      set {
        ridableItem_ = value;
      }
    }

    public const int GrabbableItemFieldNumber = 4;
    private global::ClusterVR.CreatorKit.Proto.GrabbableItem grabbableItem_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.GrabbableItem GrabbableItem {
      get { return grabbableItem_; }
      set {
        grabbableItem_ = value;
      }
    }

    public const int ScriptableItemFieldNumber = 5;
    private global::ClusterVR.CreatorKit.Proto.ScriptableItem scriptableItem_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.ScriptableItem ScriptableItem {
      get { return scriptableItem_; }
      set {
        scriptableItem_ = value;
      }
    }

    public const int AccessoryItemFieldNumber = 6;
    private global::ClusterVR.CreatorKit.Proto.AccessoryItem accessoryItem_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.AccessoryItem AccessoryItem {
      get { return accessoryItem_; }
      set {
        accessoryItem_ = value;
      }
    }

    public const int ItemAudioSetListFieldNumber = 7;
    private static readonly pb::FieldCodec<global::ClusterVR.CreatorKit.Proto.ItemAudioSet> _repeated_itemAudioSetList_codec
        = pb::FieldCodec.ForMessage(58, global::ClusterVR.CreatorKit.Proto.ItemAudioSet.Parser);
    private readonly pbc::RepeatedField<global::ClusterVR.CreatorKit.Proto.ItemAudioSet> itemAudioSetList_ = new pbc::RepeatedField<global::ClusterVR.CreatorKit.Proto.ItemAudioSet>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<global::ClusterVR.CreatorKit.Proto.ItemAudioSet> ItemAudioSetList {
      get { return itemAudioSetList_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as Item);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(Item other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Meta, other.Meta)) return false;
      if (!object.Equals(MovableItem, other.MovableItem)) return false;
      if (!object.Equals(RidableItem, other.RidableItem)) return false;
      if (!object.Equals(GrabbableItem, other.GrabbableItem)) return false;
      if (!object.Equals(ScriptableItem, other.ScriptableItem)) return false;
      if (!object.Equals(AccessoryItem, other.AccessoryItem)) return false;
      if(!itemAudioSetList_.Equals(other.itemAudioSetList_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (meta_ != null) hash ^= Meta.GetHashCode();
      if (movableItem_ != null) hash ^= MovableItem.GetHashCode();
      if (ridableItem_ != null) hash ^= RidableItem.GetHashCode();
      if (grabbableItem_ != null) hash ^= GrabbableItem.GetHashCode();
      if (scriptableItem_ != null) hash ^= ScriptableItem.GetHashCode();
      if (accessoryItem_ != null) hash ^= AccessoryItem.GetHashCode();
      hash ^= itemAudioSetList_.GetHashCode();
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
      if (meta_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Meta);
      }
      if (movableItem_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(MovableItem);
      }
      if (ridableItem_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(RidableItem);
      }
      if (grabbableItem_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(GrabbableItem);
      }
      if (scriptableItem_ != null) {
        output.WriteRawTag(42);
        output.WriteMessage(ScriptableItem);
      }
      if (accessoryItem_ != null) {
        output.WriteRawTag(50);
        output.WriteMessage(AccessoryItem);
      }
      itemAudioSetList_.WriteTo(output, _repeated_itemAudioSetList_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (meta_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Meta);
      }
      if (movableItem_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(MovableItem);
      }
      if (ridableItem_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(RidableItem);
      }
      if (grabbableItem_ != null) {
        output.WriteRawTag(34);
        output.WriteMessage(GrabbableItem);
      }
      if (scriptableItem_ != null) {
        output.WriteRawTag(42);
        output.WriteMessage(ScriptableItem);
      }
      if (accessoryItem_ != null) {
        output.WriteRawTag(50);
        output.WriteMessage(AccessoryItem);
      }
      itemAudioSetList_.WriteTo(ref output, _repeated_itemAudioSetList_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (meta_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Meta);
      }
      if (movableItem_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(MovableItem);
      }
      if (ridableItem_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(RidableItem);
      }
      if (grabbableItem_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(GrabbableItem);
      }
      if (scriptableItem_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(ScriptableItem);
      }
      if (accessoryItem_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(AccessoryItem);
      }
      size += itemAudioSetList_.CalculateSize(_repeated_itemAudioSetList_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(Item other) {
      if (other == null) {
        return;
      }
      if (other.meta_ != null) {
        if (meta_ == null) {
          Meta = new global::ClusterVR.CreatorKit.Proto.ItemMeta();
        }
        Meta.MergeFrom(other.Meta);
      }
      if (other.movableItem_ != null) {
        if (movableItem_ == null) {
          MovableItem = new global::ClusterVR.CreatorKit.Proto.MovableItem();
        }
        MovableItem.MergeFrom(other.MovableItem);
      }
      if (other.ridableItem_ != null) {
        if (ridableItem_ == null) {
          RidableItem = new global::ClusterVR.CreatorKit.Proto.RidableItem();
        }
        RidableItem.MergeFrom(other.RidableItem);
      }
      if (other.grabbableItem_ != null) {
        if (grabbableItem_ == null) {
          GrabbableItem = new global::ClusterVR.CreatorKit.Proto.GrabbableItem();
        }
        GrabbableItem.MergeFrom(other.GrabbableItem);
      }
      if (other.scriptableItem_ != null) {
        if (scriptableItem_ == null) {
          ScriptableItem = new global::ClusterVR.CreatorKit.Proto.ScriptableItem();
        }
        ScriptableItem.MergeFrom(other.ScriptableItem);
      }
      if (other.accessoryItem_ != null) {
        if (accessoryItem_ == null) {
          AccessoryItem = new global::ClusterVR.CreatorKit.Proto.AccessoryItem();
        }
        AccessoryItem.MergeFrom(other.AccessoryItem);
      }
      itemAudioSetList_.Add(other.itemAudioSetList_);
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
            if (meta_ == null) {
              Meta = new global::ClusterVR.CreatorKit.Proto.ItemMeta();
            }
            input.ReadMessage(Meta);
            break;
          }
          case 18: {
            if (movableItem_ == null) {
              MovableItem = new global::ClusterVR.CreatorKit.Proto.MovableItem();
            }
            input.ReadMessage(MovableItem);
            break;
          }
          case 26: {
            if (ridableItem_ == null) {
              RidableItem = new global::ClusterVR.CreatorKit.Proto.RidableItem();
            }
            input.ReadMessage(RidableItem);
            break;
          }
          case 34: {
            if (grabbableItem_ == null) {
              GrabbableItem = new global::ClusterVR.CreatorKit.Proto.GrabbableItem();
            }
            input.ReadMessage(GrabbableItem);
            break;
          }
          case 42: {
            if (scriptableItem_ == null) {
              ScriptableItem = new global::ClusterVR.CreatorKit.Proto.ScriptableItem();
            }
            input.ReadMessage(ScriptableItem);
            break;
          }
          case 50: {
            if (accessoryItem_ == null) {
              AccessoryItem = new global::ClusterVR.CreatorKit.Proto.AccessoryItem();
            }
            input.ReadMessage(AccessoryItem);
            break;
          }
          case 58: {
            itemAudioSetList_.AddEntriesFrom(input, _repeated_itemAudioSetList_codec);
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
            if (meta_ == null) {
              Meta = new global::ClusterVR.CreatorKit.Proto.ItemMeta();
            }
            input.ReadMessage(Meta);
            break;
          }
          case 18: {
            if (movableItem_ == null) {
              MovableItem = new global::ClusterVR.CreatorKit.Proto.MovableItem();
            }
            input.ReadMessage(MovableItem);
            break;
          }
          case 26: {
            if (ridableItem_ == null) {
              RidableItem = new global::ClusterVR.CreatorKit.Proto.RidableItem();
            }
            input.ReadMessage(RidableItem);
            break;
          }
          case 34: {
            if (grabbableItem_ == null) {
              GrabbableItem = new global::ClusterVR.CreatorKit.Proto.GrabbableItem();
            }
            input.ReadMessage(GrabbableItem);
            break;
          }
          case 42: {
            if (scriptableItem_ == null) {
              ScriptableItem = new global::ClusterVR.CreatorKit.Proto.ScriptableItem();
            }
            input.ReadMessage(ScriptableItem);
            break;
          }
          case 50: {
            if (accessoryItem_ == null) {
              AccessoryItem = new global::ClusterVR.CreatorKit.Proto.AccessoryItem();
            }
            input.ReadMessage(AccessoryItem);
            break;
          }
          case 58: {
            itemAudioSetList_.AddEntriesFrom(ref input, _repeated_itemAudioSetList_codec);
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class ItemMeta : pb::IMessage<ItemMeta>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<ItemMeta> _parser = new pb::MessageParser<ItemMeta>(() => new ItemMeta());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<ItemMeta> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.ItemTemplateReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ItemMeta() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ItemMeta(ItemMeta other) : this() {
      name_ = other.name_.Clone();
      size_ = other.size_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ItemMeta Clone() {
      return new ItemMeta(this);
    }

    public const int NameFieldNumber = 1;
    private static readonly pb::FieldCodec<global::ClusterVR.CreatorKit.Proto.LocalizedText> _repeated_name_codec
        = pb::FieldCodec.ForMessage(10, global::ClusterVR.CreatorKit.Proto.LocalizedText.Parser);
    private readonly pbc::RepeatedField<global::ClusterVR.CreatorKit.Proto.LocalizedText> name_ = new pbc::RepeatedField<global::ClusterVR.CreatorKit.Proto.LocalizedText>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<global::ClusterVR.CreatorKit.Proto.LocalizedText> Name {
      get { return name_; }
    }

    public const int SizeFieldNumber = 2;
    private static readonly pb::FieldCodec<uint> _repeated_size_codec
        = pb::FieldCodec.ForUInt32(18);
    private readonly pbc::RepeatedField<uint> size_ = new pbc::RepeatedField<uint>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<uint> Size {
      get { return size_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as ItemMeta);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(ItemMeta other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!name_.Equals(other.name_)) return false;
      if(!size_.Equals(other.size_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= name_.GetHashCode();
      hash ^= size_.GetHashCode();
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
      name_.WriteTo(output, _repeated_name_codec);
      size_.WriteTo(output, _repeated_size_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      name_.WriteTo(ref output, _repeated_name_codec);
      size_.WriteTo(ref output, _repeated_size_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      size += name_.CalculateSize(_repeated_name_codec);
      size += size_.CalculateSize(_repeated_size_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(ItemMeta other) {
      if (other == null) {
        return;
      }
      name_.Add(other.name_);
      size_.Add(other.size_);
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
            name_.AddEntriesFrom(input, _repeated_name_codec);
            break;
          }
          case 18:
          case 16: {
            size_.AddEntriesFrom(input, _repeated_size_codec);
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
            name_.AddEntriesFrom(ref input, _repeated_name_codec);
            break;
          }
          case 18:
          case 16: {
            size_.AddEntriesFrom(ref input, _repeated_size_codec);
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class LocalizedText : pb::IMessage<LocalizedText>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<LocalizedText> _parser = new pb::MessageParser<LocalizedText>(() => new LocalizedText());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<LocalizedText> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.ItemTemplateReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public LocalizedText() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public LocalizedText(LocalizedText other) : this() {
      langCode_ = other.langCode_;
      text_ = other.text_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public LocalizedText Clone() {
      return new LocalizedText(this);
    }

    public const int LangCodeFieldNumber = 1;
    private string langCode_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string LangCode {
      get { return langCode_; }
      set {
        langCode_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    public const int TextFieldNumber = 2;
    private string text_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Text {
      get { return text_; }
      set {
        text_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as LocalizedText);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(LocalizedText other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (LangCode != other.LangCode) return false;
      if (Text != other.Text) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (LangCode.Length != 0) hash ^= LangCode.GetHashCode();
      if (Text.Length != 0) hash ^= Text.GetHashCode();
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
      if (LangCode.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(LangCode);
      }
      if (Text.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Text);
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
      if (LangCode.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(LangCode);
      }
      if (Text.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Text);
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
      if (LangCode.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(LangCode);
      }
      if (Text.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Text);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(LocalizedText other) {
      if (other == null) {
        return;
      }
      if (other.LangCode.Length != 0) {
        LangCode = other.LangCode;
      }
      if (other.Text.Length != 0) {
        Text = other.Text;
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
            LangCode = input.ReadString();
            break;
          }
          case 18: {
            Text = input.ReadString();
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
            LangCode = input.ReadString();
            break;
          }
          case 18: {
            Text = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class MovableItem : pb::IMessage<MovableItem>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<MovableItem> _parser = new pb::MessageParser<MovableItem>(() => new MovableItem());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<MovableItem> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.ItemTemplateReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public MovableItem() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public MovableItem(MovableItem other) : this() {
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public MovableItem Clone() {
      return new MovableItem(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as MovableItem);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(MovableItem other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
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
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(MovableItem other) {
      if (other == null) {
        return;
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
        }
      }
    }
    #endif

  }

  public sealed partial class RidableItem : pb::IMessage<RidableItem>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<RidableItem> _parser = new pb::MessageParser<RidableItem>(() => new RidableItem());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<RidableItem> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.ItemTemplateReflection.Descriptor.MessageTypes[4]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public RidableItem() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public RidableItem(RidableItem other) : this() {
      seat_ = other.seat_;
      hasExitTransform_ = other.hasExitTransform_;
      exitTransform_ = other.exitTransform_;
      hasLeftGrip_ = other.hasLeftGrip_;
      leftGrip_ = other.leftGrip_;
      hasRightGrip_ = other.hasRightGrip_;
      rightGrip_ = other.rightGrip_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public RidableItem Clone() {
      return new RidableItem(this);
    }

    public const int SeatFieldNumber = 1;
    private uint seat_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public uint Seat {
      get { return seat_; }
      set {
        seat_ = value;
      }
    }

    public const int HasExitTransformFieldNumber = 2;
    private bool hasExitTransform_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasExitTransform {
      get { return hasExitTransform_; }
      set {
        hasExitTransform_ = value;
      }
    }

    public const int ExitTransformFieldNumber = 3;
    private uint exitTransform_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public uint ExitTransform {
      get { return exitTransform_; }
      set {
        exitTransform_ = value;
      }
    }

    public const int HasLeftGripFieldNumber = 4;
    private bool hasLeftGrip_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasLeftGrip {
      get { return hasLeftGrip_; }
      set {
        hasLeftGrip_ = value;
      }
    }

    public const int LeftGripFieldNumber = 5;
    private uint leftGrip_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public uint LeftGrip {
      get { return leftGrip_; }
      set {
        leftGrip_ = value;
      }
    }

    public const int HasRightGripFieldNumber = 6;
    private bool hasRightGrip_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasRightGrip {
      get { return hasRightGrip_; }
      set {
        hasRightGrip_ = value;
      }
    }

    public const int RightGripFieldNumber = 7;
    private uint rightGrip_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public uint RightGrip {
      get { return rightGrip_; }
      set {
        rightGrip_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as RidableItem);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(RidableItem other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Seat != other.Seat) return false;
      if (HasExitTransform != other.HasExitTransform) return false;
      if (ExitTransform != other.ExitTransform) return false;
      if (HasLeftGrip != other.HasLeftGrip) return false;
      if (LeftGrip != other.LeftGrip) return false;
      if (HasRightGrip != other.HasRightGrip) return false;
      if (RightGrip != other.RightGrip) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (Seat != 0) hash ^= Seat.GetHashCode();
      if (HasExitTransform != false) hash ^= HasExitTransform.GetHashCode();
      if (ExitTransform != 0) hash ^= ExitTransform.GetHashCode();
      if (HasLeftGrip != false) hash ^= HasLeftGrip.GetHashCode();
      if (LeftGrip != 0) hash ^= LeftGrip.GetHashCode();
      if (HasRightGrip != false) hash ^= HasRightGrip.GetHashCode();
      if (RightGrip != 0) hash ^= RightGrip.GetHashCode();
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
      if (Seat != 0) {
        output.WriteRawTag(8);
        output.WriteUInt32(Seat);
      }
      if (HasExitTransform != false) {
        output.WriteRawTag(16);
        output.WriteBool(HasExitTransform);
      }
      if (ExitTransform != 0) {
        output.WriteRawTag(24);
        output.WriteUInt32(ExitTransform);
      }
      if (HasLeftGrip != false) {
        output.WriteRawTag(32);
        output.WriteBool(HasLeftGrip);
      }
      if (LeftGrip != 0) {
        output.WriteRawTag(40);
        output.WriteUInt32(LeftGrip);
      }
      if (HasRightGrip != false) {
        output.WriteRawTag(48);
        output.WriteBool(HasRightGrip);
      }
      if (RightGrip != 0) {
        output.WriteRawTag(56);
        output.WriteUInt32(RightGrip);
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
      if (Seat != 0) {
        output.WriteRawTag(8);
        output.WriteUInt32(Seat);
      }
      if (HasExitTransform != false) {
        output.WriteRawTag(16);
        output.WriteBool(HasExitTransform);
      }
      if (ExitTransform != 0) {
        output.WriteRawTag(24);
        output.WriteUInt32(ExitTransform);
      }
      if (HasLeftGrip != false) {
        output.WriteRawTag(32);
        output.WriteBool(HasLeftGrip);
      }
      if (LeftGrip != 0) {
        output.WriteRawTag(40);
        output.WriteUInt32(LeftGrip);
      }
      if (HasRightGrip != false) {
        output.WriteRawTag(48);
        output.WriteBool(HasRightGrip);
      }
      if (RightGrip != 0) {
        output.WriteRawTag(56);
        output.WriteUInt32(RightGrip);
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
      if (Seat != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Seat);
      }
      if (HasExitTransform != false) {
        size += 1 + 1;
      }
      if (ExitTransform != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(ExitTransform);
      }
      if (HasLeftGrip != false) {
        size += 1 + 1;
      }
      if (LeftGrip != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(LeftGrip);
      }
      if (HasRightGrip != false) {
        size += 1 + 1;
      }
      if (RightGrip != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(RightGrip);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(RidableItem other) {
      if (other == null) {
        return;
      }
      if (other.Seat != 0) {
        Seat = other.Seat;
      }
      if (other.HasExitTransform != false) {
        HasExitTransform = other.HasExitTransform;
      }
      if (other.ExitTransform != 0) {
        ExitTransform = other.ExitTransform;
      }
      if (other.HasLeftGrip != false) {
        HasLeftGrip = other.HasLeftGrip;
      }
      if (other.LeftGrip != 0) {
        LeftGrip = other.LeftGrip;
      }
      if (other.HasRightGrip != false) {
        HasRightGrip = other.HasRightGrip;
      }
      if (other.RightGrip != 0) {
        RightGrip = other.RightGrip;
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
            Seat = input.ReadUInt32();
            break;
          }
          case 16: {
            HasExitTransform = input.ReadBool();
            break;
          }
          case 24: {
            ExitTransform = input.ReadUInt32();
            break;
          }
          case 32: {
            HasLeftGrip = input.ReadBool();
            break;
          }
          case 40: {
            LeftGrip = input.ReadUInt32();
            break;
          }
          case 48: {
            HasRightGrip = input.ReadBool();
            break;
          }
          case 56: {
            RightGrip = input.ReadUInt32();
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
            Seat = input.ReadUInt32();
            break;
          }
          case 16: {
            HasExitTransform = input.ReadBool();
            break;
          }
          case 24: {
            ExitTransform = input.ReadUInt32();
            break;
          }
          case 32: {
            HasLeftGrip = input.ReadBool();
            break;
          }
          case 40: {
            LeftGrip = input.ReadUInt32();
            break;
          }
          case 48: {
            HasRightGrip = input.ReadBool();
            break;
          }
          case 56: {
            RightGrip = input.ReadUInt32();
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class GrabbableItem : pb::IMessage<GrabbableItem>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<GrabbableItem> _parser = new pb::MessageParser<GrabbableItem>(() => new GrabbableItem());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<GrabbableItem> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.ItemTemplateReflection.Descriptor.MessageTypes[5]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public GrabbableItem() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public GrabbableItem(GrabbableItem other) : this() {
      hasGrip_ = other.hasGrip_;
      grip_ = other.grip_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public GrabbableItem Clone() {
      return new GrabbableItem(this);
    }

    public const int HasGripFieldNumber = 1;
    private bool hasGrip_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool HasGrip {
      get { return hasGrip_; }
      set {
        hasGrip_ = value;
      }
    }

    public const int GripFieldNumber = 2;
    private uint grip_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public uint Grip {
      get { return grip_; }
      set {
        grip_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as GrabbableItem);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(GrabbableItem other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (HasGrip != other.HasGrip) return false;
      if (Grip != other.Grip) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (HasGrip != false) hash ^= HasGrip.GetHashCode();
      if (Grip != 0) hash ^= Grip.GetHashCode();
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
      if (HasGrip != false) {
        output.WriteRawTag(8);
        output.WriteBool(HasGrip);
      }
      if (Grip != 0) {
        output.WriteRawTag(16);
        output.WriteUInt32(Grip);
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
      if (HasGrip != false) {
        output.WriteRawTag(8);
        output.WriteBool(HasGrip);
      }
      if (Grip != 0) {
        output.WriteRawTag(16);
        output.WriteUInt32(Grip);
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
      if (HasGrip != false) {
        size += 1 + 1;
      }
      if (Grip != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Grip);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(GrabbableItem other) {
      if (other == null) {
        return;
      }
      if (other.HasGrip != false) {
        HasGrip = other.HasGrip;
      }
      if (other.Grip != 0) {
        Grip = other.Grip;
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
            HasGrip = input.ReadBool();
            break;
          }
          case 16: {
            Grip = input.ReadUInt32();
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
            HasGrip = input.ReadBool();
            break;
          }
          case 16: {
            Grip = input.ReadUInt32();
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class ScriptableItem : pb::IMessage<ScriptableItem>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<ScriptableItem> _parser = new pb::MessageParser<ScriptableItem>(() => new ScriptableItem());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<ScriptableItem> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.ItemTemplateReflection.Descriptor.MessageTypes[6]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ScriptableItem() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ScriptableItem(ScriptableItem other) : this() {
      sourceCode_ = other.sourceCode_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ScriptableItem Clone() {
      return new ScriptableItem(this);
    }

    public const int SourceCodeFieldNumber = 1;
    private string sourceCode_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string SourceCode {
      get { return sourceCode_; }
      set {
        sourceCode_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as ScriptableItem);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(ScriptableItem other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (SourceCode != other.SourceCode) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (SourceCode.Length != 0) hash ^= SourceCode.GetHashCode();
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
      if (SourceCode.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(SourceCode);
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
      if (SourceCode.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(SourceCode);
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
      if (SourceCode.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(SourceCode);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(ScriptableItem other) {
      if (other == null) {
        return;
      }
      if (other.SourceCode.Length != 0) {
        SourceCode = other.SourceCode;
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
            SourceCode = input.ReadString();
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
            SourceCode = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class OffsetTransform : pb::IMessage<OffsetTransform>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<OffsetTransform> _parser = new pb::MessageParser<OffsetTransform>(() => new OffsetTransform());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<OffsetTransform> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.ItemTemplateReflection.Descriptor.MessageTypes[7]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public OffsetTransform() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public OffsetTransform(OffsetTransform other) : this() {
      translationRotationScale_ = other.translationRotationScale_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public OffsetTransform Clone() {
      return new OffsetTransform(this);
    }

    public const int TranslationRotationScaleFieldNumber = 1;
    private static readonly pb::FieldCodec<float> _repeated_translationRotationScale_codec
        = pb::FieldCodec.ForFloat(10);
    private readonly pbc::RepeatedField<float> translationRotationScale_ = new pbc::RepeatedField<float>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<float> TranslationRotationScale {
      get { return translationRotationScale_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as OffsetTransform);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(OffsetTransform other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!translationRotationScale_.Equals(other.translationRotationScale_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= translationRotationScale_.GetHashCode();
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
      translationRotationScale_.WriteTo(output, _repeated_translationRotationScale_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      translationRotationScale_.WriteTo(ref output, _repeated_translationRotationScale_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      size += translationRotationScale_.CalculateSize(_repeated_translationRotationScale_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(OffsetTransform other) {
      if (other == null) {
        return;
      }
      translationRotationScale_.Add(other.translationRotationScale_);
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
          case 10:
          case 13: {
            translationRotationScale_.AddEntriesFrom(input, _repeated_translationRotationScale_codec);
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
          case 10:
          case 13: {
            translationRotationScale_.AddEntriesFrom(ref input, _repeated_translationRotationScale_codec);
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class AttachCaseToAvatar : pb::IMessage<AttachCaseToAvatar>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<AttachCaseToAvatar> _parser = new pb::MessageParser<AttachCaseToAvatar>(() => new AttachCaseToAvatar());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<AttachCaseToAvatar> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.ItemTemplateReflection.Descriptor.MessageTypes[8]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AttachCaseToAvatar() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AttachCaseToAvatar(AttachCaseToAvatar other) : this() {
      defaultHumanBodyBoneName_ = other.defaultHumanBodyBoneName_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AttachCaseToAvatar Clone() {
      return new AttachCaseToAvatar(this);
    }

    public const int DefaultHumanBodyBoneNameFieldNumber = 1;
    private string defaultHumanBodyBoneName_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string DefaultHumanBodyBoneName {
      get { return defaultHumanBodyBoneName_; }
      set {
        defaultHumanBodyBoneName_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as AttachCaseToAvatar);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(AttachCaseToAvatar other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (DefaultHumanBodyBoneName != other.DefaultHumanBodyBoneName) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (DefaultHumanBodyBoneName.Length != 0) hash ^= DefaultHumanBodyBoneName.GetHashCode();
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
      if (DefaultHumanBodyBoneName.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(DefaultHumanBodyBoneName);
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
      if (DefaultHumanBodyBoneName.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(DefaultHumanBodyBoneName);
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
      if (DefaultHumanBodyBoneName.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(DefaultHumanBodyBoneName);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(AttachCaseToAvatar other) {
      if (other == null) {
        return;
      }
      if (other.DefaultHumanBodyBoneName.Length != 0) {
        DefaultHumanBodyBoneName = other.DefaultHumanBodyBoneName;
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
            DefaultHumanBodyBoneName = input.ReadString();
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
            DefaultHumanBodyBoneName = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class AccessoryItem : pb::IMessage<AccessoryItem>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<AccessoryItem> _parser = new pb::MessageParser<AccessoryItem>(() => new AccessoryItem());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<AccessoryItem> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.ItemTemplateReflection.Descriptor.MessageTypes[9]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AccessoryItem() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AccessoryItem(AccessoryItem other) : this() {
      defaultOffsetTransform_ = other.defaultOffsetTransform_ != null ? other.defaultOffsetTransform_.Clone() : null;
      switch (other.AttachCaseCase) {
        case AttachCaseOneofCase.AttachCaseToAvatar:
          AttachCaseToAvatar = other.AttachCaseToAvatar.Clone();
          break;
      }

      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AccessoryItem Clone() {
      return new AccessoryItem(this);
    }

    public const int DefaultOffsetTransformFieldNumber = 3;
    private global::ClusterVR.CreatorKit.Proto.OffsetTransform defaultOffsetTransform_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.OffsetTransform DefaultOffsetTransform {
      get { return defaultOffsetTransform_; }
      set {
        defaultOffsetTransform_ = value;
      }
    }

    public const int AttachCaseToAvatarFieldNumber = 2;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.AttachCaseToAvatar AttachCaseToAvatar {
      get { return attachCaseCase_ == AttachCaseOneofCase.AttachCaseToAvatar ? (global::ClusterVR.CreatorKit.Proto.AttachCaseToAvatar) attachCase_ : null; }
      set {
        attachCase_ = value;
        attachCaseCase_ = value == null ? AttachCaseOneofCase.None : AttachCaseOneofCase.AttachCaseToAvatar;
      }
    }

    private object attachCase_;
    public enum AttachCaseOneofCase {
      None = 0,
      AttachCaseToAvatar = 2,
    }
    private AttachCaseOneofCase attachCaseCase_ = AttachCaseOneofCase.None;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AttachCaseOneofCase AttachCaseCase {
      get { return attachCaseCase_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearAttachCase() {
      attachCaseCase_ = AttachCaseOneofCase.None;
      attachCase_ = null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as AccessoryItem);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(AccessoryItem other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(DefaultOffsetTransform, other.DefaultOffsetTransform)) return false;
      if (!object.Equals(AttachCaseToAvatar, other.AttachCaseToAvatar)) return false;
      if (AttachCaseCase != other.AttachCaseCase) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (defaultOffsetTransform_ != null) hash ^= DefaultOffsetTransform.GetHashCode();
      if (attachCaseCase_ == AttachCaseOneofCase.AttachCaseToAvatar) hash ^= AttachCaseToAvatar.GetHashCode();
      hash ^= (int) attachCaseCase_;
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
      if (attachCaseCase_ == AttachCaseOneofCase.AttachCaseToAvatar) {
        output.WriteRawTag(18);
        output.WriteMessage(AttachCaseToAvatar);
      }
      if (defaultOffsetTransform_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(DefaultOffsetTransform);
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
      if (attachCaseCase_ == AttachCaseOneofCase.AttachCaseToAvatar) {
        output.WriteRawTag(18);
        output.WriteMessage(AttachCaseToAvatar);
      }
      if (defaultOffsetTransform_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(DefaultOffsetTransform);
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
      if (defaultOffsetTransform_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(DefaultOffsetTransform);
      }
      if (attachCaseCase_ == AttachCaseOneofCase.AttachCaseToAvatar) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(AttachCaseToAvatar);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(AccessoryItem other) {
      if (other == null) {
        return;
      }
      if (other.defaultOffsetTransform_ != null) {
        if (defaultOffsetTransform_ == null) {
          DefaultOffsetTransform = new global::ClusterVR.CreatorKit.Proto.OffsetTransform();
        }
        DefaultOffsetTransform.MergeFrom(other.DefaultOffsetTransform);
      }
      switch (other.AttachCaseCase) {
        case AttachCaseOneofCase.AttachCaseToAvatar:
          if (AttachCaseToAvatar == null) {
            AttachCaseToAvatar = new global::ClusterVR.CreatorKit.Proto.AttachCaseToAvatar();
          }
          AttachCaseToAvatar.MergeFrom(other.AttachCaseToAvatar);
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
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 18: {
            global::ClusterVR.CreatorKit.Proto.AttachCaseToAvatar subBuilder = new global::ClusterVR.CreatorKit.Proto.AttachCaseToAvatar();
            if (attachCaseCase_ == AttachCaseOneofCase.AttachCaseToAvatar) {
              subBuilder.MergeFrom(AttachCaseToAvatar);
            }
            input.ReadMessage(subBuilder);
            AttachCaseToAvatar = subBuilder;
            break;
          }
          case 26: {
            if (defaultOffsetTransform_ == null) {
              DefaultOffsetTransform = new global::ClusterVR.CreatorKit.Proto.OffsetTransform();
            }
            input.ReadMessage(DefaultOffsetTransform);
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
          case 18: {
            global::ClusterVR.CreatorKit.Proto.AttachCaseToAvatar subBuilder = new global::ClusterVR.CreatorKit.Proto.AttachCaseToAvatar();
            if (attachCaseCase_ == AttachCaseOneofCase.AttachCaseToAvatar) {
              subBuilder.MergeFrom(AttachCaseToAvatar);
            }
            input.ReadMessage(subBuilder);
            AttachCaseToAvatar = subBuilder;
            break;
          }
          case 26: {
            if (defaultOffsetTransform_ == null) {
              DefaultOffsetTransform = new global::ClusterVR.CreatorKit.Proto.OffsetTransform();
            }
            input.ReadMessage(DefaultOffsetTransform);
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class ItemAudioSet : pb::IMessage<ItemAudioSet>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<ItemAudioSet> _parser = new pb::MessageParser<ItemAudioSet>(() => new ItemAudioSet());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<ItemAudioSet> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.ItemTemplateReflection.Descriptor.MessageTypes[10]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ItemAudioSet() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ItemAudioSet(ItemAudioSet other) : this() {
      id_ = other.id_;
      audio_ = other.audio_ != null ? other.audio_.Clone() : null;
      loop_ = other.loop_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ItemAudioSet Clone() {
      return new ItemAudioSet(this);
    }

    public const int IdFieldNumber = 1;
    private string id_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string Id {
      get { return id_; }
      set {
        id_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    public const int AudioFieldNumber = 2;
    private global::ClusterVR.CreatorKit.Proto.Audio audio_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.Audio Audio {
      get { return audio_; }
      set {
        audio_ = value;
      }
    }

    public const int LoopFieldNumber = 3;
    private bool loop_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Loop {
      get { return loop_; }
      set {
        loop_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as ItemAudioSet);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(ItemAudioSet other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (!object.Equals(Audio, other.Audio)) return false;
      if (Loop != other.Loop) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (Id.Length != 0) hash ^= Id.GetHashCode();
      if (audio_ != null) hash ^= Audio.GetHashCode();
      if (Loop != false) hash ^= Loop.GetHashCode();
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
      if (Id.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Id);
      }
      if (audio_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(Audio);
      }
      if (Loop != false) {
        output.WriteRawTag(24);
        output.WriteBool(Loop);
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
      if (Id.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Id);
      }
      if (audio_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(Audio);
      }
      if (Loop != false) {
        output.WriteRawTag(24);
        output.WriteBool(Loop);
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
      if (Id.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Id);
      }
      if (audio_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Audio);
      }
      if (Loop != false) {
        size += 1 + 1;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(ItemAudioSet other) {
      if (other == null) {
        return;
      }
      if (other.Id.Length != 0) {
        Id = other.Id;
      }
      if (other.audio_ != null) {
        if (audio_ == null) {
          Audio = new global::ClusterVR.CreatorKit.Proto.Audio();
        }
        Audio.MergeFrom(other.Audio);
      }
      if (other.Loop != false) {
        Loop = other.Loop;
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
            Id = input.ReadString();
            break;
          }
          case 18: {
            if (audio_ == null) {
              Audio = new global::ClusterVR.CreatorKit.Proto.Audio();
            }
            input.ReadMessage(Audio);
            break;
          }
          case 24: {
            Loop = input.ReadBool();
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
            Id = input.ReadString();
            break;
          }
          case 18: {
            if (audio_ == null) {
              Audio = new global::ClusterVR.CreatorKit.Proto.Audio();
            }
            input.ReadMessage(Audio);
            break;
          }
          case 24: {
            Loop = input.ReadBool();
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class Audio : pb::IMessage<Audio>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Audio> _parser = new pb::MessageParser<Audio>(() => new Audio());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<Audio> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.ItemTemplateReflection.Descriptor.MessageTypes[11]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Audio() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Audio(Audio other) : this() {
      switch (other.AudioCaseCase) {
        case AudioCaseOneofCase.Pcm:
          Pcm = other.Pcm.Clone();
          break;
      }

      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Audio Clone() {
      return new Audio(this);
    }

    public const int PcmFieldNumber = 1;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.Pcm Pcm {
      get { return audioCaseCase_ == AudioCaseOneofCase.Pcm ? (global::ClusterVR.CreatorKit.Proto.Pcm) audioCase_ : null; }
      set {
        audioCase_ = value;
        audioCaseCase_ = value == null ? AudioCaseOneofCase.None : AudioCaseOneofCase.Pcm;
      }
    }

    private object audioCase_;
    public enum AudioCaseOneofCase {
      None = 0,
      Pcm = 1,
    }
    private AudioCaseOneofCase audioCaseCase_ = AudioCaseOneofCase.None;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public AudioCaseOneofCase AudioCaseCase {
      get { return audioCaseCase_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearAudioCase() {
      audioCaseCase_ = AudioCaseOneofCase.None;
      audioCase_ = null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as Audio);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(Audio other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Pcm, other.Pcm)) return false;
      if (AudioCaseCase != other.AudioCaseCase) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (audioCaseCase_ == AudioCaseOneofCase.Pcm) hash ^= Pcm.GetHashCode();
      hash ^= (int) audioCaseCase_;
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
      if (audioCaseCase_ == AudioCaseOneofCase.Pcm) {
        output.WriteRawTag(10);
        output.WriteMessage(Pcm);
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
      if (audioCaseCase_ == AudioCaseOneofCase.Pcm) {
        output.WriteRawTag(10);
        output.WriteMessage(Pcm);
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
      if (audioCaseCase_ == AudioCaseOneofCase.Pcm) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Pcm);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(Audio other) {
      if (other == null) {
        return;
      }
      switch (other.AudioCaseCase) {
        case AudioCaseOneofCase.Pcm:
          if (Pcm == null) {
            Pcm = new global::ClusterVR.CreatorKit.Proto.Pcm();
          }
          Pcm.MergeFrom(other.Pcm);
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
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            global::ClusterVR.CreatorKit.Proto.Pcm subBuilder = new global::ClusterVR.CreatorKit.Proto.Pcm();
            if (audioCaseCase_ == AudioCaseOneofCase.Pcm) {
              subBuilder.MergeFrom(Pcm);
            }
            input.ReadMessage(subBuilder);
            Pcm = subBuilder;
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
            global::ClusterVR.CreatorKit.Proto.Pcm subBuilder = new global::ClusterVR.CreatorKit.Proto.Pcm();
            if (audioCaseCase_ == AudioCaseOneofCase.Pcm) {
              subBuilder.MergeFrom(Pcm);
            }
            input.ReadMessage(subBuilder);
            Pcm = subBuilder;
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class Pcm : pb::IMessage<Pcm>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Pcm> _parser = new pb::MessageParser<Pcm>(() => new Pcm());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<Pcm> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.ItemTemplateReflection.Descriptor.MessageTypes[12]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Pcm() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Pcm(Pcm other) : this() {
      channels_ = other.channels_;
      sampleRate_ = other.sampleRate_;
      data_ = other.data_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Pcm Clone() {
      return new Pcm(this);
    }

    public const int ChannelsFieldNumber = 1;
    private uint channels_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public uint Channels {
      get { return channels_; }
      set {
        channels_ = value;
      }
    }

    public const int SampleRateFieldNumber = 2;
    private uint sampleRate_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public uint SampleRate {
      get { return sampleRate_; }
      set {
        sampleRate_ = value;
      }
    }

    public const int DataFieldNumber = 3;
    private static readonly pb::FieldCodec<float> _repeated_data_codec
        = pb::FieldCodec.ForFloat(26);
    private readonly pbc::RepeatedField<float> data_ = new pbc::RepeatedField<float>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<float> Data {
      get { return data_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as Pcm);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(Pcm other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Channels != other.Channels) return false;
      if (SampleRate != other.SampleRate) return false;
      if(!data_.Equals(other.data_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (Channels != 0) hash ^= Channels.GetHashCode();
      if (SampleRate != 0) hash ^= SampleRate.GetHashCode();
      hash ^= data_.GetHashCode();
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
      if (Channels != 0) {
        output.WriteRawTag(8);
        output.WriteUInt32(Channels);
      }
      if (SampleRate != 0) {
        output.WriteRawTag(16);
        output.WriteUInt32(SampleRate);
      }
      data_.WriteTo(output, _repeated_data_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (Channels != 0) {
        output.WriteRawTag(8);
        output.WriteUInt32(Channels);
      }
      if (SampleRate != 0) {
        output.WriteRawTag(16);
        output.WriteUInt32(SampleRate);
      }
      data_.WriteTo(ref output, _repeated_data_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      if (Channels != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(Channels);
      }
      if (SampleRate != 0) {
        size += 1 + pb::CodedOutputStream.ComputeUInt32Size(SampleRate);
      }
      size += data_.CalculateSize(_repeated_data_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(Pcm other) {
      if (other == null) {
        return;
      }
      if (other.Channels != 0) {
        Channels = other.Channels;
      }
      if (other.SampleRate != 0) {
        SampleRate = other.SampleRate;
      }
      data_.Add(other.data_);
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
            Channels = input.ReadUInt32();
            break;
          }
          case 16: {
            SampleRate = input.ReadUInt32();
            break;
          }
          case 26:
          case 29: {
            data_.AddEntriesFrom(input, _repeated_data_codec);
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
            Channels = input.ReadUInt32();
            break;
          }
          case 16: {
            SampleRate = input.ReadUInt32();
            break;
          }
          case 26:
          case 29: {
            data_.AddEntriesFrom(ref input, _repeated_data_codec);
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
