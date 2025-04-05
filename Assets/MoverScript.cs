using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MoverScript : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 3f;

    private CharacterController controller;
    private Vector3 target;

    public Vector3 speedVector;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        target = pointB.position;
    }

    void Update()
    {
        Vector3 direction = (target - transform.position).normalized;
        speedVector = direction * speed;
        controller.Move(direction * speed * Time.deltaTime);

        // Switch direction when close to the target
        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = (target == pointA.position) ? pointB.position : pointA.position;
        }
    }
}