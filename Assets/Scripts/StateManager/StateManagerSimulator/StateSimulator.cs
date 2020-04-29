using AudioManager;
using Container;
using Enums;
using Sirenix.OdinInspector;
using StateManager.States.GameStates.Template;
using UnityEngine;
using VideoManager;
using Zenject;

namespace StateManager.StateManagerSimulator
{
    [TypeInfoBox("That Component Simulate the State Machine \n" +
                 "that mean , that the simulator change automaticaly the states until current Scene")]
    public class StateSimulator : MonoBehaviour
    {
        private static StateSimulator _stateSimulator;
        [SerializeField,
         BoxGroup("Simulation"),
         GUIColor(0.3f, 0.8f, 0.8f, 1f),
         InfoBox("Please don't use in the Intro Scene")]
        private bool simulate = false;
        [SerializeField,
         BoxGroup("Simulation"),
         GUIColor(0.3f, 0.8f, 0.8f, 1f),
         EnumToggleButtons,
         InfoBox("Please set the Current Scene")]
        private Scenes scenes;

        private Cloud cloud;
        private StateMachineClass<AState> stateMachineClass;
        private AudioController audioController;
        private VideoController videoController;

        [Inject]
        private void Init(Cloud cloud,StateMachineClass<AState> stateMachineClass,AudioController audioController, VideoController videoController)
        {
            this.cloud = cloud;
            this.stateMachineClass = stateMachineClass;
            this.audioController = audioController;
            this.videoController = videoController;
        }
        private void Awake()
        {
            if(simulate)
            {
                //in Intro State stead
                //load the all Resources in to Cloud
                cloud.LoadAllResources();
                audioController.SetImportData();
                videoController.SetImportData();
            }
        }

        private void Start()
        {
            if (simulate)
            {
                // 0                                 :     first game state is Intro
                // cloud.GetGameStates.Count-1       :     last game state is Loading
                // we don't need this game states to simulate
                for (int i = 1; i < cloud.GetGameStates.Count-1; i++)
                {
                    stateMachineClass.CurrentState = cloud.GetGameStates[i];
                    
                    if (cloud.GetGameState(scenes) == stateMachineClass.CurrentState) return;
                }
            }
        }
    }
}