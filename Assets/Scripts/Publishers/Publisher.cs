using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Publisher 
{
    [field: SerializeField] public string company { get; private set; }
    [SerializeField] private float auditoryPercent;
    [SerializeField] private float paymentPercent;

    public Publisher(Company company)
    {
        this.company = company.name;

        auditoryPercent = Random.Range(0.1f, 1f);

        paymentPercent = (1 - auditoryPercent) * Random.Range(0.25f, 0.66f);
    }

    public float GetPayment(int gameSize)
    {
        float payment = Constans.sizesPrices[gameSize] * paymentPercent;
        return payment > 1? payment : 1;
    }

    public int GetAuditory(int gameSize)
    {
        //auditory -= auditory % 10;
        float auditory = Constans.maxPublisherAuditory * Constans.sizesScale[gameSize] * auditoryPercent;
        return (int)(auditory - auditory % 10);
    }
}