using AudioManager;
using Container;
using Enums;
using InputManager;
using InputWrapper;
using So_Template;
using SpawnManager;
using StateManager;
using StateManager.States.EmploeeStates;
using StateManager.States.GameStates.Template;
using UnityEngine;
using UnityEngine.Audio;
using VideoManager;
using Zenject.ProjectContext.Signals;

namespace Zenject.ProjectContext
{
    [CreateAssetMenu(fileName = "GlobalBindings", menuName = "Installers/GlobalBindings")]
    public class GlobalBindings : ScriptableObjectInstaller<GlobalBindings>
    {
        
        public override void InstallBindings()
        {
            // here will be only the Scene Bindings
            CompanyData companyData = Resources.LoadAll<CompanyData>("SO")[0];

            Container.Bind<FinalStateManager>().AsSingle().NonLazy();
            Container.Bind<StateController<GameState>>().AsSingle().NonLazy();
            Container.Bind<StateController<RunTimeState>>().AsSingle().NonLazy();
            Container.Bind<StateMachineClass<AState>>().AsSingle().NonLazy();
            Container.Bind<InputController>().AsSingle().NonLazy();
            Container.Bind<AudioController>().AsSingle().NonLazy();
            Container.Bind<VideoController>().AsSingle().NonLazy();
            Container.Bind<InputBinding>().AsSingle().NonLazy();
            Container.Bind<CompanyData>().FromScriptableObject(companyData).AsSingle().NonLazy();
            Container.Bind<AudioMixer>().FromResource("Settings/AudioSettings").AsSingle().NonLazy();
            Container.Bind<Cloud>().AsSingle().NonLazy();
            Container.Bind<SpawnController>().AsSingle().NonLazy();
            Container.Bind<SceneManager.SceneManager>().AsSingle().NonLazy();
            Container.Bind<MonoBehaviourSignal>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }
    }
}