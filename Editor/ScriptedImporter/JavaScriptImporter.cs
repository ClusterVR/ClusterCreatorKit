using UnityEngine;
using UnityEditor.AssetImporters;
using System.IO;
using ClusterVR.CreatorKit.Item.Implements;

namespace ClusterVR.CreatorKit.Editor.ScriptedImporter
{
    [ScriptedImporter(1, "js")]
    public sealed class JavaScriptImporter : UnityEditor.AssetImporters.ScriptedImporter
    {
        public override void OnImportAsset(AssetImportContext ctx)
        {
            var text = File.ReadAllText(ctx.assetPath);
            var asset = ScriptableObject.CreateInstance<JavaScriptAsset>();
            asset.text = text;
            ctx.AddObjectToAsset("main obj", asset);
            ctx.SetMainObject(asset);
        }
    }
}
