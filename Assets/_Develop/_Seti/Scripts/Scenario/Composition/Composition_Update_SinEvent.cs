using UnityEngine;

namespace Seti
{
    [CreateAssetMenu(fileName = "New Event Update Action", menuName = "Scenario/Composition/Event/Sin Event Update")]
    public class Composition_Update_SinEvent : CompositionObject
    {
        [Header("Variables")]
        [SerializeField]
        private int eventIndex;

        public override void Execute(GameObject obj)
        {
            //Manager_Data.Instance.sinEvent[eventIndex] = true;
        }
    }
}