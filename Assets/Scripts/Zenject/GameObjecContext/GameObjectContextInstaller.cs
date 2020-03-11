using Zenject;

namespace DefaultNamespace
{
    public class GameObjectContextInstaller : MonoInstaller<GameObjectContextInstaller>
    {
        public override void InstallBindings()
        {
            GameObjectSignalModule.Install(Container);
        }
    }
}