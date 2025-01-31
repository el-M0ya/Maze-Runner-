using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ChangeCharacter : MonoBehaviour
{
    public  List<Character> ListPlayers = new List<Character>();

    public TextMeshProUGUI Player_Name;
    public TextMeshProUGUI Player_Prop;
    public TextMeshProUGUI Player_Hability;
    [NonSerialized]
    public  int _playerindex = 0;
    public static ChangeCharacter Instance{get; private set;}

    public void Awake()
    {
        if(Instance == null) Instance = this;
        else Debug.LogError("Mas de un ChangeCharacter");
        Show();
    }

    public void NextPlayer()
    {
        if(ListPlayers.Count == 0)
        {
           Show();
           return; 
        } 
        ListPlayers[_playerindex].gameObject.SetActive(false);
        if(_playerindex < ListPlayers.Count - 1)_playerindex += 1 ;
        else _playerindex = 0;
        Debug.Log(_playerindex);

        Show();
  }
    public void BackPlayer()
    {
        if(ListPlayers.Count == 0)
        {
           Show();
           return; 
        } 
        ListPlayers[_playerindex].gameObject.SetActive(false);
        if(_playerindex > 0)_playerindex -= 1 ;
        else _playerindex = ListPlayers.Count - 1;
        Debug.Log(_playerindex);
        
        Show();
   }
    public void Show()
    {
        if(ListPlayers.Count == 0) Player_Name.text = "Hora de Jugar";
        else
        {
            ListPlayers[_playerindex].gameObject.SetActive(true);
            Player_Name.text = ListPlayers[_playerindex].name;
            Player_Hability.text = $"{ListPlayers[_playerindex].GetComponent<Character>().Hability_Description}";
            Player_Prop.text = $"Vida: {ListPlayers[_playerindex].GetComponent<Character>().Max_Life}\n\nTiempo: {ListPlayers[_playerindex].GetComponent<Character>()._Time}\n\nVelocidad: {ListPlayers[_playerindex].GetComponent<Character>().Initial_Speed}\n\nCoolDown: {ListPlayers[_playerindex].GetComponent<Character>().CoolDown}";
        }
     }

}
