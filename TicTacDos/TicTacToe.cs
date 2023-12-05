namespace TTT;

public class TicTacToe(int size)
{
  public string?[,] Board { get; } = new string[size, size];

  public readonly int Size = size;

  public string Player = "X";

  public void ChangePlayer() => Player = Player == "X" ? "O" : "X";

  private bool GameOver;

  public void Play(int row, int col)
  {
    if (!GameOver && Board[row, col] == null)
    {
      Board[row, col] = Player;
      IsGameOver();
      if (!GameOver)
      {
        ShowMessage?.Invoke(this, new MessageEventArgs($"Thinking..."));
        ChangePlayer();
        scores.Clear();
        BestMove();
        ChooseMove();
        ShowMessage?.Invoke(this, new MessageEventArgs($"Moves {scores.Count}"));
        IsGameOver();
        ChangePlayer();
      }
    }
  }

  private void ChooseMove()
  {
    int high = Enumerable.Max(scores.Select(score => score.Score));
    var bestMoves = scores.Where(score => score.Score == high);
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

  readonly Stack<(int row, int col, string player)> Moves = new();
  private int BestMove()
  {
    int score = 0;
    for (int r = 0; r < Size; r++)
    {
      for (int c = 0; c < Size; c++)
      {
        if (Board[r, c] == null)
        {
          score = 0;
          Board[r, c] = Player;
          if (!CheckWon())
          {
            Moves.Push((r, c, Player));
            ChangePlayer();
            score += BestMove();
            ChangePlayer();
            Moves.Pop();
          }
          else
          {
            score += Player == "O" ? -1 : 1;
          }
          Board[r, c] = null;
          if (Moves.Count == 0)
          {
            scores.Add(new Move
            {
              Row = r,
              Col = c,
              Score = score
            });
          }
        }
      }
    }
    return score;
  }

  public List<Move> scores = [];

  public event EventHandler<MessageEventArgs>? ShowMessage;
}
