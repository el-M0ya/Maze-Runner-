using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Unity.Mathematics;
using System.Diagnostics;
using TMPro;


public class TurnManager : MonoBehaviour
{
    [Header("AudioClips")]
    public AudioClip JutiaFast;
    public AudioClip Vagabund;
    public AudioClip Drunk_Boing;
    public AudioClip KidLaugh;
    public AudioClip Bomb;
    public AudioClip Jama;
    public AudioClip GlowUp;
    public AudioClip Tin;
    public AudioClip Caer;
    public AudioClip StartSound;
    public AudioClip Imposible;
    public GameObject MusicBack;
    public GameObject MusicRepa;
    public Start_Turn start_Turn;
    [Header("Personajes y habilidades")]
    public List<Skills> skills = new List<Skills>();
    public List<GameObject> Skill_UI = new List<GameObject>();
    public List<Character> players = new List<Character>();
    public TextMeshProUGUI CoolDownText;
   private int _currentplayersindex = 0;
   public GameObject Dead_UI;
   public GameObject Key_UI;


   [NonSerialized]
   public Character  _currentplayer ;

    [NonSerialized]
   public int [,] Lab;

    [Header("Referencias")]
    public Tilemap tilemap;
    public TileBase Wall_Tile;
    public TileBase PureWall_Tile;
    public TileBase Way_Tile ;
    public TileBase Trap_1_Tile ; 
    public TileBase Trap_2_Tile ;
    public TileBase Exit_Tile;
    public TileBase Jail;

    [Header("Dimensiones")]
    public int high ;
    public int lenght;
    [Header("Bonificaciones")]
    public GameObject COCONUT;
    public GameObject Carrot;
    public GameObject Bistec;
    public GameObject Guayaba;
    public GameObject GoldenGuayaba;
    public GameObject Key;
    public GameObject KidBomb;
    [NonSerialized]
    public GameObject Fruit;
    private System.Random random = new System.Random();
    public ListaDePlayers listaDePlayers;

