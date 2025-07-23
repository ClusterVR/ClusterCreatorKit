using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.ExternalEndpoint;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Utils;

namespace ClusterVR.CreatorKit.Editor.Repository
{
    public sealed class ExternalCallEndpointRepository
    {
        public static readonly ExternalCallEndpointRepository Instance = new();

        readonly Reactive<ExternalCallEndpoint[]> endpointList = new();
        public IReadOnlyReactive<ExternalCallEndpoint[]> EndpointList => endpointList;

        private ExternalCallEndpointRepository() { }

        public void Clear()
        {
            endpointList.Val = null;
        }

        public async Task LoadEndpointListAsync(string accessToken, CancellationToken cancellationToken)
        {
            try
            {
                var response = await APIServiceClient.GetEndpointListAsync(accessToken, cancellationToken);
                endpointList.Val = response.Endpoints;
            }
            catch (Exception)
            {
                endpointList.Val = null;
                throw;
            }
        }

        public async Task RegisterEndpointAsync(string accessToken, string url, CancellationToken cancellationToken)
        {
            var response = await APIServiceClient.RegisterEndpointAsync(accessToken, url, cancellationToken);
            var list = endpointList.Val;
            list = list.Append(new ExternalCallEndpoint(response.EndpointId, response.Url))
                .ToArray();
            endpointList.Val = list;
        }

        public async Task DeleteEndpointAsync(string accessToken, string endpointId, CancellationToken cancellationToken)
        {
            await APIServiceClient.DeleteEndpointAsync(accessToken, endpointId, cancellationToken);
            var list = endpointList.Val;
            list = list.Where(endpoint => endpoint.EndpointId != endpointId).ToArray();
            endpointList.Val = list;
        }
    }
}
