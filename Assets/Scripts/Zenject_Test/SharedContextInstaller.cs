using Zenject;

namespace Signals.Zenject_Test
{
    public class SharedContextInstaller : MonoInstaller<SharedContextInstaller>
    {
        public override void InstallBindings()
        {
            SignalModule.Install(Container);
        }
    }
}