using UnityEngine;

public class PenMovement : MonoBehaviour
{
    public Vector3 forceDirection = new Vector3(1f, 0f, 0f); // Adjust for horizontal movement
    public float forceMagnitude = 5f; // Adjust if necessary
    public Vector3 torqueDirection = new Vector3(0f, 0f, 0f); // Adjust for realistic rotation
    public float torqueMagnitude = 10f; // Adjust if necessary

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.isKinematic = false;
     
    }

    public void MovePen()
    {
        Debug.Log("MovePen called.");
        rb.AddForce(forceDirection.normalized * forceMagnitude, ForceMode.Impulse);
        rb.AddTorque(torqueDirection.normalized * torqueMagnitude, ForceMode.Impulse);
        Debug.Log("Force and torque applied.");
    }
}
