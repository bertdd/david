namespace TTT;

public class TicTacToe(int size)
{
  public string?[,] Board { get; } = new string[size, size];

  public readonly int Size = size;

  public string Player = "X";

  public void ChangePlayer() => Player = Player == "X" ? "O" : "X";

  private bool GameOver;

  public void Play(GameCell cell)
  {
    if (!GameOver && Board[cell.Row, cell.Col] == null)
    {
      Board[cell.Row, cell.Col] = Player;
      IsGameOver();
      if (!GameOver)
      {
        ChangePlayer();
        BestMove();
        IsGameOver();
        ChangePlayer();
      }
    }
  }

  private void IsGameOver()
  {
    if (CheckWon())
    {
      ShowMessage?.Invoke(this, new MessageEventArgs($"Game over {Player} won!"));
      GameOver = true;
    }
  }

  private bool CheckWon()
  {
    for (int i = 0; i < Size; i++)
    {
      if (CheckHorizontal(i) || CheckVertical(i))
      {
        return true;
      }
    }
    return CheckDiagonal();
  }

  private bool CheckDiagonal()
  {
    for (int i = 0; i < Size; i++)
    {
      if (Board[i, i] != Player)
      {
        for (int j = 0; j < Size; j++)
        { 
          if (Board[j, Size - j - 1] != Player)
          { 
            return false;
          }
        }
        return true;
      }
    }
    return true;
  }

  private bool CheckVertical(int c)
  {
    for (int row = 0; row < Size; row++)
    {
      if (Board[row, c] != Player)
      {
        return false;
      }
    }
    return true;
  }

  private bool CheckHorizontal(int r)
  {
    for (int col = 0; col < Size; col++)
    {
      if (Board[r, col] != Player)
      {
        return false;
      }
    }
    return true;
  }

  private int BestMove()
  {
    int moves = 0;
    for (int r = 0; r < Size; r++)
    {
      for (int c = 0; c < Size; c++)
      {
        if (Board[r, c] == null)
        {
          moves++;
          Board[r, c] = Player;
          if (!CheckWon())
          {
            ChangePlayer();
            BestMove();
            ChangePlayer();
          }
          Board[r, c] = null;
        }
      }
    }
    return moves;
  }

  public event EventHandler<MessageEventArgs>? ShowMessage;
}
