using System.Collections.Generic;
using UnityEngine;

namespace Seti
{
    [CreateAssetMenu(fileName = "DB_Propulsor", menuName = "Database/DB_Propulsor")]
    public class DB_Propulsor : ScriptableObject
    {
        [HideInInspector]
        [SerializeReference]
        public List<Propulsor> propulsors;

        private void OnValidate()
        {
            int removedCount = propulsors.RemoveAll(b => b == null);

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