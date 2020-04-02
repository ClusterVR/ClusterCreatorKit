
 Shader "Outline/StenciledOutline"
 {
     Properties
     {
         _Color("Color", Color) = (1,0,0,1)
         _Thickness("Thickness", float) = 4
     }
     SubShader
     {
         Tags { "Queue"="Geometry" "IgnoreProjector"="True" "RenderType"="Transparent" }
         Blend SrcAlpha OneMinusSrcAlpha
         Cull Off
         ZTest always
         Pass
         {
             ZWrite On
             Stencil {
                 Ref 1
                 Comp NotEqual
                 Pass Replace
             }
             CGPROGRAM
             #include "UnityCG.cginc"
             #pragma vertex vert
             #pragma fragment frag

             half4 _Color;
             float _Thickness;

             struct v2f
             {
                 float4 pos : SV_POSITION;
             };

             v2f vert(appdata_base v)
             {
                 v2f OUT;

                 float3 worldNormalLength = length(mul((float3x3)transpose(unity_WorldToObject), v.normal));
                 float3 outlineOffset = 0.01 * _Thickness * worldNormalLength * v.normal;
                 OUT.pos = UnityObjectToClipPos(v.vertex + outlineOffset);

                 return OUT;
             }

             half4 frag(v2f IN) : COLOR
             {
                 return _Color;
             }

             ENDCG
         }
     }
     FallBack "Diffuse"
 }
