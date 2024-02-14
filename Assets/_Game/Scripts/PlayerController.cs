using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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
    private Rigidbody rb;

    float counter = 0.7f;
    private Vector3 targetPosition;
    public float initialSpeed = 0.05f;
    private float speed = 0.05f;
    public float smoothTime = 0.1f; // Smoothing time for position interpolation
    private Vector3 velocity = Vector3.zero; // Velocity for smoothing


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        float Hinput = Input.GetAxis("Horizontal");
        if (Hinput < 0 || isLeftBtnClick)
        {
            Debug.Log("Left");
            this.transform.position = new Vector3(rb.transform.position.x - (speed + 0.00f), rb.transform.position.y, rb.transform.position.z);
        }
        else if (Hinput > 0 || isRightBtnClick)
        {
            Debug.Log("Right");
            this.transform.position = new Vector3(rb.transform.position.x + (speed + 0.005f), rb.transform.position.y, rb.transform.position.z);
        }

        rb.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y, rb.transform.position.z + speed);
        //Player.transform.position += new Vector3(Hinput * Time.deltaTime * 6, 0, speed);

        //rb.transform.position = Vector3.Lerp(rb.transform.position, targetPosition, smoothTime);

        if (transform.position.y < -3)
        {
            Time.timeScale = 0;
            GameOverPanel.SetActive(true);
            transform.position = new Vector3(0, 0, 0);
        }
    }   
    
    //*******************    TRIGGER VARIABLES    *********************
    
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Ground")
        {
            transform.position = new Vector3( Mathf.Clamp(transform.position.x, -2.5f, 4.15f),transform.position.y, transform.position.z);
            Debug.Log("Ground colided");
        }
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
                Debug.Log("Time is = "+Time.timeScale);
                Common.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(GameOverSound);
                Debug.Log("Game Overrr");
                GameOverPanel.SetActive(true);
                Time.timeScale = 0;
            }
        }
        if (other.gameObject.tag == "Complete")
        {
            Common.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(CompleteSound);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if(other.gameObject.tag == "Complete2")
        {
            Common.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(CompleteSound);
            CompletePanel.SetActive(true);
        }
        if (other.gameObject.tag == "Over")
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

    bool isLeftBtnClick, isRightBtnClick;
    public void LeftBtnClicked()
    {
        Debug.Log("Clicked");
        isLeftBtnClick =! isLeftBtnClick;
    }
    public void RightBtnClicked()
    {
        Debug.Log("Right");
        isRightBtnClick =! isRightBtnClick;
    }

    

    private Vector2 startPos;
    private float swipeThreshold = 50f;

    private bool isTouchingLeft = false;
    private bool isTouchingRight = false;

    private bool isSwipeUp = false;
    private bool isTouching = false;



    void OnTouchBegan(Vector2 touchPosition)
    {
        // Determine if touch is on the left or right side
        float screenWidth = Screen.width;
        float screenCenter = screenWidth / 2f;
        bool isLeftSide = touchPosition.x < screenCenter;

        if (isLeftSide)
        {
            isTouchingLeft = true;
            isTouchingRight = false; // Reset right side flag
        }
        else
        {
            isTouchingRight = true;
            isTouchingLeft = false; // Reset left side flag
        }
    }

    void OnTouchMoved(Vector2 touchPosition)
    {
        // Check for swipe logic
        float deltaY = touchPosition.y - startPos.y;

        if (deltaY > swipeThreshold)
        {
            isSwipeUp = true;
            isTouching = false; // Disable left/right movement while swiping up
        }
    }

    void OnTouchEnded()
    {
        isTouchingLeft = false;
        isTouchingRight = false;
        isSwipeUp = false;
        isTouching = false;

        // Reset the target position when touch ends
        targetPosition = rb.transform.position;
    }

    void CheckTouchInput()
    {
        foreach (Touch touch in Input.touches)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    OnTouchBegan(touch.position);
                    break;
                case TouchPhase.Moved:
                    OnTouchMoved(touch.position);

                    break;
                case TouchPhase.Ended:
                    OnTouchEnded();
                    break;
            }
        }
    }
}
