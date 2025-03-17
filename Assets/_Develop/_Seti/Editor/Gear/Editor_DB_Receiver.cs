using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Seti
{
    [CustomEditor(typeof(DB_Receiver))]
    public class Editor_DB_Receiver : Editor
    {
        private DB_Receiver receiverDB;

        public override void OnInspectorGUI()
        {
            receiverDB = (DB_Receiver)target;

            EditUtility.SubjectLine(2, "집속부 DB");

            // 기본 Inspector 그리기
            DrawDefaultInspector();

            // 리프레시 버튼
            if (GUILayout.Button("집속부 : DB 갱신"))
            {
                RefreshReceiverList();
            }

            // Behaviour 리스트 표시
            DrawReceiverList();

            EditUtility.DrawLine(2);

            // 삭제 경고 메시지 추가
            EditorGUILayout.HelpBox("집속부를 DB에서 삭제하려면 먼저 상단의 Remove 버튼을 눌러 직렬화 정보를 우선적으로 제거하길 권장합니다.", MessageType.Warning);

        }

        private void RefreshReceiverList()
        {
            var guids = AssetDatabase.FindAssets("t:Receiver", new[] { "Assets/_Develop/_Seti/_Gear/Parts" });

            receiverDB.receivers.Clear();

            // 먼저 정렬한 후 리스트에 추가
            var sortedReceivers = guids
                .Select(guid => AssetDatabase.LoadAssetAtPath<Receiver>(AssetDatabase.GUIDToAssetPath(guid)))
                .Where(receiver => receiver != null)
                .OrderBy(receiver => receiver.GenNo)
                .ThenBy(receiver => receiver.Name)
                .ToList();

            // 정렬된 리스트 추가
            receiverDB.receivers.AddRange(sortedReceivers);

            EditorUtility.SetDirty(receiverDB);
            Debug.Log($"Receiver DB 갱신 완료: {receiverDB.receivers.Count}개의 집속부가 등록되었습니다.");
        }

        private void DrawReceiverList()
        {
            EditUtility.SubjectLine(Color.gray, 2, "집속부 : DB List");

            int j = 0;
            for (int i = 0; i < receiverDB.receivers.Count; i++)
            {
                if (j != receiverDB.receivers[i].GenNo)
                {
                    j = receiverDB.receivers[i].GenNo;
                    if (j != 1)
                        EditUtility.DrawLine(1);
                    EditorGUILayout.LabelField(receiverDB.receivers[i].GenerationTag, EditorStyles.boldLabel);
                }

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(receiverDB.receivers[i].Name);

                if (GUILayout.Button("Remove"))
                {
                    receiverDB.receivers.RemoveAt(i);
                    EditorUtility.SetDirty(receiverDB);
                }

                EditorGUILayout.EndHorizontal();
            }
        }
    }
}