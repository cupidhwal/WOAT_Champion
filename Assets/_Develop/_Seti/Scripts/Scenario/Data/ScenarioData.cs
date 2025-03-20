using System.Collections.Generic;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Scenario µ•¿Ã≈Õ ƒ∏Ω∂»≠
    /// </summary>
    [CreateAssetMenu(fileName = "New Scenario", menuName = "Scenario/Scenario")]
    public class ScenarioData : ScriptableObject
    {
        [Header("Dialogue : Properties")]
        public int id;
        public string title;
        public List<Dialogue> dialogues;
    }
}