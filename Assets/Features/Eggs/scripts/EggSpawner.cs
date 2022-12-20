using System.Collections.Generic;
using Zenject;

namespace Features.Egg
{
    public class EggSpawner
    {
        private readonly EggController.Pool _controllerPool;
        private readonly SignalBus _signalBus;

        private Dictionary<int, EggController> _eggs = new Dictionary<int, EggController>();

        public EggSpawner(
            EggController.Pool controllerPool,
            SignalBus signalBus)          
        {
            _controllerPool = controllerPool;
            _signalBus = signalBus;            
            _signalBus.Subscribe<EndOfEggWaySignal>(EndOfEggWaySignalHandler);
        }

        public void Spawn(int id, float difficulty)
        {
            var item = _controllerPool.Spawn(new EggControllerProtocol(id, difficulty));
            if(_eggs.ContainsKey(item.ID))
            {
                _eggs.Remove(item.ID);
            }
            else
            {
                _eggs.Add(item.ID, item);
            }
        }

        public void ClearAll()
        {
            var list = new List<int>(_eggs.Keys);
            foreach (var key in list)
            {
                _controllerPool.Despawn(_eggs[key]);
                _eggs.Remove(key);
            }            
        }

        private void EndOfEggWaySignalHandler(EndOfEggWaySignal signal)
        {            
            if(_eggs.ContainsKey(signal.EggController.ID))
            {
                _eggs.Remove(signal.EggController.ID);
            }
            _controllerPool.Despawn(signal.EggController);
        }
    }
}