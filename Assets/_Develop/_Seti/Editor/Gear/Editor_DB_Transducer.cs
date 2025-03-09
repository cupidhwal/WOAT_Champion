using System;
using System.Linq;
using Unity.Android.Gradle.Manifest;
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
            // Transducer를 구현한 모든 클래스 탐색
            var guids = AssetDatabase.FindAssets("t:Transducer", new[] { "Assets/_Develop/_Seti/_Gear/Parts" });

            transducerDB.transducers.Clear();

            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                var transducer = AssetDatabase.LoadAssetAtPath<Transducer>(path);
                transducerDB.transducers.Add(transducer);
            }

            EditorUtility.SetDirty(transducerDB);
            Debug.Log($"Transducer DB 갱신 완료: {transducerDB.transducers.Count}개의 집속부가 등록되었습니다.");
        }

        private void DrawTransducerList()
        {
            EditUtility.SubjectLine(Color.gray, 2, "변환부 : DB List");

            for (int i = 0; i < transducerDB.transducers.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(transducerDB.transducers[i].Name);

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