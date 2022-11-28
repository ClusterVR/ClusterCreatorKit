using System;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace ClusterVR.CreatorKit.Item.Implements
{
    [RequireComponent(typeof(Item)), DisallowMultipleComponent]
    public sealed class ScriptableItem : MonoBehaviour, IScriptableItem
    {
        [SerializeField] JavaScriptAsset sourceCodeAsset;
        [SerializeField, TextArea] string sourceCode;

        bool isSourceCodeInitialized;

        Item item;
        public IItem Item
        {
            get
            {
                if (item != null)
                {
                    return item;
                }
                if (this == null)
                {
                    return null;
                }
                return item = GetComponent<Item>();
            }
        }

        public event Action<string> OnSourceCodeChanged;

        public string GetSourceCode()
        {
            if (isSourceCodeInitialized)
            {
                return sourceCode;
            }
            if (sourceCodeAsset != null)
            {
                sourceCode = sourceCodeAsset.text;
            }
            sourceCode ??= "";
            isSourceCodeInitialized = true;
            return sourceCode;
        }

        public void SetSourceCode(string sourceCode)
        {
            Assert.IsTrue(isSourceCodeInitialized);

            if (this.sourceCode == sourceCode)
            {
                return;
            }
            this.sourceCode = sourceCode;
            OnSourceCodeChanged?.Invoke(sourceCode);
        }

        public void Construct(string sourceCode)
        {
            Assert.IsFalse(isSourceCodeInitialized);

            this.sourceCode = sourceCode;
            isSourceCodeInitialized = true;
        }

        public bool IsValid()
        {
            return GetByteCount() <= Constants.Constants.ScriptableItemMaxSourceCodeByteCount;
        }

        public int GetByteCount()
        {
            return Encoding.UTF8.GetByteCount(GetSourceCode());
        }
    }
}
