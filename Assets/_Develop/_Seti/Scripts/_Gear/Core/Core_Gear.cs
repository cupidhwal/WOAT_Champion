using UnityEngine;

namespace Seti
{
    [CreateAssetMenu(fileName = "Core_Gear", menuName = "Scriptable Objects/Core_Gear")]
    public class Core_Gear : Core
    {
        [Header("RidingGear")]
        public Receiver receiver;
        public Transducer transducer;
    }
}