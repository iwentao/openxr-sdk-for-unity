/*
 * Copyright (c) 2019-2024, Lenovo.
 * MIT license.
 * Author: Wentao Sun 
 */

using UnityEditor;
using UnityEditor.XR.OpenXR.Features;

namespace Unity.XR.OpenXR.Features.UnityOpenXR
{
    [OpenXRFeatureSet(
    FeatureIds = new string[] {
            UnityOpenXRFeature.featureId,
    },
    UiName = "OpenXR for Unity",
    Description = "Feature set to use OpenXR in Unity for OXR compliant devices.",
    FeatureSetId = featureSetId,
    SupportedBuildTargets = new BuildTargetGroup[] { BuildTargetGroup.Android }
)]
    class UnityOpenXRFeatureSet
    {
        public const string featureSetId = "com.lenovo.openxr.oxr4unity.features";
    }
} 
