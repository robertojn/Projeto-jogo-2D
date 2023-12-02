using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
#if UNITY_EDITOR
using UnityEditor.SearchService;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;



public class MenuPrincipal : MonoBehaviour
{
    public GameObject Menu;
    [SerializeField] private string NomeDoJogo;
    [SerializeField] private GameObject painelCreditos;
    [SerializeField] private GameObject Creditos2;
    [SerializeField] private GameObject MenuInicial;
   public void Jogar()
    {
        Menu.SetActive(false);
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

    public void ProxCreditos()
    {
        Creditos2.SetActive(true);
        painelCreditos.SetActive(false);
    }
    public void ProxCreditosVoltar()
    {
        Creditos2.SetActive(false);
        painelCreditos.SetActive(true);
    }

    public void Sair()
    {
        Application.Quit();
    }
}
