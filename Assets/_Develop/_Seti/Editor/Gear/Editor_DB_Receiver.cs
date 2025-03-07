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
            // 삭제된 클래스가 있는지 확인하고 정리
            receiverDB.receivers.RemoveAll(receiver => receiver == null);

            // Receiver를 구현한 모든 클래스 탐색
            var allReceivers = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(t => typeof(Receiver).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToList();

            int addedCount = 0;

            foreach (var receiver in allReceivers)
            {
                // 중복 검사
                if (!receiverDB.receivers.Any(f => f.GetType() == receiver))
                {
                    var newReceiver = Activator.CreateInstance(receiver) as Receiver;
                    receiverDB.receivers.Add(newReceiver);
                    addedCount++;
                }
            }

            EditorUtility.SetDirty(receiverDB); // 변경 사항 저장
            Debug.Log($"Receiver DB 갱신: 새 집속부가 {addedCount}개 추가되었습니다.");
        }

        private void DrawReceiverList()
        {
            EditUtility.SubjectLine(Color.gray, 2, "집속부 : DB List");

            for (int i = 0; i < receiverDB.receivers.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(receiverDB.receivers[i].GetType().Name);

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