using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
[RequireComponent(typeof(Camera))]
public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private Camera cameraToActivate;
    [SerializeField] private Camera cameraToDeactivate;

    private bool playerInside;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Camera Switch Triggered");

            if (cameraToActivate != null && cameraToDeactivate != null)
            {
                cameraToActivate.enabled = true;
                cameraToDeactivate.enabled = false;
            }
        }
        else if (playerInside && Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log("Camera Switch Triggered");
            if (cameraToActivate != null && cameraToDeactivate != null)
            {
                cameraToActivate.enabled = false;
                cameraToDeactivate.enabled = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}
