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

        readonly Queue<StandardCommentScreenViewCell> cells = new Queue<StandardCommentScreenViewCell>();

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
            cells.Enqueue(cell);

            if (cells.Count > itemCount)
            {
                Destroy(cells.Dequeue().gameObject);
            }
        }

        void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }
    }
}
