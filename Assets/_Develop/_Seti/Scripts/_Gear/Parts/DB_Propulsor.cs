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
                //UnityEditor.AssetDatabase.SaveAssets();
            }
        }
    }
}