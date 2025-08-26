using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class ControlGameData : MonoBehaviour
{
    public GameObject jugador;

    public GameObject prefabEnemigo;
    public Transform contenedorEnemigos;

    public string archivoDeGuardado;
    public DatosJuego datosJuegos = new DatosJuego();

    public static bool cargando = false;

    private void Awake()
    {
        archivoDeGuardado = Application.dataPath + "/datosJuego.json";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            CargarDatos();
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            GuardarDatos();
        }
    }

    private void CargarDatos()
    {
        if (!File.Exists(archivoDeGuardado))
        {
            Debug.Log("El archivo no existe");
            return;
        }

        string contenido = File.ReadAllText(archivoDeGuardado);
        datosJuegos = JsonUtility.FromJson<DatosJuego>(contenido);

        // --- Mover jugador (CharacterController) ---
        var cc = jugador.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;
        jugador.transform.position = datosJuegos.posPlayer;
        if (cc != null) cc.enabled = true;

        // --- Destruir enemigos actuales (asegúrate de que SOLO los enemigos reales tengan tag "Enemy") ---
        foreach (var enemigo in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemigo);
        }

        // --- Instanciar enemigos guardados y MARCARLOS para que NO se registren en Start() ---
        foreach (var pos in datosJuegos.posicionesEnemigos)
        {
            GameObject nuevo = Instantiate(prefabEnemigo, pos, Quaternion.identity);
            if (contenedorEnemigos != null)
                nuevo.transform.SetParent(contenedorEnemigos);

            var vida = nuevo.GetComponent<VidaEnemigo>();
            if (vida != null)
            {
                // Muy importante: evita que se registren en Start()
                vida.registrarEnStart = false;
            }
        }

        // --- Fijar el conteo directamente según lo instanciado ---
        EnemyManager.instance.EstablecerConteo(datosJuegos.posicionesEnemigos.Count);

        Debug.Log("Datos cargados correctamente");
    }

    private void GuardarDatos()
    {
        var nuevosDatos = new DatosJuego
        {
            posPlayer = jugador.transform.position,
            posicionesEnemigos = new List<Vector3>()
        };

        foreach (var enemigo in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            nuevosDatos.posicionesEnemigos.Add(enemigo.transform.position);
        }

        string cadenaJSON = JsonUtility.ToJson(nuevosDatos, true);
        File.WriteAllText(archivoDeGuardado, cadenaJSON);

        Debug.Log("Archivo Guardado");
    }

}
