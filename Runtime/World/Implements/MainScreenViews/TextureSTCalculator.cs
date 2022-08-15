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

            var mainTexPosition = Vector2.zero;
            var mainTexScale = Vector2.one;

            var textureSize = new Vector2(texture.width, texture.height);

            mainTexScale.x = textureSize.y / textureSize.x * aspectRatio;
            if (flipY)
            {
                mainTexScale /= mainTexScale.x;
                mainTexScale.y *= -1;
            }
            else
            {
                mainTexScale /= Mathf.Min(mainTexScale.x, mainTexScale.y);
            }

            mainTexPosition += Vector2.one * 0.5f;
            mainTexPosition -= mainTexScale / 2.0f;

            return new Vector4(mainTexScale.x, mainTexScale.y, mainTexPosition.x, mainTexPosition.y);
        }
    }
}
