using UnityEngine;

namespace Seti
{
    [CreateAssetMenu(fileName = "New Transducer", menuName = "Gear/Parts/Transducer/Sample")]
    public class Sample_Transducer : Transducer
    {
        public override void Excute()
        {
            Debug.Log("샘플 : 변환부 실행!");
        }
    }
}