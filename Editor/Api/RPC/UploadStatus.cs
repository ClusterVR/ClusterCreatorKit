using VJson;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public sealed class UploadStatus
    {
        [JsonField(Name = "file_id")] public string FileId;
        [JsonField(Name = "status")] public StatusEnum Status;
        [JsonField(Name = "reason")] public string Reason;

        [Json(EnumConversion = EnumConversionType.AsString)]
        public enum StatusEnum
        {
            [JsonField(Name = "VALIDATING")] Validating,
            [JsonField(Name = "ERROR")] Error,
            [JsonField(Name = "COMPLETED")] Completed,
        }
    }
}
