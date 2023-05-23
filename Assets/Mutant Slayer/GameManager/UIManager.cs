using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private List<Panel> allPanels = new List<Panel>();

    public static UIManager instance;

    private void Awake()
    {
        OpenUIPanel("MainMenu");
        if (instance == null)
        {
            instance = this;
        }
    }

    public void OpenUIPanel(string name)
    {
        Panel match = allPanels.Find(x => x.name == name);

        if (match != null)
        {
            CloseAll();
            match.obj.SetActive(true);
        }
        else
        {
            Debug.LogWarning("There is no panel with name " + name + "!");
            return;
        }


        Pause(match.pause);
    }

    public void CloseAll()
    {
        foreach (Panel p in allPanels)
        {
            p.obj.SetActive(false);
        }
    }

    public void Pause(bool pause)
    {
        if (pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}

[System.Serializable]
public class Panel
{
    public string name;
    public GameObject obj;
    public bool pause;
}
