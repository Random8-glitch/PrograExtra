using UnityEngine;
using System.Collections.Generic;

public class Raycostco : MonoBehaviour
{
    [SerializeField]
    private int danio;

    [SerializeField]
    private int fuerza;

    [SerializeField]
    private int cantidad;

    [SerializeField]
    private GameObject bala;

    [SerializeField]
    private Transform shooter;

    [SerializeField]
    private float fuerzaBakla;

    private GameObject objeto;
    public List <GameObject> chofer = new();

  

    [SerializeField]
    private LayerMask enemyMask;

    public Animator animator;




    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Transform clone = Instantiate(bala, shooter.position, shooter.rotation).transform;
            clone.GetComponent<Rigidbody>().AddForce(transform.forward * fuerzaBakla);

            AudioManager.Instance.PlaySound("BalaShoot");

            animator.SetTrigger("IsAttacking");

            Destroy(clone.gameObject, 10);

            

        }

        
    }
}
