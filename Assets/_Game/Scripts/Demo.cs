using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demo : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    int val = 1;
    public void ButtonClicked(string name)
    {
        if(val == 2) 
        {
            string GetName = PlayerPrefs.GetString("NameSet");
            if (GetName == name)
            {
                Debug.Log("Same object select other one");
            }
            else
            {
                Debug.Log("First Button Name = " + GetName);
                Debug.Log("Second Button Name = " + name);
                val = 1;
            }
        }
        else
        {
            Debug.Log("else called");
            PlayerPrefs.SetString("NameSet", name);
            val = 2;
            if(gameObject.name == name)
            {
                Debug.Log("NAme is = "+name);
            }
        }

    }
}
