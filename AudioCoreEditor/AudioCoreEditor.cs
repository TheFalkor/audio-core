using UnityEngine;
using UnityEditor;

namespace AudioCoreLib
{
    [InitializeOnLoad]
    class AudioCoreEditor
    {
        static AudioCoreEditor()
        {
            if (SessionState.GetBool("AudioCoreEditorInitialized", false))
                return;

            SessionState.SetBool("AudioCoreEditorInitialized", true);

            string[] res = System.IO.Directory.GetFiles(Application.dataPath, "AudioCoreEditor.dll", System.IO.SearchOption.AllDirectories);
            if (res.Length == 0)
                return;

            string path = res[0].Replace("\\", "/");
            path = path[(path.IndexOf("/Assets/") + 1)..];


            PluginImporter importer = AssetImporter.GetAtPath(path) as PluginImporter;
            if (importer.GetCompatibleWithPlatform(EditorUserBuildSettings.activeBuildTarget) || importer.GetCompatibleWithAnyPlatform())
            {
                importer.SetCompatibleWithAnyPlatform(false);
                importer.SetCompatibleWithPlatform(EditorUserBuildSettings.activeBuildTarget, false);
                importer.SetCompatibleWithEditor(true);
                importer.SaveAndReimport();
            }
        }
    }
}
