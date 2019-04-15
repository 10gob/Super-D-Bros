using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Al crear un script y darle de nombre GameManager, aparece automaticamente el icono de un engranaje en el script
//este script se lo damos a un objeto vacio (al que hemos llamado Game Manager) que hemos creado en la scene. Es recomendable
//dar una etiqueta de color y colocarlo en el (0,0), o en alguna otra posicion en la que lo encontremos facilmente.


//Posibles estados del videojuego
public enum GameState             
{
    menu, inGame, gameOver
}

public class GameManager : MonoBehaviour
{   
    //Variable que referencia al propio gamemanager
    public static GameManager sharedInstance;        //Singleton. De esta manera conseguimos que solo exista un GameManager

    public GameState currentStateGame = GameState.menu;

    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        BackToMenu();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Start"))     //esta forma de configurar los botones se utiliza para juegos multiplataforma. Lo que hacemos es crear un boton llamado "Start"
        {                                     //que posteriormente podremos asignar a una tecla del teclado, un boton de nintendo switch, etc. 
            StartGame();                      // para configurar este boton debemos irnos a edit-->project settings-->input y crear un nuevo boton. Ej: si pone 18 escribimos 19
        }
    }

    public void StartGame()
    {
        SetGameState(GameState.inGame);
    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }

    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    void SetGameState(GameState newGameState)    //este metodo sirve para cambiar el estado del juego
    {
        if(newGameState == GameState.menu)
        {
            //hay que preparar la escena de unity para mostrar el menu
        }

        if(newGameState == GameState.inGame)
        {
            //hay que prepara la escena de unity para jugar
        }

        if(newGameState == GameState.gameOver)
        {
            //hay que preparar la escena de unity para el game over
        }

        //asignamos el estado de juego actual al que nos ha llegado por parametros
        this.currentStateGame = newGameState;
    }
}
