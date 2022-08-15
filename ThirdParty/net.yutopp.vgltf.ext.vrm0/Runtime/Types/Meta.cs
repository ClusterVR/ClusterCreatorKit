//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using VJson;
using VJson.Schema;

namespace VGltf.Ext.Vrm0.Types
{
    [JsonSchema(Title = "vrm.meta",
                Id = "vrm.meta.schema.json"/* TODO: Fix usage of Id */)]
    public class Meta
    {
        [JsonField(Name = "title")]
        [JsonSchema(Description = "Title of VRM model.")]
        public string Title;

        [JsonField(Name = "version")]
        [JsonSchema(Description = "Version of VRM model.")]
        public string Version;

        [JsonField(Name = "author")]
        [JsonSchema(Description = "Author of VRM model.")]
        public string Author;

        [JsonField(Name = "contactInformation")]
        [JsonSchema(Description = "Contact Information of VRM model author.")]
        public string ContactInformation;

        [JsonField(Name = "reference")]
        [JsonSchema(Description = "Reference of VRM model.")]
        public string Reference;

        // TODO: Make this value to an optional value
        // When the value is -1, it means that texture is not specified.
        [JsonField(Name = "texture"), JsonFieldIgnorable(WhenValueIs = -1)]
        [JsonSchema(Minimum = 0,
                    Description = "Thumbnail of VRM model. It is an index of glTF textures.")]
        public int Texture = -1;

        [JsonField(Name = "allowedUserName")]
        [JsonSchema(Description = "A person who can perform with this avatar."), JsonSchemaRequired]
        public AllowedUserEnum AllowedUserName;

        [JsonField(Name = "violentUssageName"), JsonSchemaRequired] // TODO: fix typo in future spec
        [JsonSchema(Description = "Permission to perform violent acts with this avatar.")]
        public UsageLicenseEnum ViolentUsage;

        [JsonField(Name = "sexualUssageName"), JsonSchemaRequired] // TODO: fix typo in future spec
        [JsonSchema(Description = "Permission to perform sexual acts with this avatar.")]
        public UsageLicenseEnum SexualUsage;

        [JsonField(Name = "commercialUssageName"), JsonSchemaRequired] // TODO: fix typo in future spec
        [JsonSchema(Description = "For commercial use")]
        public UsageLicenseEnum CommercialUsage;

        [JsonField(Name = "otherPermissionUrl"), JsonFieldIgnorable]
        [JsonSchema(Description = "If there are any conditions not mentioned above, put the URL link of the license document here.")]
        public string OtherPermissionUrl;

        [JsonField(Name = "licenseName"), JsonSchemaRequired]
        [JsonSchema(Description = "License type.")]
        public LicenseEnum License;

        [JsonField(Name = "otherLicenseUrl"), JsonFieldIgnorable]
        [JsonSchema(Description = "If “Other” is selected, put the URL link of the license document here.")]
        public string OtherLicenseUrl;

        //
        [Json(EnumConversion = EnumConversionType.AsString)]
        public enum AllowedUserEnum
        {
            [JsonField] OnlyAuthor,
            [JsonField] ExplicitlyLicensedPerson,
            [JsonField] Everyone,
        }

        [Json(EnumConversion = EnumConversionType.AsString)]
        public enum UsageLicenseEnum
        {
            [JsonField] Disallow,
            [JsonField] Allow,
        }

        [Json(EnumConversion = EnumConversionType.AsString)]
        public enum LicenseEnum
        {
            [JsonField] Redistribution_Prohibited,
            [JsonField] CC0,
            [JsonField] CC_BY,
            [JsonField] CC_BY_NC,
            [JsonField] CC_BY_SA,
            [JsonField] CC_BY_NC_SA,
            [JsonField] CC_BY_ND,
            [JsonField] CC_BY_NC_ND,
            [JsonField] Other
        }
    }
}
