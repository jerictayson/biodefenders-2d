using System.Collections;
using System.Collections.Generic;
using Player;
using TMPro;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private float angle = 9f;
    [SerializeField]
    private Transform _enemy;
    private float _transitionTime = 2f;
    private bool _isColliding;
    [SerializeField]
    private GameObject _dialog;
    [SerializeField]
    private GameObject _continueButton;
    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private string[] _sentences;

    private int _index = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Pathogen"))
        {
            _enemy = other.gameObject.transform;
            Debug.Log("Enemy Detected");
            _isColliding = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // follow the player
        var position = _target.position;
        if(_sentences[_index] == _text.text && _index != 1)
        {
            _continueButton.SetActive(true);
        }
        
        if(_dialog.activeInHierarchy && _index == 1)
            PlayerMovement.CanShoot = true;
        
        if (_isColliding)
        {
            Vector3 desiredPosition = new Vector3(_enemy.position.x, _enemy.position.y, transform.position.z);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _transitionTime * Time.fixedDeltaTime);
            transform.position = smoothedPosition;
            PlayerMovement.CanMove = false;

            if (!_dialog.activeInHierarchy)
            {
                _dialog.SetActive(true);
                StartCoroutine(Type());
            }
        }
        else
        {
            
            transform.position = new Vector3(position.x + angle, position.y, transform.position.z);
            PlayerMovement.CanMove = true;
                
        }
    }

    public void NextLine()
    {
        _continueButton.SetActive(false);
        if (_index < _sentences.Length - 1)
        {
            _index++;
            _text.text = "";
            StartCoroutine(Type());
        }
        else
        {
            _text.text = "";
            _dialog.SetActive(false);
        }
    }
    
    IEnumerator Type()
    {
        foreach (var letter in _sentences[_index].ToCharArray())
        {
            _text.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
