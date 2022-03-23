using UnityEngine;

namespace AudioKor.Extensions
{
    [System.Serializable]
    public class Music
    {
        [Tooltip("Reference name of audio clip. \nExample: MENU_MUSIC, FOREST_AMBIENCE..")]
        public string musicName;

        [Tooltip("Audio clip. Yep.")]
        public AudioClip audioClip;
    }


    [CreateAssetMenu(fileName = "Music Database", menuName = "AudioKor Databases/Music Database", order = 1)]
    public class MusicDatabaseSO : ScriptableObject
    {
        public Music[] musicDatabase;
    }
}