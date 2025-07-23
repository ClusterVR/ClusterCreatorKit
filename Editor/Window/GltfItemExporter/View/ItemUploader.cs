using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ClusterVR.CreatorKit.Editor.Api.RPC;

namespace ClusterVR.CreatorKit.Editor.Window.GltfItemExporter.View
{
    public sealed class ItemUploader
    {
        readonly IItemUploadService uploadService;
        readonly ItemBuilder itemBuilder;

        public ItemUploader(IItemBuilderDependencies dependencies, ItemBuilder itemBuilder)
        {
            uploadService = dependencies.UploadService;
            this.itemBuilder = itemBuilder;
        }

        public async Task UploadItemAsync(Action<float> setProgressRate, ItemViewModel[] items, string verifiedToken,
            bool isBeta, CancellationToken cancellationToken)
        {
            uploadService.SetAccessToken(verifiedToken);

            var validItemViews = items.Where(item => item.IsValid).ToArray();
            var length = validItemViews.Length;
            foreach (var (itemView, i) in validItemViews.Select((item, i) => (item, i)))
            {
                cancellationToken.ThrowIfCancellationRequested();

                setProgressRate(i * 100f / length);
                var zipBinary = await itemBuilder.BuildZippedItemBinaryAsync(itemView.UploadingItem);
                await uploadService.UploadItemAsync(zipBinary, isBeta, cancellationToken);
            }
        }
    }
}
