using UnityEngine;

namespace ClusterVR.CreatorKit.Preview.PlayerController
{
    public static class CameraControlSettings
    {
        const string SensitivityKey = "CameraControlSensitivityKey";
        const string InvertVerticalKey = "CameraControlSensitivityKey";
        const string InvertHorizontalKey = "CameraControlSensitivityKey";

        static CameraControlSettings()
        {
            sensitivity = PlayerPrefs.HasKey(SensitivityKey) ? PlayerPrefs.GetFloat(SensitivityKey) : 0.5f;
            invertVertical = PlayerPrefs.HasKey(InvertVerticalKey) && PlayerPrefs.GetInt(InvertVerticalKey) > 0;
            invertHorizontal = PlayerPrefs.HasKey(InvertHorizontalKey) && PlayerPrefs.GetInt(InvertHorizontalKey) > 0;
        }

        static float sensitivity;
        public static float Sensitivity
        {
            get => sensitivity;
            set
            {
                sensitivity = value;
                PlayerPrefs.SetFloat(SensitivityKey, value);
            }
        }

        static bool invertVertical;
        public static bool InvertVertical
        {
            get => invertVertical;
            set
            {
                invertVertical = value;
                PlayerPrefs.SetInt(InvertVerticalKey, value ? 1 : 0);
            }
        }

        static bool invertHorizontal;
        public static bool InvertHorizontal
        {
            get => invertHorizontal;
            set
            {
                invertHorizontal = value;
                PlayerPrefs.SetInt(InvertHorizontalKey, value ? 1 : 0);
            }
        }
    }
}