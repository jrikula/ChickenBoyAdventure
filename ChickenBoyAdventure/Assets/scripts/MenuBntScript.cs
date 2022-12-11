using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBntScript : MonoBehaviour
{
 
    public void GoToGame()
    {
        SceneManager.LoadScene("FinalScene1");
    }

}
