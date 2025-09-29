using UnityEngine;

namespace ClusterVR.CreatorKit.World.Implements.MainScreenViews
{
    public static class TextureSTCalculator
    {
        public static Vector4 CalcOverlapTextureST(Texture texture, float aspectRatio, bool flipY)
        {
            if (texture == null)
            {
                return new Vector4(1, 1, 0, 0);
            }

            var textureSize = new Vector2(texture.width, texture.height);
            var textureAspectRatio = textureSize.x / textureSize.y;
            Vector2 mainTexScale;

            if (textureAspectRatio > aspectRatio)
            {
                mainTexScale = new Vector2(1, textureAspectRatio / aspectRatio);
            }
            else
            {
                mainTexScale = new Vector2(aspectRatio / textureAspectRatio, 1);
            }

            if (flipY)
            {
                mainTexScale.y *= -1;
            }

            var mainTexPosition = Vector2.one * 0.5f - mainTexScale / 2.0f;
            return new Vector4(mainTexScale.x, mainTexScale.y, mainTexPosition.x, mainTexPosition.y);
        }
    }
}
