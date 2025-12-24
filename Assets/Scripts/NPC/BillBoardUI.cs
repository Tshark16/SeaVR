using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    public Transform cam;

    private void LateUpdate()
    {
        if (cam == null)
            cam = Camera.main.transform;

        transform.LookAt(transform.position + cam.rotation * Vector3.forward,
                         cam.rotation * Vector3.up);
    }
}
