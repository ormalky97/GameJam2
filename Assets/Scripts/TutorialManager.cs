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
                    nextMessage = "Welcome!\nLet's get acquainted with our\nyet-to-be-colonized planet.\n\n Press OK to continue";
                    ShowTutorial(nextTitle, nextMessage, Vector2.zero, new Vector2(350f, 350f), true);
                    break;


                case 0:
                    //Controls
                    nextTitle = "Controls";
                    nextMessage = "Use the mouse to control the game,\nmouse wheel to zoom in and out.\nUse the Arrow Keys or WASD to move the camera around,\n";
                    nextMessage += "and Space to reset the view on the Colony Center.";
                    ShowTutorial(nextTitle, nextMessage, Vector2.zero, new Vector2(600f, 350f), true);
                    break;

                case 1:
                    //resources 1
                    nextTitle = "Resources";
                    nextMessage = "First let's go over the Resources.\n\n At the top left corner is our Resources Panel.";
                    nextMessage += "\n We will use them to build and defend our Colony.\nThe icons represent\nFood, Metal, Oil and Population\ntop to bottom.";
                    ShowTutorial(nextTitle, nextMessage, Vector2.zero, new Vector2(500f, 350f), true);
                    break;

                case 2:
                    //resources 2
                    nextTitle = "Resources";
                    nextMessage = "Each resource is gathered by\na different type of Building,\nwith the exception of Population,\nof which you will learn about in a moment.";
                    ShowTutorial(nextTitle, nextMessage, Vector2.zero, new Vector2(500f, 350f), true);
                    break;

                case 3:
                    //building 1
                    nextTitle = "Building";
                    nextMessage = "At the bottom we have the Build Menu,\nused to build different Buildings that gather\nresources, produce power or\ndefend the colony.";
                    ShowTutorial(nextTitle, nextMessage, Vector2.zero, new Vector2(500f, 350f), true);
                    break;

                case 4:
                    //building 2
                    nextTitle = "Building";
                    nextMessage = "The Food, Metal and Oil categories contain buildings that gather the respective Resources.";
                    nextMessage += "\n\n Click on Food now to open up the\nFood Category Building Menu";
                    ShowTutorial(nextTitle, nextMessage, Vector2.zero, new Vector2(500f, 350f), true);
                    break;

                case 5:
                    //building 3
                    nextTitle = "Building";
                    nextMessage = "As you can see, three different Buildings gather food. You can hover over a building button";
                    nextMessage += " to see this Building's details: production, cost, etc.";
                    check = FindObjectOfType<BuildCategory>().GetActiveCategory();
                    if (check != null && check.name == "Food Panel")
                        ShowTutorial(nextTitle, nextMessage, Vector2.zero, new Vector2(500f, 350f), true);
                    break;

                case 6:
                    //building field
                    nextTitle = "Build a Field";
                    nextMessage = "Go ahead and build a Field to start producing Food.\n";
                    nextMessage += "You will notice that you can only build in a certain area around our Colony Center, this area is represented by the green circle";
                    ShowTutorial(nextTitle, nextMessage, Vector2.zero, new Vector2(500f, 350f), true);
                    break;

                case 7:
                    //building mine and pump
                    nextTitle = "Build a Surface Mine\nand an Oil Pump";
                    nextMessage = "Nice! now your Field will automatically produce Food every second.\n\n";
                    nextMessage += "Now place a Surface Mine from the Metal Category, and an Oil Pump from the Oil Category, to start producing those as well.";
                    if (Stage7())
                    {
                        Time.timeScale = 0;
                        ShowTutorial(nextTitle, nextMessage, Vector2.zero, new Vector2(500f, 350f), true);
                    }
                    break;

                case 8:
                    //population 1
                    Time.timeScale = 1;
                    nextTitle = "Population";
                    nextMessage = "Good job! now let's go over to the Population Catergory\nwhile you gain resources\n";
                    nextMessage += "Population is made out of three factors:\nAvailable Population\nUsed Population (working)\nMax Population.\n\n";
                    nextMessage += "Every time you build a new Building,\nthis Building's Population 'Cost' is the amount of\nAvailable Population needed in order to build it.\n";
                    nextMessage += "While building it, said amount of Colonists\nwill be added to Used Population.\n";
                    nextMessage += "This is represented in the Resources Panel as: \n Used Population / Available Population";
                    if (Stage8())
                    {
                        ShowTutorial(nextTitle, nextMessage, Vector2.zero, new Vector2(700f, 550f), false);
                    }
                    break;

                case 9:
                    //build pod
                    nextTitle = "Build a Pod";
                    nextMessage = "Max Population is the maximum amount of population our colony can house.\n ";
                    nextMessage += "To increase our Max Population, use the Population Category in the build menu.\n\n";
                    nextMessage += "Try to build a Pod now";
                    ShowTutorial(nextTitle, nextMessage, Vector2.zero, new Vector2(500f, 350f), false);
                    break;

                case 10:
                    //population 2
                    nextTitle = "Population";
                    nextMessage = "Well done!\n";
                    nextMessage += "But as you can see in the Resources Panel,\nour Population hasn't increased just yet.\n";
                    nextMessage += "Building a Population Category Building only increases\nour potential housing ability.\n\n";
                    nextMessage += "New Colonists arrive to our Colony every minute or so,\nbased on our resources.\n";
                    nextMessage += "In the Resources Panel, you can see the amount of\nadditional Colonists our colony can house,\nrepresented by the number in brackets.\n\n";
                    nextMessage += "Having a low Resources-per-Colonists value will lower the\npotential amount of Colonists joining our Colony.\n";
                    nextMessage += "However, a high Resources-per-Colonists value\n will increase the amount of Colonists joining our Colony.";
                    if (Stage10())
                        ShowTutorial(nextTitle, nextMessage, Vector2.zero, new Vector2(600f, 600f), false);
                    break;

                case 11:
                    //turrets 1
                    nextTitle = "Turrets";
                    nextMessage = "Various local habitants are marching towards our Colony,\n build Turrets from the Turrets Category in the Build Menu to defend it.\n";
                    nextMessage += "There are different types of turrets, with different stats.\nFor now let's just build two basic turrets.";
                    ShowTutorial(nextTitle, nextMessage, Vector2.zero, new Vector2(600f, 350f), false);
                    break;

                case 12:
                    //turrets 2
                    nextTitle = "Turrets";
                    nextMessage = "Good, now let's see our new Turrets in action!";
                    if (Stage12())
                    {
                        GetComponent<TutorialSpawner>().SendWave();
                        ShowTutorial(nextTitle, nextMessage, Vector2.zero, new Vector2(500f, 350f), true);
                    }
                    break;

                case 13:
                    //destroyed
                    nextTitle = "Destruction";
                    nextMessage = "Enemies can damage and destroy Buildings. Colonists who worked in destroyed buildings are lost as well.\n\n";
                    nextMessage += "In case of a destruction of a Population Category Building,\nOur Colony housing capacity (Max Population) will decrease.\n\nIf our Used Population exceeds our Available Population,\n";
                    nextMessage += "Our latest Building will be destroyed due to being undermanned, thus adjusting our Used Population.\n";
                    nextMessage += "This will repeat until our Used Population is no longer higher than our Available Population";
                    nextMessage += "\n\n YOU WILL LOSE THE GAME WHEN OUR COLONY CENTER IS DESTROYED!";
                    if (NoEnemies())
                        ShowTutorial(nextTitle, nextMessage, Vector2.zero, new Vector2(650f, 550f), false);
                    break;

                case 14:
                    //Stations
                    nextTitle = "Power Stations";
                    nextMessage = "The last Building Category is Stations.\nA Power Station increases the range inside of which we can expand our Colony.";
                    ShowTutorial(nextTitle, nextMessage, Vector2.zero, new Vector2(500f, 350f), false);
                    break;

                case 15:
                    //Upkeep
                    nextTitle = "Upkeep";
                    nextMessage = "Once a minute, our colony pays it's Upkeep Costs,\ndecreasing our Resources as follows:\n";
                    nextMessage += "5 Food per Idle Colonist, 10 Food per Used Colonist\n";
                    nextMessage += "60 Metal per Turret and 60 Oil per Power Station.\n\n";
                    nextMessage += "If you don't have enough Resources to pay the Upkeep,\nTurrets will stop working, Buildings outside of the Colony Center's range\nwill become temporarily disabled,\n and Colonists will leave the colony.";
                    ShowTutorial(nextTitle, nextMessage, Vector2.zero, new Vector2(700f, 400f), false);
                    break;

                case 16:
                    //Upgrading
                    nextTitle = "Let's Upgrade";
                    nextMessage = "In order to increase our production, let's upgrade some buildings.\n\n";
                    nextMessage += "An upgraded Building can only be placed on top of the previous tier Building of the same category.\nFor example, a Farm can only be placed on top of a Field, and a Castle can only be placed on top of a Farm.\n\n";
                    nextMessage += "Upgrading a building will replace the old building with the next tier one.\n";
                    nextMessage += "Try upgrading our Field to a Farm";
                    ShowTutorial(nextTitle, nextMessage, Vector2.zero, new Vector2(700f, 400f), false);
                    break;

                case 17:
                    //Score
                    nextTitle = "Score";
                    nextMessage = "Awesome! Finally, let's go over the score.\n";
                    nextMessage += "The score is displayed in the top right corner of the screen, and consists of two factors:\n";
                    nextMessage += "Survival Time and Colony Max Radius.\n\nThis means playing safe and building close to the Colony Center will result in a lower score.";
                    if(Stage17())
                        ShowTutorial(nextTitle, nextMessage, Vector2.zero, new Vector2(500f, 400f), false);
                    break;

                case 18:
                    //End
                    nextTitle = "End of Tutorial";
                    nextMessage = "That's it! You're finally ready to Colonize!\n\n";
                    nextMessage += "There are additional hidden game mechanics\nfor you to discover...\n\n";
                    nextMessage += "You can go back to the Main Menu to play the game, or continue practicing the basics here.\nEnemies will not spawn here so you can take your time and learn.";
                    ShowTutorial(nextTitle, nextMessage, Vector2.zero, new Vector2(500f, 400f), true);
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
