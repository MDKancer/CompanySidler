using Enums;
using GameCloud;
using SceneController;
using SpawnManager;
using StateMachine;
using UnityEngine;

namespace BootManager
{
    public class ReferenceService
    {
        private static ReferenceService _referenceService;
        
        public readonly MonoBehaviour monobehaviour;
        public readonly Container container;
        public readonly StateController<GameState> gameStateController;
        public readonly StateController<RunTimeState> runtimeStateController;
        public readonly SpawnController spawnController;
        public readonly SceneManager sceneManager;
        
        public readonly CompanyData companyData;
        public readonly GameState gameState;
        
        public ReferenceService(MonoBehaviour monobehaviour)
        {
            if(_referenceService == null)
            {        
                this.monobehaviour = monobehaviour;
                container = new Container();
                gameStateController = new StateController<GameState>();
                runtimeStateController = new StateController<RunTimeState>();
                spawnController = new SpawnController();
                sceneManager = new SceneManager();
            }
            else
            {
                _referenceService = ReferenceService._referenceService;
                this.monobehaviour = ReferenceService._referenceService.monobehaviour;
                container = ReferenceService._referenceService.container;
                gameStateController = ReferenceService._referenceService.gameStateController;
                runtimeStateController = ReferenceService._referenceService.runtimeStateController;
                spawnController = ReferenceService._referenceService.spawnController;
                sceneManager = ReferenceService._referenceService.sceneManager;
            }
        }
    }
}