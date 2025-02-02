using System;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{
    public AudioClip AY_AY_AY;
    public AudioClip Imposible;

    [NonSerialized]
    public int Team = -1;
    public int PlayerNumber;
    public string Hability_Description;
    public bool Is_Key = false;
    public int Skill;
    [NonSerialized]
    public Vector2 Start_Position;
    // Habilidades
    [NonSerialized]
    public bool Is_SkillActive;
    [NonSerialized]
    public int Count_Vagabund = 0;
    [NonSerialized]
    public bool Is_Drunk = false;
    [NonSerialized]
    public bool Is_Repa = false;
    [NonSerialized]
    public bool Is_Guachineo = false;
    [NonSerialized]
    public bool Is_Jutia = false;
    bool trap2;
    [NonSerialized]
    public bool Is_Dead = false;

    public GameObject Skill_Sprite;
    [NonSerialized]
    public bool Can_Move = false;
    [NonSerialized]
    public UnityEvent onMoveFinished = new UnityEvent();

    [NonSerialized]
    public UnityEvent onDead = new UnityEvent();
    [NonSerialized]
    public bool LookRight = true;

    [Range(3, 10)]
    public float _Time = 5;
    [NonSerialized]
    public Stopwatch stopwatch = new Stopwatch();
    [NonSerialized]
    public Stopwatch final = new Stopwatch();
    Stopwatch trap2cooldown = new Stopwatch();
    [NonSerialized]
    public Stopwatch guachineo = new Stopwatch();

    public float CameraSize;
    [NonSerialized]
    public float _remainingTime = 10;
    [NonSerialized]
    public bool Is_Move = false;
    [NonSerialized]
    public Animator animator;
    [NonSerialized]
    public Vector2 puntomov;
    private Vector2 input;
    public static int[,] Lab;

    [Range(1, 10)]
    public int Max_Life;
    [NonSerialized]
    public int Life;
    public int _currentCoolDown;
    public int CoolDown;
    [NonSerialized]
    public float Speed;
    [Range(1, 20)]
    public float Initial_Speed;
    void Start()
    {
        animator = GetComponent<Animator>();
        Lab = TurnManager.Instance.Lab;
        animator.SetBool("Is_Running", false);
        Speed = Initial_Speed;
    }
    void Update()
    {
        if(SceneManager.GetActiveScene().name != "Principal Scene") return;
        if(TurnManager.Instance._currentplayer.Life <= 0)
        {
            onDead.Invoke();
            TurnManager.Instance._currentplayer.Can_Move = false;
            input.x = 0;
            input.y = 0;

        }
        if(Is_Repa)
        {
            if ((guachineo.ElapsedMilliseconds / 100) % 5 == 0 && (guachineo.ElapsedMilliseconds / 100) % 10 != 0)
            {
                transform.localScale = new Vector2(-1, transform.localScale.y);
            }
            if ((guachineo.ElapsedMilliseconds / 100) % 10 == 0)
            {
                transform.localScale = new Vector2(1, transform.localScale.y);
            }
        }
        
        if(trap2)
        {
            
            if(!trap2cooldown.IsRunning)
            {
                TurnManager.Instance._currentplayer.gameObject.GetComponent<SpriteRenderer>().color = new Color(50 , 0 , 0);
                trap2cooldown.Start();
                TurnManager.Instance._currentplayer.Life -= 1;
                UI.Instance.ShowLife();
            }
            if(trap2cooldown.ElapsedMilliseconds >= 1500 || final.IsRunning)
            {
                trap2 = false;
                trap2cooldown.Reset();
                trap2cooldown.Stop();
                Can_Move = true;
                if(TurnManager.Instance._currentplayer.Count_Vagabund > 0)TurnManager.Instance._currentplayer.gameObject.GetComponent<SpriteRenderer>().color = new Color(0 , 100 , 0);
                else TurnManager.Instance._currentplayer.gameObject.GetComponent<SpriteRenderer>().color = new Color(255 , 255 , 255);
            }
        }

        if (Can_Move)
        {
            input.x = Input.GetAxis("Horizontal");

            input.y = Input.GetAxis("Vertical");
            if (input.x > 0)
                input.x = 1;
            if (input.x < 0)
                input.x = -1;
            if (input.y > 0)
                input.y = 1;
            if (input.y < 0)
                input.y = -1;

            if (LookRight && input.x < 0 && !Is_Repa)
            {
                LookRight = !LookRight;
                transform.localScale = new Vector2(-1, transform.localScale.y);
            }
            if (!LookRight && input.x > 0 && !Is_Repa)
            {
                LookRight = !LookRight;
                transform.localScale = new Vector2(1, transform.localScale.y);
            }
            Muevete();
        }
        if(!Can_Move && final.IsRunning)
        {
           input.x = 0;
           input.y = 0;
           Muevete(); 
        } 
        
        _remainingTime = _Time - stopwatch.ElapsedMilliseconds / 1000;
        if(final.ElapsedMilliseconds >= 1000) 
        {
            final.Reset();
            final.Stop();
            FinishMovement();
        }
        //Si se acaban el tiempo para
        if (_remainingTime == 0)
        {
            if(!final.IsRunning)
            {
                Can_Move = false;
                final.Start();
            }
        }
        
    }

    public void Muevete()
    {
        if (Is_Move)
        {
            // Mover a la siguiente casilla

            transform.position = Vector2.MoveTowards(transform.position, puntomov, Speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, puntomov) == 0)
            {
                // Caer en Trampa 1
                if(Lab[(int)puntomov.x , (int)puntomov.y] == (int)Metodos.Tile.T_Trap_1 )
                {
                    AudioManager.Instance.Play_Audio(TurnManager.Instance.Caer);
                    final.Start();
                    Can_Move = false;
                    input.x = 0;
                    input.y = 0;
                }
                // Caer en trampa 2
                if(Lab[(int)puntomov.x , (int)puntomov.y] == (int)Metodos.Tile.T_Trap_2 )
                {
                    AudioManager.Instance.Play_Audio(AY_AY_AY);
                    trap2 = true;
                    Can_Move = false;
                }
                // Caer en casilla final
                if(Lab[(int)puntomov.x , (int)puntomov.y] == (int)Metodos.Tile.T_Exit && TurnManager.Instance._currentplayer.Is_Key)
                {
                    SceneManager.LoadScene(3);
                }

                Is_Move = false;
                animator.SetBool("Is_Running", false);
            }
        }
        if ((input.x != 0 ^ input.y != 0) && !Is_Move )
        {

            Vector2 Punto_Evaluar = new Vector2(transform.position.x, transform.position.y) + input;

            if (Lab[(int)Punto_Evaluar.x, (int)Punto_Evaluar.y] != (int)Metodos.Tile.T_Wall && Lab[(int)Punto_Evaluar.x, (int)Punto_Evaluar.y] != (int)Metodos.Tile.T_Pure_Wall)
            {
                Is_Move = true;
                animator.SetBool("Is_Running", true);
                puntomov += input;
            }
        }
        if(!Can_Move) animator.SetBool("Is_Running", false);

    }
    public void FinishMovement()
    {
        UnityEngine.Debug.Log("SE ACABO");
        onMoveFinished.Invoke();
    }
}

