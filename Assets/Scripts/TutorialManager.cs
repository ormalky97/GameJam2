using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [Header("Refs")]
    public Text title;
    public Text text;
    public RectTransform panel;

    [Header("Debug")]
    public int stage = -1;

    string nextTitle;
    string nextMessage;
    bool inMessage = false;

    GameObject check;

    private void Update()
    {
        if(!inMessage)
        {
            switch (stage)
            {
                case -1:
                    //tutorial start
                    nextTitle = "Tutorial";
                    nextMessage = "Welcome!\nLet's get acquainted with our yet-to-be-colonized planet.\n\n Press OK to continue";
                    ShowTutorial(nextTitle, nextMessage, Vector2.zero, new Vector2(500f, 500f), true);
                    break;


                case 0:
                    //Controls
                    nextTitle = "Controls";
                    nextMessage = "Use the mouse to control the game, Mouse wheel to zoom in and out.\nUse the Arrow Keys or WASD to move the camera around,\nand Space to reset the view to the Colony Center.";
                    ShowTutorial(nextTitle, nextMessage, Vector2.zero, new Vector2(500f, 500f), true);
                    break;

                case 1:
                    //resources 1
                    nextTitle = "Resources";
                    nextMessage = "First let's go over the Resources.\n\n At the top left corner is our Resources Panel.";
                    nextMessage += "\n We will use them to build and defend our Colony.\nThe icons, top to bottom, represent\nFood, Metal, Oil and Population.";
                    ShowTutorial(nextTitle, nextMessage, Vector2.zero, new Vector2(500f, 500f), true);
                    break;

                case 2:
                    //resources 2
                    nextTitle = "Resources";
                    nextMessage = "Each resource is gathered by a different type of Building,\nwith the exception of Population,\nof which we will learn about in a moment.";
                    ShowTutorial(nextTitle, nextMessage, Vector2.zero, new Vector2(500f, 500f), true);
                    break;

                case 3:
                    //building 1
                    nextTitle = "Building";
                    nextMessage = "At the bottom we have the Build Menu,\nused to build different Buildings that gather\nresources, power or defend the colony";
                    ShowTutorial(nextTitle, nextMessage, Vector2.zero, new Vector2(500f, 500f), true);
                    break;

                case 4:
                    //building 2
                    nextMessage = "The Food, Metal and Oil categories contain buildings that gather the respective Resources.";
                    nextMessage += "\n\n Click on Food now to open up the Food Category Building Menu";
                    ShowTutorial(nextMessage, Vector2.zero, new Vector2(500f, 300f), true);
                    break;

                case 5:
                    //building 3
                    nextMessage = "As you can see, three different Buildings gather food. You can hover over a building button";
                    nextMessage += " to see this Building's details: production, cost, etc.";
                    check = FindObjectOfType<BuildCategory>().GetActiveCategory();
                    if (check != null && check.name == "Food Panel")
                        ShowTutorial(nextMessage, Vector2.zero, new Vector2(500f, 300f), true);
                    break;

                case 6:
                    //building field
                    nextMessage = "Go ahead and build a Field to start producing Food.\n";
                    nextMessage += "You will notice that you can only build in a certain area around our Colony Center, this area is represented by the green circle";
                    ShowTutorial(nextMessage, Vector2.zero, new Vector2(500f, 300f), true);
                    break;

                case 7:
                    //building mine and pump
                    nextMessage = "Nice! now your Field will automatically produce Food every second.\n\n";
                    nextMessage += "Now place a Surface Mine from the Metal Category, and an Oil Pump from the Oil Category, to start producing those as well.";
                    if(Stage7())
                        ShowTutorial(nextMessage, Vector2.zero, new Vector2(500f, 300f), true);
                    break;

                case 8:
                    //population 1
                    nextMessage = "Good job! now let's go over to the Population Catergory while you gain resources\n";
                    nextMessage += "Population is made out of three factors: Available Population, Used Population (working) and Max Colony Habitants.\n\n";
                    nextMessage += "Each time you build a new Building, this Building's Population 'Cost' is the amount of Available Population needed in order to build it.\n";
                    nextMessage += "building it, this amount of Colonists will be added to Used Population.\n";
                    nextMessage += "This is represented in the Resources Panel as: \n Used Population / Available Population";
                    if (Stage8())
                        ShowTutorial(nextMessage, Vector2.zero, new Vector2(700f, 400f), false);
                    break;

                case 9:
                    //build pod
                    nextMessage = "Max Habitants (or Max Population) is the maximum amount of population our colony can house\n ";
                    nextMessage += "To increase our Max Population, use the Population Category in the build menu\n\n";
                    nextMessage += "Try to build a Pod now";
                    ShowTutorial(nextMessage, Vector2.zero, new Vector2(400f, 400f), false);
                    break;

                case 10:
                    //population 2
                    nextMessage = "Well done!\n";
                    nextMessage += "But as you can see in the Resources Panel, our Population hasn't increased just yet.\nBuilding a Population Category Building only increases your potential housing ability.\n\n";
                    nextMessage += "New Colonists arrive to our Colony every minute or so, based on our resources.\n";
                    nextMessage += "In the Resources Panel, you can see the amount of additional Colonists our colony can house,\nrepresented by the number in brackets.\n";
                    nextMessage += "Having a low Resources-per-Colonists value will lower the potential amount of Colonists joining our Colony.\n";
                    nextMessage += "However, a high Resources-per-Colonists value will increase the amount of Colonists joining our Colony.";
                    if (Stage10())
                        ShowTutorial(nextMessage, Vector2.zero, new Vector2(700f, 500f), false);
                    break;

                case 11:
                    //turrets 1
                    nextMessage = "Enemies are planning continuous attacks on our Colony,\n build Turrets from the Turrets Category in the Build Menu to defend it.\n";
                    nextMessage += "There are different types of turrets, with different stats.\nFor now let's just build two basic turrets.";
                    ShowTutorial(nextMessage, Vector2.zero, new Vector2(300f, 300f), false);
                    break;

                case 12:
                    //turrets 2
                    nextMessage = "Good, now let's see our new Turrets in action!";
                    if (Stage12())
                    {
                        GetComponent<TutorialSpawner>().SendWave();
                        ShowTutorial(nextMessage, Vector2.zero, new Vector2(300f, 300f), true);
                    }
                    break;

                case 13:
                    //destroyed
                    nextMessage = "Enemies can damage and destroy Buildings. Colonists who worked in destroyed buildings are lost as well.\n";
                    nextMessage += "In case of a destruction of a Population Category Building, Our Colony housing capacity will decrease.\n If our Used Population exceeds our available population,\n";
                    nextMessage += "Our latest Building will be destroyed due to being undermanned, thus adjusting our Used Population.\n";
                    nextMessage += "This will repeat until our Used Population is no longer higher than our Available Population";
                    nextMessage += "\n\n YOU WILL LOSE THE GAME WHEN OUR COLONY CENTER IS DESTROYED!";
                    if (NoEnemies())
                        ShowTutorial(nextMessage, Vector2.zero, new Vector2(500f, 500f), false);
                    break;

                case 14:
                    //Stations
                    nextMessage = "The last Building Category is Stations. A Power Station increases the range inside of which you can expand our Colony.";
                    ShowTutorial(nextMessage, Vector2.zero, new Vector2(300f, 300f), false);
                    break;

                case 15:
                    //Upkeep
                    nextMessage = "Once a minute, our colony pays it's Upkeep Costs, decreasing our Resources as follows:\n";
                    nextMessage += "5 Food per Free Colonist, 10 Food per Used Colonist\n";
                    nextMessage += "60 Metal per Turret and 60 Oil per Station\n";
                    nextMessage += "If you don't have enough Resources to pay the Upkeep,\nTurrets will stop working, Buildings outside of the Colony Center's range will become temporarily disabled,\n and Colonists will leave the colony.";
                    ShowTutorial(nextMessage, Vector2.zero, new Vector2(500f, 500f), false);
                    break;

                case 16:
                    //Upgrading
                    nextMessage = "In order to increase our production, let's upgrade some buildings.\n";
                    nextMessage += "An upgraded Building can only be placed on top of the previous tier Building of the same category.\nFor example, a Farm can only be placed on top of a Field, and a Castle can only be placed on top of a Farm.\n";
                    nextMessage += "Upgrading a building will replace the old building with the next tier one.\n";
                    nextMessage += "Try upgrading your Field to a Farm";
                    ShowTutorial(nextMessage, Vector2.zero, new Vector2(500f, 500f), false);
                    break;

                case 17:
                    //Score
                    nextMessage = "Awesome! Finally, let's go over the score.\n";
                    nextMessage += "The score is displayed in the top right corner of the screen, and consists of two factors:\n";
                    nextMessage += "Survival Time and Colony Max Radius.\n\nThis means playing safe and building close to the Colony Center will result in a lower score.";
                    if(Stage17())
                        ShowTutorial(nextMessage, Vector2.zero, new Vector2(500f, 500f), false);
                    break;

                case 18:
                    //End
                    nextMessage = "That's it! You're finally ready to Colonize!\n\n";
                    nextMessage += "There are additional hidden game mechanics for you to discover...\n";
                    nextMessage += "You can go back to the Main Menu to play the game, or continue practicing the basics here.\nEnemies will not spawn here so you can take your time and learn.";
                    ShowTutorial(nextMessage, Vector2.zero, new Vector2(500f, 500f), true);
                    break;

            }
        }
    }

    public void Continue()
    {
        panel.gameObject.SetActive(false);

        Time.timeScale = 1f;
        FindObjectOfType<PauseMenu>().isPaused = false;

        stage++;
        inMessage = false;
    }

    public void ShowTutorial(string headline, string message, Vector2 position, Vector2 size, bool pause)
    {
        panel.gameObject.SetActive(true);

        if (pause)
        {
            Time.timeScale = 0f;
            FindObjectOfType<PauseMenu>().isPaused = true;
        }

        panel.sizeDelta = size;
        panel.localPosition = position;

        title.text = headline;
        text.text = message;
        inMessage = true;
    }

    bool Stage7()
    {
        foreach(GameObject building in FindObjectOfType<BuildingsManager>().buildings)
        {
            if(building.GetComponent<Sites>().title == "Field")
                return true;
        }
        return false;
    }

    bool Stage8()
    {
        bool pump = false;
        bool mine = false;
        foreach (GameObject building in FindObjectOfType<BuildingsManager>().buildings)
        {
            if (building.GetComponent<Sites>().title == "Oil Pump")
                pump = true;

            if (building.GetComponent<Sites>().title == "Surface Mine")
                mine = true;

            if (pump && mine)
                return true;
        }
        return false;
    }

    bool Stage10()
    {
        foreach (GameObject building in FindObjectOfType<BuildingsManager>().buildings)
        {
            if (building.GetComponent<Sites>().title == "Pod")
                return true;
        }
        return false;
    }

    bool Stage12()
    {
        int count = 0;
        foreach (GameObject building in FindObjectOfType<BuildingsManager>().buildings)
        {
            if (building.GetComponent<Sites>().title == "Turret")
                count++;
            if (count >= 2)
                return true;
        }
        return false;
    }

    bool NoEnemies()
    {
        if (FindObjectOfType<Enemy>() == null)
            return true;
        else
            return false;
    }

    bool Stage17()
    {
        foreach (GameObject building in FindObjectOfType<BuildingsManager>().buildings)
        {
            if (building.GetComponent<Sites>().title == "Farm")
                return true;
        }
        return false;
    }
}
