namespace david;

public class TicTacToe
{
  public TicTacToe(int size)
  {
    board = new string[size, size];
  }

  readonly string[,] board;
}
