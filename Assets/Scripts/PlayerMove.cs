using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    private CharacterController controller;

    private float movX;
    private float movZ;

    [SerializeField]
    [Range(0f, 100f)]
    private float movSpeed;

    [SerializeField]
    [Range(0f, 100f)]
    private float jumpSpeed;

    private Vector3 VelocityY;

    [SerializeField]
    private float gravedad = -9.8f;

    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float radio;
    [SerializeField]
    private LayerMask whatIsGround;

    public Animator animator;

    private void Awake()
    {
       controller = GetComponent<CharacterController>();
       groundCheck = transform.GetChild(2);
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, radio, whatIsGround);

        VelocityY.y += gravedad * Time.deltaTime;

        if(isGrounded && VelocityY.y <= 0)
        {
            VelocityY.y = 0;
        }

        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            VelocityY.y = Mathf.Sqrt(jumpSpeed* gravedad*-2);
            AudioManager.Instance.PlaySound("Jump");
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            VelocityY.y = -1 * (Mathf.Sqrt(jumpSpeed * gravedad * -2));
        }

        controller.Move(VelocityY*Time.deltaTime);

        movX = Input.GetAxis("Horizontal") * movSpeed * Time.deltaTime;
        movZ = Input.GetAxis("Vertical") * movSpeed * Time.deltaTime;

        Vector3 movimiento = transform.right* movX + transform.forward * movZ;

        controller.Move(movimiento);
    }

    
}
