//
// Copyright (c) 2019- yutopp (yutopp@gmail.com)
//
// Distributed under the Boost Software License, Version 1.0. (See accompanying
// file LICENSE_1_0.txt or copy at  https://www.boost.org/LICENSE_1_0.txt)
//

using System.Collections.Generic;

namespace VGltf.Unity
{
    public abstract class ImporterRefHookable<T>
    {
        protected List<T> Hooks = new List<T>();

        public ImporterRefHookable()
        {
        }

        public void AddHook(T hook)
        {
            Hooks.Add(hook);
        }

        public abstract IImporterContext Context { get; }
    }
}
