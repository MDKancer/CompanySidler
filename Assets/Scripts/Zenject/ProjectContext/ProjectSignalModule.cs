using Zenject.ProjectContext.Signals;
using Zenject.SceneContext.Signals;

namespace Zenject.ProjectContext
{
    public class ProjectSignalModule : Installer<ProjectSignalModule>
    {
        /// <summary>
        /// <remarks>ProjectContext Prefab should be in Resource Folder</remarks>
        /// <example>Assets/Resources/ProjectContext.asset</example>
        /// </summary>
        public override void InstallBindings()
        {
            //Here will be only the Project / Global Signals declared
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<ShowBuildingData>().OptionalSubscriber();
            Container.DeclareSignal<CurrentCompanySignal>().OptionalSubscriber();
            Container.DeclareSignal<GameStateSignal>().OptionalSubscriber();
        }
    }
}