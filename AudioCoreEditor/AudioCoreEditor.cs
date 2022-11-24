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

            string[] res = System.IO.Directory.GetFiles(Application.dataPath, "AudioCoreEditor.dll", System.IO.SearchOption.AllDirectories);
            if (res.Length == 0)
                return;

            string path = res[0].Replace("\\", "/");
            path = path.Substring(path.IndexOf("/Assets/") + 1);


            PluginImporter importer = AssetImporter.GetAtPath(path) as PluginImporter;
            importer.SetCompatibleWithPlatform(EditorUserBuildSettings.activeBuildTarget, false);

            SessionState.SetBool("AudioCoreEditorInitialized", true);
        }
    }
}
