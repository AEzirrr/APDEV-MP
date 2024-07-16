using UnityEngine;
using UnityEngine.SceneManagement;

public class ClassSelection : MonoBehaviour
{
    public ClassStats warriorStats = new ClassStats("Warrior", 18, 12, 16, 10, 8, 14);
    public ClassStats mageStats = new ClassStats("Mage", 8, 12, 10, 18, 16, 10);
    public ClassStats rogueStats = new ClassStats("Rogue", 10, 18, 14, 12, 10, 16);
    public ClassStats clericStats = new ClassStats("Cleric", 14, 10, 14, 12, 18, 12);

    public void SelectWarrior()
    {
        SetPlayerClass(warriorStats);
        LoadMarketScene();
    }

    public void SelectMage()
    {
        SetPlayerClass(mageStats);
        LoadMarketScene();
    }

    public void SelectRogue()
    {
        SetPlayerClass(rogueStats);
        LoadMarketScene();
    }

    public void SelectCleric()
    {
        SetPlayerClass(clericStats);
        LoadMarketScene();
    }

    private void SetPlayerClass(ClassStats classStats)
    {
        PlayerPrefs.SetString("PlayerClassName", classStats.className);
        PlayerPrefs.SetInt("PlayerStrength", classStats.strength);
        PlayerPrefs.SetInt("PlayerDexterity", classStats.dexterity);
        PlayerPrefs.SetInt("PlayerConstitution", classStats.constitution);
        PlayerPrefs.SetInt("PlayerIntelligence", classStats.intelligence);
        PlayerPrefs.SetInt("PlayerWisdom", classStats.wisdom);
        PlayerPrefs.SetInt("PlayerCharisma", classStats.charisma);
    }

    public void OnBackButtonClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void LoadMarketScene()
    {
        SceneManager.LoadScene("MarketScene");
    }
}
