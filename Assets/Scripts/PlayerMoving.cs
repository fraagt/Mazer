using UnityEngine;

public class PlayerMoving : MonoBehaviour
{
    public float speed;


    private Rigidbody2D _rigidbody;

    private float horizontalMove = 0f;
    private float verticalMove = 0f;

    void Start()
    {
        horizontalMove = 0f;
        verticalMove = 0f;

        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = Vector2.zero;
    }

    void Update()
    {
        verticalMove = Input.GetAxisRaw("Vertical") * speed;
        horizontalMove = Input.GetAxisRaw("Horizontal") * speed;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity += new Vector2(horizontalMove, verticalMove);
    }
}
