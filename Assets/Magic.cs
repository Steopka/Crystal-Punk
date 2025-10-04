using UnityEngine;
using UnityEngine.InputSystem;

public class Magic : MonoBehaviour
{
    InputActionAsset InputActions;
    InputAction magAction1, magAction2, magAction3, mouse;
    public string _counter="";
    [SerializeField]
    private GameObject _ActiveSpell_1, _ActiveSpell_2, _ActiveSpell_3, spawnPos;
    public Vector3 mousePos,WorldPos;
    Vector2 spellDir;


    private void Start()
    {
        magAction1 = InputSystem.actions.FindAction("Mag1");
        magAction2 = InputSystem.actions.FindAction("Mag2");
        magAction3 = InputSystem.actions.FindAction("Mag3");
        mouse = InputSystem.actions.FindAction("Mouse");

        _counter = "";
    }
    
    void Update()
    {
        mousePos =Camera.main.ScreenToWorldPoint( mouse.ReadValue<Vector2>());
        mousePos.z = Camera.main.transform.position.z+Camera.main.nearClipPlane;
        Debug.Log(mousePos);
        spellDir = (mousePos - transform.position).normalized;
        InputControler();
        MagicController();
    }
    void InputControler()
    {
        if (magAction1.WasPressedThisFrame())
        {
            _counter += "1";
        }
        if (magAction2.WasPressedThisFrame())
        {
            _counter += "2";
        }
        if (magAction3.WasPressedThisFrame())
        {
            _counter += "3";
        }
    }
    void MagicController()
    {
        switch (_counter) 
        {
            case "111": 
                ActiveSpell_1();
                _counter = "";
                break;
            case "222":
                ActiveSpell_2();
                _counter = "";
                break;
            case "333":
                ActiveSpell_3();
                _counter = "";
                break;
            default:
                if (_counter.Length > 3)
                    _counter = "";
                break;
        }



    }

    void ActiveSpell_1()
    {
        GameObject s = Instantiate(_ActiveSpell_1,spawnPos.transform.position, transform.rotation);
        s.GetComponent<Rigidbody2D>().AddForce(spellDir*30, ForceMode2D.Impulse);
    }
    void ActiveSpell_2()
    {
        GameObject s = Instantiate(_ActiveSpell_2,spawnPos.transform.position, transform.rotation);
        s.GetComponent<Rigidbody2D>().AddForce(spellDir * 30, ForceMode2D.Impulse);

    }
    void ActiveSpell_3()
    {
        GameObject s = Instantiate(_ActiveSpell_3, spawnPos.transform.position, transform.rotation);
        s.GetComponent<Rigidbody2D>().AddForce(spellDir * 30, ForceMode2D.Impulse);
    }
}
