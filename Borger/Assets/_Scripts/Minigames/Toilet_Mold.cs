using UnityEngine;

public class Toilet_Mold : MonoBehaviour
{
    [SerializeField] private GameObject moldPrefab;
    private int currentmoldtimer;
    [SerializeField] private int basemoldtimer;
    private GameObject[] CurrentMolds;

    private void Start()
    {
        currentmoldtimer = basemoldtimer;
    }

    private void Update()
    {
        if (currentmoldtimer <= 0)
        {
            SpawnMold();
            currentmoldtimer = basemoldtimer;
        }
        else
        {
            currentmoldtimer--;
        }
    }

    void SpawnMold()
    {
        CurrentMolds = GameObject.FindGameObjectsWithTag("Mold");
        if (CurrentMolds.Length < 5)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.2f, 1f), 0);
            Instantiate(moldPrefab, spawnPosition, Quaternion.identity, gameObject.transform);
        }
    }

}
