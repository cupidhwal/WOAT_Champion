//namespace Seti
//{
//    /// <summary>
//    /// 보드 타입 라이딩기어의 에너지 소비 패턴
//    /// </summary>
//    public class Strategy_Board : IStrategy_Energy
//    {
//        public void ConsumeEnergy(PlayerEnergy energy)
//        {
//            RidingBoard board = (RidingBoard)energy.GetComponent<Player>().ridingGear;

//            // 인핸스 모드 실행: 에너지 소모 5배
//            if (board.OnEnhance)
//                energy.UseElectricity(board.RequireEnergy * 4f);
//            energy.UseElectricity(board.RequireEnergy);
//        }
//    }
//}