//========= Copyright 2016-2018, HTC Corporation. All rights reserved. ===========

Shader "ClusterCreatorKit/Mirror"
{
    Properties
    {
        _LeftEyeTexture("Left Eye Texture", 2D) = "white" {}
        // unused, but keep for compatibility
        _RightEyeTexture("Right Eye Texture", 2D) = "white" {}
    }

    SubShader
    {
        PackageRequirements
        {
            "com.unity.render-pipelines.universal": "17.2.0"
        }

        Tags
        {
            "RenderType" = "Opaque"
            "IgnoreProjector" = "True"
            "UniversalMaterialType" = "Unlit"
            "RenderPipeline" = "UniversalPipeline"
        }

        Pass
        {
            Name "MirrorUrp"

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile __ IS_MIRROR_RENDERING
            #pragma multi_compile __ USE_OBJECT_SPACE
            #pragma target 3.0
            #pragma multi_compile_fog
            #pragma multi_compile_instancing

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            TEXTURE2D(_LeftEyeTexture);
            SAMPLER(sampler_LeftEyeTexture);
            float4 _LeftEyeTexture_ST;

            #if USE_OBJECT_SPACE
            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float2 uv : TEXCOORD0;
                float4 positionCS : SV_POSITION;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            Varyings vert(Attributes input)
            {
                Varyings output;
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
                output.positionCS = TransformObjectToHClip(input.positionOS.xyz);
                output.uv = input.uv;
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                #ifdef IS_MIRROR_RENDERING
                discard;
                #endif

                return SAMPLE_TEXTURE2D(_LeftEyeTexture, sampler_LeftEyeTexture, TRANSFORM_TEX(input.uv, _LeftEyeTexture));
            }
            #else
            struct Attributes
            {
                float4 positionOS : POSITION;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
                float4 screenPos : TEXCOORD0;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            Varyings vert(Attributes input)
            {
                Varyings output;
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(output);
                VertexPositionInputs vertexInput = GetVertexPositionInputs(input.positionOS.xyz);
                output.positionCS = vertexInput.positionCS;
                output.screenPos = vertexInput.positionNDC;
                return output;
            }

            half4 frag(Varyings input) : SV_Target
            {
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);

                #ifdef IS_MIRROR_RENDERING
                discard;
                #endif

                float2 screenUV = input.screenPos.xy / input.screenPos.w;

                #if UNITY_SINGLE_PASS_STEREO
                float4 scaleOffset = unity_StereoScaleOffset[unity_StereoEyeIndex];
                screenUV = (screenUV - scaleOffset.zw) / scaleOffset.xy;
                #endif

                return SAMPLE_TEXTURE2D(_LeftEyeTexture, sampler_LeftEyeTexture,
                                        TRANSFORM_TEX(screenUV, _LeftEyeTexture));
            }
            #endif
            ENDHLSL
        }

        Pass
        {
            Name "MirrorDepthOnly"
            Tags
            {
                "LightMode" = "DepthOnly"
            }

            // -------------------------------------
            // Render State Commands
            ZWrite On
            ColorMask R

            HLSLPROGRAM
            #pragma target 2.0

            // -------------------------------------
            // Shader Stages
            #pragma vertex DepthOnlyVertex
            #pragma fragment DepthOnlyFragment

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing

            // -------------------------------------
            // Includes
            #include "Packages/com.unity.render-pipelines.universal/Shaders/DepthOnlyPass.hlsl"
            ENDHLSL
        }

        Pass
        {
            Name "MirrorDepthNormalsOnly"
            Tags
            {
                "LightMode" = "DepthNormalsOnly"
            }

            // -------------------------------------
            // Render State Commands
            ZWrite On

            HLSLPROGRAM
            #pragma target 2.0

            // -------------------------------------
            // Shader Stages
            #pragma vertex DepthNormalsVertex
            #pragma fragment DepthNormalsFragment

            //--------------------------------------
            // GPU Instancing
            #pragma multi_compile_instancing

            // -------------------------------------
            // Includes
            #include "Packages/com.unity.render-pipelines.universal/Shaders/UnlitDepthNormalsPass.hlsl"
            ENDHLSL
        }
    }

    SubShader
    {
        Tags
        {
            "RenderType"="Opaque"
        }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile __ IS_MIRROR_RENDERING
            #pragma multi_compile __ USE_OBJECT_SPACE
            #pragma target 3.0
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            sampler2D _LeftEyeTexture;
            float4 _LeftEyeTexture_ST;

            #if USE_OBJECT_SPACE
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (float4 vertex : POSITION, float2 uv : TEXCOORD0)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(vertex);
                o.uv = uv;
                return o;
            }

            fixed4 frag (float2 uv : TEXCOORD0) : SV_Target
            {
                #ifdef IS_MIRROR_RENDERING
                discard;
                #endif

                return tex2D(_LeftEyeTexture, TRANSFORM_TEX(uv, _LeftEyeTexture));
            }
            #else
            struct v2f
            {
                float4 vertex : SV_POSITION;
                float4 screenPos : TEXCOORD0;
            };

            v2f vert(float4 vertex : POSITION)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(vertex);
                o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                #ifdef IS_MIRROR_RENDERING
                discard;
                #endif

                float2 screenUV = i.screenPos.xy / i.screenPos.w;

                #if UNITY_SINGLE_PASS_STEREO
                float4 scaleOffset = unity_StereoScaleOffset[unity_StereoEyeIndex];
                screenUV = (screenUV - scaleOffset.zw) / scaleOffset.xy;
                #endif

                return tex2D(_LeftEyeTexture, TRANSFORM_TEX(screenUV, _LeftEyeTexture));;
            }
            #endif
            ENDCG
        }
    }
}