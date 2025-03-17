using System.Collections.Generic;
using UnityEngine;

namespace Seti
{
    [CreateAssetMenu(fileName = "DB_Transducer", menuName = "Database/DB_Transducer")]
    public class DB_Transducer : ScriptableObject
    {
        [HideInInspector]
        [SerializeReference]
        public List<Transducer> transducers;

#if UNITY_EDITOR
        private void OnValidate()
        {
            int removedCount = transducers.RemoveAll(b => b == null);

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
#endif
    }
}