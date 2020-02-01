using DapperDino.GGJ2020.DataTypes;
using UnityEngine;

namespace DapperDino.GGJ2020.Movements
{
    public class PlayerInputConverter : MonoBehaviour
    {

        [SerializeField] private Transform characterTransform = null;
        [SerializeField] private UnityMovemementDataEvent OnMovementDataProcessed = new UnityMovemementDataEvent();

        private Transform cameraTransform;

        private void Start() => cameraTransform = Camera.main.transform;

        public void Process(SerializableVector2 input)
        {
            OnMovementDataProcessed.Invoke(new MovementData
            {
                Input = new Vector3(input.x, input.y),
                CameraTransform = cameraTransform,
                CharacterTransform = characterTransform
            });
        }
    }
}
