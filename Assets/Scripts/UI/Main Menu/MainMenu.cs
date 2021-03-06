using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject opciones;
    public GameObject mainMenu;
    public string nameScense;

    private void Awake()
    {
        Cursor.visible = true;
    }

    public void Play()
    {
        SceneManager.LoadScene(nameScense);
    }
    public void Options()
    {
        mainMenu.SetActive(false);
        opciones.SetActive(true);
    }
    public void Menu()
    {
        mainMenu.SetActive(true);
        opciones.SetActive(false);
    }
    public void Exit()
    {
        Debug.Log("Salir");
        Application.Quit();
    }
}
