using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Para definir el metodo jump vamos a necesitar dos variables: jumpforce y el rigidbody del personaje
    public float jumpForce = 55f;
    private Rigidbody2D rigidbody;

    public LayerMask ground;        /*layer mask sirve para crear una capa. Esto es necesario para el salto. 
                                     *Ademas debemos crear en unity una nueva capa llamada ground y aplicarsela a todos los suelos
                                     *Lo mejor es aplicarla al prefab para asi tenerla en todos los hijos
                                     * Luego en el script del jugador debemos seleccionar la capa ground
                                     */

    public Animator animation;           //creamos esta variable para la animacion del personaje
                                         //en unity tenemos que arrastrar el sprite del jugador a la casilla de animator que nos ha aparecido en el inspector.
    public float runningSpeed = 2.1f;

    public static PlayerController sharedInstance;     //player tambien es un singleton, de manera que solo puede haber uno en el juego. Lo inicializamos en el awake. 

    //Antes de que el juego comience, necesitamos que la variable rigidbody tome el rigidbody del personaje. 
    //Si lo hicieramos en el start, podrian pasar algunos segundos antes de que el rigidbody se configurara. 
    //Todo lo que suceda en el awake esta listo antes de empezar a ejecutarse el juego. 

    void Awake()
    {
        sharedInstance = this;        //con esto evitamos que si tuvieramos dos jugadores en la partida, los dos saltaran, por ejemplo.     
        rigidbody = GetComponent<Rigidbody2D>(); //Get component sirve para recorrer el inspector, viendo todas las componentes
                                                 //que tiene un objeto, en este caso el player, para tomar la componente deseada.
                                                 //En este caso, el rigidbody.
    }

    // Start is called before the first frame update
    void Start()
    {
        animation.SetBool("isAlive", true);          //hacemos que al iniciarse el videojuego, las variables boleanas tocar el suelo y estar vivo sean verdaderas
        animation.SetBool("isGrounded", true);       //Set bool, set float, etc atribuye el valor que queramos a una variable.
    }

    // Update is called once per frame. El update es llamado hasta 60 veces por segundo. 
    void Update() 
    {
        if (GameManager.sharedInstance.currentStateGame == GameState.inGame)  //con esta linea conseguimos que el personaje no se mueva mientras estamos en los menus o en el game over
        {                                                                     //Solo nos movemos durante el inGame
            if (IsTouchingTheGround())
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) //detectamos si el jugador pulsa la tecla espacio para saltar y si esta en el suelo                                                         
                {                                      //click izquierdo del raton
                    Jump();      //La idea es que en el star, en el update y en el awake, el codigo este lo mas limpio posible.
                }
            }

            animation.SetBool("isGrounded", IsTouchingTheGround());   //con esta linea comprobamos si el jugador esta tocando el suelo en cada frame
                                                                      //Para ello utilizamos el metodo ya creado IsTouchingTheGround
        }

    }

    void FixedUpdate()     //los movimientos como avanzar y retroceder los definimos siempre en el fixedupdate
    {
        if (GameManager.sharedInstance.currentStateGame == GameState.inGame)  
        {
            if (Input.GetKey(KeyCode.D))
            {
                rigidbody.velocity = new Vector2(runningSpeed, rigidbody.velocity.y);  //el vector velocidad del rigidbody sera: (runningSpeed, la velocidad que tuviera en y )
            }

            if (Input.GetKey(KeyCode.A))
            {
                rigidbody.velocity = new Vector2(-runningSpeed, rigidbody.velocity.y);
            }
        }
            
    }

    /*SOBRE EL MOVIMIENTO Y SU IMPLEMENTACION 
     * Input.GetKeyDown (up, etc) detecta si pulsamos la tecla. En el caso afirmativo, ejecuta la orden una unica vez.
     * Input.GetKey (up, etc) detecta si la tecla se mantiene pulsada, haciendo que la orden se ejecute de manera continua. 
     */

    void Jump()
    {
        rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            //AddForce sirve para aplicar una fuerza. Vector2.up es un vector en 2d que apunta hacia arriba. 
            //ForceMode2d sirve para aplicar una fuerza constante o un impulso. En el caso de un salto, elegimos impulso. 

            //Como F=m*a, la aceleracion del salto sera a=F/m. La aceleración del salto dependera de la jumpforce aplicada y de 
            //la masa del rigidbody. Podemos balancear masa y fuerza a mano o usando el checkbox "use auto mass". Esta opcion establece 
            //una masa para el rigidbody en función del tamaño de este.
    }

    bool IsTouchingTheGround()             //este metodo detecta si el jugador esta en el suelo
    {
        if (Physics2D.Raycast(this.transform.position, Vector2.down, 0.2f, ground))                                                                  
        {
            return true;
        }
        else
        {
            return false; 
        }
        
        /*physics2D.raycast               sirve para trazar un rayo
         *this.transform.position         desde la posicion del jugador
         * vector2.down                   apuntando hacia abajo
         * 0.2f:                          a una distancia maxima de 0.2 metros
         * ground:                        indica que nos hemos encontrado con la capa suelo. 
         */
    }

    public void Kill()
    {
        GameManager.sharedInstance.GameOver();
        this.animation.SetBool("isAlive", false);
    }
}
