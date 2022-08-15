Shader "Hidden/VGltfUnity/UnityOcclusionTexToGltf"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
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

            // https://github.com/KhronosGroup/glTF/issues/1593
            // glTF (sRGB)
            //  R: AO is always sampled from the red channel
            //  G: [unused]
            //  B: [unused]
            //  A: [ignored]

            // https://catlikecoding.com/unity/tutorials/rendering/part-10/
            // Unity (sRGB)
            //  R: [unused]
            //  G: Unity's standard shader uses the G color channel of the occlusion map
            //  B: [unused]
            //  A: [ignored]

            // Unity -> glTF
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 c = tex2D(_MainTex, i.uv);
                return fixed4(c.g, 0.0f, 0.0f, 1.0f);
            }
            ENDCG
        }
    }
}
