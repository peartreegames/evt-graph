using EvtGraph;
using UnityEngine;
using UnityEngine.InputSystem;


    public class PlayerController : MonoBehaviour
    {
        public float speed = 5f;
        
        [SerializeField] private CharacterController controller;
        
        public PlayerInput input;
        public Vector2 movement;
        private bool _isMoving;
        private Camera _camera;
        [SerializeField] private float turnSmoothTime = 0.1f;
        private float turnSmoothVelocity;
        
        protected void Awake()
        {
            _camera = Camera.main;
            overlaps = new Collider[1];
            input = GetComponent<PlayerInput>();
            input.actions.FindActionMap("Player").Enable();
        }

        private void Update()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            var move = new Vector3(movement.x, 0f, movement.y).normalized;
            _isMoving = move.sqrMagnitude > Mathf.Epsilon;
            if (_isMoving)
            {
                var target = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + _camera.transform.eulerAngles.y;
                var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, target, ref turnSmoothVelocity,
                    turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                move = Quaternion.Euler(0f, target, 0f) * Vector3.forward;
            }
            controller.SimpleMove(move * speed);
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            movement = context.ReadValue<Vector2>();
        }

        private Collider[] overlaps;
        public void OnFire(InputAction.CallbackContext context)
        {
            if (!context.started) return;
            var trans = transform;
            var size = Physics.OverlapBoxNonAlloc(trans.position + trans.forward + Vector3.up, Vector3.one * 0.5f, overlaps, trans.rotation, LayerMask.GetMask("Interactable"));
            if (size == 0) return;
            for (int i = 0; i < size; i++)
            {
                if (overlaps[i].TryGetComponent<EvtInteractTrigger>(out var interactable)) interactable.Interact(gameObject);
            }
        }
    }