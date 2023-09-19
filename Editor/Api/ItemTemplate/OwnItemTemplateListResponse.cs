using System;
using System.Collections.Generic;
using ClusterVR.CreatorKit.Editor.Api.Common;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Api.ItemTemplate
{
    [Serializable]
    public sealed class OwnItemTemplateListResponse
    {
        [SerializeField] OwnItemTemplate[] ownItemTemplates;
        [SerializeField] SearchPageData pageData;

        public IReadOnlyList<OwnItemTemplate> OwnItemTemplates => ownItemTemplates;
        public SearchPageData PageData => pageData;
    }
}
