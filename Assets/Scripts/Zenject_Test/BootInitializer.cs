using Enums;
using GameCloud;
using SceneController;
using SpawnManager;
using StateMachine;
using UnityEngine;
using Zenject;

namespace Zenject_Initializer
{
    [CreateAssetMenu(fileName = "GameInstaller", menuName = "Installers/GameInstaller")]
    public class BootInitializer : ScriptableObjectInstaller<BootInitializer>
    {
        public override void InstallBindings()
        {

            Container.Bind<Container>().AsSingle().NonLazy();
            Container.Bind<StateController<GameState>>().AsSingle().NonLazy();
            Container.Bind<StateController<RunTimeState>>().AsSingle().NonLazy();
            Container.Bind<SpawnController>().AsSingle().NonLazy();
            Container.Bind<SceneManager>().AsSingle().NonLazy();
            
        }
    }
}