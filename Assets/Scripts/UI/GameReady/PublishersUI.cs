using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PublishersUI : MonoBehaviour
{
    public static PublishersUI instance;

    [SerializeField] private GameObject PublishersPanel;
    [SerializeField] private PublisherUI[] panels;

    private void Awake()
    {
        instance = this;
    }

    public void OpenPublishersPanel(Game game)
    {
        PublishersPanel.SetActive(true);
        SetInfo(game);
    }

    private void SetInfo(Game game)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetInfo(PublishersManager.instance.publishers[i], game);
        }
    }

    public void PublisherChoosed(Publisher publisher)
    {
        PublishersPanel.SetActive(false);
        (ProjectManager.instance.project as Game).publisher = publisher;
        GameReadyUI.instance.Release();
    }
}