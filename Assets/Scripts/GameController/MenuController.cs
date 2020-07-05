using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{

    public static MenuController instance;

    [SerializeField]
    private GameObject[] birds;

    void MakeInstance() {
        if (instance == null) {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayGame() {
        SceneFader.instance.FadeIn("Gameplay");
    }

}
