using Features.Egg;
using Features.Game;
using Features.Player;
using Zenject;
public class ApplicationInstaller : MonoInstaller<ApplicationInstaller>
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        PlayerInstaller.Install(Container);
        EggInstaller.Install(Container);

        Container
            .Bind<UIService>()
            .AsSingle();

        Container
            .Bind<GameManager>()
            .AsSingle();

        Container
            .Bind<ApplicationStarter>()
            .AsSingle()
            .NonLazy();
    }
}
