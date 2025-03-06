using UnityEngine;

namespace Seti
{
    [CreateAssetMenu(fileName = "New Receiver", menuName = "Gear Parts/Receiver/Sample")]
    public class Sample_Receiver : Receiver
    {
        public override void Excute()
        {
            Debug.Log("基敲 : 笼加何 角青!");
        }
    }
}