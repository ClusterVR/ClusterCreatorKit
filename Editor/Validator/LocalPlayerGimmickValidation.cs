﻿using System.Linq;
using System.Reflection;
using ClusterVR.CreatorKit.Gimmick;
using ClusterVR.CreatorKit.Gimmick.Implements;
using ClusterVR.CreatorKit.Translation;
using ClusterVR.CreatorKit.World;
using ClusterVR.CreatorKit.World.Implements.PlayerLocalUI;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Validator
{
    public static class LocalPlayerGimmickValidation
    {
        public static bool IsValid(IGlobalGimmick globalGimmick)
        {
            if (globalGimmick.Target != GimmickTarget.Player)
            {
                return true;
            }
            return IsLocalizableConditionSatisfied((Component) globalGimmick);
        }

        public static bool IsValid(GimmickTarget gimmickTarget, Component component)
        {
            if (gimmickTarget != GimmickTarget.Player)
            {
                return true;
            }
            return IsLocalizableConditionSatisfied(component);
        }

        public static bool IsLocalizable(Component component)
        {
            return GetLocalizableAttribute(component) != null;
        }

        static bool IsLocalizableConditionSatisfied(Component component)
        {
            var localizableAttribute = GetLocalizableAttribute(component);
            if (localizableAttribute == null)
            {
                return false;
            }
            if (localizableAttribute.LocalizableCondition ==
                LocalizableGlobalGimmickAttribute.Condition.Always)
            {
                return true;
            }
            return IsInPlayerLocal(component);
        }

        static LocalizableGlobalGimmickAttribute GetLocalizableAttribute(Component component)
        {
            return component.GetType()
                .GetCustomAttributes(typeof(LocalizableGlobalGimmickAttribute))
                .FirstOrDefault() as LocalizableGlobalGimmickAttribute;
        }

        static bool IsInPlayerLocal(Component component)
        {
            return component.GetComponentsInParent<IPlayerLocal>(true).Any();
        }

        public static readonly string ErrorMessage =
            TranslationUtility.GetMessage(TranslationTable.cck_gimmicktarget_playerlocalui_only, nameof(GimmickTarget), FormatTarget(GimmickTarget.Player), nameof(PlayerLocalUI));

        static string FormatTarget(GimmickTarget target)
        {
            switch (target)
            {
                case GimmickTarget.Player:
                    return "LocalPlayer";
                default:
                    return target.ToString();
            }
        }
    }
}
