using System;
using Enums;
using GameCloud;
using SceneController;
using StateMachine;
using StateMachine.States;
using UnityEngine;
using Zenject;

public class BootInit : MonoBehaviour
{
    public bool simulateGame;
    public GameState gameState;
    private SignalBus signalBus;
    private Container container;
    private SceneManager sceneManager;
    private StateController<GameState> gameStateController;
    private StateMachineClass<AState> stateMachineClass;
    
    [Inject]
    private void Init(SignalBus signalBus,
        Container container,
        SceneManager sceneManager,
        StateController<GameState> gameStateController,
        StateMachineClass<AState> stateMachineClass)
    {
        this.signalBus = signalBus;
        this.container = container;
        this.sceneManager = sceneManager;
        this.gameStateController = gameStateController;
        this.stateMachineClass = stateMachineClass;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            stateMachineClass.CurrentState = new Intro();

        }
        catch (Exception e)
        {
            Debug.Log($" The {this}.Init method wasn't executed");
            Debug.LogError(e);
            throw;
        }
    }
}
