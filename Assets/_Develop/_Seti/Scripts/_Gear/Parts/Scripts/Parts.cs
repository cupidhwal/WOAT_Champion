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
        private string partsGeneration;

        // 속성
        public Sprite Icon => partsIcon;
        public string Name => partsName;
        public string Generation => partsGeneration;

        // 정의
        public abstract void Excute();
    }
}