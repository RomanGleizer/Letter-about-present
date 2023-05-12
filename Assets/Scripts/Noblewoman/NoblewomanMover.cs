using UnityEngine;

public class NoblewomanMover : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;

    private Transform[] _points;
    private int _currentPoint;

    private void Start()
    {
        _points = new Transform[_path.childCount];
        for (int i = 0; i < _path.childCount; i++)
            _points[i] = _path.GetChild(i);
    }

    private void Update()
    {
        var target = _points[_currentPoint];
        transform.position = Vector3.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
    
        if (transform.position == target.position)
        {
            _currentPoint++;
            if (_currentPoint >= _points.Length) _currentPoint = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MoveUpPoint>())
            SwitchBoolStates("IsMoveUp", "IsMoveLeft");

        if (collision.GetComponent<MoveRightPoint>())
            SwitchBoolStates("IsMoveRight", "IsMoveUp");

        if (collision.GetComponent<MoveDownPoint>())
            SwitchBoolStates("IsMoveDown", "IsMoveRight");

        if (collision.GetComponent<MoveLeftPoint>())
            SwitchBoolStates("IsMoveLeft", "IsMoveDown");
    }

    private void SwitchBoolStates(string first, string second)
    {
        _animator.SetBool(first, true);
        _animator.SetBool(second, false);
    }
}
