using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIndexing : MonoBehaviour
{
    public GameObject[] elements;
    int index = 0;
    public void ResetIndexing()
    {
        elements[index].SetActive(false);
        index = 0;
        elements[index].SetActive(true);
    }
    public void Next()
    {
        elements[index].SetActive(false);
        CheckIfNextIndexAvailable();
        elements[index].SetActive(true);
    }
    public void Previous()
    {
        elements[index].SetActive(false);
        CheckIfPreviousIndexAvailable();
        elements[index].SetActive(true);
    }

    void CheckIfNextIndexAvailable()
    {
        if(index + 1 >= elements.Length)
        {
            index = 0;
        }
        else
        {
            index++;
        }
    }

    void CheckIfPreviousIndexAvailable()
    {
        if (index - 1 < 0)
        {
            index = elements.Length-1;
        }
        else
        {
            index--;
        }
    }
}
