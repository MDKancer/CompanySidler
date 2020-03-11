using Zenject;
using Zenject_Signals;

namespace Zenject_Initializer
{
    public class SceneSignalModule : Installer<SceneSignalModule>
    {
        public override void InstallBindings()
        {
            //Here will be only the Scene Signals declared
            
            
            Container.DeclareSignal<ApplyEmployeeSignal>().OptionalSubscriber();
            Container.DeclareSignal<QuitEmployeeSignal>().OptionalSubscriber();
            Container.DeclareSignal<CustomerSignals>().OptionalSubscriber();
            
            Container.DeclareSignal<ShowBuildingData>().OptionalSubscriber();
            Container.DeclareSignal<UpdateUIWindow>().OptionalSubscriber();
            Container.DeclareSignal<StartProjectSignal>().OptionalSubscriber();
            Container.DeclareSignal<CloseProjectSignal>().OptionalSubscriber();
            
        }
    }
}