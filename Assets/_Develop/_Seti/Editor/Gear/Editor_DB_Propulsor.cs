using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Seti
{
    [CustomEditor(typeof(DB_Propulsor))]
    public class Editor_DB_Propulsor : Editor
    {
        private DB_Propulsor propulsorDB;

        public override void OnInspectorGUI()
        {
            propulsorDB = (DB_Propulsor)target;

            EditUtility.SubjectLine(2, "구동부 DB");

            // 기본 Inspector 그리기
            DrawDefaultInspector();

            // 리프레시 버튼
            if (GUILayout.Button("구동부 : DB 갱신"))
            {
                RefreshPropulsorList();
            }

            // Behaviour 리스트 표시
            DrawPropulsorList();

            EditUtility.DrawLine(2);

            // 삭제 경고 메시지 추가
            EditorGUILayout.HelpBox("구동부를 DB에서 삭제하려면 먼저 상단의 Remove 버튼을 눌러 직렬화 정보를 우선적으로 제거하길 권장합니다.", MessageType.Warning);

        }

        private void RefreshPropulsorList()
        {
            // 삭제된 클래스가 있는지 확인하고 정리
            propulsorDB.propulsors.RemoveAll(propulsor => propulsor == null);

            // Transducer를 구현한 모든 클래스 탐색
            var allPropulsors = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(t => typeof(Propulsor).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
                .ToList();

            int addedCount = 0;

            foreach (var propulsor in allPropulsors)
            {
                // 중복 검사
                if (!propulsorDB.propulsors.Any(f => f.GetType() == propulsor))
                {
                    var newPropulsor = Activator.CreateInstance(propulsor) as Propulsor;
                    propulsorDB.propulsors.Add(newPropulsor);
                    addedCount++;
                }
            }

            EditorUtility.SetDirty(propulsorDB); // 변경 사항 저장
            Debug.Log($"Propulsor DB 갱신: 새 구동부가 {addedCount}개 추가되었습니다.");
        }

        private void DrawPropulsorList()
        {
            EditUtility.SubjectLine(Color.gray, 2, "구동부 : DB List");

            for (int i = 0; i < propulsorDB.propulsors.Count; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(propulsorDB.propulsors[i].GetType().Name);

                if (GUILayout.Button("Remove"))
                {
                    propulsorDB.propulsors.RemoveAt(i);
                    EditorUtility.SetDirty(propulsorDB);
                }

                EditorGUILayout.EndHorizontal();
            }
        }
    }
}