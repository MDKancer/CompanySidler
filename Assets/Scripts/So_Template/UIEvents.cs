using Enums;
using SceneController;
using StateMachine;
using UnityEngine;
using Zenject;

public class UIEvents : MonoBehaviour
{
    [Inject]
    public StateController<GameState> gameStateController;
    [Inject]
    public SceneManager sceneManager;
    public void PlayGame()
    {
        sceneManager.GoTo(Scenes.GAME);  
    }
    public void StartGame()
    {
        sceneManager.GoTo(Scenes.PREGAME);
    }
    
    public void Exit()
    {
        //first
        gameStateController.CurrentState = GameState.EXIT;
        //second
        Application.Quit();
    }
}
