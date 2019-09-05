﻿using System;
 using Constants;
using GameCloud;
 using InputManager;
 using SceneController;
 using SpawnManager;
 using StateMachine;
using UnityEngine;

namespace BootManager
{
    public class Boot : MonoBehaviour
    {
        public static Boot boot_Instance { get; private set; }

        public static MonoBehaviour monobehaviour;
        public static Container container;
        public static StateController<GameState> gameStateController;
        public static StateController<RunTimeState> runtimeStateController;
        public static SpawnController spawnController;
        public static SceneManager sceneManager;
        
        private  InputController inputController;
        

        /// <summary>
        /// Hier wird alles referenzen Instanziert. und den Boot als Singelton gemacht
        /// </summary>
        void Awake()
        {
            if (boot_Instance == null)
            {
                boot_Instance = this;
                monobehaviour = this;
                container = new Container();
                gameStateController = new StateController<GameState>();
                runtimeStateController = new StateController<RunTimeState>();
                spawnController = new SpawnController();
                sceneManager = new SceneManager();
                inputController = new InputController();
                
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
            Booting();
            
        }
        void Start()
        {
            AllBegin();
        }

        private void Update()
        {
            inputController.Do();
        }

        /// <summary>
        /// Hier wird alles Inizialisiert was man im Awake braucht
        /// </summary>
        private void Booting()
        {
          runtimeStateController.CurrentState = RunTimeState.NONE;
          
          container.LoadAllResources();
          
          spawnController.InitialWaveSpawn();
          
        }
        /// <summary>
        /// Hier wird alles Inizialisiert was man am Start braucht.
        /// </summary>
        private void AllBegin()
        {
            if (gameStateController.CurrentState == GameState.GAME)
            {
                runtimeStateController.CurrentState = RunTimeState.PLAYING;
            }
        }

        private void Run()
        {
            
        }
    }
}