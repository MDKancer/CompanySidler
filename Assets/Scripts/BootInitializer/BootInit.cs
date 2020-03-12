using Enums;
using SceneController;
using StateMachine;
using UnityEngine;
using Zenject;
using Zenject_Signals;

public class BootInit : MonoBehaviour
{
    public bool simulateGame;
    public GameState gameState;
    private SignalBus signalBus;
    private SceneManager sceneManager;
    private StateController<GameState> gameStateController;
    
    [Inject]
    private void Init(SignalBus signalBus,SceneManager sceneManager,StateController<GameState> gameStateController)
    {
        this.signalBus = signalBus;
        this.sceneManager = sceneManager;
        this.gameStateController = gameStateController;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        gameStateController.CurrentState = gameState;
        this.signalBus.Fire(new GameStateSignal
        {
            state =  gameState
        });
        sceneManager.GoTo(Scenes.MAIN_MENU);
    }
}
