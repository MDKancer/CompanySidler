namespace Zenject.GameObjecContext
{
    public class GameObjectContextInstaller : MonoInstaller<GameObjectContextInstaller>
    {
        public override void InstallBindings()
        {
            GameObjectSignalModule.Install(Container);
        }
    }
}