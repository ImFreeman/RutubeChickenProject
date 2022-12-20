using UnityEngine;
using Zenject;

namespace Features.Egg
{    
    public class EggSpawnManager : ITickable
    {
        private readonly System.Random _random;
        private readonly SignalBus _signalBus;
        private readonly TickableManager _tickableManager;
        private readonly EggSpawner _eggSpawner;        

        private readonly int _numberOfWays;

        private readonly float _maxDifficulty;
        private readonly float _deltaDifficulty;
        private readonly float _startDifficulty;

        private readonly float _minSpawnTimeOffset;
        private readonly float _deltaSpawnTimeOffset;
        private readonly float _startSpawnTimeOffset;

        private readonly int _eggsForNextLevel;

        private int _currentEggCount;
        private float _currentTime;
        private float _currentDifficulty;
        private float _currentSpawnTimeOffset;


        public EggSpawnManager(
            EggSpawner eggSpawner,
            EggDropConfig eggDropConfig,
            SignalBus signalBus,
            EggSpawnConfig config,
            TickableManager tickableManager)
        {
            _numberOfWays = eggDropConfig.Waypoints.Length;
            _eggSpawner = eggSpawner;
            _random = new System.Random();
            _signalBus = signalBus;

            _maxDifficulty = config.MaxDifficulty;
            _deltaDifficulty = config.DeltaDifficulty;
            _startDifficulty = config.StartDifficulty;

            _minSpawnTimeOffset = config.MinSpawnTimeOffset;
            _deltaSpawnTimeOffset = config.DeltaSpawnTimeOffset;
            _startSpawnTimeOffset = config.StartSpawnTimeOffset;

            _eggsForNextLevel = config.EggsForNextLevel;
            _tickableManager = tickableManager;
            _signalBus.Subscribe<EndOfEggWaySignal>(EndOfEggWaySignalHandler);
        }

        public void Start()
        {
            _tickableManager.Add(this);
            _currentTime = 0f;
            _currentDifficulty = _startDifficulty;
            _currentSpawnTimeOffset = _startSpawnTimeOffset;
            _currentEggCount = 0;
            
        }

        public void Stop()
        {
            _tickableManager.Remove(this);
            _eggSpawner.ClearAll();            
        }

        public void Tick()
        {
            _currentTime += Time.deltaTime;
            if(_currentTime >= _currentSpawnTimeOffset)
            {
                _eggSpawner.Spawn(_random.Next(0, _numberOfWays), _currentDifficulty);
                _currentTime = 0;
            }
        }

        private void EndOfEggWaySignalHandler(EndOfEggWaySignal signal)
        {            
            _currentEggCount++;
            if(_currentEggCount >= _eggsForNextLevel)
            {
                _currentEggCount = 0;
                _currentDifficulty = Mathf.Max(_maxDifficulty, _currentDifficulty - _deltaDifficulty);
                _currentSpawnTimeOffset = Mathf.Max(_minSpawnTimeOffset, _currentSpawnTimeOffset - _deltaSpawnTimeOffset);
            }
        }
    }
}