// Original source:
//   https://github.com/vrm-c/UniVRM/blob/v0.129.1/Assets/VRM10/MToon10/Shaders/vrmc_materials_mtoon_geometry_uv.hlsl
// Copyright (c) 2020 VRM Consortium
// Copyright (c) 2018 Masataka SUMI for MToon
// Licensed under the MIT License
//   https://github.com/vrm-c/UniVRM/blob/v0.129.1/LICENSE.txt
//
// Modified by Cluster, Inc.

#ifndef VRMC_MATERIALS_MTOON_GEOMETRY_UV_INCLUDED
#define VRMC_MATERIALS_MTOON_GEOMETRY_UV_INCLUDED

#include "./vrmc_materials_mtoon_render_pipeline.hlsl"
#include "./vrmc_materials_mtoon_define.hlsl"
#include "./vrmc_materials_mtoon_utility.hlsl"
// #include "./vrmc_materials_mtoon_input.hlsl"
#include "./ClusterMToonUrpInput.hlsl"

inline float GetMToonGeometry_Uv_Time(const float2 uvRaw)
{
    if (MToon_IsParameterMapOn())
    {
        // return MTOON_SAMPLE_TEXTURE2D(_UvAnimMaskTex, uvRaw).b * _Time.y;
        return MTOON_SAMPLE_TEXTURE2D(_UvAnimMaskTexture, uvRaw).b * _Time.y;
    }
    return _Time.y;
}

inline float2 GetMToonGeometry_Uv(const float2 geometryUv)
{
    // get raw uv with _MainTex_ST
    const float2 uvRaw = TRANSFORM_TEX(geometryUv, _MainTex);

    const float uvAnimationTime = GetMToonGeometry_Uv_Time(uvRaw);
    // const float2 translate = uvAnimationTime * float2(_UvAnimScrollXSpeed, _UvAnimScrollYSpeed);
    const float2 translate = uvAnimationTime * float2(_UvAnimScrollX, _UvAnimScrollY);
    // const float rotateRad = frac(uvAnimationTime * _UvAnimRotationSpeed) * PI_2;
    const float rotateRad = frac(uvAnimationTime * _UvAnimRotation) * PI_2;
    const float cosRotate = cos(rotateRad);
    const float sinRotate = sin(rotateRad);
    const float2 rotatePivot = float2(0.5, 0.5);
    return mul(float2x2(cosRotate, -sinRotate, sinRotate, cosRotate), uvRaw + translate - rotatePivot) + rotatePivot;
}

#endif
