using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    private System.Random rnd = new System.Random();

    public GameObject[] planetTypes;
    public GameObject systemContainer;
    // public Constellation[] constellations;

    // Distances between systems and planets, TODO: make it customizable at create game screen
    private float min_dist = 40;
    private float max_dist = 100;
    private float min_sys_dist = 30;
    private float max_sys_dist = 40;

    private Vector2 distanceKeeper = Vector2.zero; // distance between systems

    // For id listing purposes
    private int starIdCounter = 1, systemIdCounter = 1;

    void Start()
    {
        rndSysCreator(10);
    }

    Star rndStarCreator(StarSystem sys)
    {
        Star s = new Star(StarNameGenerator(), rnd.Next(15), planetTypes[rnd.Next(planetTypes.Length)], starIdCounter, sys);
        starIdCounter++;
        return s;
    }

    void rndSysCreator(int maxStarAmount)
    {
        // Create system object
        int systemStarAmount = rnd.Next(4, maxStarAmount);
        StarSystem system = new StarSystem(StarSystemNameGenerator(), systemIdCounter, systemStarAmount);
        systemIdCounter++;

        // Get a random location for the system to be in
        distanceKeeper = new Vector2(distanceKeeper.x + Random.Range(min_sys_dist, max_sys_dist), distanceKeeper.y + Random.Range(min_sys_dist, max_sys_dist));
        Vector3 rndSysPos = new Vector3(Random.Range(min_dist, max_dist) + distanceKeeper.x, 0, Random.Range(min_dist, max_dist) + distanceKeeper.y); // variable to store random system position

        // Instnatiate a center point at the system location
        GameObject emptyObj = new GameObject();
        GameObject sysCenter = Instantiate(emptyObj, rndSysPos, Quaternion.identity);

        sysCenter.name = "sys" + system.sysId + " " + system.sysName;
        if (systemContainer != null)
            sysCenter.transform.parent = systemContainer.transform;
        else
            Debug.Log("StarGenerator>> System container missing! Please add it in public input");

        system.sysCenter = sysCenter;

        // Randomly generate and add stars to system
        for (int i = 0; i < systemStarAmount; i++)
        {
            system.AddStarsToSystem(rndStarCreator(system));
        }

        // Instantiate stars in a straight line going out from the center point(so they could then spin around it in a ring shape)
        Vector3 starInSysPosX = rndSysPos;

        for (int i = 0; i < system.sysStars.Length; i++)
        {
            starInSysPosX.x += rnd.Next((int)min_dist, (int)max_dist);
            GameObject tempObj = Instantiate(system.sysStars[i].starType, starInSysPosX, Quaternion.identity);

            tempObj.name = "sys" + system.sysStars[i].belongingSys.sysId + " star" + system.sysStars[i].starId;
            if (systemContainer != null)
                tempObj.transform.parent = sysCenter.transform;
        }


        // Check for stars in system, if there are stars then instantiate them around the center point, make sure there aren't two stars touching.
        // Make sure there aren't two systems close to each other, or make it so if there are then they will start colliding, change their names and give them a colliding status.


        // TODO: Make random system generation, use the min_dist and max_dist for distances between stars
    }

    string StarSystemNameGenerator()
    {
        string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
        string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
        string systemName = "";

        int lettersInName = 0;
        int nameLength = rnd.Next(4, 13);

        while (lettersInName < nameLength)
        {
            systemName += consonants[rnd.Next(consonants.Length)];
            systemName += vowels[rnd.Next(vowels.Length)];

            lettersInName = systemName.Length;
        }

        return char.ToUpper(systemName[0]) + systemName.Substring(1);
    }

    string StarNameGenerator()
    {
        string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
        string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };

        string[] numbers = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        string starName = "";

        int lettersInName = 0;
        int nameLength = rnd.Next(4, 6);

        while (lettersInName < nameLength)
        {
            starName += consonants[rnd.Next(consonants.Length)];
            starName += consonants[rnd.Next(consonants.Length)];
            starName += vowels[rnd.Next(vowels.Length)];

            lettersInName = starName.Length;
        }

        int numAmount = rnd.Next(3);


        for (int i = 0; i <= numAmount; i++)
        {
            starName += numbers[rnd.Next(9)];
        }

        return char.ToUpper(starName[0]) + starName.Substring(1);
    }
}


