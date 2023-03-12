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
    List<GameObject> forest_hex = new List<GameObject>();
    List<GameObject> water_hex = new List<GameObject>();
    List<Hex> can_select = new List<Hex>();

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

            // if (selected_hex.hex_type == Hex.HexType.Default)
            // {
            //     GameManager.cur_turn++;
            // }
            
            selected_hex.Disable_Highlight();
            foreach (Vector3Int neightbour in neighbours)
            {
                hex_grid.Get_Tile_At(neightbour).Disable_Highlight();
            }

            // 这里判断是否为可选位置
            Debug.Log(GameManager.cur_turn);
            if (selected_hex.hex_type == Hex.HexType.Default)
            {
                if (can_select.Contains(selected_hex))
                {
                    Replace_Hex(result);
                    GameManager.cur_turn++;
                }
                else
                {
                    if (GameManager.cur_turn % 2 == 0 && forest_hex.Count == 0)
                    {
                        Replace_Hex(result);
                        GameManager.cur_turn++;
                    }
                    else if (GameManager.cur_turn % 2 == 1 && water_hex.Count == 0)
                    {
                        Replace_Hex(result);
                        GameManager.cur_turn++;
                    }
                }
                
            }
            // 需要在这里把所有同类hex都做一次bfs
            List<Vector3Int> all_cords = new List<Vector3Int>(); 

            if (GameManager.cur_turn % 2 == 0)
            {
                foreach (GameObject same_type_hex in forest_hex)
                {
                    all_cords.Add(same_type_hex.GetComponent<Hex>().hex_coords);
                }
            }
            else
            {
                foreach (GameObject same_type_hex in water_hex)
                {
                    all_cords.Add(same_type_hex.GetComponent<Hex>().hex_coords);
                }
            }

            BFSResult bfs_result = new BFSResult{};
            // neighbours = new List<Vector3Int>(bfs_result.Get_Range_Positions());
            neighbours = new List<Vector3Int>();
            foreach (Vector3Int coords in all_cords)
            {
                bfs_result = GraphSearch.BFS_Get_Range(hex_grid, coords, 10);
                List<Vector3Int> temp = new List<Vector3Int>(bfs_result.Get_Range_Positions());
                
                neighbours.AddRange(temp);
            }

            can_select = new List<Hex>();

            // 在这里判断一下hex类型，排除一些不要闪烁材质就可以
            foreach (Vector3Int neightbour in neighbours)
            {
                Hex cur_hex = hex_grid.Get_Tile_At(neightbour);
                if (cur_hex.hex_type == Hex.HexType.Default)
                {
                    cur_hex.Enable_Highlight();
                    can_select.Add(cur_hex);
                }
                
            }
            selected_hex.Disable_Highlight();

            // 这里判断游戏是否结束
            if (can_select.Count == 0 && GameManager.cur_turn > 2)
            {
                GameManager.is_over = true;
            }

            

            // Debug.Log(neighbours.Count);
            // Debug.Log($"neighbours for {selected_hex.hex_coords} are:");
            // foreach (Vector3Int neighbours_pos in neighbours)
            // {
            //     Debug.Log(neighbours_pos);
            // }
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
        
        if (GameManager.cur_turn % 2 == 0)
        {
            Material mat = forest_tile;
            child.GetComponent<MeshRenderer>().material = mat;
            // Debug.Log("Child Name: " + child.GetComponent<MeshFilter>().mesh);
            result.GetComponent<Hex>().hex_type = Hex.HexType.Forest;
            forest_hex.Add(result);
        }

        if (GameManager.cur_turn % 2 == 1)
        {
            Material mat = water_tile;
            child.GetComponent<MeshRenderer>().material = mat;
            // Debug.Log("Child Name: " + child.GetComponent<MeshFilter>().mesh);
            result.GetComponent<Hex>().hex_type = Hex.HexType.Water;
            water_hex.Add(result);
        }
    }
}
