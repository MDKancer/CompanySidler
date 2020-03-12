using BootManager;
using Enums;
using GameCloud;
using SpawnManager;
using StateMachine;
using UnityEngine;
using Zenject;
using SceneController;
using Zenject_Signals;


namespace Zenject_Initializer
{
    [CreateAssetMenu(fileName = "GlobalBindings", menuName = "Installers/GlobalBindings")]
    public class GlobalBindings : ScriptableObjectInstaller<GlobalBindings>
    {
        
        public override void InstallBindings()
        {
            // here will be only the Scene Bindings
            
            CompanyData companyData = Resources.LoadAll<CompanyData>("SO")[0];

            Container.Bind<BootController>().AsSingle().NonLazy();
            Container.Bind<FinalStateManager>().AsSingle().NonLazy();
            Container.Bind<Container>().AsSingle().NonLazy();
            Container.Bind<StateController<GameState>>().AsSingle().NonLazy();
            Container.Bind<StateController<RunTimeState>>().AsSingle().NonLazy();
            Container.Bind<SpawnController>().AsSingle().NonLazy();
            Container.Bind<CompanyData>().FromScriptableObject(companyData).AsSingle().NonLazy();
            Container.Bind<MonoBehaviourSignal>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
            Container.Bind<SceneManager>().AsSingle().NonLazy();
            
        }
    }
}