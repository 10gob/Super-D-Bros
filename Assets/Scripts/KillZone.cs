using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Creamos la zona de muerte creando un objeto vacio al que añadimos un box collider, un sprite renderer (en el caso de que queramos ver la zona de muerte dibujada) y un script. 
//En el box collider debemos marcar la opcion In Trigger. El In Trigger es un lanzador de eventos, en el momento en el que un objeto toque el box collider, se activa el evento 
//Con la opcion In Trigger marcada en el box collider, este deja de comportarse como un objeto físico, es decir, no chocaremos ni interactuaremos con el.  

public class KillZone : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)    
    {
        if (other.tag=="Player")   //si el other (que es un collider2D) tiene la etiqueta Player
        {
            PlayerController.sharedInstance.Kill();    //cuando se activa el trigger, se inicia el metodo kill, que se encuentra en el script del jugador
        } 
    }
}
