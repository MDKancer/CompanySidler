using Zenject;

namespace Signals.Zenject_Test
{
    public class SignalModule : Installer<SignalModule>
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<CustomerSignals>().OptionalSubscriber();
        }
    }
}