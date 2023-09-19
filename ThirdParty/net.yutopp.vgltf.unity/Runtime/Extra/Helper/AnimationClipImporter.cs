//
// Copyright (c) 2022- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace VGltf.Unity.Ext.Helper
{
    public abstract class AnimationClipImporterHook
    {
        public abstract Task<IndexedResource<AnimationClip>> Import(IImporterContext context, int animIndex, CancellationToken ct);
    }

    public sealed class AnimationClipImporter : ImporterHookBase
    {
        readonly AnimationClipImporterHook[] _hooks;
        readonly List<AnimationClip> _clipRefs;

        public AnimationClipImporter(AnimationClipImporterHook[] hooks, List<AnimationClip> clipRefs)
        {
            _hooks = hooks;
            _clipRefs = clipRefs;
        }

        public override async Task PostHook(IImporterContext context, CancellationToken ct)
        {
            var gltf = context.Container.Gltf;

            for(var i=0; i<gltf.Animations.Count; ++i)
            {
                foreach(var hook in _hooks)
                {
                    var r = await hook.Import(context, i, ct);
                    if (r != null)
                    {
                        // If the clip was processed, ignore hooks subsequently (per clips).
                        _clipRefs.Add(r.Value); // mutate the container

                        await context.TimeSlicer.Slice(ct);

                        break;
                    }
                }
            }
        }
    }
}
