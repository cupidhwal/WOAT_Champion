using UnityEngine;

namespace Seti
{
    [CreateAssetMenu(fileName = "Core_Boots", menuName = "Scriptable Objects/Core_Boots")]
    public class Core_Boots : Core_Gear
    {
        public Propulsor_Electronic propulsor;

        [Header("Enhance Mode : Boots")]
        public EnhanceMode_Boots enhance;

        //public override void Get()
        //{
        //    if (receiver)
        //    {
        //        boots.Parts_Change_Receiver(receiver);
        //    }
        //    if (transducer)
        //    {
        //        boots.Parts_Change_Transducer(transducer);
        //    }
        //    if (propulsor)
        //    {
        //        boots.Parts_Change_Propulsor(propulsor);
        //    }
        //}

        //public override void Set()
        //{
        //    receiver = boots.Receiver;
        //    transducer = boots.Transducer;
        //    propulsor = boots.Propulsor;
        //}
    }
}