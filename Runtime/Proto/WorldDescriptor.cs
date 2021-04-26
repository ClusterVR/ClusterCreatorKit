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
            "ChZ3b3JsZF9kZXNjcmlwdG9yLnByb3RvEhJjbHVzdGVyLmNyZWF0b3JraXQi",
            "NgoPV29ybGREZXNjcmlwdG9yEiMKG3BlcnNpc3RlZF9wbGF5ZXJfc3RhdGVf",
            "a2V5cxgBIAMoCUIqWgtjbHVzdGVyL3JwY6oCGkNsdXN0ZXJWUi5DcmVhdG9y",
            "S2l0LlByb3RvYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::ClusterVR.CreatorKit.Proto.WorldDescriptor), global::ClusterVR.CreatorKit.Proto.WorldDescriptor.Parser, new[]{ "PersistedPlayerStateKeys" }, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class WorldDescriptor : pb::IMessage<WorldDescriptor> {
    private static readonly pb::MessageParser<WorldDescriptor> _parser = new pb::MessageParser<WorldDescriptor>(() => new WorldDescriptor());
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<WorldDescriptor> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ClusterVR.CreatorKit.Proto.WorldDescriptorReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public WorldDescriptor() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public WorldDescriptor(WorldDescriptor other) : this() {
      persistedPlayerStateKeys_ = other.persistedPlayerStateKeys_.Clone();
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public WorldDescriptor Clone() {
      return new WorldDescriptor(this);
    }

    public const int PersistedPlayerStateKeysFieldNumber = 1;
    private static readonly pb::FieldCodec<string> _repeated_persistedPlayerStateKeys_codec
        = pb::FieldCodec.ForString(10);
    private readonly pbc::RepeatedField<string> persistedPlayerStateKeys_ = new pbc::RepeatedField<string>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<string> PersistedPlayerStateKeys {
      get { return persistedPlayerStateKeys_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as WorldDescriptor);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(WorldDescriptor other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if(!persistedPlayerStateKeys_.Equals(other.persistedPlayerStateKeys_)) return false;
      return true;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= persistedPlayerStateKeys_.GetHashCode();
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      persistedPlayerStateKeys_.WriteTo(output, _repeated_persistedPlayerStateKeys_codec);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += persistedPlayerStateKeys_.CalculateSize(_repeated_persistedPlayerStateKeys_codec);
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(WorldDescriptor other) {
      if (other == null) {
        return;
      }
      persistedPlayerStateKeys_.Add(other.persistedPlayerStateKeys_);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            input.SkipLastField();
            break;
          case 10: {
            persistedPlayerStateKeys_.AddEntriesFrom(input, _repeated_persistedPlayerStateKeys_codec);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
