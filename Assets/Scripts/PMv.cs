using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public Animator animator;

    [Header("Controles (elige uno)")]
    public bool useArrows = false; // Player 2: true | Player 1: false

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float moveX = 0f;
        float moveZ = 0f;

        if (useArrows)
        {
            // Player 2: usa las flechas
            if (Input.GetKey(KeyCode.RightArrow)) moveX = 1f;
            if (Input.GetKey(KeyCode.LeftArrow)) moveX = -1f;
            if (Input.GetKey(KeyCode.UpArrow)) moveZ = 1f;
            if (Input.GetKey(KeyCode.DownArrow)) moveZ = -1f;
        }
        else
        {
            // Player 1: usa WASD
            if (Input.GetKey(KeyCode.D)) moveX = 1f;
            if (Input.GetKey(KeyCode.A)) moveX = -1f;
            if (Input.GetKey(KeyCode.W)) moveZ = 1f;
            if (Input.GetKey(KeyCode.S)) moveZ = -1f;
        }

        Vector3 move = new Vector3(moveX, 0f, moveZ).normalized;
        controller.SimpleMove(move * speed);

        if (move.magnitude > 0.1f)
            transform.forward = move;

        if (animator != null)
            animator.SetFloat("Speed", move.magnitude);
    }
}
