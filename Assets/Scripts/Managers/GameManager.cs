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
        private static GameState m_state = GameState.Playing;
        private static GameState m_lastState = GameState.Playing;
        
        public static GameState State => m_state;

        public static event Action<GameState> SwitchStateEvent;

        public static Transform Player { get; private set; }

        public static void BackGameState()
        {
            SwitchGameState(m_lastState);
        }
        
        public static void SwitchGameState(GameState state)
        {
            Debug.Log("进入: " + state);
            m_lastState = m_state;
            m_state = state;
            // 切换 map
            switch (m_state)
            {
                case GameState.Playing:
                    InputHandler.SwitchToPlayer();
                    break;
                case GameState.Story:
                    // TODO: 剧情 map
                    InputHandler.SwitchToPlayer();
                    break;
                case GameState.Puzzle:
                    InputHandler.SwitchToPlayer();
                    break;
                case GameState.PinLock:
                    InputHandler.SwitchToLockPick();
                    break;
            }

            SwitchStateEvent?.Invoke(m_state);
        }

        public static void SetPlayerTransform(Transform player) => Player = player;
    }
}
