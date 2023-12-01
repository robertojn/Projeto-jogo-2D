using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] private string NomeDoJogo;
    [SerializeField] private GameObject painelCreditos;
    [SerializeField] private GameObject MenuInicial;
   public void Jogar()
    {
        SceneManager.LoadScene(NomeDoJogo);
    }

    public void AbrirCreditos()
    {
        painelCreditos.SetActive(true);
        MenuInicial.SetActive(false);
    }

    public void FecharCreditos()
    {
        painelCreditos.SetActive(false);
        MenuInicial.SetActive(true);
    }

    public void Sair()
    {
        Application.Quit();
    }
}
