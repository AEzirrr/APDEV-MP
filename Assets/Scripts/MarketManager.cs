using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MarketManager : MonoBehaviour
{
    private Scene gameScene;
    [SerializeField]
    private MarketUIDocument marketUIDocument;

    void Start()
    {
        gameScene = SceneManager.GetSceneByName("MarketScene");
        if (marketUIDocument == null)
        {
            Debug.LogError("MarketUIDocument not found");
        }
    }

    public void OnDiceButtonClicked(string sceneName)
    {
        marketUIDocument.HideDialogueBox();
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
        GameSceneOBJ(gameScene, false);
    }

    private void GameSceneOBJ(Scene scene, bool active)
    {
        foreach (GameObject obj in scene.GetRootGameObjects())
        {
            if (obj.name == "MarketManager") //Will change soon to accommodate all area scenes
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
    }
}
