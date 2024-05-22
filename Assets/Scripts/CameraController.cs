using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] float edgeScrollSpeed = 10f;
    [SerializeField] float rotateSpeed = 15f;
    [SerializeField] bool useRotateCamera = false;
    [SerializeField] bool useEdgeScroll = true;

    void Update()
    {
        if(useRotateCamera)
            HandleCameraRotation();
        if(useEdgeScroll)
            HandleCameraMovementEdgeScrolling();
    }
    private void HandleCameraMovementEdgeScrolling()
    {
        Vector3 inputDir = new Vector3(0, 0, 0);

            int edgeScrollSize = 20;

            if (Input.mousePosition.x < edgeScrollSize) {
                inputDir.x = -1f;
            }
            if (Input.mousePosition.y < edgeScrollSize) {
                inputDir.z = -1f;
            }
            if (Input.mousePosition.x > Screen.width - edgeScrollSize) {
                inputDir.x = +1f;
            }
            if (Input.mousePosition.y > Screen.height - edgeScrollSize) {
                inputDir.z = +1f;
            }

            Vector3 moveDir = transform.forward * inputDir.z + transform.right * inputDir.x;

            transform.position += moveDir * edgeScrollSpeed * Time.deltaTime;
    }
    private void HandleCameraRotation() {
        float rotateDir = 0f;
        if (Input.GetKey(KeyCode.Q)) rotateDir = +1f;
        if (Input.GetKey(KeyCode.E)) rotateDir = -1f;

        transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed * Time.deltaTime, 0);
    }
}
