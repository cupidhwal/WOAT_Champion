using System.Collections.Generic;
using UnityEngine;

namespace Seti
{
    [CreateAssetMenu(fileName = "DB_Receiver", menuName = "Database/DB_Receiver")]
    public class DB_Receiver : ScriptableObject
    {
        [HideInInspector]
        [SerializeReference]
        public List<Receiver> receivers = new();

        private void OnValidate()
        {
            int removedCount = receivers.RemoveAll(b => b == null);

            if (removedCount > 0)
            {
                UnityEditor.EditorUtility.SetDirty(this);
                // Unity의 다음 프레임에서 저장하도록 지연 실행
                UnityEditor.EditorApplication.delayCall += () =>
                {
                    UnityEditor.AssetDatabase.SaveAssets();
                };
            }
        }
    }
}