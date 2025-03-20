using System.IO;
using UnityEngine;

namespace Seti
{
    public class JsonManager
    {
        // JSON 저장 (파일로 저장)
        public static void Save<T>(T data, string filePath)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(filePath, json);
        }

        // JSON 불러오기 (파일에서 읽어오기)
        public static T Load<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Debug.LogWarning($"파일을 찾을 수 없음: {filePath}");
                return default;
            }
            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<T>(json);
        }

        // 파일 존재 여부 확인
        public static bool Exists(string filePath) => File.Exists(filePath);
    }
}