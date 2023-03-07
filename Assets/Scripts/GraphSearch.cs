using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphSearch
{
    public static BFSResult BFS_Get_Range(HexGrid hex_grid, Vector3Int start_point, int movement_point)
    {
        Dictionary<Vector3Int, Vector3Int?> visited_nodes = new Dictionary<Vector3Int, Vector3Int?>(); //for each node and its parent, ? means can be null
        Dictionary<Vector3Int, int> cost_sofar = new Dictionary<Vector3Int, int>();
        Queue<Vector3Int> nodes_to_visit_queue = new Queue<Vector3Int>();

        nodes_to_visit_queue.Enqueue(start_point);
        cost_sofar.Add(start_point, 0);
        visited_nodes.Add(start_point, null);

        while (nodes_to_visit_queue.Count > 0)
        {
            Vector3Int cur_node = nodes_to_visit_queue.Dequeue();
            foreach (Vector3Int neighbour_position in hex_grid.Get_Neighbours_For(cur_node))
            {
                // if (hex_grid.Get_Tile_At(neighbour_position).Is_Obstacle())
                //     continue;

                int node_cost = hex_grid.Get_Tile_At(neighbour_position).Get_Cost();
                int cur_cost = cost_sofar[cur_node];
                int new_cost = cur_cost + node_cost;

                if (new_cost <= movement_point)
                {
                    if (!visited_nodes.ContainsKey(neighbour_position))
                    {
                        visited_nodes[neighbour_position] = cur_node;
                        cost_sofar[neighbour_position] = new_cost;
                        nodes_to_visit_queue.Enqueue(neighbour_position);
                    }
                    else if (cost_sofar[neighbour_position] > new_cost) // update to find the less cost way to go
                    {
                        cost_sofar[neighbour_position] = new_cost;
                        visited_nodes[neighbour_position] = cur_node; // store the parent node
                    }
                }
            }
        }



        return new BFSResult {visited_nodes_dict = visited_nodes };

    }

    public static List<Vector3Int> Generate_Path_BFS(Vector3Int cur, Dictionary<Vector3Int, Vector3Int?> visited_nodes_dict)
    {
        List<Vector3Int> path = new List<Vector3Int>();
        path.Add(cur); // add path backward, start from destination
        while (visited_nodes_dict[cur] != null)
        {
            path.Add(visited_nodes_dict[cur].Value);
            cur = visited_nodes_dict[cur].Value;
        }
        path.Reverse();
        return path.Skip(1).ToList(); // skip the start position
    }
    
}

public struct BFSResult
{   
    public Dictionary<Vector3Int, Vector3Int?> visited_nodes_dict;
    public List<Vector3Int> Get_Path_To(Vector3Int destination)
    {
        if (visited_nodes_dict.ContainsKey(destination) == false)
        {
            return new List<Vector3Int>();
        }
        return GraphSearch.Generate_Path_BFS(destination, visited_nodes_dict);
    }

    public bool Is_Hex_Pos_In_Range(Vector3Int position)
    {
        return visited_nodes_dict.ContainsKey(position);
    }

    public IEnumerable<Vector3Int> Get_Range_Positions() // use for highlight
    {
        return visited_nodes_dict.Keys; // all neighbour child
    }
}
