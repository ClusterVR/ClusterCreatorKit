using ClusterVR.CreatorKit.Validator;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ClusterVR.CreatorKit.World
{
    [RequireComponent(typeof(Collider))]
    [RequireIsTriggerSettings]
    public sealed class SubScene : MonoBehaviour, ISubScene
    {
        [SerializeField, HideInInspector] string sceneName;

#if UNITY_EDITOR
        [SerializeField] SceneAsset scene;
#endif

        public string SceneName
        {
            get => sceneName;
            set => sceneName = value;
        }

        public event OnStayAffectableAreaEventHandler OnStayAffectableAreaEvent;
        public event OnLeaveAffectableAreaEventHandler OnLeaveAffectableAreaEvent;

#if UNITY_EDITOR
        SceneAsset ISubScene.UnityScene => scene;
#endif

        void OnTriggerStay(Collider other)
        {
            OnStayAffectableAreaEvent?.Invoke(new OnStayAffectableAreaEventArgs(other.gameObject, (ISubScene) this));
        }

        void OnTriggerExit(Collider other)
        {
            OnLeaveAffectableAreaEvent?.Invoke(new OnLeaveAffectableAreaEventArgs(other.gameObject, (ISubScene) this));
        }

        void OnValidate()
        {
            foreach (var collier in GetComponentsInChildren<Collider>(true))
            {
                collier.isTrigger = true;
            }
        }
    }
}
