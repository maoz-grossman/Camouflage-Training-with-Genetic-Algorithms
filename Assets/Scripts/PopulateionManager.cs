using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PopulateionManager : MonoBehaviour
{
    public GameObject personPrefab;
    public int populationSize;
    List<GameObject> population = new List<GameObject>();
    public static float elapsed = 0;
    int trialTime = 10;
    int generation = 1;

    GUIStyle guistyle = new GUIStyle();

    //Borders
    [SerializeField]
    GameObject leftBorder;
    [SerializeField]
    GameObject rightBorder;
    [SerializeField]
    GameObject upBorder;
    [SerializeField]
    GameObject downBorder;

    private float left=0;
    private float right=0;
    private float up=0;
    private float down=0;

    // Start is called before the first frame update
    void Start()
    {
        left = leftBorder.transform.position.x;
        right = rightBorder.transform.position.x;
        up = upBorder.transform.position.y;
        down = downBorder.transform.position.y;
 

        for (int i = 0; i < populationSize; i++)
        {
            Vector3 pos = new Vector3(Random.Range(left, right), Random.Range(down, up), 0);
            GameObject go = Instantiate(personPrefab, pos, Quaternion.identity);
            go.GetComponent<DNA>().R = Random.Range(0.0f,1.0f);
            go.GetComponent<DNA>().B = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().G = Random.Range(0.0f, 1.0f);
            go.GetComponent<DNA>().personScale = Random.Range(0.25f, 0.8f);
            population.Add(go);
        }
    }

    private void OnGUI()
    {
        guistyle.fontSize = 20;
        guistyle.normal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 20), "Generation: " + generation, guistyle);
        GUI.Label(new Rect(10, 65, 100, 20), "Trial Time: " + (int)elapsed, guistyle);
    }

    GameObject Breed(GameObject parent1, GameObject parent2)
    {
        Vector3 pos = new Vector3(Random.Range(left, right), Random.Range(down, up), 0);
        GameObject offspring = Instantiate(personPrefab, pos, Quaternion.identity);
        DNA dna1 = parent1.GetComponent<DNA>();
        DNA dna2 = parent2.GetComponent<DNA>();
        //swap parent DNA 
        offspring.GetComponent<DNA>().R = Random.Range(0, 10) < 5 ? dna1.R : dna2.R;
        offspring.GetComponent<DNA>().G = Random.Range(0, 10) < 5 ? dna1.G : dna2.G;
        offspring.GetComponent<DNA>().B = Random.Range(0, 10) < 5 ? dna1.B : dna2.B;
        offspring.GetComponent<DNA>().personScale= Random.Range(0, 10) < 5 ? dna1.personScale : dna2.personScale;
        return offspring;

    }


    void BreedNewPopulation()
    {
        List<GameObject> newPopulation = new List<GameObject>();
        List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<DNA>().timeToDie).ToList();
        population.Clear();
        //Bred upper half of sorted list
        for (int i= (int) (sortedList.Count/2.0f)-1; i<sortedList.Count-1;i++)
        {
            population.Add(Breed(sortedList[i], sortedList[i + 1]));
            population.Add(Breed(sortedList[i + 1], sortedList[i]));
        }

        //Destory all population and previous population
        for (int i=0; i< sortedList.Count; ++i)
        {
            Destroy(sortedList[i]);
        }
        generation++;
    }


    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if(elapsed>trialTime)
        {
            BreedNewPopulation();
            elapsed = 0;
        }
    }
}
