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
		Tags { "RenderType"="Opaque" }

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
			float4 vert (float4 vertex : POSITION) : SV_POSITION
			{
				return UnityObjectToClipPos(vertex);
			}

			fixed4 frag (UNITY_VPOS_TYPE screenPos : VPOS) : SV_Target
			{
				#ifdef IS_MIRROR_RENDERING
					discard;
				#endif

				float2 screenUV = screenPos.xy / _ScreenParams.xy;

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