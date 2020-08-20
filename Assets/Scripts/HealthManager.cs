using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {

    public List<GameObject> hearts;

    public static HealthManager instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("Health Manager Instance Already Created");
        }
    }
    public void RemoveHeart(){
        if (hearts.Count > 0)
        {
            GameObject lastHeart = hearts[hearts.Count - 1];
            lastHeart.SetActive(false);
            hearts.Remove(lastHeart);
            if (hearts.Count == 0)
            {
                PlayerController.instance.Die();
            }
        }

    }
    
        
    }

