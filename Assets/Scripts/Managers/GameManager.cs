using System;
using System.Collections.Generic;
using Characters;
using Interact;
using UI;
using UnityEngine;

namespace GM
{
    public enum GameState
    {
        // 游玩
        Playing,
        UI
    }
    
    public static class GameManager
    {
        public static Transform Player { get; private set; }

        public static void SetPlayerTransform(Transform player) => Player = player;

        private static GameState m_currentState = GameState.Playing;
        
        private static Stack<GameState> m_stateRecord = new Stack<GameState>();

        public static event Action<GameState> SwitchStateEvent;

        public static Transform player;
        // 玩家出生点
        public static Vector3 originalPlayerPos;

        public static void ResumeState()
        {
            m_currentState = m_stateRecord.Pop();
        }

        public static void ClearState(GameState toState = GameState.Playing)
        {
            m_stateRecord.Clear();
            m_currentState = toState;
        }
        
        public static void SwitchState(GameState state, bool recordState = true)
        {
            if (recordState)
                m_stateRecord.Push(m_currentState);
            m_currentState = state;

            switch (state)
            {
                case GameState.Playing:
                    InputHandler.SwitchToPlayer();
                    break;
                case GameState.UI:
                    InputHandler.SwitchToUI();
                    break;
            }
            
            SwitchStateEvent?.Invoke(state);
        }

        public static void GameOver(Texture gameOverSprite)
        {
            GameUiManager.Instance.ShowPanel(GameUiManager.Instance.failedPanel);
            
            // 传入死亡原因
            GameUiManager.Instance.failedPanel.ChangeImage(gameOverSprite);
        }

        public static void ResetPlayer()
        {
            player.position = originalPlayerPos;
            var playerComp = player.GetComponent<Player>();
            playerComp.StateMachine.ChangeState(playerComp.IdleState);
        }
    }
}
