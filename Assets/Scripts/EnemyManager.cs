using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public int enemigosRestantes = 0;

    [SerializeField] public Text textoUI;

    private void Awake()
    {
        // Patrón Singleton para acceso global
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ActualizarTexto();
    }

    public void RegistrarEnemigo()
    {
        enemigosRestantes++;
        ActualizarTexto();
    }

    public void EliminarEnemigo()
    {
        enemigosRestantes--;
        ActualizarTexto();

        if (enemigosRestantes <= 0)
        {
            Debug.Log("¡Todos los enemigos han sido eliminados!");

            SceneManager.LoadScene("Inicio");
        }
    }

    public void EstablecerConteo(int cantidad)
    {
        enemigosRestantes = Mathf.Max(0, cantidad);
        ActualizarTexto();
    }

    private void ActualizarTexto()
    {
        if (textoUI != null)
            textoUI.text = "Enemigos: " + enemigosRestantes;
    }
}
