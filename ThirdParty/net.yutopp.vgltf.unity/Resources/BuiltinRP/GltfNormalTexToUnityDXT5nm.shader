Shader "Hidden/VGltfUnity/GltfNormalTexToUnityDXT5nm"
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

            // https://www.khronos.org/registry/glTF/specs/2.0/glTF-2.0.html#additional-textures
            // glTF
            //  R: f(X "[-1 .. 1]")
            //  G: f(Y "[-1 .. 1]")
            //  B: f(Z "(0 .. 1]")
            //  A: [ignored]
            //    where f x = (x + 1) * 0.5

            // UnityCG.cginc
            // Unity
            //  DXT5nm (R=1, G=y, B=1, A=x) or BC5 (R=x, G=y, B=0, A=1)

            // glTF -> Unity
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 c = tex2D(_MainTex, i.uv);
                return fixed4(c.r, c.g, 0 /* hard-coded, ignored in shader */, 1.0f);
            }
            ENDCG
        }
    }
}
