using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Test
    /// </summary>
    public class Test_Scenario : MonoBehaviour
    {
        // 필드
        public string fileName;

        // 라이프 사이클
        private void Start()
        {
            LoadScenario();
        }

        void LoadScenario()
        {
            // Resources 폴더에서 파일 불러오기
            TextAsset jsonFile = Resources.Load<TextAsset>($"Dialogues/{fileName}");

            if (jsonFile == null)
            {
                Debug.LogError($"파일을 찾을 수 없습니다: {fileName}.json");
                return;
            }

            // JSON을 ScenarioDatabase 객체로 변환
            Scenarios scenario = JsonUtility.FromJson<Scenarios>(jsonFile.text);

            if (scenario == null || scenario.Scenario.Count == 0)
            {
                Debug.LogError("시나리오 데이터를 불러오지 못했습니다.");
                return;
            }

            // 첫 번째 시나리오 로드 테스트
            Scenario scene = scenario.Scenario[0];
            Debug.Log($"시나리오 로드 완료: {scene.title}");

            foreach (var dialogue in scene.dialogues)
            {
                Debug.Log($"{dialogue.name}: {dialogue.sentence}");
            }
        }
    }
}