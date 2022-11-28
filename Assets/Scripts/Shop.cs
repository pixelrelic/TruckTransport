using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{   

    [SerializeField] int id;
    [SerializeField] GameObject scoreTextPrefab;

    public int GetId()
    {
        return id;
    }

    public void ShowScorePopup(int score)
    {
        Instantiate(scoreTextPrefab,transform.position,transform.rotation,transform);

    }
}
