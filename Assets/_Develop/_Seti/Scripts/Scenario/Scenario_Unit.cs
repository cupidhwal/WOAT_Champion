using UnityEngine;

namespace Seti
{
    /// <summary>
    /// 대화 참여자의 추상 클래스, 시나리오 데이터를 관리
    /// </summary>
    public abstract class Scenario_Unit : MonoBehaviour
    {
        public abstract void Execute();
    }
}