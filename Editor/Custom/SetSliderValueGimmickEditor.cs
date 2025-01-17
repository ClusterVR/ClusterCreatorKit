﻿using ClusterVR.CreatorKit.Gimmick.Implements;
using UnityEditor;

namespace ClusterVR.CreatorKit.Editor.Custom
{
    [CustomEditor(typeof(SetSliderValueGimmick), isFallback = true), CanEditMultipleObjects]
    public class SetSliderValueGimmickEditor : VisualElementEditor
    {
    }
}
