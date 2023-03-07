using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    // public static Dictionary<Vector3Int, Hex> all_hex = new Dictionary<Vector3Int, Hex>(); // 用于其他脚本中回传动态更新
    Dictionary<Vector3Int, Hex> hex_tile_dict = new Dictionary<Vector3Int, Hex>();
    Dictionary<Vector3Int, List<Vector3Int>> hex_tile_neighbours_dict = new Dictionary<Vector3Int, List<Vector3Int>>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (Hex hex in FindObjectsOfType<Hex>())
        {
            hex_tile_dict[hex.hex_coords] = hex;
            // all_hex[hex.hex_coords] = hex;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Hex Get_Tile_At(Vector3Int hexcoordinates)
    {
        Hex result = null;
        hex_tile_dict.TryGetValue(hexcoordinates, out result);
        return result;
    }

    public List<Vector3Int> Get_Neighbours_For(Vector3Int hexcoordinates) // awesome algorithm
    {
        if (hex_tile_dict.ContainsKey(hexcoordinates) == false)
        {
            return new List<Vector3Int>();
        }

        if (hex_tile_neighbours_dict.ContainsKey(hexcoordinates))
        {
            return hex_tile_neighbours_dict[hexcoordinates];
        }

        hex_tile_neighbours_dict.Add(hexcoordinates, new List<Vector3Int>());

        foreach (Vector3Int direction in Direction.Get_Direction_List(hexcoordinates.z))
        {
            if (hex_tile_dict.ContainsKey(hexcoordinates + direction))
            {
                hex_tile_neighbours_dict[hexcoordinates].Add(hexcoordinates + direction);
            }
        }
        return hex_tile_neighbours_dict[hexcoordinates];
    }
}

// https://www.youtube.com/watch?v=htZijEO7ZmE
public static class Direction
{
    public static List<Vector3Int> direction_offset_odd = new List<Vector3Int>
    {
        new Vector3Int(-1,0,1), // north west
        new Vector3Int(0,0,1), // north east
        new Vector3Int(1,0,0), // east
        new Vector3Int(-1,0,0), // west
        new Vector3Int(0,0,-1), // south east
        new Vector3Int(-1,0,-1), // south west

    };

    public static List<Vector3Int> direction_offset_even = new List<Vector3Int>
    {
        new Vector3Int(0,0,1), // north west
        new Vector3Int(1,0,1), // north east
        new Vector3Int(1,0,0), // east
        new Vector3Int(-1,0,0), // west
        new Vector3Int(1,0,-1), // south east
        new Vector3Int(0,0,-1), // south west
    };

    public static List<Vector3Int> Get_Direction_List(int z) //somekind of lamda expression, idk I just copy that
        => z % 2 == 0 ? direction_offset_even : direction_offset_odd;
    
}
