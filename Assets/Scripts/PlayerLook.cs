using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private Transform padre;

    private float mox;
    private float moy;

    [SerializeField] 
    private float mouseSenseX;

    [SerializeField]
    private float mouseSenseY;

    private float rotX = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        padre = transform.parent;
    }

    private void Update()
    {
        mox = Input.GetAxis("Mouse X") * mouseSenseX * Time.deltaTime;
        padre.Rotate(0, mox, 0);

        moy = Input.GetAxis("Mouse Y") * mouseSenseY * Time.deltaTime;
        rotX -= moy;

        rotX = Mathf.Clamp(rotX, -90, 90);
        transform.localRotation = Quaternion.Euler(rotX, 0, 0);
    }
}
