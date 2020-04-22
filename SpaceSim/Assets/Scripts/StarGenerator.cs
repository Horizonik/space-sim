using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarGenerator : MonoBehaviour
{
    public GameObject[] planetTypes;
    public Star[] randomStars;
    System.Random rnd = new System.Random();

    void Start()
    {
        randomStars = new Star[10];
        StarSystemCreator();
    }

    // Randomly creates stars and systems for testing purposes
    void StarSystemCreator()
    {
        for (int i = 0; i < randomStars.Length; i++)
        {
            Star s = new Star("star" + i, rnd.Next(15), planetTypes[rnd.Next(planetTypes.Length)]);
            randomStars[i] = s;
        }

        StarSystem sSystem = new StarSystem(StarSystemNameGenerator(), randomStars);
        sSystem.starsInSystem();
    }

    string StarSystemNameGenerator()
    {
        string[] consonants = {"b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x"};
        string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
        string systemName = "";

        int lettersInName = 0;
        int nameLength = rnd.Next(4, 13);

        while (lettersInName < nameLength)
        {
            systemName += consonants[rnd.Next(consonants.Length)];
            systemName += vowels[rnd.Next(vowels.Length)];

            lettersInName+= 2;
        }

        return systemName;
    }
}

public class Star
{
    public string starName;
    public GameObject starType;
    public float starSize;

    public Star(string name, float size, GameObject type)
    {
        this.starName = name;
        this.starSize = size;
        this.starType = type;
    }
}

public class StarSystem
{
    public int starAmount; // Amount of stars on system
    public string systemName; // System's name
    public Star[] systemStars; // Stars on system

    // Max & Min distances between planets on system, will be used for random distance later on
    private float min_dist = 10;
    private float max_dist = 40;

    public StarSystem(string name, Star[] stars)
    {
        this.systemName = name;
        this.systemStars = stars;
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
