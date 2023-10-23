using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerController : MonoBehaviour
{
    //[SerializeField] Color myColor;
    [SerializeField] Renderer myRenderer;
    [SerializeField] GameObject PickupPraf;
    [SerializeField] GameObject Player;

    //[SerializeField] List<GameObject> AllCollectedObject;
    [SerializeField] bool isRunning;

    [SerializeField] List<Color> AllColors;
    [SerializeField] GameObject ColorChanger;

    [SerializeField] List<GameObject> CollectedObject;
    //float lerpSpeed = 9;

    float counter = 0.7f;

    void Start()
    {
        int randomIndex;

        Debug.Log("Last obj called");
        Debug.Log("Last obj Completee");
        randomIndex = Random.Range(0, AllColors.Count);
        ColorChanger.gameObject.GetComponent<MeshRenderer>().material.color = AllColors[randomIndex];
    }

    private void Update()
    {
        float Hinput = Input.GetAxis("Horizontal");
        //float Vinput = Input.GetAxis("Vertical");

        Player.transform.position += new Vector3(Hinput * Time.deltaTime * 6, 0, Time.deltaTime * 6);
    }
    //*******************    TRIGGER VARIABLES    *********************
    //bool isOn;
    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("Collison Detectt");
        if (other.gameObject.tag == "Pickup")
        {
            Debug.Log("1");

            //GameObject g = Instantiate(PickupPraf, this.transform);
            //g.transform.position += new Vector3(0, counter, 0);
            Vector3 spawnPosition = transform.position + Vector3.up * counter;
            GameObject g = Instantiate(PickupPraf, spawnPosition, Quaternion.identity, this.transform);
            CollectedObject.Add(g);
            counter += 0.4f;

            Destroy(other.gameObject);
        }
        if(other.gameObject.tag == "enemy")
        {
            if(CollectedObject.Count > 0)
            {
                Debug.Log("IF callled");
                GameObject lastObj = CollectedObject[CollectedObject.Count - 1];

                CollectedObject.RemoveAt(CollectedObject.Count - 1);

                Destroy(lastObj);
                Destroy(other.gameObject);
                Debug.Log("IF   Completeee");
            }
            else
            {
                Debug.Log("Game Overrr");
            }
        }
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ColorChange")
        {
            Debug.Log("Color channge called");
            Player.GetComponent<MeshRenderer>().material.color = other.gameObject.GetComponent<MeshRenderer>().material.color;
            //myRenderer.material.color = other.gameObject.GetComponent<MeshRenderer>().material.color;
            for (int i = 0; i < CollectedObject.Count; i++)
            {
                CollectedObject[i].gameObject.GetComponent<MeshRenderer>().material.color = Player.GetComponent<MeshRenderer>().material.color;
            }
            Debug.Log("Color channge Complete ");
        }
    }
    //IEnumerator TriggerOnOff()
    //{
    //    yield return new WaitForSeconds(0.3f);
    //    isOn = true;
    //}
    //void SetColor(Color inColor)
    //{
    //    myColor = inColor;
    //    myRenderer.material.SetColor("_Color", myColor);
    //}
}
