using UnityEngine;

namespace Seti
{
    public abstract class Propulsor_Kinetic : Propulsor
    {
        // 필드
        [SerializeField, ReadOnly]
        protected float agility;
        [SerializeField]
        protected float acceleration;
        [SerializeField]
        protected float momentum;

        // 속성
        public float Agility => agility;
        public float Acceleration => acceleration;
        public float Momentum => momentum;

        private void OnValidate()
        {
            agility = GenScale / performance;
        }
    }
}