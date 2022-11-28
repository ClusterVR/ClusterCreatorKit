using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ClusterVR.CreatorKit.Editor.Api.RPC
{
    public sealed class UploadStatusChecker
    {
        readonly string accessToken;
        readonly string statusApiUrl; // アップロードが成功したかポーリングで確認するAPIのURL

        const int MaxRetryCount = 3;

        public UploadStatusChecker(string accessToken, string statusApiUrl)
        {
            this.accessToken = accessToken;
            this.statusApiUrl = statusApiUrl;
        }

        public async Task CheckUploadStatusAsync(CancellationToken cancellationToken)
        {
            using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            timeoutCts.CancelAfter(TimeSpan.FromSeconds(30));

            try
            {
                await CheckUploadStatusAsyncImpl(timeoutCts.Token);
            }
            catch (OperationCanceledException e) when (e.CancellationToken == timeoutCts.Token)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    throw new OperationCanceledException(e.Message, e, cancellationToken);
                }
                else
                {
                    throw new TimeoutException();
                }
            }
        }

        async Task CheckUploadStatusAsyncImpl(CancellationToken cancellationToken)
        {
            var retryCount = 0;
            var backOffMs = 1000;

            while (true)
            {
                try
                {
                    var result = await ApiClient.GetStatus(accessToken, statusApiUrl, cancellationToken);

                    var serializer = new VJson.JsonSerializer(typeof(UploadStatus));
                    var uploadStatus = (UploadStatus) serializer.Deserialize(result);
                    switch (uploadStatus.Status)
                    {
                        case UploadStatus.StatusEnum.Validating:
                            await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
                            continue;
                        case UploadStatus.StatusEnum.Completed:
                            return;
                        case UploadStatus.StatusEnum.Error:
                            throw new Exception(uploadStatus.Reason);
                        default:
                            throw new NotImplementedException();
                    }
                }
                catch (HttpException e)
                {
                    if (retryCount >= MaxRetryCount)
                    {
                        throw;
                    }
                    switch (e.GetHttpCode())
                    {
                        case 500:
                        case 503:
                        case 504:
                            break;
                        default:
                            throw;
                    }

                    ++retryCount;
                    await Task.Delay(backOffMs, cancellationToken);
                    backOffMs *= 2;
                }
            }
        }
    }
}
