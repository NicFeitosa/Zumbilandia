using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Numerics;

public class ChecarAtaque : MonoBehaviour
{
    public static bool checkAtq = false;

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.tag == "Player")
        {
            checkAtq = true;
        }
    }
}