using Features.Player;
using UnityEngine;
using Zenject;

namespace Features.Egg
{
    public readonly struct EndOfEggWaySignal
    {
        public readonly EggController EggController;
        public readonly PositionVertical PositionVertical;
        public readonly PositionHorizontal PositionHorizontal;        

        public EndOfEggWaySignal(
            PositionVertical positionVertical,
            PositionHorizontal positionHorizontal,
            EggController eggController)
        {
            PositionVertical = positionVertical;
            PositionHorizontal = positionHorizontal;
            EggController = eggController;            
        }
    }
    public readonly struct EggControllerProtocol
    {
        public readonly int WayID;
        public readonly float Difficulty;        
        public EggControllerProtocol(int wayID, float difficulty)
        {
            WayID = wayID;
            Difficulty = difficulty;            
        }
    }
    public class EggController : ITickable
    {
        public int ID => _view.GetInstanceID();

        private readonly EggDropConfig _config;
        private readonly EggView.Pool _viewPool;        
        private readonly TickableManager _tickableManager;
        private readonly SignalBus _signalBus;       

        private EggView _view;                
        private Waypoint[] _currentWay;
        private float _currentDifficulty;
        private int _currentModelID;
        private int _currentID;
        private float _currentTime;        

        public EggController(
            EggDropConfig config,
            EggView.Pool viewPool,
            TickableManager tickableManager,
            SignalBus signalBus)
        {
            _config = config;
            _viewPool = viewPool;
            _tickableManager = tickableManager;
            _signalBus = signalBus;
        }

        private void Init(EggControllerProtocol protocol)
        {            
            _currentModelID = protocol.WayID;
            _currentWay = _config.Waypoints[_currentModelID].Waypoints;
            _currentDifficulty = protocol.Difficulty;
            _currentID = 0;
            _currentTime = 0f;
            _view = _viewPool.Spawn(new EggViewProtocol(_currentWay[0].Position, _currentWay[0].Rotation));
            _tickableManager.Add(this);
        }

        private void Despawn()
        {
            _tickableManager.Remove(this);
            _viewPool.Despawn(_view);
        }

        public void Tick()
        {
            _currentTime += Time.deltaTime;
            if(_currentTime >= _currentDifficulty)
            {
                _currentID++;
                if(_currentID < _currentWay.Length)
                {
                    _view.SetPosition(_currentWay[_currentID].Position);
                    _view.SetRotation(_currentWay[_currentID].Rotation);
                    _currentTime = 0f;
                }
                else
                {
                    var model = _config.Waypoints[_currentModelID];
                    _signalBus.Fire(new EndOfEggWaySignal(
                        model.PositionVertical,
                        model.PositionHorizontal,
                        this));
                }
            }
        }
        
        public class Pool : MemoryPool<EggControllerProtocol, EggController>
        {
            protected override void Reinitialize(EggControllerProtocol p1, EggController item)
            {
                item.Init(p1);
            }

            protected override void OnDespawned(EggController item)
            {
                item.Despawn();
            }
        }
    }
}