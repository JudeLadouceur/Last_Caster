using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private bool movingForward;
    private bool movingRight;
    private bool movingBack;
    private bool movingLeft;

    private Rigidbody rb;
    private InputAction moveAction;

    public float speed = 5;
    public float maximumSpeed = 15;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");

        StartCoroutine(DebugInfo());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 inputMovement = new Vector3(moveAction.ReadValue<Vector2>().x, 0, moveAction.ReadValue<Vector2>().y) * speed;
        Vector3 totalMovement = inputMovement;
        rb.AddForce(totalMovement);
        if (rb.velocity.magnitude > maximumSpeed)
        {
            float excessSpeed = rb.velocity.magnitude - maximumSpeed;
            Vector3 speedFix = -rb.velocity.normalized * excessSpeed;
            rb.AddForce(speedFix, ForceMode.Impulse);
        }
    }

    public IEnumerator DebugInfo()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            print(rb.velocity.magnitude);
        }
    }
}
