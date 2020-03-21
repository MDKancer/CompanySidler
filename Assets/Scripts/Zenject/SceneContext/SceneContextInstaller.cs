namespace Zenject.SceneContext
{
    public class SceneContextInstaller : MonoInstaller<SceneContextInstaller>
    {
        public override void InstallBindings()
        {
            SceneSignalModule.Install(Container);
        }
    }
}