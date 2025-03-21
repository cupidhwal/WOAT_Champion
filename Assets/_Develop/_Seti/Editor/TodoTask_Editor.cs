using UnityEngine;
using UnityEditor;

namespace Seti
{
    [CustomEditor(typeof(TodoTask))]
    public class TodoTask_Editor : Editor
    {
        private Vector2 scrollPos;

        public override void OnInspectorGUI()
        {
            TodoTask task = (TodoTask)target;

            // TodoObject 필드 표시
            EditorGUILayout.PropertyField(serializedObject.FindProperty("note"));

            if (task.note != null)
            {
                // 해야 할 일 레이블
                EditorGUILayout.LabelField("해야 할 일", EditorStyles.boldLabel);

                // GUIStyle 수정: 자동 줄 바꿈 설정
                GUIStyle textAreaStyle = new GUIStyle(EditorStyles.textArea);
                textAreaStyle.wordWrap = true;  // 줄 바꿈 활성화

                // ScrollView 추가 (긴 텍스트도 보기 편하게)
                scrollPos = EditorGUILayout.BeginScrollView(scrollPos, GUILayout.Height(500));
                EditorGUI.BeginChangeCheck();
                string newText = EditorGUILayout.TextArea(task.note.toDo, textAreaStyle, GUILayout.ExpandHeight(true));
                EditorGUILayout.EndScrollView();

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(task.note, "Edit To Do List");
                    task.note.toDo = newText;
                    EditorUtility.SetDirty(task.note); // 변경 사항 저장
                }
            }
            else
            {
                EditorGUILayout.HelpBox("TodoObject를 할당하세요.", MessageType.Warning);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}