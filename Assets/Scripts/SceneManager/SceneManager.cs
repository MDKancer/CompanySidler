
using System;
using System.Collections;
using System.Linq;
using Enums;
using PlayerView;
using StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Zenject_Signals;

namespace SceneController
{
    public class SceneManager
    {
        private SignalBus signalBus;
        private StateController<GameState> gameStateController;
        
        private Scenes lastScene;
        private Scenes currentScene;
        private AsyncOperation nextScene;
        private MonoBehaviour monoBehaviour;
        
        [Inject]
        private void Init(SignalBus signalBus,
            StateController<GameState> gameStateController,MonoBehaviourSignal monoBehaviourSignal)
        {
            this.signalBus = signalBus;
            this.gameStateController = gameStateController;
            this.monoBehaviour = monoBehaviourSignal;
        }


        public void GoTo(Scenes scenes)
        {
            CurrentScene = scenes;
            nextScene =  UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scenes.ToString());
            
            nextScene.completed += Debuging;
            IEnumerator gamestate = SetGameState();
            monoBehaviour.StartCoroutine(gamestate);
        }

        public Scenes CurrentScene
        {
            get => currentScene;
            private set
            {
                lastScene = currentScene;
                currentScene = value;
            }
        }

        public Scenes LastScene => lastScene;

        public void SwitchToLastScene()
        {
            
            Scenes temp = currentScene;
            currentScene = lastScene;
            lastScene = temp;
            UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene.ToString(),LoadSceneMode.Single);
        }

        private IEnumerator SetGameState()
        {
            while (nextScene.progress < 0.9f)
            {
//                Debug.Log($"{CurrentScene} loading {nextScene.progress} %");
                yield return null;
            }
        }

        private void Debuging(AsyncOperation asyncOperation)
        {
          // Debug.Log($"{CurrentScene} : progress {asyncOperation.progress*100f} %  is Done {nextScene.isDone}");
            GameState gameState = (GameState) Enum.GetValues(typeof(GameState)).GetValue((int) CurrentScene);
            gameStateController.CurrentState = gameState;
            //Debug.Log($"is loaded {gameStateController.CurrentState }");
            signalBus.Fire(new GameStateSignal
            {
                state = gameState
            });
            
            
        }
    }
}