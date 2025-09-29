using UnityEngine;

public class PlayerChangeColor : MonoBehaviour
{
    public Color[] colors;
    public MeshRenderer mr;
    private PlayerInput input;
    public int _index = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        input = new PlayerInput();
        input.Player.Enable();
        colors = new Color[] { Color.white, Color.black, Color.yellow };
        mr = GetComponent<MeshRenderer>();
        mr.material.color = colors[_index];
    }

    // Update is called once per frame
    void Update()
    {
        if (input.Player.ChangeColor.WasPressedThisFrame())
        {
            _index = (_index + 1) % colors.Length;
            mr.material.color = colors[_index];
        }

    }
}
