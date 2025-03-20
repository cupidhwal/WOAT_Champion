using System.IO;
using UnityEngine;
using UnityEditor;

namespace Seti
{
    public class ScenarioEditor : EditorWindow
    {
        private TextAsset jsonFile;     // 유니티 에디터에서 선택할 JSON 파일
        private readonly string folderPath = "Assets/_Develop/_Seti/_Scenario/Scenarios";    // SO가 저장될 폴더 경로

        [MenuItem("Tools/JSON to SO Importer")]
        public static void ShowWindow()
        {
            GetWindow<ScenarioEditor>("JSON to SO Importer");
        }

        private void OnGUI()
        {
            GUILayout.Label("JSON to SO Importer", EditorStyles.boldLabel);

            // JSON 파일을 유니티 에디터에서 직접 선택할 수 있도록 수정
            EditorGUILayout.BeginHorizontal();
            jsonFile = (TextAsset)EditorGUILayout.ObjectField("JSON 파일을 선택하세요", jsonFile, typeof(TextAsset), false);
            if (GUILayout.Button("Browse", GUILayout.Width(60)))
            {
                string path = EditorUtility.OpenFilePanel("JSON 파일 선택", Application.dataPath, "json");
                if (!string.IsNullOrEmpty(path))
                {
                    path = "Assets" + path.Replace(Application.dataPath, "");
                    jsonFile = AssetDatabase.LoadAssetAtPath<TextAsset>(path);
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();

            if (GUILayout.Button("Import JSON"))
            {
                if (jsonFile != null)
                {
                    ImportJson();
                }
                else
                {
                    Debug.LogError("JSON 파일을 선택하세요!");
                }
            }
        }

        private void ImportJson()
        {
            if (jsonFile == null)
            {
                Debug.LogError("JSON 파일이 지정되지 않았습니다.");
                return;
            }

            // JSON 내용을 직접 읽어오기
            string jsonContent = jsonFile.text;

            if (string.IsNullOrEmpty(jsonContent))
            {
                Debug.LogError("JSON 파일이 비어 있습니다.");
                return;
            }

            // JSON을 객체로 변환
            Scenarios scenarios = JsonUtility.FromJson<Scenarios>(jsonContent);

            if (scenarios == null || scenarios.Scenario.Count == 0)
            {
                Debug.LogError("JSON 파일을 파싱하는 데 실패했습니다.");
                return;
            }

            // 저장 경로 폴더가 없으면 생성
            if (!AssetDatabase.IsValidFolder(folderPath))
            {
                AssetDatabase.CreateFolder("Assets/_Develop/_Seti/_Scenario", "Scenarios");
            }

            foreach (var scenario in scenarios.Scenario)
            {
                // 파일 이름을 JSON 파일명과 ID를 조합하여 생성
                string path = $"{folderPath}/{jsonFile.name}_{scenario.id}.asset";

                ScenarioData scenarioData = AssetDatabase.LoadAssetAtPath<ScenarioData>(path);
                if (scenarioData == null)
                {
                    scenarioData = CreateInstance<ScenarioData>();
                    AssetDatabase.CreateAsset(scenarioData, path);
                }

                // SO 데이터 설정
                scenarioData.id = scenario.id;
                scenarioData.title = scenario.title;
                scenarioData.dialogues = scenario.dialogues;

                // 변경 사항 저장
                EditorUtility.SetDirty(scenarioData);
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"JSON 파일 '{jsonFile.name}'을 SO로 변환 완료!");
        }
    }
}