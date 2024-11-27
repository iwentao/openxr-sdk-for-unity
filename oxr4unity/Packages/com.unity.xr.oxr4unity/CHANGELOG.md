# Changelog
All notable changes to this package will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

# Notes
When updating the Changelog, please ensure we follow the standards for ordering headers as outlined here: [US-0039](https://standards.ds.unity3d.com/Standards/US-0039/). Specifically:
```
Under ## headers, ### \<type\> headers are listed in this order: Added, Changed, Deprecated, Removed, Fixed, Security
```

## [0.0.2] - 2024-3-19
1. Upgrade OpenXR loader to 0.21.
2. Fix manifest gradle <queries> node duplication issue.
3. Add validation check for Min SDK version (24), Linear color space and landscape orientation.
4. Add XR_EXT_performance_settings support to set App performance level.
5. Add pass through support with QC internal interception library.
6. Add place holder for XR_FB_passthrough support. Will enable after it's supported in runtime.

## [0.0.1-preview.1] - 2024-2-27

### This is the first release of *ThinkReality OpenXR Plugin \<com.lenovo.xr.openxr\>*.
