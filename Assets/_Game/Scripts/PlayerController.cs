using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject PickupPraf;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject CompletePanel, GameOverPanel;

    //[SerializeField] List<GameObject> AllCollectedObject;
    [SerializeField] bool isRunning;

    [SerializeField] List<Color> AllColors;

    [SerializeField] List<GameObject> CollectedObject;
  
    float counter = 0.7f;
    private void Start()
    {
        
    }

    private void Update()
    {
        float Hinput = Input.GetAxis("Horizontal");
        //float Vinput = Input.GetAxis("Vertical");

        Player.transform.position += new Vector3(Hinput * Time.deltaTime * 6, 0, Time.deltaTime * 6);
    }
    //*******************    TRIGGER VARIABLES    *********************
    //bool isOn;
    bool levelflag;
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
            g.gameObject.GetComponent<MeshRenderer>().material.color = Player.GetComponent<MeshRenderer>().material.color;
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
                counter = CollectedObject.Count * 0.4f;
            }
            else
            {
                Debug.Log("Game Overrr");
                GameOverPanel.SetActive(true);
            }
        }
        if(other.gameObject.tag == "Complete")
        {
            if(!levelflag)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                levelflag = true;
            }
            else
            {
                CompletePanel.SetActive(true);
                levelflag = false;
            }
        }
        if(other.gameObject.tag == "Over")
        {
            GameOverPanel.SetActive(true);
        }
    }
    public void CompleteButtonClicked()
    {
        SceneManager.LoadScene(1);
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
    public void RetryBtnGameOverPanel()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitBtnGameOverPanel()
    {
        SceneManager.LoadScene(0);
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
