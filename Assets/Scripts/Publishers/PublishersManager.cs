using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PublishersManager : MonoBehaviour
{
    public static PublishersManager instance;

    [field: SerializeField] public Publisher[] publishers { get; private set; }

    private void Awake()
    {
        instance = this;
        InitializePublishers();
    }

    private void InitializePublishers()
    {
        var companies = Resources.LoadAll<Company>("Companies");
        if (companies.Length < Constans.publisherCount) Debug.LogError("Small amount of companies");

        List<Company> rndCompanies = new List<Company>();
        while (true)
        {
            int rnd = Random.Range(0, companies.Length);
            if (!rndCompanies.Contains(companies[rnd])) rndCompanies.Add(companies[rnd]);

            if (rndCompanies.Count == Constans.publisherCount || rndCompanies.Count == companies.Length) break;
        }

        Debug.Log(companies.Length);

        publishers = new Publisher[rndCompanies.Count];
        for (int i = 0; i < publishers.Length; i++)
        {
            publishers[i] = new Publisher(rndCompanies[i]);
        }
    }
}