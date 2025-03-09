//namespace Seti
//{
//    /// <summary>
//    /// 부츠 타입 라이딩기어의 에너지 소비 패턴
//    /// </summary>
//    public class Strategy_Boots : IStrategy_Energy
//    {
//        public void ConsumeEnergy(PlayerEnergy energy)
//        {
//            RidingBoots boots = (RidingBoots)energy.GetComponent<Player>().ridingGear;

//            energy.UseElectricity(boots.RequireEnergy);
//        }
//    }
//}