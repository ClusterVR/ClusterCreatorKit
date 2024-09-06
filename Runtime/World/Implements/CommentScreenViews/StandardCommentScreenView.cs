using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClusterVR.CreatorKit.World.Implements.CommentScreenViews
{
    public sealed class StandardCommentScreenView : MonoBehaviour, ICommentScreenView
    {
        [SerializeField] Transform content;
        [SerializeField] GameObject cellPrefab;

        const int itemCount = 10;

        readonly List<(Comment, StandardCommentScreenViewCell)> cells = new();

        public event Action OnDestroyed;

        void Awake()
        {
            if (content == null)
            {
                return;
            }
            for (var i = 0; i < content.transform.childCount; ++i)
            {
                Destroy(content.transform.GetChild(i).gameObject);
            }
            cells.Clear();
        }

        void Start()
        {
            this.DisableRichText();
            this.DisableImageRayCastTarget();
        }

        public void AddComment(Comment comment)
        {
            if (cellPrefab == null || content == null)
            {
                return;
            }

            var cell = Instantiate(cellPrefab, content).GetComponent<StandardCommentScreenViewCell>();
            cell.transform.SetAsFirstSibling();
            cell.Show(comment);
            cells.Add((comment, cell));

            if (cells.Count > itemCount)
            {
                var overflowCell = cells[0];
                cells.RemoveAt(0);
                Destroy(overflowCell.Item2.gameObject);
            }
        }

        public void RemoveComment(string commentId)
        {
            if (cellPrefab == null || content == null)
            {
                return;
            }

            var removeCellIndex = cells.FindIndex(cell => cell.Item1.Id == commentId);

            if (removeCellIndex >= 0)
            {
                var removeCell = cells[removeCellIndex];
                cells.RemoveAt(removeCellIndex);
                Destroy(removeCell.Item2.gameObject);
            }
        }

        void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }
    }
}
