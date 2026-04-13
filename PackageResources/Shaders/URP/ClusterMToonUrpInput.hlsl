#ifndef CLUSTER_MTOON_URP_INPUT_INCLUDED
#define CLUSTER_MTOON_URP_INPUT_INCLUDED

#include "./vrmc_materials_mtoon_render_pipeline.hlsl"

// Textures
MTOON_DECLARE_TEX2D(_MainTex);
MTOON_DECLARE_TEX2D(_ShadeTexture);
MTOON_DECLARE_TEX2D(_BumpMap);
MTOON_DECLARE_TEX2D(_ReceiveShadowTexture);
MTOON_DECLARE_TEX2D(_ShadingGradeTexture);
MTOON_DECLARE_TEX2D(_RimTexture);
MTOON_DECLARE_TEX2D(_SphereAdd);
MTOON_DECLARE_TEX2D(_EmissionMap);
MTOON_DECLARE_TEX2D(_OutlineWidthTexture);
// NOTE: "tex2d() * _Time.y" returns mediump value if sampler is half precision in Android VR platform
MTOON_DECLARE_TEX2D_FLOAT(_UvAnimMaskTexture);

CBUFFER_START(UnityPerMaterial)
// Main
float4 _MainTex_ST;
float4 _Color;
float4 _ShadeColor;
float _Cutoff;
float _BumpScale;
float _ReceiveShadowRate;
float _ShadingGradeRate;
float _ShadeShift;
float _ShadeToony;
float _LightColorAttenuation;
float _IndirectLightIntensity;

// Rim
float4 _RimColor;
float _RimLightingMix;
float _RimFresnelPower;
float _RimLift;

// Emission
float4 _EmissionColor;

// Outline
float _OutlineWidth;
float _OutlineScaledMaxDistance;
float4 _OutlineColor;
float _OutlineLightingMix;

// UV Animation
float _UvAnimScrollX;
float _UvAnimScrollY;
float _UvAnimRotation;
float _CullMode;
CBUFFER_END

#endif // CLUSTER_MTOON_URP_INPUT_INCLUDED
