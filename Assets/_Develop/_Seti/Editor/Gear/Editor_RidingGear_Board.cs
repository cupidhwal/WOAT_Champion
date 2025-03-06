using UnityEditor;
using UnityEngine;

namespace Seti
{
    [CustomEditor(typeof(RidingGear_Board))]
    public class Editor_RidingGear_Board : Editor
    {
        public override void OnInspectorGUI()
        {
            RidingGear_Board board = (RidingGear_Board)target;

            serializedObject.Update();

            // Receiver, Transducer, Propulsor는 기본적으로 표시
            DrawDefaultInspector();

            // Speed 필드만 읽기 전용으로 만들기
            GUI.enabled = false; // 인스펙터에서 수정 불가 상태
            EditorGUILayout.FloatField("maxSpeed", board.MaxSpeed);
            GUI.enabled = true;  // 다시 수정 가능 상태로 복구

            serializedObject.ApplyModifiedProperties();
        }
    }
}