    public static TurnManager Instance{get; private set;}
    // Awake is called before Start
    private void Awake()
    {
        if(Instance == null) Instance = this;
        else UnityEngine.Debug.LogError("Mas de un TurnManager");

        UnityEngine.Debug.LogAssertion(SelectPlayer.PlayersToPlay.Count);

        
    }
    // Start is called before the first frame update
     private void Start()
    {
        
        players = listaDePlayers.CreateListPlayers();
        // Generar Mapa
        Clean_Map();

        high = Dimensiones.X;
        lenght = Dimensiones.Y;
        Generar_Mapa();
        if(players.Count == 0)
        {
            UnityEngine.Debug.LogError("No hay jugadores asignados al TurnManager");
            enabled = false;
            return;
        }
        // Cada jugador conserva su posicion inicial y no tiene cooldown
        for(int i = 0 ; i < players.Count ; i++)
        {
            players[i].puntomov = players[i].transform.position;
            players[i]._currentCoolDown = 0;
        }
        // Vidas no se muestren al principio
        for(int i = 0 ; i < 10 ; i++)
        {
            UI.Instance.Life_Off(i);
        }
        _currentplayer = players[_currentplayersindex];
        StartNewTurn();
    }
    // Genera el mapa en donde se va a jugar
    public void Generar_Mapa()
    {
        Lab = Metodos.Generar_Array(high , lenght);  
        tilemap.ClearAllTiles();
        int Players = players.Count;
        while(Players > 0)
        {
            UnityEngine.Debug.LogWarning("Crear");
            Metodos.Gen_Special_Limit(Lab , (int)Metodos.Tile.T_Jail);
            Players--;
            
        }
        To_Start(players , Lab);
        Metodos.Gen_Map(Lab ,  tilemap,  Wall_Tile ,  PureWall_Tile ,  Way_Tile ,  Trap_1_Tile ,  Trap_2_Tile , Exit_Tile , Jail);
        Gen_Fruits(Lab);
        Gen_Keys(Lab);

        
    }
    // Limpia el mapa
    public void Clean_Map()
    {
        tilemap.ClearAllTiles();
         
        
    }
    // Coloca los personajes en la casilla de salida
    public void To_Start(List<Character> personajes , int[,] Lab)
    {
        List<Vector3> Start= new List<Vector3>();
        // Guardar las posiciones de salida
        for (int i = 0; i < Lab.GetLength(0); i++)
        {
            for (int j = 0; j < Lab.GetLength(1); j++)
            {
                if(Lab[i , j] == (int)Metodos.Tile.T_Jail)
                {
                    Start.Add(new Vector3(i , j));
                }
            }
        }
        // Cambiar el orden de las posiciones de salida
        System.Random random = new System.Random();
        for (int i = 0; i < Start.Count; i++)
        {
            int AlBerro = random.Next(0 , Start.Count);
            Vector3 cambio = Start[i];
            Start[i] = Start[AlBerro];
            Start[AlBerro] = cambio;
        }
        // Asignar las cada jugador a una posicion de salida
        for (int i = 0; i < Start.Count; i++)
        {
            players[i].transform.localPosition = Start[i];
            players[i].Start_Position = Start[i];
        }
    }
    // Empieza un nuevo turno
    private void StartNewTurn()
    {
       
        if(_currentplayer == null)
        {
            UnityEngine.Debug.LogError("No hay jugador");
        }
        _currentplayer = players[_currentplayersindex];
        // Sonido de Inicio de Turno
        AudioManager.Instance.Play_Audio(StartSound);
        
       UnityEngine.Debug.Log("Turno de " + _currentplayer.name);
        // Si no tiene vida, reiniciala
        if(_currentplayer.Life <= 0) 
        {
            _currentplayer.Life = _currentplayer.Max_Life;
        }

        //Mostrar si tiene o no una llave
        if(_currentplayer.Is_Key) Key_UI.SetActive(true);

        // La habilidad del borracho se activa
        if(_currentplayer.Is_Drunk)  
        {
            Skill_UI[(int)Skill.Drunk].SetActive(true);
            Camera.main.transform.rotation = new quaternion(0 , 0 , 180 , 0);
        }

        // Activar la habilidad del vagabundo
        if(_currentplayer.Count_Vagabund > 0)
        {
            Skill_UI[(int)Skill.Vagabund].SetActive(true);
            _currentplayer.Life += -1;
            _currentplayer.Count_Vagabund += -1;
        }
        // Si no tienes veneno ni estas muerto, entonces debes tener una coloracion normal
        if(_currentplayer.Count_Vagabund == 0 && !_currentplayer.Is_Dead) 
            _currentplayer.gameObject.GetComponent<SpriteRenderer>().color = new Color(255 , 255 , 255);

        if(_currentplayer.Is_Repa) Skill_UI[(int)Skill.Repa].SetActive(true);

        // Desactivar Guachineo
        if(_currentplayer.Is_Guachineo)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].Is_Repa)
                {
                    
                    players[i].Speed = players[i].Initial_Speed; 
                    players[i].Is_Repa = false;
                    players[i].guachineo.Reset();
                    players[i].guachineo.Stop(); 
                }
            }
            _currentplayer.Is_Guachineo = false;

        MusicRepa.GetComponent<AudioSource>().Pause();
        MusicBack.GetComponent<AudioSource>().Play();
           
        } 
        // Colocar el sprite de la habilidad
        _currentplayer.Skill_Sprite.SetActive(true);
        // La camara sigue al jugador
        Camera_Controller.objetivo = _currentplayer.transform;

        //Mostrar vidas 
        UI.Instance.ShowLife();
    
         start_Turn.Pause();
        _currentplayer.onMoveFinished.AddListener(EndTurn);
        _currentplayer.onDead.AddListener(Dead);

        //Mostrar CoolDown
        CoolDownText.text = _currentplayer._currentCoolDown.ToString();
    
    }
    // MUERTEE!!!
    public void Dead()
    {
        Dead_UI.SetActive(true);
        _currentplayer.onDead.RemoveListener(Dead);
        // Sonido Imposible
        AudioManager.Instance.Play_Audio(Imposible);
        // Ponerlo de color rojo
        _currentplayer.gameObject.GetComponent<SpriteRenderer>().color = new Color(255 , 0 , 0);

        
        _currentplayer.final.Start();
        _currentplayer.Can_Move = false;
        
        _currentplayer.animator.SetBool("Is_Running", false);
        
    }
    // Finaliza el turno
    public void EndTurn()
    {
        _currentplayer.onMoveFinished.RemoveListener(EndTurn);
        _currentplayer.onDead.RemoveListener(Dead);
        _currentplayer.stopwatch.Stop();
        _currentplayer.stopwatch.Reset();
        _currentplayer.final.Stop();
        _currentplayer.final.Reset();
        _currentplayer.Can_Move = false;
        _currentplayer.Is_Move = false;
        _currentplayer.animator.SetBool("Is_Running", false);

        // Desactivar todos los UI de estados
        for (int i = 0; i < Skill_UI.Count; i++)
        {
            Skill_UI[i].SetActive(false);
        }
        Dead_UI.SetActive(false);
        Key_UI.SetActive(false);

        if(_currentplayer.Is_Drunk)
        {
           Camera.main.transform.rotation = new quaternion(0 , 0 , 0 , 0); 
           _currentplayer.Is_Drunk = false;
        } 

        if(_currentplayer.Is_Jutia)
        {
           _currentplayer.Speed = _currentplayer.Initial_Speed; 
           _currentplayer.Is_Jutia = false;
        } 

        if(_currentplayer._currentCoolDown > 0)_currentplayer._currentCoolDown --;

        if(_currentplayer.Is_SkillActive)
        {
            _currentplayer._currentCoolDown = _currentplayer.CoolDown;
            _currentplayer.Is_SkillActive = false;
        } 
        _currentplayer.Skill_Sprite.SetActive(false);

            _currentplayersindex++;
            UnityEngine.Debug.Log("Cambio");
            if(_currentplayersindex >= players.Count)
            {
              _currentplayersindex = 0;
            }

            StartNewTurn();
        
        
    }
    // Termiba el juego
    public void EndGame()
    {
        UnityEngine.Debug.Log("El juego ha terminado");
    }
   // Genera las frutas
    public void Gen_Fruits(int[,] Lab)
    {
        for(int i = 1 ; i < Lab.GetLength(0) ; i++ )
        {
            for(int j = 1 ; j < Lab.GetLength(1) ; j++ )
            {
                if(Lab[i , j] == (int)Metodos.Tile.T_Chest) 
                {
                    int aleatory = random.Next(1 , 11);
                    switch(aleatory)
                    {
                        case 1:
                        Fruit = Instantiate(Carrot , new Vector3(i , j) , new Quaternion());
                        break;
                        case 2:
                        Fruit = Instantiate(GoldenGuayaba , new Vector3(i , j) , new Quaternion());
                        break;
                        case 3:case 4:
                        Fruit = Instantiate(COCONUT , new Vector3(i , j) , new Quaternion());
                        break;
                        case 5: case 6: case 7:
                        Fruit = Instantiate(Guayaba , new Vector3(i , j) , new Quaternion());
                        break;
                        case 8: case 9: case 10:
                        Fruit = Instantiate(Bistec , new Vector3(i , j) , new Quaternion());
                        break;

                    }
                }
            }
        }
    }
    public void Gen_Keys(int[,] Lab)
    {
        int cantidad = players.Count;
        while (cantidad > 0)
        {
            int i = UnityEngine.Random.Range(1, Lab.GetLength(0) - 2);
            int j = UnityEngine.Random.Range(1, Lab.GetLength(1) - 2);
            if (Lab[i, j] == (int)Metodos.Tile.T_Way)
            {
                Fruit = Instantiate(Key , new Vector3(i , j) , new Quaternion());
                cantidad--;
            }

        }
        return;
    }
    // Activa las habilidades
    public void ActivateSkill()
    {
        if(_currentplayer._currentCoolDown == 0 && !_currentplayer.Is_SkillActive)
        {
            skills[_currentplayer.Skill].UseHability();
            _currentplayer.Is_SkillActive = true;
        }
    }
}