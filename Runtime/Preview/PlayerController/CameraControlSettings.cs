using UnityEngine;

namespace ClusterVR.CreatorKit.Preview.PlayerController
{
    public static class CameraControlSettings
    {
        const string SensitivityKey = "CameraControlSensitivityKey";
        const string InvertVerticalKey = "CameraControlSensitivityKey";
        const string InvertHorizontalKey = "CameraControlSensitivityKey";
        const string StandingEyeHeightKey = "StandingEyeHeightKey";
        const string SittingEyeHeightKey = "SittingEyeHeightKey";

        static CameraControlSettings()
        {
            sensitivity = PlayerPrefs.HasKey(SensitivityKey) ? PlayerPrefs.GetFloat(SensitivityKey) : 0.5f;
            invertVertical = PlayerPrefs.HasKey(InvertVerticalKey) && PlayerPrefs.GetInt(InvertVerticalKey) > 0;
            invertHorizontal = PlayerPrefs.HasKey(InvertHorizontalKey) && PlayerPrefs.GetInt(InvertHorizontalKey) > 0;
            standingEyeHeight = PlayerPrefs.HasKey(StandingEyeHeightKey) ? PlayerPrefs.GetFloat(StandingEyeHeightKey) : 1.5f;
            sittingEyeHeight = PlayerPrefs.HasKey(SittingEyeHeightKey) ? PlayerPrefs.GetFloat(SittingEyeHeightKey) : 0.65f;
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

        static float standingEyeHeight;

        public static float StandingEyeHeight
        {
            get => standingEyeHeight;
            set
            {
                standingEyeHeight = Mathf.Max(0f, value);
                PlayerPrefs.SetFloat(StandingEyeHeightKey, standingEyeHeight);
                if (sittingEyeHeight > standingEyeHeight)
                {
                    SittingEyeHeight = standingEyeHeight;
                }
            }
        }

        static float sittingEyeHeight;

        public static float SittingEyeHeight
        {
            get => sittingEyeHeight;
            set
            {
                sittingEyeHeight = Mathf.Clamp(value, 0f, standingEyeHeight);
                PlayerPrefs.SetFloat(SittingEyeHeightKey, sittingEyeHeight);
            }
        }
    }
}
