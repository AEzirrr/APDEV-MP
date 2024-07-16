using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuUiDocument : MonoBehaviour
{
    [SerializeField] GameObject DeveloperMenu;
    private VisualElement root;
    private Button StartButton;
    private Button DeveloperButton;
    private Button QuitButton;

    [SerializeField] private AudioClip ButtonSFX;


    // Start is called before the first frame update
    void Start()
    {
        this.root = GetComponent<UIDocument>().rootVisualElement;
        this.StartButton = this.root.Q<Button>("StartButton");
        this.DeveloperButton = this.root.Q<Button>("DeveloperButton");
        this.QuitButton = this.root.Q<Button>("QuitButton");

        this.StartButton.clicked += this.OnStartButtonClicked;
        this.DeveloperButton.clicked += this.OnDeveloperButtonClicked;
        this.QuitButton.clicked += this.OnQuitButtonClicked;
    }

    private void OnStartButtonClicked()
    {
        SFXManager.instance.PlaySfxClip(ButtonSFX, transform, .1f);
        SceneManager.LoadScene("ClassSelection");
    }

    private void OnDeveloperButtonClicked()
    {
        SFXManager.instance.PlaySfxClip(ButtonSFX, transform, .1f);
        root.style.display = DisplayStyle.None;
        DeveloperMenu.SetActive(true);
    }

    private void OnQuitButtonClicked()
    {
        SFXManager.instance.PlaySfxClip(ButtonSFX, transform, .1f);
        Application.Quit();
    }

    public void DisplayMenu()
    {
        root.style.display = DisplayStyle.Flex;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
