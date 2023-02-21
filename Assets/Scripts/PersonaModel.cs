using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonaModel : MonoBehaviour
{
    [SerializeField] private GameObject[] models;

    private Animator[] animators;

    private void Awake()
    {
        animators = GetComponentsInChildren<Animator>();
        ProjectManager.DevelopmentEvent.AddListener(SetAnimation);
    }

    private void Start()
    {
        if (ProjectManager.instance.project != null) SetAnimation(true);
    }

    public void SetAnimation(bool isWorking)
    {
        for (int i = 0; i < animators.Length; i++)
        {
            animators[i].SetBool("isTyping", isWorking);
        }
    }

    public void SetModel(int num)
    {
        for (int i = 0; i < models.Length; i++)
        {
            if (i == num) { models[i].SetActive(true); continue; }
            models[i].SetActive(false);
        }
    }

    public void HideModel()
    {
        for (int i = 0; i < models.Length; i++)
        {
            models[i].SetActive(false);
        }
    }


    // for animations test

    /*
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) SetAnimation(false);
        if (Input.GetKeyDown(KeyCode.N)) SetAnimation(true);
    }
    */
}
