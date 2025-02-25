Shader "ClusterVR/InternalSDK/MainScreen"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BackGroundColor ("BackGroundColor", Color) = (0, 0, 0, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _BackGroundColor;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float ep = 0.001;
                fixed4 col = tex2D(_MainTex, clamp(i.uv, ep, 1 - ep));
                if (length(max(abs(i.uv - 0.5) - 0.5 ,0.0)) > ep)
                {
                    clip(_BackGroundColor.a - 0.5);
                    col.rgb = _BackGroundColor.rgb;
                }
                else
                {
                    col.rgb = lerp(float3(1, 1, 1), col.rgb, col.a);
                }

                UNITY_APPLY_FOG(i.fogCoord, col);

                return float4(col.rgb, 1);
            }
            ENDCG
        }
    }
}
