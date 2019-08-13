using System;
using BootManager;
using Constants;
using UnityEngine;

public class UIDispatcher : MonoBehaviour
{
        // Es ist nur temporär um zu wissen ob alles gut läuft.
        public  GameState currentGameState;

        private GameObject creditPanel;
        private void Awake()
        {
                creditPanel = GameObject.Find("CreditsPanel");
                creditPanel.SetActive(false);
        }

        private void Update()
        {
                CheckCurrentState();
        }

        public void PlayGame()
        {
                //first
                Boot.gameStateController.CurrentState = GameState.GAME;
                //second
                Boot.sceneManager.GoTo(Scenes.GAME);
        }

        public void Options()
        {
        }

        public void Credits()
        {
                creditPanel.SetActive(true);
        }

        public void Exit()
        {
                //first
                Boot.gameStateController.CurrentState = GameState.EXIT;
                //second
                Application.Quit();
        }
        
        /// <summary>
        /// Es ist nur temporär um zu wissen ob alles gut Läuft.
        /// </summary>
        private void CheckCurrentState()
        {
                if(Boot.gameStateController != null)
                {
                        if (currentGameState != Boot.gameStateController.CurrentState)
                        {
                                currentGameState = Boot.gameStateController.CurrentState;
                        }
                }
        }
}
