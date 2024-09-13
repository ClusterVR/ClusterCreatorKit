Shader "ClusterCreatorKit/SilhouetteWall"
{
    Properties
    {
        _BaseColor("Dot Color", Color) = (0.482,0.478,0.655,1)
        _DotColor("Base Color", Color) = (0.580,0.580,0.733,1)
        _DotScale("Dot Scale", Float) = 140.0
        _DotSize("Dot Size", Float) = 0.3
    }
    SubShader
    {
        Tags
        {
            "RenderType"="Transparent" "Queue"="Transparent-1"
        }
        LOD 100

        Pass
        {
            ZTest LEqual
            Stencil
            {
                Ref 2
                Comp Equal
                Pass Keep
                Fail Keep
                ZFail Keep
            }
            ZWrite Off

            Blend SrcAlpha OneMinusSrcAlpha
            Cull Back

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            #include "UnityCG.cginc"
            fixed4 _BaseColor;
            fixed4 _DotColor;
            float _DotScale;
            float _DotSize;

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 screenPos : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                float2 screenUV = i.screenPos.xy / i.screenPos.w;
                float aspectRatio = _ScreenParams.x / _ScreenParams.y;
                screenUV.x *= aspectRatio;
                screenUV *= _DotScale;
                float2x2 rotation = float2x2(0.7071, -0.7071, 0.7071, 0.7071);
                screenUV = mul(rotation, screenUV);
                float2 grid = frac(screenUV);
                float dist = length(grid - 0.5);
                float pattern = step(dist, _DotSize);
                fixed4 color = lerp(_DotColor, _BaseColor, pattern);

                return color;
            }
            ENDCG
        }
    }
}
