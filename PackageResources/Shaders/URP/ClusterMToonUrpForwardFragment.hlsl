#ifndef CLUSTER_MTOON_URP_FORWARD_FRAGMENT_INCLUDE
#define CLUSTER_MTOON_URP_FORWARD_FRAGMENT_INCLUDE

#include "./ClusterMToonUrpInput.hlsl"
#include "./vrmc_materials_mtoon_render_pipeline.hlsl"
#include "./vrmc_materials_mtoon_define.hlsl"
#include "./vrmc_materials_mtoon_utility.hlsl"
#include "./vrmc_materials_mtoon_attribute.hlsl"


inline bool IsOutlinePass()
{
    #if defined(MTOON_OUTLINE_WIDTH_WORLD) || defined(MTOON_OUTLINE_WIDTH_SCREEN)
    return true;
    #else
    return false;
    #endif
}

struct AdditionalLightingInput
{
    half3 normalWS;
    half shadeToony;
    half3 shadeColor;
    half3 litColor;
    half alpha;
};

half4 GetMToonURPAdditionalLighting(Light light, AdditionalLightingInput input)
{
    // ref: vrmc_materials_mtoon_lighting_unity.hlsl

    const half dotNL = dot(input.normalWS, light.direction);
    const half shade = mtoon_linearstep(-1.0 + input.shadeToony, +1.0 - input.shadeToony, dotNL);
    const half shadow = light.shadowAttenuation * light.distanceAttenuation * 0.5 * (min(0, dotNL) + 1);

    const half3 albedo = lerp(input.shadeColor, input.litColor, shade);
    const half3 direct = albedo * light.color * shadow;
    const half3 lighting = direct;

    const half3 baseCol = lighting;

    return half4(baseCol, input.alpha);
}

