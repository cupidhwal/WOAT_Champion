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

        private void OnValidate()
        {
            int removedCount = transducers.RemoveAll(b => b == null);

            if (removedCount > 0)
            {
                UnityEditor.EditorUtility.SetDirty(this);
                //UnityEditor.AssetDatabase.SaveAssets();
            }
        }
    }
}