using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Coin SFX")]

    [field: SerializeField] public EventReference coinCollected { get; private set;}
   public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found More then 1 FMOD Event in scene");
        }
        instance = this;
    }
}
