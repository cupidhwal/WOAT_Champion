using UnityEditor;
using UnityEngine;

namespace Seti
{
    [CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false; // GUI를 비활성화 (읽기 전용)
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = true;  // GUI 상태 원래대로 복구
        }
    }
}