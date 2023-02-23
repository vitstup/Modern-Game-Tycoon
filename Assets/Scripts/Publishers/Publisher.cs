using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Publisher 
{
    [field: SerializeField] public string company { get; private set; }
    [field: SerializeField] public int auditory { get; private set; }
    [field: SerializeField] public float paymentPercent { get; private set; }

    public Publisher(Company company)
    {
        this.company = company.name;

        auditory = Random.Range(Constans.minPublisherAuditory, Constans.maxPublisherAuditory);

        auditory -= auditory % 10;

        paymentPercent = (float)auditory / Constans.maxPublisherAuditory * Random.Range(0.25f, 0.66f);
    }

    public float GetPayment(int gameSize)
    {
        float payment = Constans.sizesPrices[gameSize] * paymentPercent;
        return payment > 1? payment : 1;
    }
}