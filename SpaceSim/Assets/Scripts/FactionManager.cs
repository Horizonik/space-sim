using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionManager : MonoBehaviour
{
    private System.Random rnd = new System.Random();
    
    string RandomFactionNameGen()
    {
        string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x", "je", "ve", "mi" };
        string[] vowels = { "a", "e", "i", "o", "u", "ae", "y", "ou", "au", "ex" };
        string factionName = "";

        int lettersInName = 0;
        int nameLength = rnd.Next(5, 10);

        while (lettersInName < nameLength)
        {
            factionName += consonants[rnd.Next(consonants.Length)];
            factionName += vowels[rnd.Next(vowels.Length)];

            lettersInName = factionName.Length;
        }

        return char.ToUpper(factionName[0]) + factionName.Substring(1);
    }
}

public class Faction
{
    public string facName;
    public string facRace;
    public Region facRegion; // region the faction resides in
    private int facTradePoints; // points gained by trading with the faction, affects trading, more points means more things to trade with and for, get more points by trading with said faction
    private int facLawPoints; // points gained by breaking the law in the region of the faction, the more points mean the more police will be after you when in the region
    private int facRelationPoints; // set to 0 by default on first interaction with faction, the relationship with the faction

    public Faction(string name, string race, int tradePoints, int lawPoints, int relationPoints, Region region)
    {
        this.facName = name;
        this.facRace = race;
        this.facTradePoints = tradePoints;
        this.facLawPoints = lawPoints;
        this.facRelationPoints = relationPoints;
        this.facRegion = region;
    }
}
