using Zenject;

namespace Zenject_Initializer
{
    public class SharedContextInstaller : MonoInstaller<SharedContextInstaller>
    {
        public override void InstallBindings()
        {
            SignalModule.Install(Container);
        }
    }
}