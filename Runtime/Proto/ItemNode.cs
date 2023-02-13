#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace ClusterVR.CreatorKit.Proto {

  public static partial class ItemNodeReflection {

    #region Descriptor
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static ItemNodeReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Ch5hcGkvY3JlYXRvcmtpdC9pdGVtX25vZGUucHJvdG8SEmNsdXN0ZXIuY3Jl",
            "YXRvcmtpdCKWAQoISXRlbU5vZGUSOgoPcGh5c2ljYWxfc2hhcGVzGAEgAygL",
            "MiEuY2x1c3Rlci5jcmVhdG9ya2l0LlBoeXNpY2FsU2hhcGUSPAoQbWFpbl9z",
            "Y3JlZW5fdmlldxgCIAEoCzIiLmNsdXN0ZXIuY3JlYXRvcmtpdC5NYWluU2Ny",
            "ZWVuVmlldxIQCghkaXNhYmxlZBgDIAEoCCI5Cg1QaHlzaWNhbFNoYXBlEigK",
            "BXNoYXBlGAEgASgLMhkuY2x1c3Rlci5jcmVhdG9ya2l0LlNoYXBlIsABCgVT",
            "aGFwZRImCgNib3gYASABKAsyFy5jbHVzdGVyLmNyZWF0b3JraXQuQm94SAAS",
            "LAoGc3BoZXJlGAIgASgLMhouY2x1c3Rlci5jcmVhdG9ya2l0LlNwaGVyZUgA",
            "Ei4KB2NhcHN1bGUYAyABKAsyGy5jbHVzdGVyLmNyZWF0b3JraXQuQ2Fwc3Vs",
            "ZUgAEigKBG1lc2gYBCABKAsyGC5jbHVzdGVyLmNyZWF0b3JraXQuTWVzaEgA",
            "QgcKBXNoYXBlIl0KA0JveBIrCgZjZW50ZXIYASABKAsyGy5jbHVzdGVyLmNy",
            "ZWF0b3JraXQuVmVjdG9yMxIpCgRzaXplGAIgASgLMhsuY2x1c3Rlci5jcmVh",
            "dG9ya2l0LlZlY3RvcjMiRQoGU3BoZXJlEisKBmNlbnRlchgBIAEoCzIbLmNs",
            "dXN0ZXIuY3JlYXRvcmtpdC5WZWN0b3IzEg4KBnJhZGl1cxgCIAEoAiKyAQoH",
            "Q2Fwc3VsZRIrCgZjZW50ZXIYASABKAsyGy5jbHVzdGVyLmNyZWF0b3JraXQu",
            "VmVjdG9yMxI4CglkaXJlY3Rpb24YAiABKA4yJS5jbHVzdGVyLmNyZWF0b3Jr",
            "aXQuQ2Fwc3VsZS5EaXJlY3Rpb24SDgoGaGVpZ2h0GAMgASgCEg4KBnJhZGl1",
            "cxgEIAEoAiIgCglEaXJlY3Rpb24SBQoBWBAAEgUKAVkQARIFCgFaEAIiMwoE",
            "TWVzaBIYChB2ZXJ0ZXhfcG9zaXRpb25zGAEgAygCEhEKCXRyaWFuZ2xlcxgC",
            "IAMoBSIbCgdWZWN0b3IzEhAKCGVsZW1lbnRzGAEgAygCIqABCg5NYWluU2Ny",
            "ZWVuVmlldxIbChNzY3JlZW5fYXNwZWN0X3JhdGlvGAEgASgCEmUKJXVubGl0",
            "X25vbl90aWxlZF93aXRoX2JhY2tncm91bmRfY29sb3IYAiABKAsyNC5jbHVz",
            "dGVyLmNyZWF0b3JraXQuVW5saXROb25UaWxlZFdpdGhCYWNrZ3JvdW5kQ29s",
            "b3JIAEIKCghtYXRlcmlhbCI8CiBVbmxpdE5vblRpbGVkV2l0aEJhY2tncm91",
            "bmRDb2xvchIYChBiYWNrZ3JvdW5kX2NvbG9yGAEgAygCQi1aDmNsdXN0ZXIu",
            "bXUvcnBjqgIaQ2x1c3RlclZSLkNyZWF0b3JLaXQuUHJvdG9iBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.ItemNode), global::ClusterVR.CreatorKit.Proto.ItemNode.Parser, new[]{ "PhysicalShapes", "MainScreenView", "Disabled" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.PhysicalShape), global::ClusterVR.CreatorKit.Proto.PhysicalShape.Parser, new[]{ "Shape" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.Shape), global::ClusterVR.CreatorKit.Proto.Shape.Parser, new[]{ "Box", "Sphere", "Capsule", "Mesh" }, new[]{ "Shape" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.Box), global::ClusterVR.CreatorKit.Proto.Box.Parser, new[]{ "Center", "Size" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.Sphere), global::ClusterVR.CreatorKit.Proto.Sphere.Parser, new[]{ "Center", "Radius" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.Capsule), global::ClusterVR.CreatorKit.Proto.Capsule.Parser, new[]{ "Center", "Direction", "Height", "Radius" }, null, new[]{ typeof(global::ClusterVR.CreatorKit.Proto.Capsule.Types.Direction) }, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.Mesh), global::ClusterVR.CreatorKit.Proto.Mesh.Parser, new[]{ "VertexPositions", "Triangles" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.Vector3), global::ClusterVR.CreatorKit.Proto.Vector3.Parser, new[]{ "Elements" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.MainScreenView), global::ClusterVR.CreatorKit.Proto.MainScreenView.Parser, new[]{ "ScreenAspectRatio", "UnlitNonTiledWithBackgroundColor" }, new[]{ "Material" }, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.UnlitNonTiledWithBackgroundColor), global::ClusterVR.CreatorKit.Proto.UnlitNonTiledWithBackgroundColor.Parser, new[]{ "BackgroundColor" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class ItemNode : pb::IMessage<ItemNode>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<ItemNode> _parser = new pb::MessageParser<ItemNode>(() => new ItemNode());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<ItemNode> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.ItemNodeReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ItemNode() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ItemNode(ItemNode other) : this() {
      physicalShapes_ = other.physicalShapes_.Clone();
      mainScreenView_ = other.mainScreenView_ != null ? other.mainScreenView_.Clone() : null;
      disabled_ = other.disabled_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ItemNode Clone() {
      return new ItemNode(this);
    }

    public const int PhysicalShapesFieldNumber = 1;
    private static readonly pb::FieldCodec<global::ClusterVR.CreatorKit.Proto.PhysicalShape> _repeated_physicalShapes_codec
        = pb::FieldCodec.ForMessage(10, global::ClusterVR.CreatorKit.Proto.PhysicalShape.Parser);
    private readonly pbc::RepeatedField<global::ClusterVR.CreatorKit.Proto.PhysicalShape> physicalShapes_ = new pbc::RepeatedField<global::ClusterVR.CreatorKit.Proto.PhysicalShape>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<global::ClusterVR.CreatorKit.Proto.PhysicalShape> PhysicalShapes {
      get { return physicalShapes_; }
    }

    public const int MainScreenViewFieldNumber = 2;
    private global::ClusterVR.CreatorKit.Proto.MainScreenView mainScreenView_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.MainScreenView MainScreenView {
      get { return mainScreenView_; }
      set {
        mainScreenView_ = value;
      }
    }

    public const int DisabledFieldNumber = 3;
    private bool disabled_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Disabled {
      get { return disabled_; }
      set {
        disabled_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as ItemNode);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(ItemNode other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!physicalShapes_.Equals(other.physicalShapes_)) return false;
      if (!object.Equals(MainScreenView, other.MainScreenView)) return false;
      if (Disabled != other.Disabled) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= physicalShapes_.GetHashCode();
      if (mainScreenView_ != null) hash ^= MainScreenView.GetHashCode();
      if (Disabled != false) hash ^= Disabled.GetHashCode();
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
      physicalShapes_.WriteTo(output, _repeated_physicalShapes_codec);
      if (mainScreenView_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(MainScreenView);
      }
      if (Disabled != false) {
        output.WriteRawTag(24);
        output.WriteBool(Disabled);
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
      physicalShapes_.WriteTo(ref output, _repeated_physicalShapes_codec);
      if (mainScreenView_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(MainScreenView);
      }
      if (Disabled != false) {
        output.WriteRawTag(24);
        output.WriteBool(Disabled);
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
      size += physicalShapes_.CalculateSize(_repeated_physicalShapes_codec);
      if (mainScreenView_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(MainScreenView);
      }
      if (Disabled != false) {
        size += 1 + 1;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(ItemNode other) {
      if (other == null) {
        return;
      }
      physicalShapes_.Add(other.physicalShapes_);
      if (other.mainScreenView_ != null) {
        if (mainScreenView_ == null) {
          MainScreenView = new global::ClusterVR.CreatorKit.Proto.MainScreenView();
        }
        MainScreenView.MergeFrom(other.MainScreenView);
      }
      if (other.Disabled != false) {
        Disabled = other.Disabled;
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
            physicalShapes_.AddEntriesFrom(input, _repeated_physicalShapes_codec);
            break;
          }
          case 18: {
            if (mainScreenView_ == null) {
              MainScreenView = new global::ClusterVR.CreatorKit.Proto.MainScreenView();
            }
            input.ReadMessage(MainScreenView);
            break;
          }
          case 24: {
            Disabled = input.ReadBool();
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
            physicalShapes_.AddEntriesFrom(ref input, _repeated_physicalShapes_codec);
            break;
          }
          case 18: {
            if (mainScreenView_ == null) {
              MainScreenView = new global::ClusterVR.CreatorKit.Proto.MainScreenView();
            }
            input.ReadMessage(MainScreenView);
            break;
          }
          case 24: {
            Disabled = input.ReadBool();
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class PhysicalShape : pb::IMessage<PhysicalShape>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<PhysicalShape> _parser = new pb::MessageParser<PhysicalShape>(() => new PhysicalShape());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<PhysicalShape> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.ItemNodeReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public PhysicalShape() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public PhysicalShape(PhysicalShape other) : this() {
      shape_ = other.shape_ != null ? other.shape_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public PhysicalShape Clone() {
      return new PhysicalShape(this);
    }

    public const int ShapeFieldNumber = 1;
    private global::ClusterVR.CreatorKit.Proto.Shape shape_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.Shape Shape {
      get { return shape_; }
      set {
        shape_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as PhysicalShape);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(PhysicalShape other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Shape, other.Shape)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (shape_ != null) hash ^= Shape.GetHashCode();
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
      if (shape_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Shape);
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
      if (shape_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Shape);
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
      if (shape_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Shape);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(PhysicalShape other) {
      if (other == null) {
        return;
      }
      if (other.shape_ != null) {
        if (shape_ == null) {
          Shape = new global::ClusterVR.CreatorKit.Proto.Shape();
        }
        Shape.MergeFrom(other.Shape);
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
            if (shape_ == null) {
              Shape = new global::ClusterVR.CreatorKit.Proto.Shape();
            }
            input.ReadMessage(Shape);
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
            if (shape_ == null) {
              Shape = new global::ClusterVR.CreatorKit.Proto.Shape();
            }
            input.ReadMessage(Shape);
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class Shape : pb::IMessage<Shape>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Shape> _parser = new pb::MessageParser<Shape>(() => new Shape());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<Shape> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.ItemNodeReflection.Descriptor.MessageTypes[2]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Shape() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Shape(Shape other) : this() {
      switch (other.ShapeCase) {
        case ShapeOneofCase.Box:
          Box = other.Box.Clone();
          break;
        case ShapeOneofCase.Sphere:
          Sphere = other.Sphere.Clone();
          break;
        case ShapeOneofCase.Capsule:
          Capsule = other.Capsule.Clone();
          break;
        case ShapeOneofCase.Mesh:
          Mesh = other.Mesh.Clone();
          break;
      }

      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Shape Clone() {
      return new Shape(this);
    }

    public const int BoxFieldNumber = 1;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.Box Box {
      get { return shapeCase_ == ShapeOneofCase.Box ? (global::ClusterVR.CreatorKit.Proto.Box) shape_ : null; }
      set {
        shape_ = value;
        shapeCase_ = value == null ? ShapeOneofCase.None : ShapeOneofCase.Box;
      }
    }

    public const int SphereFieldNumber = 2;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.Sphere Sphere {
      get { return shapeCase_ == ShapeOneofCase.Sphere ? (global::ClusterVR.CreatorKit.Proto.Sphere) shape_ : null; }
      set {
        shape_ = value;
        shapeCase_ = value == null ? ShapeOneofCase.None : ShapeOneofCase.Sphere;
      }
    }

    public const int CapsuleFieldNumber = 3;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.Capsule Capsule {
      get { return shapeCase_ == ShapeOneofCase.Capsule ? (global::ClusterVR.CreatorKit.Proto.Capsule) shape_ : null; }
      set {
        shape_ = value;
        shapeCase_ = value == null ? ShapeOneofCase.None : ShapeOneofCase.Capsule;
      }
    }

    public const int MeshFieldNumber = 4;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.Mesh Mesh {
      get { return shapeCase_ == ShapeOneofCase.Mesh ? (global::ClusterVR.CreatorKit.Proto.Mesh) shape_ : null; }
      set {
        shape_ = value;
        shapeCase_ = value == null ? ShapeOneofCase.None : ShapeOneofCase.Mesh;
      }
    }

    private object shape_;
    public enum ShapeOneofCase {
      None = 0,
      Box = 1,
      Sphere = 2,
      Capsule = 3,
      Mesh = 4,
    }
    private ShapeOneofCase shapeCase_ = ShapeOneofCase.None;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public ShapeOneofCase ShapeCase {
      get { return shapeCase_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearShape() {
      shapeCase_ = ShapeOneofCase.None;
      shape_ = null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as Shape);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(Shape other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Box, other.Box)) return false;
      if (!object.Equals(Sphere, other.Sphere)) return false;
      if (!object.Equals(Capsule, other.Capsule)) return false;
      if (!object.Equals(Mesh, other.Mesh)) return false;
      if (ShapeCase != other.ShapeCase) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (shapeCase_ == ShapeOneofCase.Box) hash ^= Box.GetHashCode();
      if (shapeCase_ == ShapeOneofCase.Sphere) hash ^= Sphere.GetHashCode();
      if (shapeCase_ == ShapeOneofCase.Capsule) hash ^= Capsule.GetHashCode();
      if (shapeCase_ == ShapeOneofCase.Mesh) hash ^= Mesh.GetHashCode();
      hash ^= (int) shapeCase_;
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
      if (shapeCase_ == ShapeOneofCase.Box) {
        output.WriteRawTag(10);
        output.WriteMessage(Box);
      }
      if (shapeCase_ == ShapeOneofCase.Sphere) {
        output.WriteRawTag(18);
        output.WriteMessage(Sphere);
      }
      if (shapeCase_ == ShapeOneofCase.Capsule) {
        output.WriteRawTag(26);
        output.WriteMessage(Capsule);
      }
      if (shapeCase_ == ShapeOneofCase.Mesh) {
        output.WriteRawTag(34);
        output.WriteMessage(Mesh);
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
      if (shapeCase_ == ShapeOneofCase.Box) {
        output.WriteRawTag(10);
        output.WriteMessage(Box);
      }
      if (shapeCase_ == ShapeOneofCase.Sphere) {
        output.WriteRawTag(18);
        output.WriteMessage(Sphere);
      }
      if (shapeCase_ == ShapeOneofCase.Capsule) {
        output.WriteRawTag(26);
        output.WriteMessage(Capsule);
      }
      if (shapeCase_ == ShapeOneofCase.Mesh) {
        output.WriteRawTag(34);
        output.WriteMessage(Mesh);
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
      if (shapeCase_ == ShapeOneofCase.Box) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Box);
      }
      if (shapeCase_ == ShapeOneofCase.Sphere) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Sphere);
      }
      if (shapeCase_ == ShapeOneofCase.Capsule) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Capsule);
      }
      if (shapeCase_ == ShapeOneofCase.Mesh) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Mesh);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(Shape other) {
      if (other == null) {
        return;
      }
      switch (other.ShapeCase) {
        case ShapeOneofCase.Box:
          if (Box == null) {
            Box = new global::ClusterVR.CreatorKit.Proto.Box();
          }
          Box.MergeFrom(other.Box);
          break;
        case ShapeOneofCase.Sphere:
          if (Sphere == null) {
            Sphere = new global::ClusterVR.CreatorKit.Proto.Sphere();
          }
          Sphere.MergeFrom(other.Sphere);
          break;
        case ShapeOneofCase.Capsule:
          if (Capsule == null) {
            Capsule = new global::ClusterVR.CreatorKit.Proto.Capsule();
          }
          Capsule.MergeFrom(other.Capsule);
          break;
        case ShapeOneofCase.Mesh:
          if (Mesh == null) {
            Mesh = new global::ClusterVR.CreatorKit.Proto.Mesh();
          }
          Mesh.MergeFrom(other.Mesh);
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
            global::ClusterVR.CreatorKit.Proto.Box subBuilder = new global::ClusterVR.CreatorKit.Proto.Box();
            if (shapeCase_ == ShapeOneofCase.Box) {
              subBuilder.MergeFrom(Box);
            }
            input.ReadMessage(subBuilder);
            Box = subBuilder;
            break;
          }
          case 18: {
            global::ClusterVR.CreatorKit.Proto.Sphere subBuilder = new global::ClusterVR.CreatorKit.Proto.Sphere();
            if (shapeCase_ == ShapeOneofCase.Sphere) {
              subBuilder.MergeFrom(Sphere);
            }
            input.ReadMessage(subBuilder);
            Sphere = subBuilder;
            break;
          }
          case 26: {
            global::ClusterVR.CreatorKit.Proto.Capsule subBuilder = new global::ClusterVR.CreatorKit.Proto.Capsule();
            if (shapeCase_ == ShapeOneofCase.Capsule) {
              subBuilder.MergeFrom(Capsule);
            }
            input.ReadMessage(subBuilder);
            Capsule = subBuilder;
            break;
          }
          case 34: {
            global::ClusterVR.CreatorKit.Proto.Mesh subBuilder = new global::ClusterVR.CreatorKit.Proto.Mesh();
            if (shapeCase_ == ShapeOneofCase.Mesh) {
              subBuilder.MergeFrom(Mesh);
            }
            input.ReadMessage(subBuilder);
            Mesh = subBuilder;
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
            global::ClusterVR.CreatorKit.Proto.Box subBuilder = new global::ClusterVR.CreatorKit.Proto.Box();
            if (shapeCase_ == ShapeOneofCase.Box) {
              subBuilder.MergeFrom(Box);
            }
            input.ReadMessage(subBuilder);
            Box = subBuilder;
            break;
          }
          case 18: {
            global::ClusterVR.CreatorKit.Proto.Sphere subBuilder = new global::ClusterVR.CreatorKit.Proto.Sphere();
            if (shapeCase_ == ShapeOneofCase.Sphere) {
              subBuilder.MergeFrom(Sphere);
            }
            input.ReadMessage(subBuilder);
            Sphere = subBuilder;
            break;
          }
          case 26: {
            global::ClusterVR.CreatorKit.Proto.Capsule subBuilder = new global::ClusterVR.CreatorKit.Proto.Capsule();
            if (shapeCase_ == ShapeOneofCase.Capsule) {
              subBuilder.MergeFrom(Capsule);
            }
            input.ReadMessage(subBuilder);
            Capsule = subBuilder;
            break;
          }
          case 34: {
            global::ClusterVR.CreatorKit.Proto.Mesh subBuilder = new global::ClusterVR.CreatorKit.Proto.Mesh();
            if (shapeCase_ == ShapeOneofCase.Mesh) {
              subBuilder.MergeFrom(Mesh);
            }
            input.ReadMessage(subBuilder);
            Mesh = subBuilder;
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class Box : pb::IMessage<Box>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Box> _parser = new pb::MessageParser<Box>(() => new Box());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<Box> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.ItemNodeReflection.Descriptor.MessageTypes[3]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Box() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Box(Box other) : this() {
      center_ = other.center_ != null ? other.center_.Clone() : null;
      size_ = other.size_ != null ? other.size_.Clone() : null;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Box Clone() {
      return new Box(this);
    }

    public const int CenterFieldNumber = 1;
    private global::ClusterVR.CreatorKit.Proto.Vector3 center_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.Vector3 Center {
      get { return center_; }
      set {
        center_ = value;
      }
    }

    public const int SizeFieldNumber = 2;
    private global::ClusterVR.CreatorKit.Proto.Vector3 size_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.Vector3 Size {
      get { return size_; }
      set {
        size_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as Box);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(Box other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Center, other.Center)) return false;
      if (!object.Equals(Size, other.Size)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (center_ != null) hash ^= Center.GetHashCode();
      if (size_ != null) hash ^= Size.GetHashCode();
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
      if (center_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Center);
      }
      if (size_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(Size);
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
      if (center_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Center);
      }
      if (size_ != null) {
        output.WriteRawTag(18);
        output.WriteMessage(Size);
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
      if (center_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Center);
      }
      if (size_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Size);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(Box other) {
      if (other == null) {
        return;
      }
      if (other.center_ != null) {
        if (center_ == null) {
          Center = new global::ClusterVR.CreatorKit.Proto.Vector3();
        }
        Center.MergeFrom(other.Center);
      }
      if (other.size_ != null) {
        if (size_ == null) {
          Size = new global::ClusterVR.CreatorKit.Proto.Vector3();
        }
        Size.MergeFrom(other.Size);
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
            if (center_ == null) {
              Center = new global::ClusterVR.CreatorKit.Proto.Vector3();
            }
            input.ReadMessage(Center);
            break;
          }
          case 18: {
            if (size_ == null) {
              Size = new global::ClusterVR.CreatorKit.Proto.Vector3();
            }
            input.ReadMessage(Size);
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
            if (center_ == null) {
              Center = new global::ClusterVR.CreatorKit.Proto.Vector3();
            }
            input.ReadMessage(Center);
            break;
          }
          case 18: {
            if (size_ == null) {
              Size = new global::ClusterVR.CreatorKit.Proto.Vector3();
            }
            input.ReadMessage(Size);
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class Sphere : pb::IMessage<Sphere>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Sphere> _parser = new pb::MessageParser<Sphere>(() => new Sphere());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<Sphere> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.ItemNodeReflection.Descriptor.MessageTypes[4]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Sphere() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Sphere(Sphere other) : this() {
      center_ = other.center_ != null ? other.center_.Clone() : null;
      radius_ = other.radius_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Sphere Clone() {
      return new Sphere(this);
    }

    public const int CenterFieldNumber = 1;
    private global::ClusterVR.CreatorKit.Proto.Vector3 center_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.Vector3 Center {
      get { return center_; }
      set {
        center_ = value;
      }
    }

    public const int RadiusFieldNumber = 2;
    private float radius_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public float Radius {
      get { return radius_; }
      set {
        radius_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as Sphere);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(Sphere other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Center, other.Center)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Radius, other.Radius)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (center_ != null) hash ^= Center.GetHashCode();
      if (Radius != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Radius);
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
      if (center_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Center);
      }
      if (Radius != 0F) {
        output.WriteRawTag(21);
        output.WriteFloat(Radius);
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
      if (center_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Center);
      }
      if (Radius != 0F) {
        output.WriteRawTag(21);
        output.WriteFloat(Radius);
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
      if (center_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Center);
      }
      if (Radius != 0F) {
        size += 1 + 4;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(Sphere other) {
      if (other == null) {
        return;
      }
      if (other.center_ != null) {
        if (center_ == null) {
          Center = new global::ClusterVR.CreatorKit.Proto.Vector3();
        }
        Center.MergeFrom(other.Center);
      }
      if (other.Radius != 0F) {
        Radius = other.Radius;
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
            if (center_ == null) {
              Center = new global::ClusterVR.CreatorKit.Proto.Vector3();
            }
            input.ReadMessage(Center);
            break;
          }
          case 21: {
            Radius = input.ReadFloat();
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
            if (center_ == null) {
              Center = new global::ClusterVR.CreatorKit.Proto.Vector3();
            }
            input.ReadMessage(Center);
            break;
          }
          case 21: {
            Radius = input.ReadFloat();
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class Capsule : pb::IMessage<Capsule>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Capsule> _parser = new pb::MessageParser<Capsule>(() => new Capsule());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<Capsule> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.ItemNodeReflection.Descriptor.MessageTypes[5]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Capsule() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Capsule(Capsule other) : this() {
      center_ = other.center_ != null ? other.center_.Clone() : null;
      direction_ = other.direction_;
      height_ = other.height_;
      radius_ = other.radius_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Capsule Clone() {
      return new Capsule(this);
    }

    public const int CenterFieldNumber = 1;
    private global::ClusterVR.CreatorKit.Proto.Vector3 center_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.Vector3 Center {
      get { return center_; }
      set {
        center_ = value;
      }
    }

    public const int DirectionFieldNumber = 2;
    private global::ClusterVR.CreatorKit.Proto.Capsule.Types.Direction direction_ = global::ClusterVR.CreatorKit.Proto.Capsule.Types.Direction.X;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.Capsule.Types.Direction Direction {
      get { return direction_; }
      set {
        direction_ = value;
      }
    }

    public const int HeightFieldNumber = 3;
    private float height_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public float Height {
      get { return height_; }
      set {
        height_ = value;
      }
    }

    public const int RadiusFieldNumber = 4;
    private float radius_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public float Radius {
      get { return radius_; }
      set {
        radius_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as Capsule);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(Capsule other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!object.Equals(Center, other.Center)) return false;
      if (Direction != other.Direction) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Height, other.Height)) return false;
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(Radius, other.Radius)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (center_ != null) hash ^= Center.GetHashCode();
      if (Direction != global::ClusterVR.CreatorKit.Proto.Capsule.Types.Direction.X) hash ^= Direction.GetHashCode();
      if (Height != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Height);
      if (Radius != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(Radius);
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
      if (center_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Center);
      }
      if (Direction != global::ClusterVR.CreatorKit.Proto.Capsule.Types.Direction.X) {
        output.WriteRawTag(16);
        output.WriteEnum((int) Direction);
      }
      if (Height != 0F) {
        output.WriteRawTag(29);
        output.WriteFloat(Height);
      }
      if (Radius != 0F) {
        output.WriteRawTag(37);
        output.WriteFloat(Radius);
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
      if (center_ != null) {
        output.WriteRawTag(10);
        output.WriteMessage(Center);
      }
      if (Direction != global::ClusterVR.CreatorKit.Proto.Capsule.Types.Direction.X) {
        output.WriteRawTag(16);
        output.WriteEnum((int) Direction);
      }
      if (Height != 0F) {
        output.WriteRawTag(29);
        output.WriteFloat(Height);
      }
      if (Radius != 0F) {
        output.WriteRawTag(37);
        output.WriteFloat(Radius);
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
      if (center_ != null) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(Center);
      }
      if (Direction != global::ClusterVR.CreatorKit.Proto.Capsule.Types.Direction.X) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Direction);
      }
      if (Height != 0F) {
        size += 1 + 4;
      }
      if (Radius != 0F) {
        size += 1 + 4;
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(Capsule other) {
      if (other == null) {
        return;
      }
      if (other.center_ != null) {
        if (center_ == null) {
          Center = new global::ClusterVR.CreatorKit.Proto.Vector3();
        }
        Center.MergeFrom(other.Center);
      }
      if (other.Direction != global::ClusterVR.CreatorKit.Proto.Capsule.Types.Direction.X) {
        Direction = other.Direction;
      }
      if (other.Height != 0F) {
        Height = other.Height;
      }
      if (other.Radius != 0F) {
        Radius = other.Radius;
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
            if (center_ == null) {
              Center = new global::ClusterVR.CreatorKit.Proto.Vector3();
            }
            input.ReadMessage(Center);
            break;
          }
          case 16: {
            Direction = (global::ClusterVR.CreatorKit.Proto.Capsule.Types.Direction) input.ReadEnum();
            break;
          }
          case 29: {
            Height = input.ReadFloat();
            break;
          }
          case 37: {
            Radius = input.ReadFloat();
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
            if (center_ == null) {
              Center = new global::ClusterVR.CreatorKit.Proto.Vector3();
            }
            input.ReadMessage(Center);
            break;
          }
          case 16: {
            Direction = (global::ClusterVR.CreatorKit.Proto.Capsule.Types.Direction) input.ReadEnum();
            break;
          }
          case 29: {
            Height = input.ReadFloat();
            break;
          }
          case 37: {
            Radius = input.ReadFloat();
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
      public enum Direction {
        [pbr::OriginalName("X")] X = 0,
        [pbr::OriginalName("Y")] Y = 1,
        [pbr::OriginalName("Z")] Z = 2,
      }

    }
    #endregion

  }

  public sealed partial class Mesh : pb::IMessage<Mesh>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Mesh> _parser = new pb::MessageParser<Mesh>(() => new Mesh());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<Mesh> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.ItemNodeReflection.Descriptor.MessageTypes[6]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Mesh() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Mesh(Mesh other) : this() {
      vertexPositions_ = other.vertexPositions_.Clone();
      triangles_ = other.triangles_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Mesh Clone() {
      return new Mesh(this);
    }

    public const int VertexPositionsFieldNumber = 1;
    private static readonly pb::FieldCodec<float> _repeated_vertexPositions_codec
        = pb::FieldCodec.ForFloat(10);
    private readonly pbc::RepeatedField<float> vertexPositions_ = new pbc::RepeatedField<float>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<float> VertexPositions {
      get { return vertexPositions_; }
    }

    public const int TrianglesFieldNumber = 2;
    private static readonly pb::FieldCodec<int> _repeated_triangles_codec
        = pb::FieldCodec.ForInt32(18);
    private readonly pbc::RepeatedField<int> triangles_ = new pbc::RepeatedField<int>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<int> Triangles {
      get { return triangles_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as Mesh);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(Mesh other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!vertexPositions_.Equals(other.vertexPositions_)) return false;
      if(!triangles_.Equals(other.triangles_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= vertexPositions_.GetHashCode();
      hash ^= triangles_.GetHashCode();
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
      vertexPositions_.WriteTo(output, _repeated_vertexPositions_codec);
      triangles_.WriteTo(output, _repeated_triangles_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      vertexPositions_.WriteTo(ref output, _repeated_vertexPositions_codec);
      triangles_.WriteTo(ref output, _repeated_triangles_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      size += vertexPositions_.CalculateSize(_repeated_vertexPositions_codec);
      size += triangles_.CalculateSize(_repeated_triangles_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(Mesh other) {
      if (other == null) {
        return;
      }
      vertexPositions_.Add(other.vertexPositions_);
      triangles_.Add(other.triangles_);
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
            vertexPositions_.AddEntriesFrom(input, _repeated_vertexPositions_codec);
            break;
          }
          case 18:
          case 16: {
            triangles_.AddEntriesFrom(input, _repeated_triangles_codec);
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
            vertexPositions_.AddEntriesFrom(ref input, _repeated_vertexPositions_codec);
            break;
          }
          case 18:
          case 16: {
            triangles_.AddEntriesFrom(ref input, _repeated_triangles_codec);
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class Vector3 : pb::IMessage<Vector3>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Vector3> _parser = new pb::MessageParser<Vector3>(() => new Vector3());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<Vector3> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.ItemNodeReflection.Descriptor.MessageTypes[7]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Vector3() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Vector3(Vector3 other) : this() {
      elements_ = other.elements_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public Vector3 Clone() {
      return new Vector3(this);
    }

    public const int ElementsFieldNumber = 1;
    private static readonly pb::FieldCodec<float> _repeated_elements_codec
        = pb::FieldCodec.ForFloat(10);
    private readonly pbc::RepeatedField<float> elements_ = new pbc::RepeatedField<float>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<float> Elements {
      get { return elements_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as Vector3);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(Vector3 other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!elements_.Equals(other.elements_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= elements_.GetHashCode();
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
      elements_.WriteTo(output, _repeated_elements_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      elements_.WriteTo(ref output, _repeated_elements_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      size += elements_.CalculateSize(_repeated_elements_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(Vector3 other) {
      if (other == null) {
        return;
      }
      elements_.Add(other.elements_);
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
            elements_.AddEntriesFrom(input, _repeated_elements_codec);
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
            elements_.AddEntriesFrom(ref input, _repeated_elements_codec);
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class MainScreenView : pb::IMessage<MainScreenView>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<MainScreenView> _parser = new pb::MessageParser<MainScreenView>(() => new MainScreenView());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<MainScreenView> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.ItemNodeReflection.Descriptor.MessageTypes[8]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public MainScreenView() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public MainScreenView(MainScreenView other) : this() {
      screenAspectRatio_ = other.screenAspectRatio_;
      switch (other.MaterialCase) {
        case MaterialOneofCase.UnlitNonTiledWithBackgroundColor:
          UnlitNonTiledWithBackgroundColor = other.UnlitNonTiledWithBackgroundColor.Clone();
          break;
      }

      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public MainScreenView Clone() {
      return new MainScreenView(this);
    }

    public const int ScreenAspectRatioFieldNumber = 1;
    private float screenAspectRatio_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public float ScreenAspectRatio {
      get { return screenAspectRatio_; }
      set {
        screenAspectRatio_ = value;
      }
    }

    public const int UnlitNonTiledWithBackgroundColorFieldNumber = 2;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public global::ClusterVR.CreatorKit.Proto.UnlitNonTiledWithBackgroundColor UnlitNonTiledWithBackgroundColor {
      get { return materialCase_ == MaterialOneofCase.UnlitNonTiledWithBackgroundColor ? (global::ClusterVR.CreatorKit.Proto.UnlitNonTiledWithBackgroundColor) material_ : null; }
      set {
        material_ = value;
        materialCase_ = value == null ? MaterialOneofCase.None : MaterialOneofCase.UnlitNonTiledWithBackgroundColor;
      }
    }

    private object material_;
    public enum MaterialOneofCase {
      None = 0,
      UnlitNonTiledWithBackgroundColor = 2,
    }
    private MaterialOneofCase materialCase_ = MaterialOneofCase.None;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public MaterialOneofCase MaterialCase {
      get { return materialCase_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void ClearMaterial() {
      materialCase_ = MaterialOneofCase.None;
      material_ = null;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as MainScreenView);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(MainScreenView other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.Equals(ScreenAspectRatio, other.ScreenAspectRatio)) return false;
      if (!object.Equals(UnlitNonTiledWithBackgroundColor, other.UnlitNonTiledWithBackgroundColor)) return false;
      if (MaterialCase != other.MaterialCase) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      if (ScreenAspectRatio != 0F) hash ^= pbc::ProtobufEqualityComparers.BitwiseSingleEqualityComparer.GetHashCode(ScreenAspectRatio);
      if (materialCase_ == MaterialOneofCase.UnlitNonTiledWithBackgroundColor) hash ^= UnlitNonTiledWithBackgroundColor.GetHashCode();
      hash ^= (int) materialCase_;
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
      if (ScreenAspectRatio != 0F) {
        output.WriteRawTag(13);
        output.WriteFloat(ScreenAspectRatio);
      }
      if (materialCase_ == MaterialOneofCase.UnlitNonTiledWithBackgroundColor) {
        output.WriteRawTag(18);
        output.WriteMessage(UnlitNonTiledWithBackgroundColor);
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
      if (ScreenAspectRatio != 0F) {
        output.WriteRawTag(13);
        output.WriteFloat(ScreenAspectRatio);
      }
      if (materialCase_ == MaterialOneofCase.UnlitNonTiledWithBackgroundColor) {
        output.WriteRawTag(18);
        output.WriteMessage(UnlitNonTiledWithBackgroundColor);
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
      if (ScreenAspectRatio != 0F) {
        size += 1 + 4;
      }
      if (materialCase_ == MaterialOneofCase.UnlitNonTiledWithBackgroundColor) {
        size += 1 + pb::CodedOutputStream.ComputeMessageSize(UnlitNonTiledWithBackgroundColor);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(MainScreenView other) {
      if (other == null) {
        return;
      }
      if (other.ScreenAspectRatio != 0F) {
        ScreenAspectRatio = other.ScreenAspectRatio;
      }
      switch (other.MaterialCase) {
        case MaterialOneofCase.UnlitNonTiledWithBackgroundColor:
          if (UnlitNonTiledWithBackgroundColor == null) {
            UnlitNonTiledWithBackgroundColor = new global::ClusterVR.CreatorKit.Proto.UnlitNonTiledWithBackgroundColor();
          }
          UnlitNonTiledWithBackgroundColor.MergeFrom(other.UnlitNonTiledWithBackgroundColor);
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
          case 13: {
            ScreenAspectRatio = input.ReadFloat();
            break;
          }
          case 18: {
            global::ClusterVR.CreatorKit.Proto.UnlitNonTiledWithBackgroundColor subBuilder = new global::ClusterVR.CreatorKit.Proto.UnlitNonTiledWithBackgroundColor();
            if (materialCase_ == MaterialOneofCase.UnlitNonTiledWithBackgroundColor) {
              subBuilder.MergeFrom(UnlitNonTiledWithBackgroundColor);
            }
            input.ReadMessage(subBuilder);
            UnlitNonTiledWithBackgroundColor = subBuilder;
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
          case 13: {
            ScreenAspectRatio = input.ReadFloat();
            break;
          }
          case 18: {
            global::ClusterVR.CreatorKit.Proto.UnlitNonTiledWithBackgroundColor subBuilder = new global::ClusterVR.CreatorKit.Proto.UnlitNonTiledWithBackgroundColor();
            if (materialCase_ == MaterialOneofCase.UnlitNonTiledWithBackgroundColor) {
              subBuilder.MergeFrom(UnlitNonTiledWithBackgroundColor);
            }
            input.ReadMessage(subBuilder);
            UnlitNonTiledWithBackgroundColor = subBuilder;
            break;
          }
        }
      }
    }
    #endif

  }

  public sealed partial class UnlitNonTiledWithBackgroundColor : pb::IMessage<UnlitNonTiledWithBackgroundColor>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<UnlitNonTiledWithBackgroundColor> _parser = new pb::MessageParser<UnlitNonTiledWithBackgroundColor>(() => new UnlitNonTiledWithBackgroundColor());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pb::MessageParser<UnlitNonTiledWithBackgroundColor> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.ItemNodeReflection.Descriptor.MessageTypes[9]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public UnlitNonTiledWithBackgroundColor() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public UnlitNonTiledWithBackgroundColor(UnlitNonTiledWithBackgroundColor other) : this() {
      backgroundColor_ = other.backgroundColor_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public UnlitNonTiledWithBackgroundColor Clone() {
      return new UnlitNonTiledWithBackgroundColor(this);
    }

    public const int BackgroundColorFieldNumber = 1;
    private static readonly pb::FieldCodec<float> _repeated_backgroundColor_codec
        = pb::FieldCodec.ForFloat(10);
    private readonly pbc::RepeatedField<float> backgroundColor_ = new pbc::RepeatedField<float>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public pbc::RepeatedField<float> BackgroundColor {
      get { return backgroundColor_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override bool Equals(object other) {
      return Equals(other as UnlitNonTiledWithBackgroundColor);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public bool Equals(UnlitNonTiledWithBackgroundColor other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!backgroundColor_.Equals(other.backgroundColor_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= backgroundColor_.GetHashCode();
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
      backgroundColor_.WriteTo(output, _repeated_backgroundColor_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      backgroundColor_.WriteTo(ref output, _repeated_backgroundColor_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public int CalculateSize() {
      int size = 0;
      size += backgroundColor_.CalculateSize(_repeated_backgroundColor_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    [global::System.CodeDom.Compiler.GeneratedCode("protoc", null)]
    public void MergeFrom(UnlitNonTiledWithBackgroundColor other) {
      if (other == null) {
        return;
      }
      backgroundColor_.Add(other.backgroundColor_);
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
            backgroundColor_.AddEntriesFrom(input, _repeated_backgroundColor_codec);
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
            backgroundColor_.AddEntriesFrom(ref input, _repeated_backgroundColor_codec);
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
