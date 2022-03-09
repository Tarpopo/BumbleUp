public interface IMove
{
    MoveType MoveType { get; }
    bool Moving { get; }
    void Move();
    void StopMove();
}

public enum MoveType
{
    Forward,
    Horizontal
}