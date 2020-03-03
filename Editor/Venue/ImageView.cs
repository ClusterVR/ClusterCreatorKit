using System.IO;
using ClusterVR.CreatorKit.Editor.Core.Venue;
using ClusterVR.CreatorKit.Editor.Core.Venue.Json;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Venue
{
    public class ImageView
    {
        Reactive<Texture2D> reactiveImageTex = new Reactive<Texture2D>();
        Reactive<string> reactiveOverlay = new Reactive<string>();
        public bool IsEmpty { get; private set; }

        public VisualElement CreateView()
        {
            var img = new Label()
            {
                style =
                {
                    unityTextAlign = TextAnchor.MiddleCenter,
                    unityBackgroundScaleMode = ScaleMode.ScaleToFit,
                    // 16:9
                    width = 256,
                    height = 144,
                }
            };
            ReactiveBinder.Bind(reactiveImageTex, imageTex => img.style.backgroundImage = imageTex);
            ReactiveBinder.Bind(reactiveOverlay, overlay => img.text = overlay);
            return img;
        }

        public void SetImageUrl(ThumbnailUrl url)
        {
            IsEmpty = string.IsNullOrEmpty(url.Url);
            if (IsEmpty)
            {
                reactiveImageTex.Val = AssetDatabase.LoadAssetAtPath<Texture2D>("Packages/mu.cluster.cluster-creator-kit/Editor/Texture/require_image.png");
                reactiveOverlay.Val = "";
                return;
            }

            var downloadThumbnailService = new DownloadThumbnailService(
                url, SetSuccess, exc => SetError());

            downloadThumbnailService.Run();

            reactiveOverlay.Val = "…";
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
            reactiveImageTex.Val = newTex;
            reactiveOverlay.Val = "";
        }

        void SetError()
        {
            reactiveImageTex.Val = Texture2D.blackTexture;
            reactiveOverlay.Val = "error";
        }
    }
}
