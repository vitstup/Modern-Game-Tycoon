using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightPanelUI : MonoBehaviour
{
    [SerializeField] private ReadyPlatformUI[] platforms;

    public void SetInfo(GameProject project)
    {
        int currentPanel = 0;
        for (int i = 0; i < project.platforms.Length; i++)
        {
            if (project.platforms[i] != null)
            {
                platforms[currentPanel].SetInfo(project.platforms[i], project);
                platforms[currentPanel].gameObject.SetActive(true);
                currentPanel++;
            } 
        }
        for (int i = currentPanel; i < platforms.Length; i++)
        {
            platforms[i].gameObject.SetActive(false);
        }
    }

    public void SetInfo(GameProject project, int[] incomes)
    {
        int currentPanel = 0;
        for (int i = 0; i < project.platforms.Length; i++)
        {
            if (project.platforms[i] != null)
            {
                platforms[currentPanel].SetInfo(project.platforms[i], project, incomes[i]);
                platforms[currentPanel].gameObject.SetActive(true);
                currentPanel++;
            }
        }
        for (int i = currentPanel; i < platforms.Length; i++)
        {
            platforms[i].gameObject.SetActive(false);
        }
    }
}
