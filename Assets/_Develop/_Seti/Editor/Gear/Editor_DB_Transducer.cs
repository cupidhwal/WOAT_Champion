using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Seti
{
    [CustomEditor(typeof(DB_Transducer))]
    public class Editor_DB_Transducer : Editor
    {
        private DB_Transducer transducerDB;

        public override void OnInspectorGUI()
        {
            transducerDB = (DB_Transducer)target;

            EditUtility.SubjectLine(2, "변환부 DB");

            // 기본 Inspector 그리기
            DrawDefaultInspector();

            // 리프레시 버튼
            if (GUILayout.Button("변환부 : DB 갱신"))
            {
                RefreshTransducerList();
            }

            // Behaviour 리스트 표시
            DrawTransducerList();

            EditUtility.DrawLine(2);

            // 삭제 경고 메시지 추가
            EditorGUILayout.HelpBox("변환부를 DB에서 삭제하려면 먼저 상단의 Remove 버튼을 눌러 직렬화 정보를 우선적으로 제거하길 권장합니다.", MessageType.Warning);

        }

        private void RefreshTransducerList()
        {
            // 삭제된 클래스가 있는지 확인하고 정리
            transducerDB.transducers.RemoveAll(transducer => transducer == null);

            // Transducer를 구현한 모든 클래스 탐색
            var allTransducers = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(t => typeof(Transducer).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToList();

            int addedCount = 0;

            foreach (var transducer in allTransducers)
            {
                // 중복 검사
                if (!transducerDB.transducers.Any(f => f.GetType() == transducer))
                {
                    var newTransducer = Activator.CreateInstance(transducer) as Transducer;
                    transducerDB.transducers.Add(newTransducer);
                    addedCount++;
                }
            }

            EditorUtility.SetDirty(transducerDB); // 변경 사항 저장
            Debug.Log($"Transducer DB 갱신: 새 변환부가 {addedCount}개 추가되었습니다.");
        }

        private void DrawTransducerList()
        {
            EditUtility.SubjectLine(Color.gray, 2, "변환부 : DB List");

            for (int i = 0; i < transducerDB.transducers.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(transducerDB.transducers[i].GetType().Name);

                if (GUILayout.Button("Remove"))
                {
                    transducerDB.transducers.RemoveAt(i);
                    EditorUtility.SetDirty(transducerDB);
                }

                EditorGUILayout.EndHorizontal();
            }
        }
    }
}