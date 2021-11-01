//========= Copyright 2016-2018, HTC Corporation. All rights reserved. ===========

Shader "ClusterCreatorKit/Mirror"
{
	Properties
	{
		_LeftEyeTexture("Left Eye Texture", 2D) = "white" {}
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
			sampler2D _RightEyeTexture;
			float4 _LeftEyeTexture_ST;
			float4 _RightEyeTexture_ST;

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

				fixed4 color = float4(0, 0, 0, 0);
				#ifdef UNITY_SINGLE_PASS_STEREO
					if (unity_StereoEyeIndex == 0)
					{
						color = tex2D(_LeftEyeTexture, TRANSFORM_TEX(uv, _LeftEyeTexture));
					}
					else
					{
						color = tex2D(_RightEyeTexture, TRANSFORM_TEX(uv, _RightEyeTexture));
					}
				#else
					if (unity_CameraProjection[0][2] < 0)
					{
						color = tex2D(_LeftEyeTexture, TRANSFORM_TEX(uv, _LeftEyeTexture));
					}
					else
					{
						color = tex2D(_RightEyeTexture, TRANSFORM_TEX(uv, _RightEyeTexture));
					}
					#endif

				return color;
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

				fixed4 color = float4(0, 0, 0, 0);
				if (unity_StereoEyeIndex == 0)
				{
					color = tex2D(_LeftEyeTexture, TRANSFORM_TEX(screenUV, _LeftEyeTexture));
				}
				else
				{
					color = tex2D(_RightEyeTexture, TRANSFORM_TEX(screenUV, _RightEyeTexture));
				}

				return color;
			}
#endif
			ENDCG
		}
	}
}