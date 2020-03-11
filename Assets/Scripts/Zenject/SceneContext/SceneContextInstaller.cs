using Zenject;

namespace Zenject_Initializer
{
    public class SceneContextInstaller : MonoInstaller<SceneContextInstaller>
    {
        public override void InstallBindings()
        {
            SceneSignalModule.Install(Container);
        }
    }
}