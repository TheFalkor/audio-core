using UnityEngine;

namespace AudioKor.Extensions
{
    [System.Serializable]
    public class Music
    {
        [Tooltip("Sets the name that will be used to access this audio clip when calling AudioKor.\nExample: MENU_MUSIC, FOREST_AMBIENCE..")]
        public string musicName;

        [Tooltip("Audio clip. Yep.")]
        public AudioClip audioClip;

        [Space]

        [Tooltip("Sets the music to loop once it stops playing.")]
        public bool loop;

        [Space]

        [Tooltip("Sets a factor of the music volume.")]
        [Range(0f, 1f)]
        public float volume = 1f;

        [Tooltip("Sets the pitch of the music to make it play slower or faster.")]
        [Range(-3f, 3f)]
        public float pitch = 1f;
    }


    [CreateAssetMenu(fileName = "Music Database", menuName = "AudioKor Databases/Music Database", order = 1)]
    public class MusicDatabaseSO : ScriptableObject
    {
        public Music[] musicDatabase;

        public Music GetMusic(string musicName)
        {
            foreach (Music m in musicDatabase)
            {
                if (m.musicName == musicName)
                {
                    return m;
                }
            }

            return null;
        }
    }
}