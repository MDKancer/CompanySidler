using System;
using System.Collections;
using Enums;
using StateMachine;
using StateMachine.States;
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
        private StateMachineClass<AState> stateMachineClass;
        
        private Scenes lastScene;
        private Scenes currentScene;
        private Scenes targetScene;
        private AsyncOperation loadingSceneOperation;
        private AsyncOperation targetSceneOperation;
        private MonoBehaviour monoBehaviour;
        
        [Inject]
        private void Init(SignalBus signalBus,
            StateController<GameState> gameStateController,
            MonoBehaviourSignal monoBehaviourSignal,
            StateMachineClass<AState> stateMachineClass)
        {
            this.signalBus = signalBus;
            this.gameStateController = gameStateController;
            this.stateMachineClass = stateMachineClass;
            this.monoBehaviour = monoBehaviourSignal;
        }


        public void GoTo(Scenes scenes)
        {

            //the scene what need to be loaded
            TargetScene = scenes;
            //the loading scene what loads the target scene
            CurrentScene = Scenes.LOADING;
            
            loadingSceneOperation =  UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(scenes.ToString());
            //when the loading Scene is completed 
            loadingSceneOperation.completed += LoadingSceneCompleted;

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
        public Scenes TargetScene
        {
            get => targetScene;
            private set => targetScene = value;
        }
        public Scenes LastScene
        {
            get =>lastScene; 
            private set => lastScene = value;
        }

        public void SwitchToLastScene()
        {
            
            Scenes temp = CurrentScene;
            CurrentScene = LastScene;
            LastScene = temp;
            
            GoTo(CurrentScene);
        }

        public float SceneProgress => targetSceneOperation.progress;
        private void LoadingSceneCompleted(AsyncOperation asyncOperation)
        {
          // Debug.Log($"{CurrentScene} : progress {asyncOperation.progress*100f} %  is Done {nextScene.isDone}");
            GameState gameState = (GameState) Enum.GetValues(typeof(GameState)).GetValue((int) CurrentScene);
            
            targetSceneOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(targetScene.ToString());
        }
    }
}