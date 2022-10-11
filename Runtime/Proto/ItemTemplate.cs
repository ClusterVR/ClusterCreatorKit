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
            "ChNpdGVtX3RlbXBsYXRlLnByb3RvEhJjbHVzdGVyLmNyZWF0b3JraXQi4wIK",
            "BEl0ZW0SKgoEbWV0YRgBIAEoCzIcLmNsdXN0ZXIuY3JlYXRvcmtpdC5JdGVt",
            "TWV0YRI1Cgxtb3ZhYmxlX2l0ZW0YAiABKAsyHy5jbHVzdGVyLmNyZWF0b3Jr",
            "aXQuTW92YWJsZUl0ZW0SNQoMcmlkYWJsZV9pdGVtGAMgASgLMh8uY2x1c3Rl",
            "ci5jcmVhdG9ya2l0LlJpZGFibGVJdGVtEjkKDmdyYWJiYWJsZV9pdGVtGAQg",
            "ASgLMiEuY2x1c3Rlci5jcmVhdG9ya2l0LkdyYWJiYWJsZUl0ZW0SOwoPc2Ny",
            "aXB0YWJsZV9pdGVtGAUgASgLMiIuY2x1c3Rlci5jcmVhdG9ya2l0LlNjcmlw",
            "dGFibGVJdGVtEkkKFXJlc2VydmVkX2l0ZW1fZmllbGRfNhgGIAEoCzIqLmNs",
            "dXN0ZXIuY3JlYXRvcmtpdC5SZXNlcnZlZEl0ZW1BdHRyaWJ1dGU2IkkKCEl0",
            "ZW1NZXRhEi8KBG5hbWUYASADKAsyIS5jbHVzdGVyLmNyZWF0b3JraXQuTG9j",
            "YWxpemVkVGV4dBIMCgRzaXplGAIgAygNIjAKDUxvY2FsaXplZFRleHQSEQoJ",
            "bGFuZ19jb2RlGAEgASgJEgwKBHRleHQYAiABKAkiDQoLTW92YWJsZUl0ZW0i",
            "pQEKC1JpZGFibGVJdGVtEgwKBHNlYXQYASABKA0SGgoSaGFzX2V4aXRfdHJh",
            "bnNmb3JtGAIgASgIEhYKDmV4aXRfdHJhbnNmb3JtGAMgASgNEhUKDWhhc19s",
            "ZWZ0X2dyaXAYBCABKAgSEQoJbGVmdF9ncmlwGAUgASgNEhYKDmhhc19yaWdo",
            "dF9ncmlwGAYgASgIEhIKCnJpZ2h0X2dyaXAYByABKA0iLwoNR3JhYmJhYmxl",
            "SXRlbRIQCghoYXNfZ3JpcBgBIAEoCBIMCgRncmlwGAIgASgNIiUKDlNjcmlw",
            "dGFibGVJdGVtEhMKC3NvdXJjZV9jb2RlGAEgASgJIjUKD09mZnNldFRyYW5z",
            "Zm9ybRIiChp0cmFuc2xhdGlvbl9yb3RhdGlvbl9zY2FsZRgBIAMoAiJBCiVS",
            "ZXNlcnZlZEl0ZW1BdHRyaWJ1dGU2RmllbGRBdHRyaWJ1dGUyEhgKEHJlc2Vy",
            "dmVkX2ZpZWxkXzEYASABKAkiwQEKFlJlc2VydmVkSXRlbUF0dHJpYnV0ZTYS",
            "PQoQcmVzZXJ2ZWRfZmllbGRfMxgDIAEoCzIjLmNsdXN0ZXIuY3JlYXRvcmtp",
            "dC5PZmZzZXRUcmFuc2Zvcm0SVQoQcmVzZXJ2ZWRfZmllbGRfMhgCIAEoCzI5",
            "LmNsdXN0ZXIuY3JlYXRvcmtpdC5SZXNlcnZlZEl0ZW1BdHRyaWJ1dGU2Rmll",
            "bGRBdHRyaWJ1dGUySABCEQoPcmVzZXJ2ZWRfY2FzZV8xQi1aDmNsdXN0ZXIu",
            "bXUvcnBjqgIaQ2x1c3RlclZSLkNyZWF0b3JLaXQuUHJvdG9iBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.Item), global::ClusterVR.CreatorKit.Proto.Item.Parser, new[]{ "Meta", "MovableItem", "RidableItem", "GrabbableItem", "ScriptableItem", "ReservedItemField6" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.ItemMeta), global::ClusterVR.CreatorKit.Proto.ItemMeta.Parser, new[]{ "Name", "Size" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.LocalizedText), global::ClusterVR.CreatorKit.Proto.LocalizedText.Parser, new[]{ "LangCode", "Text" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.MovableItem), global::ClusterVR.CreatorKit.Proto.MovableItem.Parser, null, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.RidableItem), global::ClusterVR.CreatorKit.Proto.RidableItem.Parser, new[]{ "Seat", "HasExitTransform", "ExitTransform", "HasLeftGrip", "LeftGrip", "HasRightGrip", "RightGrip" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.GrabbableItem), global::ClusterVR.CreatorKit.Proto.GrabbableItem.Parser, new[]{ "HasGrip", "Grip" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.ScriptableItem), global::ClusterVR.CreatorKit.Proto.ScriptableItem.Parser, new[]{ "SourceCode" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.OffsetTransform), global::ClusterVR.CreatorKit.Proto.OffsetTransform.Parser, new[]{ "TranslationRotationScale" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.ReservedItemAttribute6FieldAttribute2), global::ClusterVR.CreatorKit.Proto.ReservedItemAttribute6FieldAttribute2.Parser, new[]{ "ReservedField1" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.ReservedItemAttribute6), global::ClusterVR.CreatorKit.Proto.ReservedItemAttribute6.Parser, new[]{ "ReservedField3", "ReservedField2" }, new[]{ "ReservedCase1" }, null, null, null)
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
      reservedItemField6_ = other.reservedItemField6_ != null ? other.reservedItemField6_.Clone() : null;
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

    public const int ReservedItemField6FieldNumber = 6;
    private global::ClusterVR.CreatorKit.Proto.ReservedItemAttribute6 reservedItemField6_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.ReservedItemAttribute6 ReservedItemField6 {
      get { return reservedItemField6_; }
      set {
        reservedItemField6_ = value;
      }
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
      if (!object.Equals(ReservedItemField6, other.ReservedItemField6)) return false;
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
      if (reservedItemField6_ != null) hash ^= ReservedItemField6.GetHashCode();
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
      if (reservedItemField6_ != null) {
        output.WriteRawTag(50);
        output.WriteMessage(ReservedItemField6);
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
      if (reservedItemField6_ != null) {
        output.WriteRawTag(50);
        output.WriteMessage(ReservedItemField6);
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
      if (reservedItemField6_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(ReservedItemField6);
      }
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
      if (other.reservedItemField6_ != null) {
        if (reservedItemField6_ == null) {
          ReservedItemField6 = new global::ClusterVR.CreatorKit.Proto.ReservedItemAttribute6();
        }
        ReservedItemField6.MergeFrom(other.ReservedItemField6);
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
            if (reservedItemField6_ == null) {
              ReservedItemField6 = new global::ClusterVR.CreatorKit.Proto.ReservedItemAttribute6();
            }
            input.ReadMessage(ReservedItemField6);
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
            if (reservedItemField6_ == null) {
              ReservedItemField6 = new global::ClusterVR.CreatorKit.Proto.ReservedItemAttribute6();
            }
            input.ReadMessage(ReservedItemField6);
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

  public sealed partial class ReservedItemAttribute6FieldAttribute2 : pb::IMessage<ReservedItemAttribute6FieldAttribute2>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<ReservedItemAttribute6FieldAttribute2> _parser = new pb::MessageParser<ReservedItemAttribute6FieldAttribute2>(() => new ReservedItemAttribute6FieldAttribute2());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<ReservedItemAttribute6FieldAttribute2> Parser { get { return _parser; } }

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
    public ReservedItemAttribute6FieldAttribute2() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ReservedItemAttribute6FieldAttribute2(ReservedItemAttribute6FieldAttribute2 other) : this() {
      reservedField1_ = other.reservedField1_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ReservedItemAttribute6FieldAttribute2 Clone() {
      return new ReservedItemAttribute6FieldAttribute2(this);
    }

    public const int ReservedField1FieldNumber = 1;
    private string reservedField1_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public string ReservedField1 {
      get { return reservedField1_; }
      set {
        reservedField1_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as ReservedItemAttribute6FieldAttribute2);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(ReservedItemAttribute6FieldAttribute2 other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (ReservedField1 != other.ReservedField1) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (ReservedField1.Length != 0) hash ^= ReservedField1.GetHashCode();
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
      if (ReservedField1.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(ReservedField1);
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
      if (ReservedField1.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(ReservedField1);
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
      if (ReservedField1.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(ReservedField1);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(ReservedItemAttribute6FieldAttribute2 other) {
      if (other == null) {
        return;
      }
      if (other.ReservedField1.Length != 0) {
        ReservedField1 = other.ReservedField1;
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
            ReservedField1 = input.ReadString();
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
            ReservedField1 = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class ReservedItemAttribute6 : pb::IMessage<ReservedItemAttribute6>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<ReservedItemAttribute6> _parser = new pb::MessageParser<ReservedItemAttribute6>(() => new ReservedItemAttribute6());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<ReservedItemAttribute6> Parser { get { return _parser; } }

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
    public ReservedItemAttribute6() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ReservedItemAttribute6(ReservedItemAttribute6 other) : this() {
      reservedField3_ = other.reservedField3_ != null ? other.reservedField3_.Clone() : null;
      switch (other.ReservedCase1Case) {
        case ReservedCase1OneofCase.ReservedField2:
          ReservedField2 = other.ReservedField2.Clone();
          break;
      }

      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ReservedItemAttribute6 Clone() {
      return new ReservedItemAttribute6(this);
    }

    public const int ReservedField3FieldNumber = 3;
    private global::ClusterVR.CreatorKit.Proto.OffsetTransform reservedField3_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.OffsetTransform ReservedField3 {
      get { return reservedField3_; }
      set {
        reservedField3_ = value;
      }
    }

    public const int ReservedField2FieldNumber = 2;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.ReservedItemAttribute6FieldAttribute2 ReservedField2 {
      get { return reservedCase1Case_ == ReservedCase1OneofCase.ReservedField2 ? (global::ClusterVR.CreatorKit.Proto.ReservedItemAttribute6FieldAttribute2) reservedCase1_ : null; }
      set {
        reservedCase1_ = value;
        reservedCase1Case_ = value == null ? ReservedCase1OneofCase.None : ReservedCase1OneofCase.ReservedField2;
      }
    }

    private object reservedCase1_;
    public enum ReservedCase1OneofCase {
      None = 0,
      ReservedField2 = 2,
    }
    private ReservedCase1OneofCase reservedCase1Case_ = ReservedCase1OneofCase.None;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ReservedCase1OneofCase ReservedCase1Case {
      get { return reservedCase1Case_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearReservedCase1() {
      reservedCase1Case_ = ReservedCase1OneofCase.None;
      reservedCase1_ = null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as ReservedItemAttribute6);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(ReservedItemAttribute6 other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(ReservedField3, other.ReservedField3)) return false;
      if (!object.Equals(ReservedField2, other.ReservedField2)) return false;
      if (ReservedCase1Case != other.ReservedCase1Case) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (reservedField3_ != null) hash ^= ReservedField3.GetHashCode();
      if (reservedCase1Case_ == ReservedCase1OneofCase.ReservedField2) hash ^= ReservedField2.GetHashCode();
      hash ^= (int) reservedCase1Case_;
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
      if (reservedCase1Case_ == ReservedCase1OneofCase.ReservedField2) {
        output.WriteRawTag(18);
        output.WriteMessage(ReservedField2);
      }
      if (reservedField3_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(ReservedField3);
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
      if (reservedCase1Case_ == ReservedCase1OneofCase.ReservedField2) {
        output.WriteRawTag(18);
        output.WriteMessage(ReservedField2);
      }
      if (reservedField3_ != null) {
        output.WriteRawTag(26);
        output.WriteMessage(ReservedField3);
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
      if (reservedField3_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(ReservedField3);
      }
      if (reservedCase1Case_ == ReservedCase1OneofCase.ReservedField2) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(ReservedField2);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(ReservedItemAttribute6 other) {
      if (other == null) {
        return;
      }
      if (other.reservedField3_ != null) {
        if (reservedField3_ == null) {
          ReservedField3 = new global::ClusterVR.CreatorKit.Proto.OffsetTransform();
        }
        ReservedField3.MergeFrom(other.ReservedField3);
      }
      switch (other.ReservedCase1Case) {
        case ReservedCase1OneofCase.ReservedField2:
          if (ReservedField2 == null) {
            ReservedField2 = new global::ClusterVR.CreatorKit.Proto.ReservedItemAttribute6FieldAttribute2();
          }
          ReservedField2.MergeFrom(other.ReservedField2);
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
            global::ClusterVR.CreatorKit.Proto.ReservedItemAttribute6FieldAttribute2 subBuilder = new global::ClusterVR.CreatorKit.Proto.ReservedItemAttribute6FieldAttribute2();
            if (reservedCase1Case_ == ReservedCase1OneofCase.ReservedField2) {
              subBuilder.MergeFrom(ReservedField2);
            }
            input.ReadMessage(subBuilder);
            ReservedField2 = subBuilder;
            break;
          }
          case 26: {
            if (reservedField3_ == null) {
              ReservedField3 = new global::ClusterVR.CreatorKit.Proto.OffsetTransform();
            }
            input.ReadMessage(ReservedField3);
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
            global::ClusterVR.CreatorKit.Proto.ReservedItemAttribute6FieldAttribute2 subBuilder = new global::ClusterVR.CreatorKit.Proto.ReservedItemAttribute6FieldAttribute2();
            if (reservedCase1Case_ == ReservedCase1OneofCase.ReservedField2) {
              subBuilder.MergeFrom(ReservedField2);
            }
            input.ReadMessage(subBuilder);
            ReservedField2 = subBuilder;
            break;
          }
          case 26: {
            if (reservedField3_ == null) {
              ReservedField3 = new global::ClusterVR.CreatorKit.Proto.OffsetTransform();
            }
            input.ReadMessage(ReservedField3);
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
