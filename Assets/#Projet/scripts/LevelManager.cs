using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class LevelManager : MonoBehaviour
{
    private int row;
    private int col;


    private float gapRow = 1.5f;
    private float gapCol = 1.5f;

    [Range(0f, 5f)]
    private float timeBeforeReset = 1f;
    private bool resetOnGoing = false;

    public Material[] materials;
    public Material defaultMaterial;

    public GameObject itemPrefab;

    public ItemBehaviour[] items;

    private Dictionary<int, Material> itemMaterial = new Dictionary<int, Material>();

    public List<int> selected = new List<int>();

    public List<int> matches = new List<int>();

    public UnityEvent whenPlayerWins;

    private float timer = 0f;
    

    // Start is called before the first frame update
    void Start()
    {
        col =  PlayerPrefs.GetInt("col",3);
        row = PlayerPrefs.GetInt("row",4);

        items = new ItemBehaviour[row * col];
        int index = 0;

        for(int x = 0; x < col; x++)
        {
            for (int z = 0; z < row; z++)
            {
                Vector3 position = new Vector3(x * gapCol, 0, z*gapRow);
                GameObject item = Instantiate(itemPrefab, position, Quaternion.identity);
                item.GetComponent<Renderer>().material = defaultMaterial;

                items[index] = item.GetComponent<ItemBehaviour>();
                items[index].id = index;
                items[index].manager = this;

                index++;

                
            }
            
        }
        GiveMaterials();
    }

    private IEnumerator ResetMaterials(int id1, int id2)
    {
        resetOnGoing = true;
        yield return new WaitForSeconds(timeBeforeReset);
        ResetMaterial(id1);
        ResetMaterial(id2);
        resetOnGoing = false;
    }

    private IEnumerator Win()
    {
        yield return new WaitForSeconds(timeBeforeReset);
        whenPlayerWins?.Invoke();
    }
    public void RevealMaterial(int id)
    {
        if (resetOnGoing == false && !selected.Contains(id) && !matches.Contains(id))
        {
            selected.Add(id);
            Material material = itemMaterial[id];
            items[id].GetComponent<Renderer>().material = material;
            items[id].HasBeenSelected(true);
        }
        
    }

    public void ResetMaterial (int id)
    {
        items[id].GetComponent<Renderer>().material = defaultMaterial;
        items[id].HasBeenSelected(false);
        }



    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        

        if (selected.Count == 2)
        {
            if(itemMaterial[selected[0]] == itemMaterial[selected[1]])
            {
                matches.Add(selected[0]);
                matches.Add(selected[1]);
                items[selected[0]].HasBeenMatched();
                items[selected[1]].HasBeenMatched();

                if (matches.Count >= row * col)
                {
                    StartCoroutine(Win());
                    PlayerPrefs.SetFloat("timer", timer);
                }

            }
            else
            {
                StartCoroutine(ResetMaterials(selected[0], selected[1]));
                
            }
            selected.Clear();

        }


        
    }

    private void GiveMaterials()
    {
        List<int> possibilities = new List<int>();
        for (int i = 0; i < row*col; i++)
        {
            possibilities.Add(i);
        }

        for (int i= 0; i < materials.Length; i++)
        {
            if (possibilities.Count < 2) break;

            int idPoss = Random.Range(0, possibilities.Count);
            int id1 = possibilities[idPoss];
            possibilities.RemoveAt(idPoss);

            idPoss = Random.Range(0, possibilities.Count);
            int id2 = possibilities[idPoss];
            possibilities.RemoveAt(idPoss);

            itemMaterial.Add(id1, materials[i]);
            itemMaterial.Add(id2, materials[i]);
            //items[id1].GetComponent<Renderer>().material = materials[i];
            //items[id2].GetComponent<Renderer>().material = materials[i];  POUR QUE LES COULEURS APPARAISSENT A L ECRAN DIRECTEMENT
        }
    }

   
}
