using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MarketManager : MonoBehaviour
{
    public static MarketManager Instance;

    private Scene gameScene;
    [SerializeField]
    private MarketUIDocument marketUIDocument;

    public bool IsDiceRolled = false;
    public bool IsCombatDone = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        gameScene = SceneManager.GetSceneByName("MarketScene");
        if (marketUIDocument == null)
        {
            Debug.LogError("MarketUIDocument not found");
        }
    }

    public void OnDiceButtonClicked(string sceneName, int requirements, string opt, int optionChosen)
    {
        marketUIDocument.HideDialogueBox();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        RequirementProperties.statsRequirement = requirements;
        RequirementProperties.playerOpt = opt;
        RequirementProperties.OptionChosen = optionChosen;
        GameSceneOBJ(gameScene, false);
    }

    public void OnCombatButtonClicked(string sceneName)
    {
        marketUIDocument.HideDialogueBox();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        GameSceneOBJ(gameScene, false);
    }

    private void GameSceneOBJ(Scene scene, bool active)
    {
        foreach (GameObject obj in scene.GetRootGameObjects())
        {
            if (obj.name == "MarketManager")
            {
                obj.SetActive(true);
            }
            else if (obj.name == "MarketUIDocument")
            {
                obj.SetActive(true);
            }
            else if (obj.name == "EventSystem")
            {
                obj.SetActive(true);
            }
            else
            {
                obj.SetActive(active);
            }
        }
    }

    public void OnDiceSceneClosed()
    {
        SceneManager.UnloadSceneAsync("DiceRoll");
        GameSceneOBJ(gameScene, true);

        if (marketUIDocument != null)
        {
            marketUIDocument.gameObject.SetActive(true);
        }

        IsDiceRolled = true;
    }

    public void OnCombatClosed()
    {
        SceneManager.UnloadSceneAsync("CombatScene");
        GameSceneOBJ(gameScene, true);

        if (marketUIDocument != null)
        {
            marketUIDocument.gameObject.SetActive(true);
        }

        IsCombatDone = true;
    }
}
