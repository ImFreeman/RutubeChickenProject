using UnityEngine;
using Zenject;

namespace Features.Egg
{
    public readonly struct EggViewProtocol
    {
        public readonly Vector3 Position;
        public readonly Vector3 Rotation;
        public EggViewProtocol(Vector3 position, Vector3 rotation)
        {
            Position = position;
            Rotation = rotation;
        }        
    }

    public class EggView : MonoBehaviour
    {
        [SerializeField] private Transform bodyTransform;

        public void SetPosition(Vector3 position)
        {
            bodyTransform.position = position;
        }
        public void SetRotation(Vector3 rotation)
        {
            bodyTransform.rotation = new Quaternion(rotation.x, rotation.y, rotation.z, 1f);
        }

        public class Pool : MonoMemoryPool<EggViewProtocol, EggView>
        {
            protected override void Reinitialize(EggViewProtocol p1, EggView item)
            {
                item.SetPosition(p1.Position);
                item.SetRotation(p1.Rotation);
            }
        }
    }
}