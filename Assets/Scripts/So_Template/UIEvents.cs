using Enums;
using StateManager;
using StateManager.State.Template;
using UnityEngine;
using Zenject;

namespace So_Template
{
    public class UIEvents : MonoBehaviour
    {
        [Inject] public StateMachineClass<AState> stateMachineClass;
        [Inject] public SceneManager.SceneManager sceneManager;
        [Inject] private Container.Cloud cloud;
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
            stateMachineClass.CurrentState = cloud.GetGameState(Scenes.EXIT);
            //second
        }
    }
}
