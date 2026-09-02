using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Configurações de Movimento")]
    [SerializeField] private float moveSpeed = 6.0f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float gravity = -19.62f;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector3 velocity;
    private bool isGrounded;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Verifica se o personagem está tocando o chão
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Mantém o personagem assentado no chão
        }

        // Movimentação horizontal
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Aplicação da gravidade
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // Chamado automaticamente pelo Player Input (Ação Move)
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    // Chamado automaticamente pelo Player Input (Ação Jump)
    public void OnJump(InputValue value)
    {
        if (value.isPressed && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}