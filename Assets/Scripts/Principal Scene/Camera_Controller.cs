using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public static Transform objetivo;
    [SerializeField]
    float Mov_Speed = 0.25f;
    public Vector3 desplazamiento;

    private void LateUpdate()
    {
        Vector3 PosicionDeseada = objetivo.position + desplazamiento;
        Vector3 PosicionSuavizada = Vector3.Lerp(transform.position , PosicionDeseada, Mov_Speed);
        transform.position = PosicionSuavizada;
        if(TurnManager.Instance._currentplayer != null)Camera.main.orthographicSize = TurnManager.Instance._currentplayer.CameraSize;
    }
}
