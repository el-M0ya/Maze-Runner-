using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListaDePlayers : MonoBehaviour
{
    public List<Character> Players_Disponibles = new List<Character>();

    public  List<Character> CreateListPlayers()
    {
         List<Character> Players = new List<Character>();
        for (int i = 0; i < SelectPlayer.PlayersToPlay.Count; i++)
        {
            Players.Add(Players_Disponibles[SelectPlayer.intPlayers[i]]);
        }
        if(Players.Count == 0)
        {
            UnityEngine.Debug.LogError("No hay personajes para jugar");
        }

        for (int i = 0; i < Players.Count; i++)
        {
            Players[i].Team = SelectPlayer.Color[i];
        }

         return Players;
    }
}
