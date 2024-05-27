using System;
using ClusterVR.CreatorKit.Extensions;
using ClusterVR.CreatorKit.Translation;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ClusterVR.CreatorKit.World.Implements.TextView
{
    [ExecuteAlways, DisallowMultipleComponent]
    public sealed class TextView : MonoBehaviour, ITextView
    {
        const float FontSizeRate = 100f;
        const int MinFontSize = 50;
        const int MaxFontSize = 300;
        const float TextScale = 18.5f;
        const HideFlags RendererHideFlags = HideFlags.HideInInspector | HideFlags.DontSave;

        [SerializeField, HideInInspector] TextMesh textMesh;
        [SerializeField, Multiline] string text;
        [SerializeField, Range(0f, 5f)] float size = 0.1f;
        [SerializeField] TextAnchor textAnchor;
        [SerializeField] TextAlignment textAlignment;
        [SerializeField] Color color = Color.white;

        MeshRenderer meshRenderer;
        bool isFontSetAndShaderSet;
        Font font;
        Shader shader;
        Material fontMaterial;

        public string Text => text;
        public float Size => size;
        public TextAnchor TextAnchor => textAnchor;
        public TextAlignment TextAlignment => textAlignment;
        public Color Color => color;

        public void SetFontAndShader(Font font, Shader shader)
        {
            isFontSetAndShaderSet = true;
            this.font = font;
            this.shader = shader;
            UpdateFontAndShader(font, shader);
        }

        void UpdateFontAndShader(Font font, Shader shader)
        {
            if (textMesh != null)
            {
                textMesh.font = font;
            }
            if (meshRenderer != null)
            {
                var material = font == null ? null : font.material;
                if (material != null)
                {
                    if (fontMaterial != null)
                    {
                        DestroySafe(fontMaterial);
                    }
                    fontMaterial = Instantiate(material);
                    fontMaterial.shader = shader;
                    if (Application.isPlaying)
                    {
                        RendererMaterialUtility.SetSharedMaterials(meshRenderer, new[] { fontMaterial });
                    }
                    else
                    {
                        meshRenderer.sharedMaterial = fontMaterial;
                    }
                }
            }
        }

        public void SetText(string text)
        {
            this.text = text;
        }

        public void SetSize(float size)
        {
            this.size = size;
        }

        public void SetTextAnchor(TextAnchor textAnchor)
        {
            this.textAnchor = textAnchor;
        }

        public void SetTextAlignment(TextAlignment textAlignment)
        {
            this.textAlignment = textAlignment;
        }

        public void SetColor(Color color)
        {
            this.color = color;
        }

        void Awake()
        {
#if UNITY_EDITOR
            if (!ValidateInternal())
            {
                return;
            }
#endif
            Initialize();
#if UNITY_EDITOR && !CLUSTER_CREATOR_KIT_DISABLE_PREVIEW
            var font = UnityEditor.AssetDatabase.LoadAssetAtPath<Font>("Packages/mu.cluster.cluster-creator-kit/PackageResources/Fonts/NotoSansCJKjp-Regular.otf");
            var shader = UnityEditor.AssetDatabase.LoadAssetAtPath<Shader>("Packages/mu.cluster.cluster-creator-kit/PackageResources/Shaders/ZTestText.shader");
            SetFontAndShader(font, shader);
#endif
        }

        void Initialize()
        {
            textMesh = GetOrAddComponent<TextMesh>();
            textMesh.hideFlags = RendererHideFlags;
            textMesh.richText = false;
            meshRenderer = GetComponent<MeshRenderer>();
            meshRenderer.hideFlags = RendererHideFlags;
            meshRenderer.enabled = enabled;
            if (isFontSetAndShaderSet)
            {
                UpdateFontAndShader(font, shader);
            }
            UpdateRenderer();
        }

        T GetOrAddComponent<T>() where T : Component
        {
            var component = GetComponent<T>();
            if (component == null)
            {
                component = gameObject.AddComponent<T>();
            }
            return component;
        }

        void OnEnable()
        {
            if (meshRenderer != null)
            {
                meshRenderer.enabled = true;
            }
        }

        void Update()
        {
            UpdateRenderer();
        }

        void OnDisable()
        {
            if (meshRenderer != null)
            {
                meshRenderer.enabled = false;
            }
        }

        void UpdateRenderer()
        {
            textMesh.text = text;
            var fontSize = Mathf.Clamp((int) (size * FontSizeRate), MinFontSize, MaxFontSize);
            textMesh.fontSize = fontSize;
            textMesh.characterSize = size * TextScale / fontSize;
            textMesh.anchor = textAnchor;
            textMesh.alignment = textAlignment;
            textMesh.color = color;
        }

        void OnDestroy()
        {
            if (Application.isPlaying)
            {
                Destroy(textMesh);
                Destroy(meshRenderer);
            }
            else
            {
                if (meshRenderer != null)
                {
                    meshRenderer.enabled = false;
                }
            }

            if (fontMaterial != null)
            {
                DestroySafe(fontMaterial);
            }
        }

        public void DestroyRenderers()
        {
            DestroyImmediate(textMesh);
            DestroyImmediate(meshRenderer);
        }

        void DestroySafe(Object obj)
        {
            if (Application.isPlaying)
            {
                Destroy(obj);
            }
            else
            {
                DestroyImmediate(obj);
            }
        }

#if UNITY_EDITOR
        void Reset()
        {
            ValidateInternal();
        }

        bool ValidateInternal()
        {
            if (textMesh != null && textMesh.gameObject == gameObject)
            {
                return true;
            }

            return ValidateToAdd();
        }

        bool ValidateToAdd()
        {
            if (CanAdd())
            {
                return true;
            }
            else
            {
                var message = TranslationUtility.GetMessage(TranslationTable.cck_textview_add_to_renderer_object, nameof(TextView), nameof(Renderer));
                if (Application.isPlaying)
                {
                    Debug.LogError(message);
                }
                else
                {
                    UnityEditor.EditorUtility.DisplayDialog(TranslationUtility.GetMessage(TranslationTable.cck_textview_addition_failed, nameof(TextView)), message, TranslationTable.cck_cancel);
                }

                DestroyImmediate(this);
                return false;
            }
        }

        bool CanAdd()
        {
            var textMesh = GetComponent<TextMesh>();
            var renderer = GetComponent<Renderer>();
            return textMesh == null && renderer == null || textMesh != null && textMesh.hideFlags == RendererHideFlags && renderer != null && renderer.hideFlags == RendererHideFlags;
        }

        void OnValidate()
        {
            if (textMesh == null || textMesh.gameObject != gameObject)
            {
                textMesh = GetComponent<TextMesh>();
            }
        }
#endif
    }
}
