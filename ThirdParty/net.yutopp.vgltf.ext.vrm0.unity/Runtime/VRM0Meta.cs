//
// Copyright (c) 2021 - yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using UnityEngine;

namespace VGltf.Ext.Vrm0.Unity
{
    public class VRM0Meta : MonoBehaviour
    {
        [SerializeField] public string Title;
        [SerializeField] public string Version;
        [SerializeField] public string Author;
        [SerializeField] public string ContactInformation;
        [SerializeField] public string Reference;

        [SerializeField] public Types.Meta.AllowedUserEnum AllowedUserName;
        [SerializeField] public Types.Meta.UsageLicenseEnum ViolentUsage;

        [SerializeField] public Types.Meta.UsageLicenseEnum SexualUsage;

        [SerializeField] public Types.Meta.UsageLicenseEnum CommercialUsage;


        [SerializeField] public string OtherPermissionUrl;

        [SerializeField] public Types.Meta.LicenseEnum License;

        [SerializeField] public string OtherLicenseUrl;
    }
}
