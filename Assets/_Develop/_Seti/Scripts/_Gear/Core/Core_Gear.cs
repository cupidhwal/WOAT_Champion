using UnityEngine;

namespace Seti
{
    public abstract class Core_Gear : Core
    {
        [Header("RidingGear")]
        public Receiver receiver;
        public Transducer transducer;
    }
}