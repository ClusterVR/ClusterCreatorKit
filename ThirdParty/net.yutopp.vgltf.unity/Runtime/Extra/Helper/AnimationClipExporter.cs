//
// Copyright (c) 2022- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VGltf.Types.Extensions;

namespace VGltf.Unity.Ext.Helper
{
    public abstract class AnimationClipExporterHook
    {
        public abstract IndexedResource<AnimationClip> Export(IExporterContext context, AnimationClip clip);
    }

    public sealed class AnimationClipExporter : ExporterHookBase
    {
        readonly AnimationClip[] _clips;
        readonly AnimationClipExporterHook[] _hooks;

        public AnimationClipExporter(AnimationClip clip, AnimationClipExporterHook[] hooks)
        : this(new AnimationClip[] { clip }, hooks)
        {
        }

        public AnimationClipExporter(AnimationClip[] clips, AnimationClipExporterHook[] hooks)
        {
            _clips = clips;
            _hooks = hooks;
        }

        public override void PostHook(Exporter exporter, GameObject go)
        {
            foreach (var clip in _clips)
            {
                foreach (var hook in _hooks)
                {
                    var r = hook.Export(exporter.Context, clip);
                    if (r != null)
                    {
                        // If the clip was processed, ignore hooks subsequently (per clips).
                        break;
                    }
                }
            }
        }
    }
}
