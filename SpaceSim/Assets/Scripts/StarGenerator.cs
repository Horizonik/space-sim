using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    public GameObject[] planetTypes;
    public Star[] randomStars;
    System.Random rnd = new System.Random();

    public int amountOfSystems = 6;

    void Start()
    {
        randomStars = new Star[10];
        StarSystemCreator();
    }

    // Randomly creates stars and systems for testing purposes
    void StarSystemCreator()
    {
        for (int j = 0; j < amountOfSystems; j++)
        {
            for (int i = 0; i < randomStars.Length; i++)
            {
                Star s = new Star(StarNameGenerator(), rnd.Next(15), i, planetTypes[rnd.Next(planetTypes.Length)]);
                randomStars[i] = s;
            }
            StarSystem sSystem = new StarSystem(StarSystemNameGenerator(), j, randomStars);
            sSystem.starsInSystem();
        }
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

    public string StarNameGenerator()
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

public class Constellation
{
    public string conName;
    public string conAttrType; // attribute type affects what attributes the constellation provides
    public int conSystemAmount;
    public GameObject conType; // how the constellation will look

    private string[] conAttributes = { "Photonian", "Quadcentric", "Kimoterian", "Terragenetic", "Jurocantic" };
    System.Random rnd = new System.Random();

    public Constellation(string name, int amount, GameObject type)
    {
        this.conName = name;
        this.conSystemAmount = amount;
        this.conType = type;
        this.conAttrType = AttributeGenerator();
    }

    public string AttributeGenerator()
    {
        int rndNum = rnd.Next(5);
        string attr = conAttributes[rndNum];

        return attr;
    }
}

public class Star
{
    public string starName;
    public GameObject starType;
    public float starSize;
    public int starId;

    System.Random rnd = new System.Random();

    public Star(string name, float size, int id, GameObject type)
    {
        this.starName = name;
        this.starSize = size;
        this.starId = id;
        this.starType = type;
    }
}

public class StarSystem
{
    public int starAmount; // Amount of stars on system
    public string systemName; // System's name
    public int systemId;
    public Star[] systemStars; // Stars on system

    // Max & Min distances between planets on system, will be used for random distance later on
    private float min_dist = 10;
    private float max_dist = 40;

    public StarSystem(string name, int id, Star[] stars)
    {
        this.systemName = name;
        this.systemStars = stars;
        this.systemId = id;
        this.starAmount = systemStars.Length;
    }

    public void setName(string newName)
    {
        this.systemName = newName;
    }

    // Physically sets system in the world in a randomly generated way
    public void InstantiateSystem()
    {
        // TODO: Make random system generation, use the min_dist and max_dist for distances between stars
    }

    // Currently shows what planets are on the system in debug log, plan is to make it return all planets in the system
    public void starsInSystem()
    {
        Debug.Log("Planets on system " + this.systemName + " are:");
        for (int i = 0; i < this.systemStars.Length; i++)
        {
            Debug.Log(this.systemStars[i].starName + " of type " + this.systemStars[i].starType);
        }
    }
}
