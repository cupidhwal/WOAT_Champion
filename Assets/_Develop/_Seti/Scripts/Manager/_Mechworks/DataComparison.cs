using UnityEngine;

namespace Seti
{
    public abstract class DataComparison
    {
        public abstract void SetValue(RidingGear gear, Parts parts);
        protected abstract void CalSpec();
        protected abstract void CalSpec(Receiver receiver);
        protected abstract void CalSpec(Transducer transducer);
        protected abstract void CalSpec(Propulsor propulsor);
    }
}