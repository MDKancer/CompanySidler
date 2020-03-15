using Enums;
using GameCloud;
using SceneController;
using StateMachine;
using StateMachine.States;
using UnityEngine;
using Zenject;

public class UIEvents : MonoBehaviour
{
    [Inject] public StateMachineClass<AState> stateMachineClass;
    [Inject] public SceneManager sceneManager;
    [Inject] private Container container;
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
        stateMachineClass.CurrentState = container.GetGameState(Scenes.EXIT);
        //second
    }
}
