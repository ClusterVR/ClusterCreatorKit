Shader "Hidden/VGltfUnity/UnityGlossMapToGltfRoughnessMap"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Metallic ("Metallic", Float) = 0.0
        _Smoothness ("Smoothness", Float) = 0.0
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            fixed _Metallic;
            fixed _Smoothness;

            fixed smoothness_to_roughness(float glossiness)
            {
                return pow(1.0f - glossiness, 2);
            }

            // https://www.khronos.org/registry/glTF/specs/2.0/glTF-2.0.html#metallic-roughness-material
            // glTF (linear)
            //  R: [unused]
            //  G: roughness
            //  B: metalness
            //  A: [unused]

            // https://docs.unity3d.com/Manual/StandardShaderMaterialParameterMetallic.html
            // Unity (linear)
            //  R: Metalic
            //  G: [unused]
            //  B: [unused]
            //  A: Smoothness (Gloss)

            // Unity -> glTF
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 c = tex2D(_MainTex, i.uv);
                return fixed4(0.0f, smoothness_to_roughness(c.a * _Smoothness), c.r * _Metallic, 1.0f);
            }

            ENDCG
        }
    }
}
