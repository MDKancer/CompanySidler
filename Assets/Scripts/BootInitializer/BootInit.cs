using Enums;
using GameCloud;
using SceneController;
using StateMachine;
using StateMachine.States;
using UnityEngine;
using Zenject;
using Zenject_Signals;

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
        stateMachineClass.CurrentState = container.GetGameState(GameState.INTRO);
//        
//        gameStateController.CurrentState = gameState;
//        this.signalBus.Fire(new GameStateSignal
//        {
//            state =  gameState
//        });
//        sceneManager.GoTo(Scenes.MAIN_MENU);
    }
}
