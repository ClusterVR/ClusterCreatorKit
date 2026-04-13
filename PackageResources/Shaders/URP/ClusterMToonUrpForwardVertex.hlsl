#ifndef CLUSTER_MTOON_URP_FORWARD_VERTEX_INCLUDE
#define CLUSTER_MTOON_URP_FORWARD_VERTEX_INCLUDE

#include "./ClusterMToonUrpInput.hlsl"
#include "./vrmc_materials_mtoon_attribute.hlsl"
#include "./vrmc_materials_mtoon_render_pipeline.hlsl"

#define MTOON_SAMPLE_TEXTURE2D_LOD(tex,coord,lod) UNITY_SAMPLE_TEX2D_LOD(tex,coord,lod)

inline Varyings InitializeV2F(Attributes v, float4 projectedVertexCS)
{
    Varyings o = (Varyings)0;
    UNITY_SETUP_INSTANCE_ID(v);
    UNITY_TRANSFER_INSTANCE_ID(v, o);
    UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

    o.pos = projectedVertexCS;
    o.positionWS = TransformObjectToWorld(v.vertex.xyz);
    o.uv = v.texcoord0;
#if defined(_NORMALMAP)
    VertexNormalInputs normalInput = GetVertexNormalInputs(v.normalOS, v.tangentOS);
    o.normalWS = normalInput.normalWS;
    float sign = v.tangentOS.w * float(GetOddNegativeScale());
    o.tangentWS = half4(normalInput.tangentWS.xyz, sign);
#else
    o.normalWS = TransformObjectToWorldNormal(v.normalOS);
#endif
    MTOON_TRANSFER_FOG_AND_LIGHTING(o, o.pos, v.texcoord1, v.vertex.xyz);
    return o;
}

inline float4 CalculateOutlineVertexClipPosition(Attributes v)
{
    float outlineTex = MTOON_SAMPLE_TEXTURE2D_LOD(_OutlineWidthTexture, v.texcoord0, 0).r;
    
 #if defined(MTOON_OUTLINE_WIDTH_WORLD)
    float3 worldNormalLength = length(mul((float3x3)transpose(unity_WorldToObject), v.normalOS));
    float3 outlineOffset = 0.01 * _OutlineWidth * outlineTex * worldNormalLength * v.normalOS;
    float4 vertex = TransformObjectToHClip(v.vertex.xyz + outlineOffset);
 #elif defined(MTOON_OUTLINE_WIDTH_SCREEN)
    float4 nearUpperRight = mul(unity_CameraInvProjection, float4(1, 1, UNITY_NEAR_CLIP_VALUE, _ProjectionParams.y));
    float aspect = abs(nearUpperRight.y / nearUpperRight.x);
    float4 vertex = TransformObjectToHClip(v.vertex.xyz);
    float3 viewNormal = mul((float3x3)UNITY_MATRIX_IT_MV, v.normalOS.xyz);
    float3 clipNormal = mul((float3x3)UNITY_MATRIX_P, viewNormal.xyz);
    float2 projectedNormal = normalize(clipNormal.xy);
    projectedNormal *= min(vertex.w, _OutlineScaledMaxDistance);
    projectedNormal.x *= aspect;
    vertex.xy += 0.01 * _OutlineWidth * outlineTex * projectedNormal.xy * saturate(1 - abs(normalize(viewNormal).z)); // ignore offset when normal toward camera
 #else
    float4 vertex = TransformObjectToHClip(v.vertex.xyz);
 #endif
    return vertex;
}

Varyings MToonVertex(Attributes v)
{
    v.normalOS = normalize(v.normalOS);
    return InitializeV2F(v, TransformObjectToHClip(v.vertex.xyz));
}

Varyings MToonVertexOutline(Attributes v)
{
    v.normalOS = normalize(v.normalOS);
    return InitializeV2F(v, CalculateOutlineVertexClipPosition(v));
}

#endif // CLUSTER_MTOON_URP_FORWARD_VERTEX_INCLUDE
