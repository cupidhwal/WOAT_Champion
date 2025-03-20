using UnityEngine;

namespace Seti
{
    [CreateAssetMenu(fileName = "Core", menuName = "Gear/Core - Board")]
    public class Core_Board : Core_Gear
    {
        public Propulsor_Kinetic propulsor;

        [Header("Enhance Mode : Board")]
        public EnhanceMode_Board enhance;

        //// ¸Þ¼­µå
        //public override void Get()
        //{
        //    if (receiver)
        //    {
        //        Manager_Core.Instance.boards[objectIndex].Parts_Change_Receiver(receiver);
        //    }
        //    if (transducer)
        //    {
        //        Manager_Core.Instance.boards[objectIndex].Parts_Change_Transducer(transducer);
        //    }
        //    if (propulsor)
        //    {
        //        Manager_Core.Instance.boards[objectIndex].Parts_Change_Propulsor(propulsor);
        //    }
        //}

        //public override void Set()
        //{
        //    receiver = Manager_Core.Instance.boards[objectIndex].Receiver;
        //    transducer = Manager_Core.Instance.boards[objectIndex].Transducer;
        //    propulsor = Manager_Core.Instance.boards[objectIndex].Propulsor;
        //}
    }
}