using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Change_Scene_Play : MonoBehaviour
{
    public void To_PrincipalScene()
    {
        if(SelectPlayer.PlayersToPlay.Count != 0) SceneManager.LoadScene(2);
    }
}
