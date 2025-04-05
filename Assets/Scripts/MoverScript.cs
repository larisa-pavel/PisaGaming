using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MoverScript : MonoBehaviour
{
    public Vector3 start;
    public Vector3 end;
    public float speed = 3f;

    private CharacterController controller;

    public Vector3 speedVector;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 direction = (end - transform.position).normalized;
        speedVector = direction * speed;
        controller.Move(speed * Time.deltaTime * direction);

        // Switch direction when close to the target
        if (Vector3.Distance(transform.position, end) < 6f)
        {
            end = end + Vector3.down * 20f;
        }
        if (transform.position.y < -20f)
            Destroy(transform.parent.gameObject);
    }
}