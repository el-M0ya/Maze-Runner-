using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Winner : MonoBehaviour
{
    public List<Character> characters = new List<Character>();
    public TextMeshProUGUI WinnerTeamText;

    private void Start()
    {
        int WinCount = 0;
        // Proceso para mostrar a todos los jugadores ganadores
        foreach (Character character in characters)
        {
            character.gameObject.SetActive(false);
            character.transform.position = new Vector3(0 , 0);
        }
        
        int Team = TurnManager.Instance._currentplayer.Team;
        bool[] Winners = new bool[5];
        
        for (int i = 0; i < TurnManager.Instance.players.Count; i++)
        {
            if(TurnManager.Instance.players[i].Team == Team && Team > 0) 
                Winners[TurnManager.Instance.players[i].PlayerNumber] = true;
        }

        // Cambiar el texto del equipo ganador
        switch (Team)
        {
            case 1:
                WinnerTeamText.text = "Equipo Rojo";
                WinnerTeamText.color = Color.red;
                break;
            case 2:
                WinnerTeamText.text = "Equipo Azul";
                WinnerTeamText.color = Color.blue;
                break;
            case 3:
                WinnerTeamText.text = "Equipo Amarillo";
                WinnerTeamText.color = Color.yellow;
                break;
            default:
                WinnerTeamText.text = "";
                break;
        }
        
        

        if(Winners[0])
        {
            characters[0].transform.position = new Vector3(0 , 0);
            characters[0].gameObject.SetActive(true);
        }
        if(Winners[1])
        {
            characters[1].transform.position = new Vector3(4 , 0);
            characters[1].gameObject.SetActive(true);
        }
        if(Winners[2])
        {
            characters[2].transform.position = new Vector3(-4, 0);
            characters[2].gameObject.SetActive(true);
        }
        if(Winners[3])
        {
            characters[3].transform.position = new Vector3(8, 0);
            characters[3].gameObject.SetActive(true);
        }
        if(Winners[4])
        {
            characters[4].transform.position = new Vector3(-8, 0);
            characters[4].gameObject.SetActive(true);
        }

        

    }
}
