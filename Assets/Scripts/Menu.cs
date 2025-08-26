using UnityEngine;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{

    [SerializeField]
    public GameObject credit;

    public bool isCredit = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ShowCursor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Credits()
    {
        if (isCredit == false)
        {
            credit.SetActive(true);
            isCredit = true;
        }
    }

    public void CreditsF()
    {
        if (isCredit == true)
        {
            credit.SetActive(false);
            isCredit = false;
        }
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        ShowCursor();
        SceneManager.LoadScene("Juego");
    }
}
