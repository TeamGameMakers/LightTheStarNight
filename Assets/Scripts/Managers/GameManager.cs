using System;
using UnityEngine;

namespace GM
{
    public enum GameState
    {
        // 游玩
        Playing,
        // 剧情
        Story,
        // 解谜
        Puzzle,
        // 推针
        PinLock,
        CG
    }
    
    public static class GameManager
    {
        public static Transform Player { get; private set; }
        
        public static void SetPlayerTransform(Transform player) => Player = player;
    }
}
