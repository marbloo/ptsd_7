using UnityEngine;

public class FloatingCubeController : MonoBehaviour
{
    public float speed = 2f;
    public float radius = 3f;

    private Vector3 centerPosition;
    private float angle;

    void Start()
    {
        centerPosition = transform.position;
    }

    void Update()
    {
        angle += speed * Time.deltaTime; // Increment the angle based on speed and time
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;
        transform.position = centerPosition + new Vector3(x, y, 0);
    }
}
