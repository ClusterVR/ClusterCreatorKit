//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System.IO;
using VJson;
using VJson.Schema;

// Reference: https://github.com/KhronosGroup/glTF/blob/master/specification/2.0/schema/*
namespace VGltf.Types
{
    [JsonSchema(Id = "image.schema.json")]
    // TODO: oneOf required: Uri OR bufferView
    public sealed class Image : GltfChildOfRootProperty
    {
        public const string MimeTypeImageJpeg = "image/jpeg";
        public const string MimeTypeImagePng = "image/png";

        [JsonField(Name = "uri"), JsonFieldIgnorable]
        // TODO: "format": "uriref"
        public string Uri;

        [JsonField(Name = "mimeType"), JsonFieldIgnorable(WhenValueIs = "")]
        public string MimeType = "";

        [JsonField(Name = "bufferView"), JsonFieldIgnorable]
        [JsonSchemaDependencies("mimeType"), JsonSchemaRef(typeof(GltfID))]
        public int? BufferView;
    }

    public static class ImageExtensions
    {
        public static string GetExtension(this Image img)
        {
            switch (img.MimeType)
            {
                case Image.MimeTypeImageJpeg:
                    return ".jpg";
                case Image.MimeTypeImagePng:
                    return ".png";
            }

            if (img.Uri.StartsWith("data:image/jpeg;"))
            {
                return ".jpg";
            }
            else if (img.Uri.StartsWith("data:image/png;"))
            {
                return ".png";
            }
            else
            {
                return Path.GetExtension(img.Uri).ToLower();
            }
        }
    }
}
