using UnityEngine;

public class SphereScript : MonoBehaviour
{
    private Rigidbody rb;
    [HideInInspector] public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        SphereMoving();
    }

    private void SphereMoving()
    {
        float movingX = Input.GetAxis("Horizontal") * speed;
        float movingZ = Input.GetAxis("Vertical") * speed;

        Vector3 movement = new Vector3(movingX, 0, movingZ);
        rb.AddForce(movement * speed);
    }
}