float4 MToonFragment(Varyings i, float facing : VFACE) : SV_TARGET
{
    #if defined(MTOON_CLIP_IF_OUTLINE_IS_NONE)
    #if defined(MTOON_OUTLINE_WIDTH_WORLD)
    #elif defined(MTOON_OUTLINE_WIDTH_SCREEN)
    #else
    clip(-1);
    #endif
    #endif

    //UNITY_TRANSFER_INSTANCE_ID(v, o);
    UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

    // const
    const float PI_2 = 6.28318530718;
    const half EPSILON_FP16 = 0.0009765625;

    // uv
    float2 mainUv = TRANSFORM_TEX(i.uv, _MainTex);

    // uv anim
    float uvAnim = MTOON_SAMPLE_TEXTURE2D(_UvAnimMaskTexture, mainUv).r * _Time.y;
    // translate uv in bottom-left origin coordinates.
    mainUv += float2(_UvAnimScrollX, _UvAnimScrollY) * uvAnim;
    // rotate uv counter-clockwise around (0.5, 0.5) in bottom-left origin coordinates.
    float rotateRad = _UvAnimRotation * PI_2 * uvAnim;
    const float2 rotatePivot = float2(0.5, 0.5);
    mainUv = mul(float2x2(cos(rotateRad), -sin(rotateRad), sin(rotateRad), cos(rotateRad)),
                 mainUv - rotatePivot) + rotatePivot;

    // main tex
    half4 mainTex = MTOON_SAMPLE_TEXTURE2D(_MainTex, mainUv);

    // alpha
    half alpha = 1;
    #if defined(_ALPHATEST_ON)
    alpha = _Color.a * mainTex.a;
    alpha = (alpha - _Cutoff) / max(fwidth(alpha), EPSILON_FP16) + 0.5; // Alpha to Coverage
    clip(alpha - _Cutoff);
    alpha = 1.0; // Discarded, otherwise it should be assumed to have full opacity
    #endif
    #if defined(_ALPHABLEND_ON)
    alpha = _Color.a * mainTex.a;
    #if defined(SHADER_API_D3D11) // Only enable this on D3D11, where I tested it
    clip(alpha - EPSILON_FP16); // Slightly improves rendering with layered transparency
    #endif
    #endif

    // normal
    const half3 normalWS = MTOON_IS_FRONT_VFACE(facing, i.normalWS, -i.normalWS);
    #if defined(_NORMALMAP)
    const half3 normalTS = normalize(MToon_UnpackNormalScale(MTOON_SAMPLE_TEXTURE2D(_BumpMap, mainUv), _BumpScale));
    half3 worldNormal = normalize(mul(normalTS, MToon_GetTangentToWorld(normalWS, i.tangentWS)));
    #else
    half3 worldNormal = normalize(normalWS);
    #endif
    float3 worldView = normalize(lerp(_WorldSpaceCameraPos.xyz - i.positionWS.xyz, UNITY_MATRIX_V[2].xyz,
                                      unity_OrthoParams.w));
    worldNormal *= step(0, dot(worldView, worldNormal)) * 2 - 1; // flip if projection matrix is flipped
    worldNormal *= IsOutlinePass() ? -1.0 : 1.0; // flip if outline pass
    worldNormal = normalize(worldNormal);

    // Unity lighting
    MTOON_LIGHT_DESCRIPTION(i, shadowAttenuation, lightDir, lightColor);
    half dotNL = dot(lightDir, worldNormal);
    half lightAttenuation = shadowAttenuation * lerp(1, shadowAttenuation,
                                                     _ReceiveShadowRate * MTOON_SAMPLE_TEXTURE2D(
                                                         _ReceiveShadowTexture, mainUv).r);

    // Decide albedo color rate from Direct Light
    half shadingGrade = 1.0 - _ShadingGradeRate * (1.0 - MTOON_SAMPLE_TEXTURE2D(_ShadingGradeTexture, mainUv).r);
    half lightIntensity = dotNL; // [-1, +1]
    lightIntensity = lightIntensity * 0.5 + 0.5; // from [-1, +1] to [0, 1]
    lightIntensity = lightIntensity * lightAttenuation; // receive shadow
    lightIntensity = lightIntensity * shadingGrade; // darker
    lightIntensity = lightIntensity * 2.0 - 1.0; // from [0, 1] to [-1, +1]
    // tooned. mapping from [minIntensityThreshold, maxIntensityThreshold] to [0, 1]
    half maxIntensityThreshold = lerp(1, _ShadeShift, _ShadeToony);
    half minIntensityThreshold = _ShadeShift;
    lightIntensity = saturate(
        (lightIntensity - minIntensityThreshold) / max(EPSILON_FP16, (maxIntensityThreshold - minIntensityThreshold)));

    // Albedo color
    half4 shade = _ShadeColor * MTOON_SAMPLE_TEXTURE2D(_ShadeTexture, mainUv);
    half4 lit = _Color * mainTex;
    half3 col = lerp(shade.rgb, lit.rgb, lightIntensity);

    // Direct Light
    half3 lighting = lightColor;
    lighting = lerp(lighting, max(EPSILON_FP16, max(lighting.x, max(lighting.y, lighting.z))),
                    _LightColorAttenuation); // color atten
    col *= lighting;

    // Indirect Light
    half3 toonedGI = 0.5 * (SampleSH(real3(0, 1, 0)) + SampleSH(real3(0, -1, 0)));
    half3 indirectLighting = lerp(toonedGI, SampleSH(worldNormal), _IndirectLightIntensity);
    indirectLighting = lerp(indirectLighting, max(EPSILON_FP16, max(indirectLighting.x, max(indirectLighting.y, indirectLighting.z))), _LightColorAttenuation); // color atten
    col += indirectLighting * lit.rgb;

    col = min(col, lit.rgb); // comment out if you want to PBR absolutely.

    // parametric rim lighting
    half3 staticRimLighting = 1;
    half3 mixedRimLighting = lighting + indirectLighting;
    half3 rimLighting = lerp(staticRimLighting, mixedRimLighting, _RimLightingMix);
    half3 rim = pow(saturate(1.0 - dot(worldNormal, worldView) + _RimLift), max(_RimFresnelPower, EPSILON_FP16)) *
        _RimColor.rgb * MTOON_SAMPLE_TEXTURE2D(_RimTexture, mainUv).rgb;
    col += IsOutlinePass() ? half3(0, 0, 0) : rim * rimLighting;

    // additive matcap
    half3 worldCameraUp = normalize(UNITY_MATRIX_V[1].xyz);
    half3 worldViewUp = normalize(worldCameraUp - worldView * dot(worldView, worldCameraUp));
    half3 worldViewRight = normalize(cross(worldView, worldViewUp));
    half2 matcapUv = half2(dot(worldViewRight, worldNormal), dot(worldViewUp, worldNormal)) * 0.5 + 0.5;
    half3 matcapLighting = MTOON_SAMPLE_TEXTURE2D(_SphereAdd, matcapUv).rgb;
    col += IsOutlinePass() ? half3(0, 0, 0) : matcapLighting;

    // Emission
    half3 emission = MTOON_SAMPLE_TEXTURE2D(_EmissionMap, mainUv).rgb * _EmissionColor.rgb;
    col += IsOutlinePass() ? half3(0, 0, 0) : emission;

    // outline
    #if defined(MTOON_OUTLINE_COLOR_FIXED)
    col = IsOutlinePass() ? _OutlineColor.rgb : col;
    #elif defined(MTOON_OUTLINE_COLOR_MIXED)
    col = IsOutlinePass() ? _OutlineColor.rgb * lerp(half3(1, 1, 1), col, _OutlineLightingMix) : col;
    #else
    #endif

    // ref: https://docs.unity3d.com/6000.2/Documentation/Manual/urp/use-built-in-shader-methods-additional-lights-fplus.html
    #if defined(_ADDITIONAL_LIGHTS) && !defined(MTOON_PASS_OUTLINE)
    AdditionalLightingInput additionalLightingInput;
    additionalLightingInput.normalWS = worldNormal;
    additionalLightingInput.shadeToony = _ShadeToony;
    additionalLightingInput.shadeColor = shade.rgb;
    additionalLightingInput.litColor = lit.rgb;
    additionalLightingInput.alpha = alpha;

    #if USE_CLUSTER_LIGHT_LOOP
    UNITY_LOOP for (uint lightIndex = 0; lightIndex < min(URP_FP_DIRECTIONAL_LIGHTS_COUNT, MAX_VISIBLE_LIGHTS); lightIndex++)
    {
        half4 shadowMask = SAMPLE_SHADOWMASK(input.lightmapUV);
        Light additionalLight = GetAdditionalLight(lightIndex, i.positionWS, shadowMask);
        col.rgb += GetMToonURPAdditionalLighting(additionalLight, additionalLightingInput).rgb;
    }
    #endif

    InputData inputData = (InputData)0;
    inputData.normalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(i.pos);
    inputData.positionWS = i.positionWS;
    uint pixelLightCount = GetAdditionalLightsCount();
    LIGHT_LOOP_BEGIN(pixelLightCount)
        half4 shadowMask = SAMPLE_SHADOWMASK(input.lightmapUV);
        Light additionalLight = GetAdditionalLight(lightIndex, i.positionWS, shadowMask);
        col.rgb += GetMToonURPAdditionalLighting(additionalLight, additionalLightingInput).rgb;
    LIGHT_LOOP_END
    #endif

    // debug
    #if defined(MTOON_DEBUG_NORMAL)
    return float4(worldNormal * 0.5 + 0.5, alpha);
    #elif defined(MTOON_DEBUG_LITSHADERATE)
    return float4(lightIntensity * lighting, alpha);
    #endif

    float fogCoord = i.fogFactorAndVertexLight.x;
    col.rgb = MixFog(col.rgb, fogCoord);

    half4 result = half4(col, alpha);

    return result;
}

#endif // CLUSTER_MTOON_URP_FORWARD_FRAGMENT_INCLUDE