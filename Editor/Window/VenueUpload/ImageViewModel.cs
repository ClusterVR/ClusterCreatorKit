using System.IO;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using ClusterVR.CreatorKit.Editor.Utils;
using UnityEditor;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Window.VenueUpload
{
    public sealed class ImageViewModel
    {
        readonly Reactive<Texture2D> imageTex = new();
        readonly Reactive<string> overlay = new();

        internal IReadOnlyReactive<Texture2D> ImageTex => imageTex;
        internal IReadOnlyReactive<string> Overlay => overlay;

        public bool IsEmpty { get; private set; }

        public void SetImageUrl(ThumbnailUrl url)
        {
            IsEmpty = string.IsNullOrEmpty(url.Url);
            if (IsEmpty)
            {
                imageTex.Val =
                    AssetDatabase.LoadAssetAtPath<Texture2D>(
                        "Packages/mu.cluster.cluster-creator-kit/Editor/Window/Texture/require_image.png");
                overlay.Val = "";
                return;
            }

            var downloadThumbnailService = new DownloadThumbnailService(
                url, SetSuccess, exc => SetError());

            downloadThumbnailService.Run();

            overlay.Val = "…";
        }

        public void SetImagePath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                SetError();
                return;
            }

            var tex = new Texture2D(1, 1);
            tex.LoadImage(File.ReadAllBytes(path));
            tex.filterMode = FilterMode.Point;
            SetSuccess(tex);
        }

        void SetSuccess(Texture2D newTex)
        {
            imageTex.Val = newTex;
            overlay.Val = "";
        }

        void SetError()
        {
            imageTex.Val = Texture2D.blackTexture;
            overlay.Val = "error";
        }
    }
}
