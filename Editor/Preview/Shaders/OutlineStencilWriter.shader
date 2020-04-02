
 Shader "Outline/StencilWriter"
 {
     SubShader
     {
         Tags { "Queue"="Geometry" "IgnoreProjector"="True" "RenderType"="Transparent" }
         Blend Zero One
         Cull Off
         ZTest always
         Pass
         {
             ZWrite Off
             Stencil {
                 Ref 1
                 Comp always
                 Pass replace
             }
             CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag

             #include "UnityCG.cginc"

             struct v2f
             {
                 float4  pos : SV_POSITION;
             };

             v2f vert(appdata_base v)
             {
                 v2f OUT;
                 OUT.pos = UnityObjectToClipPos(v.vertex);

                 return OUT;
             }

             half4 frag(v2f IN) : COLOR
             {
                 // stencil only
                 return 0;
             }

             ENDCG

         }
     }
 }
