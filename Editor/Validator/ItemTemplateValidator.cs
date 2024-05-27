using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Item;
using ClusterVR.CreatorKit.Translation;
using ClusterVR.CreatorKit.World.Implements.CommentScreenViews;
using ClusterVR.CreatorKit.World.Implements.DespawnHeights;
using ClusterVR.CreatorKit.World.Implements.MainScreenViews;
using ClusterVR.CreatorKit.World.Implements.PlayerLocalUI;
using ClusterVR.CreatorKit.World.Implements.RankingScreenViews;
using ClusterVR.CreatorKit.World.Implements.SpawnPoints;
using ClusterVR.CreatorKit.World.Implements.WarpPortal;
using ClusterVR.CreatorKit.World.Implements.WorldGate;
using UnityEngine;

namespace ClusterVR.CreatorKit.Editor.Validator
{
    public static class ItemTemplateValidator
    {
        public readonly struct Result
        {
            public readonly struct Factor
            {
                public string Message { get; }
                public IReadOnlyCollection<Object> Contexts { get; }

                public Factor(string message, IReadOnlyCollection<Object> contexts)
                {
                    Contexts = contexts;
                    Message = message;
                }
            }

            public IEnumerable<Factor> Errors { get; }
            public IEnumerable<Factor> Warnings { get; }

            public Result(IEnumerable<Factor> errors, IEnumerable<Factor> warnings)
            {
                Errors = errors;
                Warnings = warnings;
            }
        }

        public static Result Validate(bool isBeta, IItem itemTemplate, bool onlyErrors = false)
        {
            return new Result(GetErrors(isBeta, itemTemplate),
                onlyErrors ? Enumerable.Empty<Result.Factor>() : GetWarnings(itemTemplate));
        }

        static IReadOnlyCollection<Result.Factor> GetErrors(bool isBeta, IItem itemTemplate)
        {
            var errors = new List<Result.Factor>();

            AddUnacceptableError<PlayerLocalUI>(itemTemplate, errors);

            AddMaterialError(isBeta, itemTemplate, errors);

            return errors;
        }

        static IReadOnlyCollection<Result.Factor> GetWarnings(IItem itemTemplate)
        {
            var warnings = new List<Result.Factor>();

            AddWontWorkWarning<SpawnPoint>(itemTemplate, warnings);
            AddWontWorkWarning<DespawnHeight>(itemTemplate, warnings);
            AddWontWorkWarning<StandardMainScreenView>(itemTemplate, warnings);
            AddWontWorkWarning<StandardCommentScreenView>(itemTemplate, warnings);
            AddWontWorkWarning<RankingScreenView>(itemTemplate, warnings);
            AddWontWorkWarning<PlayerEnterWarpPortal>(itemTemplate, warnings);
            AddWontWorkWarning<WorldGate>(itemTemplate, warnings);

            return warnings;
        }

        static void AddUnacceptableError<T>(IItem itemTemplate, IList<Result.Factor> error) where T : Component
        {
            var objects = itemTemplate.gameObject.GetComponentsInChildren<T>(true);
            if (objects.Any())
            {
                error.Add(new Result.Factor(TranslationUtility.GetMessage(TranslationTable.cck_item_template_contains_type, typeof(T).Name), objects));
            }
        }

        static void AddWontWorkWarning<T>(IItem itemTemplate, IList<Result.Factor> warnings) where T : Component
        {
            var objects = itemTemplate.gameObject.GetComponentsInChildren<T>(true);
            if (objects.Any())
            {
                warnings.Add(new Result.Factor(TranslationUtility.GetMessage(TranslationTable.cck_item_template_type_not_working, typeof(T).Name), objects));
            }
        }

        static void AddMaterialError(bool isBeta, IItem itemTemplate, List<Result.Factor> errors)
        {
            var gameObject = itemTemplate.gameObject;
            var itemMaterialSetList = gameObject.GetComponent<IItemMaterialSetList>();
            if (itemMaterialSetList == null)
            {
                return;
            }

            var errorMessages = ItemMaterialSetListValidator.Validate(isBeta, gameObject, itemMaterialSetList);
            errors.AddRange(errorMessages.Select(msg => new Result.Factor(msg, new[] { gameObject })));
        }
    }
}
