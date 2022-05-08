using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed = 3F;

    public float rotationSpeed = 360F;

    CharacterController characterController;
    Animator animator;
    // private static readonly int Speed = Animator.StringToHash("Speed");

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(
            Input.GetAxis("Horizontal"),
            0,
            Input.GetAxis("Vertical")
        );

        if (direction.sqrMagnitude > 0.01F)
        {
            Vector3 forward = Vector3.Slerp(
                transform.forward,
                direction,
                rotationSpeed * Time.deltaTime / Vector3.Angle(
                    transform.forward,
                    direction
                    )
                );
            
            transform.LookAt(transform.position + forward);
        }

        characterController.Move(direction * moveSpeed * Time.deltaTime);
        
        animator.SetFloat("Speed", characterController.velocity.magnitude);

        if (GameObject.FindGameObjectsWithTag("Dot").Length == 0)
        {
            Application.LoadLevel("Win");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Dot":
                Destroy(other.gameObject);
                break;
            case "Enemy":
                Application.LoadLevel("Lose");
                break;
        }
    }
}
