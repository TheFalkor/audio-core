using UnityEngine;
using UnityEditor;
using AudioCoreLib.Structures;
using AudioCoreLib.Enums;

namespace AudioCoreLib.Editors
{
    [CustomPropertyDrawer(typeof(AudioModifier))]
    public class AudioModifierEditor : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            Rect enumRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            Rect firstRect = new Rect(position.x, position.y + 30f, position.width, EditorGUIUtility.singleLineHeight);
            Rect secondRect = new Rect(position.x, position.y + 50f, position.width, EditorGUIUtility.singleLineHeight);

            SerializedProperty enumProperty = property.FindPropertyRelative("modifierType");
            SerializedProperty minimumProperty = property.FindPropertyRelative("minimumValue");
            SerializedProperty maximumProperty = property.FindPropertyRelative("maximumValue");


            enumProperty.intValue = EditorGUI.Popup(enumRect, enumProperty.displayName, enumProperty.intValue, enumProperty.enumNames);

            switch ((AudioModifierType)enumProperty.intValue)
            {
                case AudioModifierType.RANDOMIZE_PITCH:
                    if (minimumProperty.floatValue > maximumProperty.floatValue)
                    {
                        EditorGUI.HelpBox(firstRect, " Maximum value is less than minimum value!", MessageType.Warning);
                        firstRect.y += 20f;
                        secondRect.y += 20f;
                    }
                    minimumProperty.floatValue = EditorGUI.Slider(firstRect, new GUIContent(minimumProperty.displayName), Mathf.Clamp(minimumProperty.floatValue, 0.1f, 1f), 0.1f, 1f);
                    maximumProperty.floatValue = EditorGUI.Slider(secondRect, new GUIContent(maximumProperty.displayName), Mathf.Clamp(maximumProperty.floatValue, 0.1f, 1f), 0.1f, 1f);
                    break;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            SerializedProperty enumProperty = property.FindPropertyRelative("modifierType");

            switch ((AudioModifierType)enumProperty.intValue)
            {
                case AudioModifierType.RANDOMIZE_PITCH:
                    SerializedProperty minimumProperty = property.FindPropertyRelative("minimumValue");
                    SerializedProperty maximumProperty = property.FindPropertyRelative("maximumValue");
                    if (minimumProperty.floatValue > maximumProperty.floatValue)
                        return EditorGUIUtility.singleLineHeight * 5f;
                    else
                        return EditorGUIUtility.singleLineHeight * 4f;
            }

            return EditorGUIUtility.singleLineHeight * 1.75f;
        }
    }
}
