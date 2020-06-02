using System;
using System.Collections.Generic;
using UnityEngine;

namespace ClusterVR.CreatorKit.World.Implements.CommentScreenViews
{
    public class StandardCommentScreenView : MonoBehaviour, ICommentScreenView
    {
        [SerializeField] Transform content;
        [SerializeField] GameObject cellPrefab;

        const int itemCount = 10;

        readonly Queue<StandardCommentScreenViewCell> cells = new Queue<StandardCommentScreenViewCell>();

        public event Action OnDestroyed;

        void Awake()
        {
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
            var cell = Instantiate(cellPrefab, content).GetComponent<StandardCommentScreenViewCell>();
            cell.transform.SetAsFirstSibling();
            cell.Show(comment);
            cells.Enqueue(cell);

            if (cells.Count > itemCount) Destroy(cells.Dequeue().gameObject);
        }

        void OnDestroy()
        {
            OnDestroyed?.Invoke();
        }
    }
}
