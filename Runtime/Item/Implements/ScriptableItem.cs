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
        string usingSourceCode;

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

        public string GetSourceCode(bool refresh = false)
        {
            if (refresh || !isSourceCodeInitialized)
            {
                usingSourceCode = (sourceCodeAsset != null ? sourceCodeAsset.text : sourceCode) ?? "";
                isSourceCodeInitialized = true;
            }
            return usingSourceCode;
        }

        public void SetSourceCode(string sourceCode)
        {
            Assert.IsTrue(isSourceCodeInitialized);

            sourceCode ??= "";
            if (usingSourceCode == sourceCode)
            {
                return;
            }
            usingSourceCode = sourceCode;
            OnSourceCodeChanged?.Invoke(usingSourceCode);
        }

        public void Construct(string sourceCode)
        {
            Assert.IsFalse(isSourceCodeInitialized);

            this.sourceCode = usingSourceCode = sourceCode ?? "";
            isSourceCodeInitialized = true;
        }

        public bool IsValid(bool refresh = false)
        {
            return GetByteCount(refresh) <= Constants.Constants.ScriptableItemMaxSourceCodeByteCount;
        }

        public int GetByteCount(bool refresh)
        {
            return Encoding.UTF8.GetByteCount(GetSourceCode(refresh));
        }
    }
}
