/*
 * Copyright (c) 2019-2024, Lenovo.
 * MIT license.
 * Author: Wentao Sun 
 */

using System.Collections.Generic;
using UnityEngine.XR.OpenXR;
using UnityEngine.XR.OpenXR.Features;
using Object = System.Object;
using UnityEngine.XR.OpenXR.Features.Interactions;
using System;
using UnityEngine;
using System.Runtime.InteropServices;
using Unity.XR.CoreUtils;
using UnityEngine.XR.OpenXR.NativeTypes;

#if UNITY_EDITOR
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEditor;
using UnityEditor.XR.OpenXR.Features;
#endif

namespace Unity.XR.OpenXR.Features.UnityOpenXR
{
#if UNITY_EDITOR
    [OpenXRFeature(UiName = "OpenXR Base Feature",
        Desc = "Necessary to deploy an OpenXR compliant App.",
        Company = "Lenovo",
        Version = "1.0.0",
        BuildTargetGroups = new[] { BuildTargetGroup.Android },
        CustomRuntimeLoaderBuildTargets = new[] { BuildTarget.Android },
        OpenxrExtensionStrings = UnityOpenXRExtensionList,
        FeatureId = featureId
    )]
#endif

    [Serializable]
    public class UnityOpenXRFeature : OpenXRFeature
    {
        public const string featureId = "com.unity.openxr.feature.oxr4unity";

        // Extensions except XR_FB_passthrough are supported in Spaces 0.21, until 2024-3
        public const string UnityOpenXRExtensionList = "XR_FB_display_refresh_rate XR_KHR_android_thread_settings XR_EXT_performance_settings XR_FB_passthrough XR_EXT_hand_tracking XR_MSFT_hand_tracking_mesh XR_MSFT_hand_interaction XR_EXT_hand_interaction XR_EXT_palm_pose";
        public static string SDKVersion = "Unity_OpenXR_0.0.1";

        // Delegates
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate int PFN_GetInstanceProcAddr(IntPtr xrInstance, [MarshalAs(UnmanagedType.LPStr)] string name, ref IntPtr functionPtr);
        public static PFN_GetInstanceProcAddr oxrGetInstanceProcAddr;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate XrResult PFN_xrPerfSettingsSetPerformanceLevelEXT(ulong session, XrPerfSettingsDomainEXT domain, XrPerfSettingsLevelEXT level);
        private static PFN_xrPerfSettingsSetPerformanceLevelEXT oxrPerfSettingsSetPerformanceLevelEXT;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate XrResult PFN_xrEnumerateDisplayRefreshRatesFB(ulong session, uint displayRefreshRateCapacityInput, ref uint displayRefreshRateCountOutput, IntPtr /* float* */ displayRefreshRates);
        private static PFN_xrEnumerateDisplayRefreshRatesFB oxrEnumerateDisplayRefreshRatesFB;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate XrResult PFN_xrGetDisplayRefreshRateFB(ulong session, ref float displayRefreshRate);
        private static PFN_xrGetDisplayRefreshRateFB oxrGetDisplayRefreshRateFB;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate XrResult PFN_xrRequestDisplayRefreshRateFB(ulong session, float displayRefreshRate);
        private static PFN_xrRequestDisplayRefreshRateFB oxrRequestDisplayRefreshRateFB;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate XrResult PFN_xrGetSystemProperties(ulong instance, ulong systemId, IntPtr systemProperties);
        private static PFN_xrGetSystemProperties oxrGetSystemProperties;

        // XR_FB_passthrough
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate XrResult PFN_xrCreatePassthroughFB(ulong session, [In] XrPassthroughCreateInfoFB createInfo, out XrPassthroughFB outPassthrough);
        private static PFN_xrCreatePassthroughFB oxrCreatePassthroughFB;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate XrResult PFN_xrDestroyPassthroughFB(XrPassthroughFB passthrough);
        private static PFN_xrDestroyPassthroughFB oxrDestroyPassthroughFB;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate XrResult PFN_xrPassthroughStartFB(XrPassthroughFB passthrough);
        private static PFN_xrPassthroughStartFB oxrPassthroughStartFB;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate XrResult PFN_xrPassthroughPauseFB(XrPassthroughFB passthrough);
        private static PFN_xrPassthroughPauseFB oxrPassthroughPauseFB;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate XrResult PFN_xrCreatePassthroughLayerFB(ulong session, [In] XrPassthroughLayerCreateInfoFB createInfo, out XrPassthroughLayerFB outLayer);
        private static PFN_xrCreatePassthroughLayerFB oxrCreatePassthroughLayerFB;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate XrResult PFN_xrDestroyPassthroughLayerFB(XrPassthroughLayerFB layer);
        private static PFN_xrDestroyPassthroughLayerFB oxrDestroyPassthroughLayerFB;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate XrResult PFN_xrPassthroughLayerPauseFB(XrPassthroughLayerFB layer);
        private static PFN_xrPassthroughLayerPauseFB oxrPassthroughLayerPauseFB;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate XrResult PFN_xrPassthroughLayerResumeFB(XrPassthroughLayerFB layer);
        private static PFN_xrPassthroughLayerResumeFB oxrPassthroughLayerResumeFB;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate XrResult PFN_xrPassthroughLayerSetStyleFB(XrPassthroughLayerFB layer, [In] XrPassthroughStyleFB style);
        private static PFN_xrPassthroughLayerSetStyleFB oxrPassthroughLayerSetStyleFB;
    }
}

