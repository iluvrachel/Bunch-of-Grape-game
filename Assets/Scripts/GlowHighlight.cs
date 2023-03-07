using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowHighlight : MonoBehaviour
{
    Dictionary<Renderer, Material[]> glow_material_dict = new Dictionary<Renderer, Material[]>();
    Dictionary<Renderer, Material[]> original_material_dict = new Dictionary<Renderer, Material[]>();

    Dictionary<Color, Material> cached_glow_materials = new Dictionary<Color, Material>();

    public Material glow_material;
    private bool is_glowing = false;

    private void Awake()
    {
        Prepare_Material_Dictionaries();
    }

    private void Prepare_Material_Dictionaries()
    {
        Renderer[] renderer_list = GetComponentsInChildren<Renderer>();
        for (int j = 0; j < renderer_list.Length; j++)
        {
            Renderer renderer = renderer_list[j];
            Material[] original_materials = renderer.materials;
            original_material_dict.Add(renderer, original_materials);

            Material[] new_materials = new Material[renderer.materials.Length];

            for (int i = 0; i < original_materials.Length; i++)
            {
                Material mat = null;
                if (cached_glow_materials.TryGetValue(original_materials[i].color, out mat) == false)
                {
                    mat = new Material(glow_material);
                    // take color as the base color (_Color in shader)
                    mat.color = original_materials[i].color;
                    cached_glow_materials[mat.color] = mat;
                }
                new_materials[i] = mat;
            }
            glow_material_dict.Add(renderer, new_materials);
        }
        
    }

    public void Toggle_Glow(bool state) //感觉有点多此一举
    {
        if (is_glowing == state)
            return;
        is_glowing = !state;
        Toggle_Glow();
    }

    public void Toggle_Glow()
    {
        if (is_glowing == false) 
        {
            foreach (Renderer renderer in original_material_dict.Keys)
            {
                renderer.materials = glow_material_dict[renderer];
            }
        }    
        else
        {
            foreach (Renderer renderer in original_material_dict.Keys)
            {
                renderer.materials = original_material_dict[renderer];
            }
        }
        is_glowing = !is_glowing;
    }


}
