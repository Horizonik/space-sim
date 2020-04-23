using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionSeparator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

public class Region
{
    Vector3 regLocation; // xyz location of region, middle point of region land, will be used to display region name in the middle of the region on minimap
    float regSize; // size of region

    public Region(Vector3 location, float size)
    {
        this.regLocation = location;
        this.regSize = size;
    }

    public void PlaceRegionName() {
        
    }
}
