using System.Collections.Generic;
using UnityEngine;

namespace Seti
{
    /// <summary>
    /// Save & Load 기능의 전위체
    /// </summary>
    public class Manager_Core : Singleton<Manager_Core>
    {
        [Header("RidingGear : Board")]
        public List<RidingGear_Board> boards = new();

        //[Header("RidingGear : Boots")]
    }
}