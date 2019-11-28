using BootManager;
using Enums;
using UnityEngine;

[CreateAssetMenu (fileName = "UIEvents", menuName = "ScriptableObjects/UIEvents", order = 2)]
public class UIEvents : ScriptableObject
{

    public void PlayGame()
    {
        Boot.gameStateController.CurrentState = GameState.GAME;
        Boot.sceneManager.GoTo(Scenes.GAME);  
    }
    public void StartGame()
    {
        //first
        Boot.gameStateController.CurrentState = GameState.PREGAME;
        //second
        Boot.sceneManager.GoTo(Scenes.PREGAME);
    }
    
    public void Exit()
    {
        //first
        Boot.gameStateController.CurrentState = GameState.EXIT;
        //second
        Application.Quit();
    }
}
