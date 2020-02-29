using BootManager;
using Enums;
using SceneController;
using StateMachine;
using UnityEngine;
using Zenject;

//[CreateAssetMenu (fileName = "UIEvents", menuName = "ScriptableObjects/UIEvents", order = 2)]
public class UIEvents : MonoBehaviour
{
    [Inject]
    public StateController<GameState> gameStateController;
    [Inject]
    public SceneManager sceneManager;
    public void PlayGame()
    {
        gameStateController.CurrentState = GameState.GAME;
        sceneManager.GoTo(Scenes.GAME);  
    }
    public void StartGame()
    {
        //first
        gameStateController.CurrentState = GameState.PREGAME;
        //second
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
