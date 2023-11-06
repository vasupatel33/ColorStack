using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject PickupPraf;
    [SerializeField] GameObject Player;
    [SerializeField] GameObject CompletePanel, GameOverPanel, PausePanel;

    //[SerializeField] List<GameObject> AllCollectedObject;
    [SerializeField] bool isRunning;

    [SerializeField] List<Color> AllColors;

    [SerializeField] List<GameObject> CollectedObject;
    [SerializeField] AudioClip PickupSound, ClickSound, GameOverSound, EnemyPickupSound, CompleteSound;

    float counter = 0.7f;



    //[SerializeField] float moveSpeed = 6.0f;
    //[SerializeField] float swipeSpeed = 6.0f;
    //private Vector3 touchStart;
    //private Vector3 touchEnd;
    private void Start()
    {
        
    }

    private void Update()
    {
        Debug.Log("Current time = " + Time.timeScale);
        float Hinput = Input.GetAxis("Horizontal");
        Player.transform.position += new Vector3(Hinput * Time.deltaTime * 6, 0, Time.deltaTime * 6);
        if(transform.position.y < -3)
        {
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);
            transform.position = new Vector3(0, 0, 0);
        }

        //if (Input.touchCount > 0)
        //{
        //    Debug.Log("Touch begin");
        //    Touch touch = Input.GetTouch(0);

        //    if (touch.phase == TouchPhase.Began)
        //    {
        //        touchStart = touch.position;
        //    }
        //    else if (touch.phase == TouchPhase.Moved)
        //    {
        //        touchEnd = touch.position;

        //        // Calculate the swipe distance
        //        Vector2 swipeVector = touchEnd - touchStart;

        //        // Check if it's a left swipe
        //        if (swipeVector.x < 0)
        //        {
        //            // Move the player left
        //            PlayerMove(-swipeSpeed * Time.deltaTime, 0);
        //            Debug.Log("Leftt");
        //        }
        //        // Check if it's a right swipe
        //        else if (swipeVector.x > 0)
        //        {
        //            // Move the player right
        //            PlayerMove(swipeSpeed * Time.deltaTime, 0);
        //            Debug.Log("Rightt");
        //        }

        //        touchStart = touch.position;
        //    }
        //}
        //else
        //{
        //    Debug.Log("not workgin touch");
        //}
        //// Handle arrow key input
        //PlayerMove(Hinput * moveSpeed * Time.deltaTime, 0);
    }
    //private void PlayerMove(float horizontalMovement, float verticalMovement)
    //{
    //    // Move the player
    //    Vector3 movement = new Vector3(horizontalMovement, verticalMovement, 0);
    //    transform.Translate(movement);
    //}

    //*******************    TRIGGER VARIABLES    *********************
    bool levelflag;
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Pickup")
        {
           // Common.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(PickupSound);

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
                
                Common.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(EnemyPickupSound);
                GameObject lastObj = CollectedObject[CollectedObject.Count - 1];

                CollectedObject.RemoveAt(CollectedObject.Count - 1);

                Destroy(lastObj);
                Destroy(other.gameObject);
                counter = CollectedObject.Count * 0.4f;
            }
            else
            {
                //transform.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
                Time.timeScale = 0;
                Debug.Log("Time is = "+Time.timeScale);
                Common.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(GameOverSound);
                Debug.Log("Game Overrr");
                GameOverPanel.SetActive(true);
            }
        }
        if(other.gameObject.tag == "Complete")
        {
            if (!levelflag)
            {
                Common.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(CompleteSound);
                SceneManager.LoadScene(2);
            }
        }
        if(other.gameObject.tag == "Complete2")
        {
            Common.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(CompleteSound);
            CompletePanel.SetActive(true);
        }
        if(other.gameObject.tag == "Over")
        {
            Time.timeScale = 0;
            Common.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(GameOverSound);
            GameOverPanel.SetActive(true);
        }
    }
    public void CompleteButtonClicked()
    {
        Common.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(ClickSound);
        SceneManager.LoadScene(1);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ColorChange")
        {
            Debug.Log("Color channge called");
            Common.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(PickupSound);
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
        Common.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(ClickSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        Debug.Log("Time = "+Time.timeScale);
    }
    public void ExitBtnGameOverPanel()
    {
        Time.timeScale = 1;
        Common.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(ClickSound);
        SceneManager.LoadScene(0);
    }
    public void PauseBtnClicked()
    {
        Time.timeScale = 0;
        Common.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(ClickSound);
        PausePanel.SetActive(true);
    }
    public void PausePanelClose()
    {
        Time.timeScale = 1;
        Common.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(ClickSound);
        PausePanel.SetActive(false);
    }
    public void RetryBtnPausePanel()
    {
        Time.timeScale = 1;
        Common.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(ClickSound);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ExitBtnPausePanel()
    {
        Time.timeScale = 1;
        Common.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(ClickSound);
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
