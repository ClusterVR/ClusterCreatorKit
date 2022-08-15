using System;
using System.Collections.Generic;
using System.IO;
using ClusterVR.CreatorKit.Editor.Api.RPC;
using ClusterVR.CreatorKit.Editor.Api.Venue;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.View
{
    public sealed class ImageView : IDisposable
    {
        readonly Reactive<Texture2D> reactiveImageTex = new Reactive<Texture2D>();
        readonly Reactive<string> reactiveOverlay = new Reactive<string>();

        readonly List<IDisposable> disposables = new List<IDisposable>();
        public bool IsEmpty { get; private set; }

        public VisualElement CreateView()
        {
            var img = new Label()
            {
                style =
                {
                    unityTextAlign = TextAnchor.MiddleCenter,
                    unityBackgroundScaleMode = ScaleMode.ScaleToFit,
                    width = 256,
                    height = 144
                }
            };
            var imageTexDisposable = ReactiveBinder.Bind(reactiveImageTex, imageTex => img.style.backgroundImage = imageTex);
            var overlayDisposable = ReactiveBinder.Bind(reactiveOverlay, overlay => img.text = overlay);
            disposables.Add(imageTexDisposable);
            disposables.Add(overlayDisposable);
            return img;
        }

        public void SetImageUrl(ThumbnailUrl url)
        {
            IsEmpty = string.IsNullOrEmpty(url.Url);
            if (IsEmpty)
            {
                reactiveImageTex.Val =
                    AssetDatabase.LoadAssetAtPath<Texture2D>(
                        "Packages/mu.cluster.cluster-creator-kit/Editor/Window/Texture/require_image.png");
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

        public void Dispose()
        {
            foreach (var disposable in disposables)
            {
                disposable.Dispose();
            }
        }
    }
}
