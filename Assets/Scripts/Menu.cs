using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject men�paneli;
    public void oyunubaslat()
    {
        Application.LoadLevel(1);
        Time.timeScale = 1.0f;
    }
    public void men�panelia�()
    {
        men�paneli.SetActive(true);
        Time.timeScale = 0.0f;
    }
    public void oyunadevamet()
    {
        men�paneli.SetActive(false);
        Time.timeScale = 1.0f;
    }
    public void anamenuyedon()
    {
        Application.LoadLevel(0);
    }
}
