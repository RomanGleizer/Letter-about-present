using System.IO;
using UnityEngine;

public class NoblewomanMover : MonoBehaviour
{
    [SerializeField] private Transform _path;
    [SerializeField] private float _speed;
    [SerializeField] private Animator _animator;

    private Transform[] _points;
    private int _currentPoint;
    private Transform _target;

    public float Speed { get => _speed; set => _speed = value; }

    private void Start()
    {
        _points = new Transform[_path.childCount];
        for (int i = 0; i < _path.childCount; i++)
            _points[i] = _path.GetChild(i);
    }

    private void Update()
    {
        _target = _points[_currentPoint];
        transform.position = Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
    
        if (transform.position == _target.position)
        {
            _currentPoint++;
            if (_currentPoint >= _points.Length) _currentPoint = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<MoveUpPoint>()) SwitchBoolStates("IsMoveUp", "IsMoveLeft");
        if (collision.GetComponent<MoveRightPoint>()) SwitchBoolStates("IsMoveRight", "IsMoveUp");
        if (collision.GetComponent<MoveDownPoint>()) SwitchBoolStates("IsMoveDown", "IsMoveRight");
        if (collision.GetComponent<MoveLeftPoint>()) SwitchBoolStates("IsMoveLeft", "IsMoveDown");
    }

    private void SwitchBoolStates(string first, string second)
    {
        _animator.SetBool(first, true);
        _animator.SetBool(second, false);
    }

    public void SaveNobleWomanData()
    {
        var data = new NoblewomanData()
        {
            Position = transform.position,
            Target = _target,
            CurrentPoint = _currentPoint,
            IsPlayerMoveLeft = _animator.GetBool("IsMoveLeft"),
            IsPlayerMoveRight = _animator.GetBool("IsMoveRight"),
            IsPlayerMoveUp = _animator.GetBool("IsMoveUp"),
            IsPlayerMoveDown = _animator.GetBool("IsMoveDown")
        };

        var json = JsonUtility.ToJson(data, true);
        File.WriteAllText(
            Application.dataPath + "/NoblewomanData.json",
            json,
            encoding: System.Text.Encoding.UTF8);
    }

    public void LoadNobleWomanData()
    {
        var json = File.ReadAllText(
            Application.dataPath + "/NoblewomanData.json",
            encoding: System.Text.Encoding.UTF8);

        var data = JsonUtility.FromJson<NoblewomanData>(json);

        if (data.IsPlayerMoveRight) _animator.Play("NoblewomanMoveRight");
        if (data.IsPlayerMoveLeft) _animator.Play("NoblewomanMoveLeft");
        if (data.IsPlayerMoveUp) _animator.Play("NoblewomanMoveUp");
        if (data.IsPlayerMoveDown) _animator.Play("NoblewomanMoveDown");

        _target = data.Target;
        _currentPoint = data.CurrentPoint;
        transform.position = data.Position;
    }
}