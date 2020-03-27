﻿using AudioManager;
using Enums;
using InputManager;
using So_Template;
using SpawnManager;
using StateManager.State.Template;
using UnityEngine;
using VideoManager;
using Zenject;

namespace StateManager.State
{
    public class MainMenu : AState
    {
        public override void Init(SignalBus signalBus,
            Container.Cloud cloud,
            StateController<RunTimeState> runTimeStateController,
            InputController inputController,
            AudioController audioController,
            VideoController videoController,
            MonoBehaviour monoBehaviour,
            SceneManager.SceneManager sceneManager,
            SpawnController spawnController,
            CompanyData companyData)
        {
            this.signalBus = signalBus;
            this.cloud = cloud;
            this.runTimeStateController = runTimeStateController;
            this.inputController = inputController;
            this.audioController = audioController;
            this.videoController = videoController;
            this.sceneManager = sceneManager;
            this.spawnController = spawnController;
            this.monoBehaviour = monoBehaviour;
            this.companyData = companyData;

        }

        public override void OnEnter()
        {
            audioController.SetImportData();
            videoController.SetImportData();
            //Debug.Log($"Current State On Enter {this}");
        }

        public override void OnUpdate()
        {
            //Debug.Log($"Current State On Update {this}");
        }

        public override void OnExit()
        {
            //Debug.Log($"Current State On Exit {this}");
        }

        ~MainMenu()
        {
            
        }
    }
}