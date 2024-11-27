using UnityEngine;
using UnityEditor;
using System.IO;
using System;
using UnityEditor.PackageManager;

namespace Unity.XR.OpenXR.Features.UnityOpenXR
{
    public class ExportTGZ
    {
        [MenuItem("Tools/ExportTGZ")]
        public static void Exec()
        {
            BuildTGZ();
        }

        public static void BuildTGZ()
        {
            try
            {
                var outputDir = Path.Combine(Application.dataPath, "..", "release");
                if (Directory.Exists(outputDir))
                    Directory.Delete(outputDir);
                Directory.CreateDirectory(outputDir);
                Debug.Log($"Create release dir: {outputDir}");

                string packageName = "Packages/com.lenovo.xr.openxr";
                var req = Client.Pack(Path.GetFullPath(packageName), outputDir);
                while (!req.IsCompleted)
                {
                }

                if (req.Status == StatusCode.Failure || req.Error != null)
                {
                    Debug.LogError("Failed! Error Message:" + req.Error.message);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                Debug.Log($"Build packages complete!");
            }
        }
    }
}
