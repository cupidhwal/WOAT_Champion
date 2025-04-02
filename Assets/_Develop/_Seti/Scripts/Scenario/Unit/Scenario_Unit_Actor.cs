using UnityEngine;
using UnityEngine.Events;

namespace Seti
{
    public abstract class Scenario_Unit_Actor : Scenario_Unit
    {
        public UnityAction<ScenarioData> OnDialogue;
        public UnityAction OnNext;

        [Header("Scenario : Data")]
        [SerializeField]
        protected ScenarioData data_Enter;
        [SerializeField]
        protected ScenarioData data_Exit;
        [SerializeField]
        protected ScenarioData[] datas;

        public ScenarioData Enter => data_Enter;
        public ScenarioData Exit => data_Exit;
        public ScenarioData[] Datas => datas;

        public override void Execute(ScenarioData data) => OnDialogue?.Invoke(data);
        public void Next() => OnNext?.Invoke();
    }
}