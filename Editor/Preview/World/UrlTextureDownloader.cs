using System;
using System.Collections;
using System.Collections.Generic;
using ClusterVR.CreatorKit.Preview.Common;
using ClusterVR.CreatorKit.World;
using UnityEngine;
using UnityEngine.Networking;

namespace ClusterVR.CreatorKit.Editor.Preview.World
{
    public static class UrlTextureDownloader
    {
        public static void UrlTextureDownload(IEnumerable<IUrlTexture> urlTextures)
        {
            foreach (var urlTexture in urlTextures)
            {
                CoroutineGenerator.StartStaticCoroutine(TextureDownload(urlTexture));
            }
        }

        static IEnumerator TextureDownload(IUrlTexture urlTexture)
        {
            byte[] imageBlob = null;
            var gameObject = urlTexture.GameObject;
            using (var req = UnityWebRequest.Get(urlTexture.Url))
            {
                req.timeout = 10;
                yield return req.SendWebRequest();
                imageBlob = req.downloadHandler.data;
            }
            if (imageBlob != null && gameObject != null)
            {
                var texture = new Texture2D(1, 1, TextureFormat.RGBA32, false, false);
                texture.LoadImage(imageBlob);
                urlTexture.SetTexture(texture);
            }
        }
    }
}
