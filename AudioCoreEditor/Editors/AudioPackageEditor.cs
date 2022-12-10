using UnityEngine;
using UnityEditor;
using AudioCoreLib.Structures;
using AudioCoreLib.Enums;
using AudioCoreLib.Common;

namespace AudioCoreLib.Editors
{
    [CustomPropertyDrawer(typeof(AudioPackage))]
    public class AudioPackageEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            Rect enumRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            Rect audioRect = new Rect(position.x, position.y + 20f, position.width, EditorGUIUtility.singleLineHeight);

            SerializedProperty enumProperty = property.FindPropertyRelative("audioType");
            SerializedProperty clipProperty = property.FindPropertyRelative("audioClip");
            SerializedProperty clipListProperty = property.FindPropertyRelative("audioClips");


            enumProperty.intValue = EditorGUI.Popup(enumRect, enumProperty.displayName, enumProperty.intValue, Helpers.EnumToStringArray<DatabaseEntryType>());

            switch ((DatabaseEntryType)enumProperty.intValue)
            {
                case DatabaseEntryType.Single_Clip:
                    EditorGUI.PropertyField(audioRect, clipProperty);
                    break;

                case DatabaseEntryType.Cycle_Clips:
                case DatabaseEntryType.Randomize_Clip:
                    EditorGUI.PropertyField(audioRect, clipListProperty);
                    break;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty enumProperty = property.FindPropertyRelative("audioType");
            SerializedProperty typeProperty = property.FindPropertyRelative("audioClips");

            if (enumProperty.intValue != 0)
            {
                if (typeProperty.isExpanded)
                {
                    if (typeProperty.arraySize == 0)
                        return (20 * 3.5f - EditorGUIUtility.singleLineHeight) + (EditorGUIUtility.singleLineHeight * 2);
                    else
                        return (20 * (typeProperty.arraySize + 2.5f) - EditorGUIUtility.singleLineHeight) + (EditorGUIUtility.singleLineHeight * 2);
                }
            }

            return EditorGUIUtility.singleLineHeight * 2;
        }
    }
}
