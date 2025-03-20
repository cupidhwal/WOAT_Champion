using UnityEngine;

namespace Seti
{
    [CreateAssetMenu(fileName = "New Event Update Action", menuName = "Scenario/Composition/Event/Flynne Event Update")]
    public class Composition_Update_FlynneEvent : CompositionObject
    {
        [Header("Variables")]
        [SerializeField]
        private int eventIndex;

        public override void Execute(GameObject obj)
        {
            //Manager_Data.Instance.flynneEvent[eventIndex] = true;
        }
    }
}