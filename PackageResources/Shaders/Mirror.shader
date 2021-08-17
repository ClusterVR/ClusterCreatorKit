﻿//========= Copyright 2016-2018, HTC Corporation. All rights reserved. ===========

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
			#pragma target 3.0
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			sampler2D _LeftEyeTexture;
			sampler2D _RightEyeTexture;
			float4 _LeftEyeTexture_ST;
			float4 _RightEyeTexture_ST;

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
			ENDCG
		}
	}
}