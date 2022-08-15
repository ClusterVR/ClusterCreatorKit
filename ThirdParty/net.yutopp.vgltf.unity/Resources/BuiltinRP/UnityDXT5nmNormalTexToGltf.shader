Shader "Hidden/VGltfUnity/UnityDXT5nmNormalTexToGltf"
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

            // Unity -> glTF
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 c = tex2D(_MainTex, i.uv);

                fixed r = c.r * c.a; // DXT5nm (R=1, A=x) or BC5 (R=x, A=1), so R*A means r in gltf space
                fixed g = c.g;
                fixed xy = fixed2(r * 2 - 1, g * 2 - 1); // [0, 1] -> [-1, 1]
                fixed z = sqrt(1 - saturate(dot(xy, xy)));
                fixed b = (z + 1.0f) * 0.5f;

                return fixed4(r, g, b, 1);
            }
            ENDCG
        }
    }
}
