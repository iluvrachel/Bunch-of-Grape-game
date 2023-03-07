// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Hexcoordinate : MonoBehaviour
// {
//     public static float x_offset = 2, y_offset = 1, z_offset = 1.73f;

//     // private static Hexcoordinate _instance;
//     // public static Hexcoordinate instance {get{return _instance;}}

//     internal Vector3Int Get_Hex_Coords()
//         => offset_coordinates;

//     [Header("Offset Coordinates")]
//     [SerializeField]
//     public Vector3Int offset_coordinates;
//     // Start is called before the first frame update
//     void Start()
//     {
        
//     }

//     private void Awake()
//     {
//         offset_coordinates = Convert_Position_To_Offset(transform.position);
//     }

//     private Vector3Int Convert_Position_To_Offset(Vector3 position)
//     {
//         int x = Mathf.CeilToInt(position.x / x_offset);
//         int y = Mathf.RoundToInt(position.y / y_offset);
//         int z = Mathf.RoundToInt(position.z / z_offset);

//         return new Vector3Int(x,y,z);
//     }

//     // Update is called once per frame
//     void Update()
//     {
        
//     }


// }
