using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject SettingPanel;
    [SerializeField] Button MusicBtn, SoundBtn;
    [SerializeField] Sprite OffSprite, OnSprite;
    [SerializeField] Animator SettingPanelAnim;

    [SerializeField] AudioClip MusicClip, ClickSound;
    //public void LoadGame()
    //{
    //    Common.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(ClickSound);
    //    SceneManager.LoadScene(1);
    //}
    public void BackBtnSettingPanel()
    {
        Common.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(ClickSound);
        SettingPanelAnim.Play("SettinPanelClose");
        Invoke("WaitForClose", 1);
    }
    private void WaitForClose()
    {
        Common.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(ClickSound);
        SettingPanel.SetActive(false);
    }

    public void SettingPanelOpen()
    {
        Common.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(ClickSound);
        SettingPanel.SetActive(true);
        SettingPanelAnim.Play("SettinPanel");
    }
    public void StartBtn()
    {
        Common.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().PlayOneShot(ClickSound);
        SceneManager.LoadScene(1);
    }
    public void QuitBtn()
    {
        Application.Quit();
    }
    public void MusicManagement()
    {
        if (Common.instance.musicPlaying == true)
        {
            MusicBtn.GetComponent<Image>().sprite = OffSprite;
            Common.instance.gameObject.transform.GetChild(0).GetComponent<AudioSource>().mute = true;
            Common.instance.musicPlaying = false;
        }
        else
        {
            MusicBtn.GetComponent<Image>().sprite = OnSprite;
            Common.instance.musicPlaying = true;
            Common.instance.gameObject.transform.GetChild(0).GetComponent<AudioSource>().mute = false;
        }
    }
    public void MusicSet()
    {
        Common.instance.gameObject.transform.GetChild(0).GetComponent<AudioSource>().clip = MusicClip;
        Common.instance.gameObject.transform.GetChild(0).GetComponent<AudioSource>().Play();
        //CommonScript.instance.gameObject.transform.GetChild(0).GetComponent<AudioSource>().PlayOneShot(MusicClip);
        if (Common.instance.musicPlaying == true)
        {
            Common.instance.gameObject.transform.GetChild(0).GetComponent<AudioSource>().mute = false;
            MusicBtn.GetComponent<Image>().sprite = OnSprite;
        }
        else
        {
            Common.instance.gameObject.transform.GetChild(0).GetComponent<AudioSource>().mute = true;
            MusicBtn.GetComponent<Image>().sprite = OffSprite;
        }
    }
    public void SoundManagement()
    {
        if (Common.instance.soundPlaying == true)
        {
            SoundBtn.GetComponent<Image>().sprite = OffSprite;
            Common.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().mute = true;
            Common.instance.soundPlaying = false;
        }
        else
        {
            SoundBtn.GetComponent<Image>().sprite = OnSprite;
            Common.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().mute = false;
            Common.instance.soundPlaying = true;
        }
    }
    public void SoundSet()
    {
        if (Common.instance.soundPlaying == true)
        {
            Common.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().mute = false;
            SoundBtn.GetComponent<Image>().sprite = OnSprite;
        }
        else
        {
            Common.instance.gameObject.transform.GetChild(1).GetComponent<AudioSource>().mute = true;
            SoundBtn.GetComponent<Image>().sprite = OffSprite;
        }
    }
}
