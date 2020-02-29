using Enums;
using GameCloud;
using SceneController;
using SpawnManager;
using StateMachine;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameInstaller", menuName = "Installers/GameInstaller")]
public class GameInstaller : ScriptableObjectInstaller<GameInstaller>
{
    public override void InstallBindings()
    {
        //SignalBusInstaller.Install(Container);

        Container.Bind<Container>().AsSingle().NonLazy();
        Container.Bind<StateController<GameState>>().AsSingle().NonLazy();
        Container.Bind<StateController<RunTimeState>>().AsSingle().NonLazy();
        Container.Bind<SpawnController>().AsSingle().NonLazy();
        Container.Bind<SceneManager>().AsSingle().NonLazy();
        
    }
}