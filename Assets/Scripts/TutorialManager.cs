using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [Header("Refs")]
    public Text text;
    public RectTransform panel;

    [Header("Debug")]
    public int stage = -1;

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
                    nextMessage = "Welcome!\nLet's get acquainted with our yet-to-be-colonized planet.\n\n Press OK to continue";
                    ShowTutorial(nextMessage, Vector2.zero, new Vector2(300f, 300f), true);
                    break;


                case 0:
                    //Controls
                    nextMessage = "Use the mouse to control the game. Mouse wheel to zoom in and out, and Arrow Keys or WASD to move the camera around.";
                    ShowTutorial(nextMessage, Vector2.zero, new Vector2(300f, 300f), true);
                    break;

                case 1:
                    //resources 1
                    nextMessage = "First let's go over the Resources.\n\n At the top left corner is our Respurces Panel.";
                    nextMessage += "\n We will use them to build and defend your Colony. The icons, top to bottom, represent\nFood, Metal, Oil and Population.";
                    ShowTutorial(nextMessage, Vector2.zero, new Vector2(500f, 300f), true);
                    break;

                case 2:
                    //resources 2
                    nextMessage = "Each resource is gathered by a different type of Building, the only exception is Population, of which we will learn about in a moment.";
                    ShowTutorial(nextMessage, Vector2.zero, new Vector2(300f, 300f), true);
                    break;

                case 3:
                    //building 1
                    nextMessage = "At the bottom we have the Build Menu, used to build different Buildings that gather resources, power or defend the colony";
                    ShowTutorial(nextMessage, Vector2.zero, new Vector2(300f, 300f), true);
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
                    nextMessage += "You will notice that you can only build in a certain area around your Colony Center, this area is represented by the green circle";
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
                    nextMessage = "Max Habitants (or Max Population) is the maximum amount of population your colony can house\n ";
                    nextMessage += "To increase your Max Population, use the Population Category in the build menu\n\n";
                    nextMessage += "Try to build a Pod now";
                    ShowTutorial(nextMessage, Vector2.zero, new Vector2(400f, 400f), false);
                    break;

                case 10:
                    //population 2
                    nextMessage = "Well done!\n";
                    nextMessage += "But as you can see in the Resources Panel, your Population hasn't increased just yet.\nBuilding a Population Category Building only increases your potential housing ability.\n\n";
                    nextMessage += "New Colonists arrive to our Colony every minute or so, based on your resources.\n";
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
                    nextMessage += "Your latest Building will be destroyed due to being undermanned, thus adjusting your Used Population.\n";
                    nextMessage += "This will repeat until your Used Population is no longer higher than your Available Population";
                    nextMessage += "\n\n YOU WILL LOSE THE GAME WHEN YOUR COLONY CENTER IS DESTROYED!";
                    if (NoEnemies())
                        ShowTutorial(nextMessage, Vector2.zero, new Vector2(500f, 500f), false);
                    break;

                case 14:
                    //Stations
                    nextMessage = "The last Building Category is Stations. A Power Station increases the range inside of which you can expand your Colony.";
                    ShowTutorial(nextMessage, Vector2.zero, new Vector2(300f, 300f), false);
                    break;

                case 15:
                    //Upkeep
                    nextMessage = "Once a minute, your colony pays it's Upkeep Cost, decreasing your Resources as follows:\n";
                    nextMessage += "5 Food per Free Colonist, 10 Food per Used Colonist\n";
                    nextMessage += "60 Metal per Turret and 60 Oil per Station \n";
                    nextMessage += "If you don't have enough Resources to pay your Upkeep, Turrets will stop working and colonists will leave the colony.";
                    ShowTutorial(nextMessage, Vector2.zero, new Vector2(500f, 500f), false);
                    break;

                case 16:
                    //Upgrading
                    nextMessage = "In order to increase your production you will want to upgrade your buildings. \n";
                    nextMessage += "An upgraded building can only be placed on top of the previous tier building of the same type. For example a Farm can only be placed on top of a Field, and a Castle can only be placed on top of a Farm. ";
                    nextMessage += "Upgrading a building will replacing the old building with the next tier one. ";
                    nextMessage += "Try upgrading your Field into a Farm";
                    ShowTutorial(nextMessage, Vector2.zero, new Vector2(500f, 500f), false);
                    break;

                case 17:
                    //Score
                    nextMessage = "Awesome! Finally, let's go over the score.\n";
                    nextMessage += "The scores are displayed in the top right corner of the screen, as you can see there are 2 factors for the score: \n";
                    nextMessage += "How long you survive, and how big did your colony get. This means playing safe and only building close by will result in a lower score.";
                    if(Stage17())
                        ShowTutorial(nextMessage, Vector2.zero, new Vector2(500f, 500f), false);
                    break;

                case 18:
                    //End
                    nextMessage = "That's it! You're finaly ready to play the game!\n\n";
                    nextMessage += "There are a couple more mechanics in the game, but the fun is to learn by yourself! \n";
                    nextMessage += "You can go back to the Main Menu to play the game, or practice here to learn the game. Enemies will not spawn here so you can take your time and learn";
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

    public void ShowTutorial(string message, Vector2 position, Vector2 size, bool pause)
    {
        panel.gameObject.SetActive(true);

        if (pause)
        {
            Time.timeScale = 0f;
            FindObjectOfType<PauseMenu>().isPaused = true;
        }

        panel.sizeDelta = size;
        panel.localPosition = position;

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
