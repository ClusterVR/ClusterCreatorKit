using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ClusterVR.CreatorKit.Editor.EditorEvents
{
    public sealed class WorldUploadStartEventData
    {
        /// <summary>
        /// ワールドのアップロード対象となっているシーンを取得します。
        /// </summary>
        public Scene Scene { get; }

        public WorldUploadStartEventData(Scene scene)
        {
            Scene = scene;
        }
    }

    public sealed class WorldUploadEndEventData
    {
        /// <summary>
        /// ワールドのアップロードに成功していれば <c>true</c> を、そうでなければ <c>false</c> を取得します。
        /// </summary>
        public bool Success { get; }

        public WorldUploadEndEventData(bool success)
        {
            Success = success;
        }
    }

    public static class WorldUploadEvents
    {
        static readonly List<(int priority, Func<WorldUploadStartEventData, bool> callback)> UploadStartedEventHandlers = new();
        static readonly List<(int priority, Action<WorldUploadEndEventData> callback)> UploadEndedEventHandlers = new();

        /// <summary>
        /// ワールドのアップロード前に呼ばれるコールバック関数を登録します。
        /// 複数のコールバック関数が登録されたとき、priority引数が小さいものから順にコールバック関数が呼ばれます。
        /// 同一のpriorityを持つコールバック関数を複数登録した場合、それらの呼び出し順序は保証されません。
        ///
        /// コールバック関数の戻り値ではワールドのアップロードを続行出来るかどうかを指定します。
        /// アップロードを続行してよい場合はtrue、アップロードを中止すべき場合はfalseを返すように実装してください。
        /// また、コールバック関数が例外をスローした場合もアップロードは中止されます。
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="priority"></param>
        public static void RegisterOnWorldUploadStart(Func<WorldUploadStartEventData, bool> callback, int priority = 0)
        {
            UploadStartedEventHandlers.Add((priority, callback));
        }

        /// <summary>
        /// ワールドのアップロード後に呼ばれるコールバック関数を登録します。
        /// コールバック関数はアップロードが成功、失敗した場合のいずれも呼ばれます。
        /// 複数のコールバック関数が登録されたとき、priority引数が小さいものから順にコールバック関数が呼ばれます。
        /// 同一のpriorityを持つコールバック関数を複数登録した場合、それらの呼び出し順序は保証されません。
        ///
        /// この方法で登録したコールバック関数は、個別のコールバック関数が例外をスローした場合も含め、全て呼び出されます。
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="priority"></param>
        public static void RegisterOnWorldUploadEnd(Action<WorldUploadEndEventData> callback, int priority = 0)
        {
            UploadEndedEventHandlers.Add((priority, callback));
        }

        internal static bool InvokeOnWorldUploadStart(Scene scene)
        {
            var data = new WorldUploadStartEventData(scene);
            try
            {
                foreach (var item in UploadStartedEventHandlers.OrderBy(value => value.priority))
                {
                    var result = item.callback?.Invoke(data);
                    if (result == false)
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                return false;
            }

            return true;
        }

        internal static void InvokeOnWorldUploadEnd(bool success)
        {
            var data = new WorldUploadEndEventData(success);
            foreach (var item in UploadEndedEventHandlers.OrderBy(value => value.priority))
            {
                try
                {
                    item.callback?.Invoke(data);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }
    }
}
