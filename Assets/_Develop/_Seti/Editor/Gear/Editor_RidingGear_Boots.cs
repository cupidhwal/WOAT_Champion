using UnityEditor;
using UnityEngine;

namespace Seti
{
    [CustomEditor(typeof(RidingGear_Boots))]
    public class Editor_RidingGear_Boots : Editor
    {
        public override void OnInspectorGUI()
        {
            RidingGear_Boots board = (RidingGear_Boots)target;

            serializedObject.Update();

            // Receiver, Transducer, Propulsor는 기본적으로 표시
            DrawDefaultInspector();

            // Speed 필드만 읽기 전용으로 만들기
            GUI.enabled = false; // 인스펙터에서 수정 불가 상태
            EditorGUILayout.FloatField("maxPower", board.MaxPower);
            GUI.enabled = true;  // 다시 수정 가능 상태로 복구

            serializedObject.ApplyModifiedProperties();
        }
    }
}