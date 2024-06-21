using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    void Start()
    {
        instance = this;

    }

    public GameObject myPlayer;
}
