using System;
using ClusterVR.CreatorKit.Editor.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace ClusterVR.CreatorKit.Editor.Window.VenueUpload
{
    public sealed class ImageView : VisualElement
    {
        readonly Label img;

        public ImageView()
        {
            img = new Label
            {
                style =
                {
                    unityTextAlign = TextAnchor.MiddleCenter,
                    unityBackgroundScaleMode = ScaleMode.ScaleToFit,
                    width = 256,
                    height = 144
                }
            };
            hierarchy.Add(img);
        }

        public IDisposable Bind(ImageViewModel viewModel)
        {
            return Disposable.Create(
                ReactiveBinder.Bind(viewModel.ImageTex, imageTex => img.style.backgroundImage = imageTex),
                ReactiveBinder.Bind(viewModel.Overlay, overlay => img.text = overlay)
            );
        }
    }
}
