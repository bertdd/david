namespace TTT;

public class MoveEventArgs(int row, int col, string player)
{
  public int Row { get; } = row;
  public int Col { get; } = col;
  public string Player { get; } = player;
  public override string ToString() => $"board[{Row},{Col}] = {Player}";
}