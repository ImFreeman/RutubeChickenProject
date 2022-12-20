using Zenject;

namespace Features.Player
{
    public class PlayerInstaller : Installer<PlayerInstaller>
    {        
        public override void InstallBindings()
        {
            Container.DeclareSignal<PositionUpdateSignal>();
            Container.DeclareSignal<ScoreChangeSignal>();
            Container.DeclareSignal<HealthChangeSignal>();

            Container
                .Bind<PlayerConfig>()
                .FromScriptableObjectResource("Player/PlayerConfig")
                .AsSingle();
            Container
                .Bind<PlayerInputHandler>()
                .AsSingle()
                .NonLazy();
            Container
                .Bind<PlayerAnimationHandler>()
                .AsSingle();
            Container
                .Bind<PlayerDataHandler>()
                .AsSingle();
        }
    }
}