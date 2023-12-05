namespace TTT;

public class Move
{
  public int Row {  get; set; }
  public int Col { get; set; } 
  public int Score { get; set; }
  public override string ToString() => $"({Row},{Col}) = {Score}";
}
