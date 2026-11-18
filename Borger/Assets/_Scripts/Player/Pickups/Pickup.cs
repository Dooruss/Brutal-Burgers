using UnityEngine;
using UnityEngine.InputSystem;

public class Pickup : MonoBehaviour
{
    bool isHolding = false;

    [SerializeField]
    float throwForce = 600f;
    [SerializeField]
    float maxDistance = 3f;
    float distance;
    [SerializeField]
    float holdDistance = 2f;

    TempParent tempParent;
    Rigidbody rb;

    Vector3 objectPos;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tempParent = TempParent.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHolding)
            Hold();
    }

    private void OnMouseDown()
    {
        if (tempParent != null)
        {
            distance = Vector3.Distance(this.transform.position, tempParent.transform.position);

            if (distance <= maxDistance)
            {
                isHolding = true;
                rb.useGravity = false;
                rb.detectCollisions = true;

                this.transform.SetParent(tempParent.transform);
            }
        }
        else
        {
            Debug.Log("temp parent item not found in scene!");
        }
    }

    private void OnMouseUp()
    {
        Drop();
    }

    private void Hold()
    {
        distance = Vector3.Distance(transform.position, tempParent.transform.position);

        if (distance >= maxDistance)
        {
            Drop();
            return;
        }

        Vector3 targetPosition = tempParent.transform.position + tempParent.transform.forward * holdDistance;

        rb.MovePosition(targetPosition);
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        if (Input.GetMouseButtonDown(1))
        {
            Drop();
            rb.AddForce(tempParent.transform.forward * throwForce, ForceMode.Impulse);
        }
    }

    private void Drop()
    {
        if (isHolding)
        {
            isHolding = false;
            objectPos = this.transform.position;
            this.transform.position = objectPos;
            this.transform.SetParent(null);
            rb.useGravity = true;
        }
    }
}