using System;
using System.Threading.Tasks;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Utils.Extensions
{
    public static class TaskExtensions
    {
        public static void Forget(this Task task)
        {
            _ = ForgetInternal(task);
        }

        static async Task ForgetInternal(this Task task)
        {
            try
            {
                await task;
            }
            catch (OperationCanceledException)
            {
                /* do nothing */
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
