using DapperDino.GGJ2020.DataTypes;
using DapperDino.GGJ2020.Movements;
using UnityEngine;

namespace DapperDino.GGJ2020.Parts
{
    public class LegsMovementSetupHandler : MonoBehaviour
    {
        [SerializeField] private PartBehaviour partBehaviour = null;
        [SerializeField] private Transform characterTransform = null;
        [SerializeField] private MovementBehaviour movementBehaviour = null;

        private Transform cameraTransform;
        private IMovementProcessor movementProcessor;

        private void Start() => cameraTransform = Camera.main.transform;

        public void Move(SerializableVector2 input)
        {
            if (movementProcessor == null) { return; }

            movementProcessor.Process(new MovementData
            {
                Input = new Vector3(input.x, input.y),
                CameraTransform = cameraTransform,
                CharacterTransform = characterTransform
            });
        }

        public void SetUp()
        {
            if (!partBehaviour.PartInstance.TryGetComponent<MovementInputBehaviour>(out var movementInputBehaviour))
            {
                return;
            }

            if (!partBehaviour.PartInstance.TryGetComponent<IMovementProcessor>(out var movementProcessor))
            {
                return;
            }

            movementInputBehaviour.SetMovementBehaviour(movementBehaviour);

            this.movementProcessor = movementProcessor;
        }

        public void Clear() => movementProcessor = null;
    }
}
