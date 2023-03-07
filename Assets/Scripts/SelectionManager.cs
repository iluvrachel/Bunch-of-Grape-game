using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField]
    private Camera main_cam;
    public LayerMask selection_mask;
    public HexGrid hex_grid;

    public Material forest_tile;
    public Material water_tile;

    List<Vector3Int>  neighbours = new List<Vector3Int>();

    private void Awake()
    {
        if (main_cam == null)
        {
            main_cam = Camera.main;
        }
    }

    public void HandleClick(Vector3 mouse_pos)
    {
        GameObject result;
        if (Find_Target(mouse_pos, out result))
        {
            Hex selected_hex = result.GetComponent<Hex>();

            if (selected_hex.hex_type == Hex.HexType.Default)
            {
                GameManager.cur_turn++;
            }
            
            selected_hex.Disable_Highlight();
            foreach (Vector3Int neightbour in neighbours)
            {
                hex_grid.Get_Tile_At(neightbour).Disable_Highlight();
            }


            // neighbours = hex_grid.Get_Neighbours_For(selected_hex.hex_coords);

            // 需要在这里把所有同类hex都做一次bfs todo
            
            BFSResult bfs_result = GraphSearch.BFS_Get_Range(hex_grid, selected_hex.hex_coords,10);
            neighbours = new List<Vector3Int>(bfs_result.Get_Range_Positions());

            // 在这里判断一下hex类型，排除一些不要闪烁材质就可以
            foreach (Vector3Int neightbour in neighbours)
            {
                Hex cur_hex = hex_grid.Get_Tile_At(neightbour);
                if (cur_hex.hex_type == Hex.HexType.Default)
                {
                    cur_hex.Enable_Highlight();
                }
                
            }
            selected_hex.Disable_Highlight();

            if (selected_hex.hex_type == Hex.HexType.Default)
            {
                Replace_Hex(result);
            }
            

            Debug.Log(neighbours.Count);
            Debug.Log($"neighbours for {selected_hex.hex_coords} are:");
            foreach (Vector3Int neighbours_pos in neighbours)
            {
                Debug.Log(neighbours_pos);
            }
        } 
    }

    private bool Find_Target(Vector3 mouse_pos, out GameObject result)
    {
        RaycastHit hit;
        Ray ray = main_cam.ScreenPointToRay(mouse_pos);
        if (Physics.Raycast(ray, out hit, 100, selection_mask))
        {
            result = hit.collider.gameObject;
            return true;
        }
        result = null;
        return false;
    }

    void Replace_Hex(GameObject result)
    {
        Transform child = result.transform.GetChild(0).GetChild(0);
        
        if (GameManager.cur_turn % 2 == 1)
        {
            Material mat = forest_tile;
            child.GetComponent<MeshRenderer>().material = mat;
            // Debug.Log("Child Name: " + child.GetComponent<MeshFilter>().mesh);
            result.GetComponent<Hex>().hex_type = Hex.HexType.Forest;
        }

        if (GameManager.cur_turn % 2 == 0)
        {
            Material mat = water_tile;
            child.GetComponent<MeshRenderer>().material = mat;
            // Debug.Log("Child Name: " + child.GetComponent<MeshFilter>().mesh);
            result.GetComponent<Hex>().hex_type = Hex.HexType.Water;
        }
    }
}
