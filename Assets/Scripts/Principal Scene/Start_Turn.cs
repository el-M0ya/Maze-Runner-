using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class Start_Turn : MonoBehaviour
{
    public GameObject Pause_Start;
    public TextMeshProUGUI Startext;
    public TextMeshProUGUI Roundtext;
    public TextMeshProUGUI CharacterText; 
    public TextMeshProUGUI Colortext;
    Stopwatch startwatch = new Stopwatch();

    // Update is called once per frame
    void Update()
    {
        // Hacer que el letrero sea intermitente
        if ((startwatch.ElapsedMilliseconds / 100) % 5 == 0 && (startwatch.ElapsedMilliseconds / 100) % 10 != 0)
        {
            Startext.text = "";
        }
        if ((startwatch.ElapsedMilliseconds / 100) % 10 == 0)
        {
            Startext.text = "Press F to Start";
        }

        // Mostrar Ronda
        Roundtext.text = "Ronda: " + TurnManager.Instance.Round.ToString();

        // Mostrar Team
        switch (TurnManager.Instance._currentplayer.Team)
        {
            case 1:
            {
                Colortext.text = "Equipo Rojo";
                Colortext.color = Color.red;
                break;
            }
            case 2:
            {
                Colortext.text = "Equipo Azul";
                Colortext.color = Color.blue;
                break;
            }
            case 3:
            {
                Colortext.text = "Equipo Amarillo";
                Colortext.color = Color.yellow;
                break;
            }
            default:
            {
                Colortext.text = "";
                break;
            }
        }

        // Mostrar Texto de Personaje
        CharacterText.text = "Turno de " + TurnManager.Instance._currentplayer.name;

        // Reanudar al presionar F
        if (Input.GetKeyDown(KeyCode.F))
        {
            Reanud();
        }
    }
    public void Reanud()
    {
        Pause_Start.SetActive(false);
        Time.timeScale = 1;
        startwatch.Restart();
        TurnManager.Instance._currentplayer.Can_Move = true;
        TurnManager.Instance._currentplayer.stopwatch.Start();

    }

    public void Pause()
    {
        startwatch.Start();
        Pause_Start.SetActive(true);
        Time.timeScale = 0;

    }

}
