using Zenject;
using System;

namespace Features.Player
{
    public class PlayerViewCreateCommand : IDisposable
    {
        private readonly IInstantiator _instantiator;
        private readonly PlayerAnimationHandler _playerAnimationHandler;
        public PlayerViewCreateCommand(IInstantiator instantiator, PlayerAnimationHandler playerAnimationHandler)
        {
            _instantiator = instantiator;
            _playerAnimationHandler = playerAnimationHandler;
        }        

        public void Do()
        {
            var view = _instantiator.InstantiatePrefabResourceForComponent<PlayerView>("Player/PlayerPrefab");
            _playerAnimationHandler.Start(view);
        }

        public void Dispose()
        {

        }
    }
}