public class Star
{
    public StarSystem belongingSys; // star system the star belongs to
    public GameObject starType; // Prefab type of star
    public string starName;
    public float starSize;
    public int starId;

    System.Random rnd = new System.Random();

    public Star(string name, float size, GameObject type, int id, StarSystem sys)
    {
        this.starName = name;
        this.starSize = size;
        this.starType = type;
        this.starId = id;
        this.belongingSys = sys;
    }
}

public class StarSystem
{
    // Attributes
    public string sysName;
    public int sysId;
    public Star[] sysStars; // Stars on system
    // public Constellation belongingConst; // constellation the star system belongs to

    // Variables
    public int starCounter; // Amount of stars on system
    public GameObject starContainer;
    public GameObject sysCenter; // Empty gameobject at center of system

    // basic system builder, use AddStarsToSystem() to add stars
    public StarSystem(string name, int id, int starAmount)
    {
        this.sysName = name;
        this.sysId = id;
        sysStars = new Star[starAmount];
    }

    // // builder with input for star array
    // public StarSystem(string name, int id, Star[] stars)
    // {
    //     this.sysName = name;
    //     this.sysStars = stars;
    //     this.sysId = id;
    //     this.starCounter = sysStars.Length;
    // }

    public void AddStarsToSystem(Star star)
    {
        this.sysStars[this.starCounter] = star;
        this.starCounter++;
    }

    // Currently shows what planets are on the system in debug log, plan is to make it return all planets in the system
    public void GetSysStars()
    {
        Debug.Log("Planets on system " + this.sysName + " are:");
        for (int i = 0; i < this.sysStars.Length; i++)
        {
            Debug.Log(this.sysStars[i].starName + " of type " + this.sysStars[i].starType);
        }
    }
}
// public class CosmicStatus
// {
//     public string status;
//     public int statArmyPower; // army power bonus status grants
//     public int statInfluence; // higher the value the more influence owning the system or star with the status will have
//     public int cosmicSubstance; // amount of cosmic substance status will grant if destroyed

//     private string[] statusNameList = { "Alien Soverign", "Colliding", "Tower", "Benevolant" }; // TODO: Arrange name list in a way that the first ones in the list are most common

//     public CosmicStatus(int nameSpot)
//     {
//         this.status = statusNameList[nameSpot];
//         SetCosmicStatus();
//     }

//     void SetCosmicStatus()
//     {
//         if (this.status == statusNameList[0])
//         {
//             this.statInfluence = 4;
//             this.statArmyPower = 8;

//         }
//         else if (this.status == statusNameList[1])
//         {
//             this.statInfluence = 4;
//             this.statArmyPower = 8;

//         }
//         else if (this.status == statusNameList[2])
//         {
//             this.statInfluence = 4;
//             this.statArmyPower = 8;

//         }
//         else if (this.status == statusNameList[3])
//         {
//             this.statInfluence = 4;
//             this.statArmyPower = 8;
//         }
//     }

// }
// public class Constellation
// {
//     public string conName;
//     public string conAttrType; // attribute type affects what attributes the constellation provides
//     public int conSystemAmount; // amount of star systems in constellation, used for when adding or removing systems from constellation
//     public StarSystem[] systemsInConst; // star systems that are in the constellation
//     public GameObject conType; // how the constellation will look, what is its type, each constellation type will have a different color, green, red, purple, blue

//     public string[] conAttributes = { "Photonian", "Quadcentric", "Kimoterian", "Terragenetic", "Jurocantic" };
//     private System.Random rnd = new System.Random();

//     public Constellation(string name, int amount, GameObject type)
//     {
//         this.conName = name;
//         this.conSystemAmount = 0;
//         this.conType = type;
//         this.conAttrType = AttributeGenerator();
//     }

//     string AttributeGenerator()
//     {
//         int rndNum = rnd.Next(5);
//         string attr = this.conAttributes[rndNum];

//         return attr;
//     }

//     // Add the star system to the given constellation
//     public void AddStarSystemToConstellation(StarSystem system)
//     {
//         this.systemsInConst[conSystemAmount] = system;
//         conSystemAmount++;
//     }

//     public void systemsInConstellation()
//     {
//         Debug.Log(">> Star systems on constellation " + this.conName + " are:");
//         for (int i = 0; i < this.systemsInConst.Length; i++)
//         {
//             Debug.Log(this.systemsInConst[i].sysName + " that has " + this.systemsInConst[i].sysStars.Length + " stars.");
//         }
//     }
// }
