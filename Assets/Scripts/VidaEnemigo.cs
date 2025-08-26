using UnityEngine;
using UnityEngine.InputSystem.HID;

public class VidaEnemigo : MonoBehaviour
{
    [SerializeField]
    int vida;

    [HideInInspector] public bool registrarEnStart = true;

    private void Start()
    {
        if (registrarEnStart)
        {
            EnemyManager.instance.RegistrarEnemigo();
        }
    }

    public void DanioEnemigo(int danio)
    {
        vida -= danio;

        AudioManager.Instance.PlaySound("Hit");

        if (vida <= 0)
        {
            Debug.Log("Something was destroyed");
            EnemyManager.instance.EliminarEnemigo();
            AudioManager.Instance.PlaySound("Death");

            Destroy(this.gameObject);
        }
    }
}
