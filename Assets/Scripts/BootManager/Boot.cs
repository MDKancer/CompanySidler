﻿using Constants;
using GameCloud;
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
        /// <summary>
        /// Hier wird alles Inizialisiert was man im Awake braucht
        /// </summary>
        public void Booting()
        {
          container.LoadAllResources();
          
          spawnController.InitialWaveSpawn();
          
        }
        /// <summary>
        /// Hier wird alles Inizialisiert was man am Start braucht.
        /// </summary>
        public void AllBegin()
        {
            
        }
    }
}