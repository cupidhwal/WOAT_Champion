using UnityEngine;

namespace Seti
{
    /// <summary>
    /// 정의 : 부품의 기본
    /// </summary>
    public abstract class Parts : ScriptableObject
    {
        // 필드
        [Header("Parts : Core")]
        [SerializeField]
        private Sprite partsIcon;
        [SerializeField]
        private string partsName;
        [SerializeField]
        private Generation partsGeneration;
        [SerializeField, TextArea(5, 15)]
        private string description;

        // 속성
        public Sprite Icon => partsIcon;
        public string Name => partsName;
        public string GenerationTag => GenNo.ToString() + "세대";
        public int GenNo
        {
            get
            {
                int temp = partsGeneration switch
                {
                    Generation.Gen1 => 1,
                    Generation.Gen2 => 2,
                    _ => 1
                };
                return temp;
            }
        }
        public int GenScale
        {
            get
            {
                int temp = partsGeneration switch
                {
                    Generation.Gen1 => 100,
                    Generation.Gen2 => 125,
                    _ => 100
                };
                return temp;
            }
        }
        public string Description => description;

        // 정의
        public abstract void Excute();
    }
}