﻿using Enums;
using StateManager;
using StateManager.States.GameStates.Template;
using UnityEngine;
using Zenject;
using Zenject.ProjectContext.Signals;

namespace SceneManager
{
    public class SceneManager
    {
        private Scenes lastScene;
        private Scenes currentScene;
        private Scenes targetScene;
        private AsyncOperation loadingSceneOperation;
        
        private Container.Cloud cloud;
        private StateMachineClass<AState> stateMachineClass;
        
        [Inject]
        private void Init(
            Container.Cloud cloud,
            MonoBehaviourSignal monoBehaviourSignal,
            StateMachineClass<AState> stateMachineClass)
        {
            this.cloud = cloud;
            this.stateMachineClass = stateMachineClass;
        }

        /// <summary>
        /// Loaded the target scene.
        /// <remarks>Before the target will be loaded,
        /// first will be a loading scene started to show the progress of the loading of target scene. </remarks>
        /// </summary>
        public void GoTo(Scenes scenes)
        {
            //the scene what need to be loaded
            TargetScene = scenes;
            //the loading scene what loads the target scene
            CurrentScene = Scenes.LOADING;
            
            loadingSceneOperation =  UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(CurrentScene.ToString());
            //when the loading Scene is completed 
            loadingSceneOperation.completed += LoadingSceneCompleted;
        }
        public Scenes CurrentScene
        {
            get => currentScene;
            private set
            {
                LastScene = currentScene;
                currentScene = value;
            }
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
        
        /// <summary>
        /// It is the progress of the scene that is loading now.
        /// </summary>
        public float SceneProgress => loadingSceneOperation.progress;
        
        /// <summary>
        /// It is the scene what it need to be loaded.
        /// <remarks>It is not the Loading scene !!!</remarks>
        /// </summary>
        private Scenes TargetScene
        {
            get => targetScene;
            set => targetScene = value;
        }
        
        /// <summary>
        /// when the loading scene is completely loaded,
        /// will be loading up the target scene. 
        /// </summary>
        private void LoadingSceneCompleted(AsyncOperation asyncOperation)
        {
            stateMachineClass.CurrentState = cloud.GetGameState(CurrentScene);
            
            loadingSceneOperation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(TargetScene.ToString());
            
            loadingSceneOperation.completed -= LoadingSceneCompleted;
            loadingSceneOperation.completed += TargetSceneCompleted;
        }
        /// <summary>
        /// when the target scene is completely loaded,
        /// will be the current state set up. 
        /// </summary>
        private void TargetSceneCompleted(AsyncOperation asyncOperation)
        {
            CurrentScene = TargetScene;
            TargetScene = Scenes.NONE;
            
            loadingSceneOperation.completed -= TargetSceneCompleted;
            stateMachineClass.CurrentState = cloud.GetGameState(CurrentScene);
        }
    }
}