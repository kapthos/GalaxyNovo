using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loja : MonoBehaviour
{
    private UIManager _ui;

    void Start()
    {
        _ui = GameObject.Find("UIManager").GetComponent<UIManager>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _ui.ActiveLojaUI();
            //parar a musica
            //parar o controle de tiros
            //ao clicar no botão, voltar ao menu inicial com New Game e Exit
        }
    }
}
