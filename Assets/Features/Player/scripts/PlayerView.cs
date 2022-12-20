using UnityEngine;

namespace Features.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Transform nest;

        public void SetViewDirection(bool rightDirection)
        {
            spriteRenderer.flipX = rightDirection;
        }

        public void SetNestPosition(Vector3 position)
        {
            nest.position = position;
        }
    }
}