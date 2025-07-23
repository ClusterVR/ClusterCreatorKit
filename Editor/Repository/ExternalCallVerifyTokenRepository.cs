using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.ExternalEndpoint;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Utils;

namespace ClusterVR.CreatorKit.Editor.Repository
{
    public sealed class ExternalCallVerifyTokenRepository
    {
        public static readonly ExternalCallVerifyTokenRepository Instance = new();

        readonly Reactive<ExternalCallVerifyToken[]> verifyTokenList = new();
        public IReadOnlyReactive<ExternalCallVerifyToken[]> VerifyTokenList => verifyTokenList;

        private ExternalCallVerifyTokenRepository() { }

        public void Clear()
        {
            verifyTokenList.Val = null;
        }

        public async Task LoadVerifyTokenListAsync(string accessToken, CancellationToken cancellationToken)
        {
            try
            {
                var response = await APIServiceClient.GetVerifyTokenListAsync(accessToken, cancellationToken);
                verifyTokenList.Val = response.Tokens;
            }
            catch (Exception)
            {
                verifyTokenList.Val = null;
                throw;
            }
        }

        public async Task RegisterVerifyTokenAsync(string accessToken, CancellationToken cancellationToken)
        {
            var response = await APIServiceClient.RegisterVerifyTokenAsync(accessToken, cancellationToken);
            var list = verifyTokenList.Val;
            list = list.Append(new ExternalCallVerifyToken(response.RegisteredAt, response.TokenId, response.VerifyToken))
                .ToArray();
            verifyTokenList.Val = list;
        }

        public async Task DeleteVerifyTokenAsync(string accessToken, string tokenId, CancellationToken cancellationToken)
        {
            await APIServiceClient.DeleteVerifyTokenAsync(accessToken, tokenId, cancellationToken);
            var list = verifyTokenList.Val;
            list = list.Where(verifyToken => verifyToken.TokenId != tokenId).ToArray();
            verifyTokenList.Val = list;
        }
    }
}
