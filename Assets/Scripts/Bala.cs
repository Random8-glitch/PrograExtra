using UnityEngine;

public class Bala : MonoBehaviour
{

    private bool check = false;
    private GameObject colObj;

    private void Update()
    {
        if (check)
        {
            colObj.GetComponent<VidaEnemigo>().DanioEnemigo(10);
            check = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            colObj = collision.gameObject;
            check = true;
            
        }

        Destroy(gameObject, .1f);
    }
}
