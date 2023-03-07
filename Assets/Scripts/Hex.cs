using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Hex : MonoBehaviour
{
    public static float x_offset = 2, y_offset = 1, z_offset = 1.73f;
    
    [SerializeField]
    private GlowHighlight highlight;
    private Hex hexcoordinates;
    public HexType hex_type;


    public Vector3Int hex_coords;

    public int Get_Cost() // TODO
    {
        int cost = 0;
        switch (this.hex_type)
        {
            case HexType.Default:
                cost = 10;
                break;
            case HexType.Forest:
                cost = 20;
                break;
            case HexType.Water:
                cost = 20;
                break;
            default:
                cost = 20;
                throw new Exception($"Hex of type {this.hex_type} not supported");
        }
        return cost;
    }


    private void Awake()
    {
        hexcoordinates = GetComponent<Hex>();
        hex_coords = Convert_Position_To_Offset(transform.position);
        highlight = GetComponent<GlowHighlight>();
        //Debug.Log(hex_coords);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3Int Convert_Position_To_Offset(Vector3 position)
    {
        int x = Mathf.CeilToInt(position.x / x_offset);
        int y = Mathf.RoundToInt(position.y / y_offset);
        int z = Mathf.RoundToInt(position.z / z_offset);

        return new Vector3Int(x,y,z);
    }

    public void Enable_Highlight()
    {
        highlight.Toggle_Glow(true);
    }

    public void Disable_Highlight()
    {
        highlight.Toggle_Glow(false);
    }

    public enum HexType
    {
        None,
        Default,
        Forest,
        Difficult,
        Road,
        Water,
        Obstacle
    }
}
