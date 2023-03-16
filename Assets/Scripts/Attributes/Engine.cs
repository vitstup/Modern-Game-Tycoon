using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Engine
{
    [field: SerializeField] public EngineInfo info { get; private set; }
    [field: SerializeField] public bool boughted { get; private set; }

    public Engine(EngineInfo info)
    {
        this.info = info;
        if (info.licensePrice == 0) boughted = true;
    }

    public void Buy()
    {
        if (!boughted)
        {
            Main.instance.MinusMoney(info.licensePrice);
            boughted = true;
            MailManager.instance.NewMail(new EngineBoughtedMail(info.company.name, this));
        }
        else Debug.LogWarning("Trying to buy already boughted engine");
    }

    public void SetBoughted(bool boughted) // use this method only after file loading
    {
        this.boughted = boughted;
    }
}