using System;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Strategy Pattern - Root
    /// </summary>
    public interface IStrategy
    {
        Type GetStrategyType();
    }

    [System.Serializable]
    public class Strategy
    {
        [SerializeReference]
        public IStrategy strategy;  // ���� ���� ��ü
        public bool isActive;       // ���� Ȱ��ȭ ����

        public Strategy() { }
    }
}