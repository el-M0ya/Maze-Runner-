using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fruits : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D colission)
    {
        if(name == "COCONUT(Clone)")
        {
            TurnManager.Instance._currentplayer._Time += 1;
        }
        if(name == "Guayaba(Clone)" && TurnManager.Instance._currentplayer.Life < TurnManager.Instance._currentplayer.Max_Life)
        {
            TurnManager.Instance._currentplayer.Life += 1;
            for(int i = 0 ; i < 10 ; i++)
        {
            if(i < TurnManager.Instance._currentplayer.Life)   UI.Instance.Life_On(i);
            else UI.Instance.Life_Off(i);
        }

        }
        if(name == "Bistec(Clone)")
        {
            TurnManager.Instance._currentplayer.Speed += 1.5f;
            TurnManager.Instance._currentplayer.Initial_Speed += 1.5f;
        }
        if(name == "Carrot(Clone)")
        {
            TurnManager.Instance._currentplayer.CameraSize += 1;
        }
        if(name == "GoldenGuayaba(Clone)" && TurnManager.Instance._currentplayer.Max_Life < 10)
        {
            TurnManager.Instance._currentplayer.Max_Life += 1;
            TurnManager.Instance._currentplayer.Life = TurnManager.Instance._currentplayer.Max_Life;
            UI.Instance.ShowLife();
             AudioManager.Instance.Play_Audio(TurnManager.Instance.GlowUp);
        }
        if(name == "Key(Clone)" && !TurnManager.Instance._currentplayer.Is_Key)
        {

            TurnManager.Instance._currentplayer.Is_Key = true;
            TurnManager.Instance.Key_UI.SetActive(true);
            Destroy(gameObject);
            AudioManager.Instance.Play_Audio(TurnManager.Instance.Tin);

        }
        if(name == "Bomb(Clone)" && TurnManager.Instance._currentplayer.name != "Kid")
        {
            TurnManager.Instance._currentplayer.Life -= 3;
            if(TurnManager.Instance._currentplayer.Life < 0) TurnManager.Instance._currentplayer.Life = 0;
             AudioManager.Instance.Play_Audio(TurnManager.Instance.Bomb);
             Destroy(gameObject);
        }
        if( name != "Key(Clone)" &&  name != "Bomb(Clone)")
        {
          Destroy(gameObject);
          AudioManager.Instance.Play_Audio(TurnManager.Instance.Jama);  
        }
        
        
    }
}
