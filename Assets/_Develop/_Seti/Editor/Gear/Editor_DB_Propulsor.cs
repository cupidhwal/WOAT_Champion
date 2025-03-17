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
            // Transducer를 구현한 모든 클래스 탐색
            var guids = AssetDatabase.FindAssets("t:Propulsor", new[] { "Assets/_Develop/_Seti/_Gear/Parts" });

            propulsorDB.propulsors.Clear();

            // 먼저 정렬한 후 리스트에 추가
            var sortedPropulsors = guids
                .Select(guid => AssetDatabase.LoadAssetAtPath<Propulsor>(AssetDatabase.GUIDToAssetPath(guid)))
                .Where(propulsor => propulsor != null)
                .OrderBy(propulsor => propulsor.GenNo)
                .ThenBy(propulsor => propulsor.Name)
                .ToList();

            // 정렬된 리스트 추가
            propulsorDB.propulsors.AddRange(sortedPropulsors);

            EditorUtility.SetDirty(propulsorDB);
            Debug.Log($"Propulsor DB 갱신 완료: {propulsorDB.propulsors.Count}개의 집속부가 등록되었습니다.");
        }

        private void DrawPropulsorList()
        {
            EditUtility.SubjectLine(Color.gray, 2, "구동부 : DB List");

            int j = 0;
            for (int i = 0; i < propulsorDB.propulsors.Count; i++)
            {
                if (j != propulsorDB.propulsors[i].GenNo)
                {
                    j = propulsorDB.propulsors[i].GenNo;
                    if (j != 1)
                        EditUtility.DrawLine(1);
                    EditorGUILayout.LabelField(propulsorDB.propulsors[i].GenerationTag, EditorStyles.boldLabel);
                }

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(propulsorDB.propulsors[i].Name);

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