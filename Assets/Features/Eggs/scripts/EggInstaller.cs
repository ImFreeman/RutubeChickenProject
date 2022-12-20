using Zenject;

namespace Features.Egg
{
    public class EggInstaller : Installer<EggInstaller>
    {
        public override void InstallBindings()
        {
            Container.DeclareSignal<EndOfEggWaySignal>();
            Container
                .Bind<EggDropConfig>()
                .FromScriptableObjectResource("Eggs/EggDropConfig")
                .AsSingle();
            Container
                .Bind<EggSpawnConfig>()
                .FromScriptableObjectResource("Eggs/EggSpawnConfig")
                .AsSingle();
            Container
                .BindMemoryPool<EggView, EggView.Pool>()
                .WithInitialSize(8)
                .FromComponentInNewPrefabResource("Eggs/EggPrefab")
                .UnderTransformGroup("Eggs");
            Container
                .BindMemoryPool<EggController, EggController.Pool>()
                .WithInitialSize(8);
            Container
                .Bind<EggSpawner>()
                .AsSingle();
            Container
                .Bind<EggSpawnManager>()
                .AsSingle();
        }
    }
}