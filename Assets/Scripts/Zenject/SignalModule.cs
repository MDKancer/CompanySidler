using Zenject;
using Zenject_Signals;

namespace Zenject_Initializer
{
    public class SignalModule : Installer<SignalModule>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<CustomerSignals>().OptionalSubscriber();
            Container.DeclareSignal<GameStateSignal>().OptionalSubscriber();
            Container.DeclareSignal<ShowBuildingData>().OptionalSubscriber();
            Container.DeclareSignal<UpdateUIWindow>().OptionalSubscriber();
            Container.DeclareSignal<ApplyEmployeeSignal>().OptionalSubscriber();
            Container.DeclareSignal<QuitEmployeeSignal>().OptionalSubscriber();
            Container.DeclareSignal<StartProjectSignal>().OptionalSubscriber();
            Container.DeclareSignal<CloseProjectSignal>().OptionalSubscriber();
            Container.DeclareSignal<MonoBehaviourSignal>().OptionalSubscriber();
        }
    }
}