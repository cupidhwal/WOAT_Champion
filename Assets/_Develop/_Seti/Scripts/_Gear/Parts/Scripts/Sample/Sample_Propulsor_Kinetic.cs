using UnityEngine;

namespace Seti
{
    [CreateAssetMenu(fileName = "New Propulsor", menuName = "Gear Parts/Propulsor/Sample")]
    public class Sample_Propulsor_Kinetic : Propulsor_Kinetic
    {
        public override void Excute()
        {
            Debug.Log("基敲 : 备悼何 角青!");
        }
    }
}