using System.Collections.Generic;
using System.Linq;
using ClusterVR.CreatorKit.Item;
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

        public static Result Validate(IItem itemTemplate, bool onlyErrors = false)
        {
            return new Result(GetErrors(itemTemplate),
                onlyErrors ? Enumerable.Empty<Result.Factor>() : GetWarnings(itemTemplate));
        }

        static IReadOnlyCollection<Result.Factor> GetErrors(IItem itemTemplate)
        {
            var errors = new List<Result.Factor>();

            AddUnacceptableError<PlayerLocalUI>(itemTemplate, errors);

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
                error.Add(new Result.Factor($"Item Template には {typeof(T).Name} が含まれていてはいけません", objects));
            }
        }

        static void AddWontWorkWarning<T>(IItem itemTemplate, IList<Result.Factor> warnings) where T : Component
        {
            var objects = itemTemplate.gameObject.GetComponentsInChildren<T>(true);
            if (objects.Any())
            {
                warnings.Add(new Result.Factor($"Item Template の {typeof(T).Name} は動作しません", objects));
            }
        }
    }
}
