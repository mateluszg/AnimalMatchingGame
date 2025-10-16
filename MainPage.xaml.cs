namespace AnimalMatchingGame
{
  public partial class MainPage : ContentPage
  {
    public MainPage()
    {
      InitializeComponent();
    }

    private void PlayAgainButton_Clicked(object sender, EventArgs e)
    {
      AnimalButtons.IsVisible = true;
      PlayAgainButton.IsVisible = false;

      List<string> animalEmoji = [
        "🐶", "🐶",
        "🐭", "🐭",
        "🐰", "🐰",
        "🐻", "🐻",
        "🐨", "🐨",
        "🦁", "🦁",
        "🐯", "🐯",
        "🐮", "🐮"
      ];

      foreach (var button in AnimalButtons.Children.OfType<Button>())
      {
        int randomIndex = Random.Shared.Next(animalEmoji.Count);
        string randomEmoji = animalEmoji[randomIndex];
        button.Text = randomEmoji;
        animalEmoji.RemoveAt(randomIndex);
      }

      Dispatcher.StartTimer(TimeSpan.FromSeconds(.1), TimerTick);
    }

    int tenthOfSecondsElapsed = 0;

    private bool TimerTick()
    {
      if (!this.IsLoaded) return false;

      tenthOfSecondsElapsed++;
      TimeElapsed.Text = "Czas: " + (tenthOfSecondsElapsed / 10F).ToString("0,0 s");

      if (PlayAgainButton.IsVisible)
      {
        tenthOfSecondsElapsed = 0;
        return false;
      }

      return true;
    }

    Button lastClicked;
    bool findingMatch = false;
    int matchesFound;

    private void Button_Clicked(object sender, EventArgs e)
    {
      if (sender is Button buttonClicked)
      {
        if (!string.IsNullOrWhiteSpace(buttonClicked.Text) && (findingMatch == false))
        {
          buttonClicked.BackgroundColor = Colors.Red;
          lastClicked = buttonClicked;
          findingMatch = true;
        }
        else
        {
          if ((buttonClicked != lastClicked) && (buttonClicked.Text == lastClicked.Text) && (!String.IsNullOrWhiteSpace(buttonClicked.Text)))
          {
            matchesFound++;
            lastClicked.Text = " ";
            buttonClicked.Text = " ";
          }
          lastClicked.BackgroundColor = Colors.LightBlue;
          buttonClicked.BackgroundColor = Colors.LightBlue;
          findingMatch = false;
        }
      }
      if (matchesFound == 8)
      {
        matchesFound = 0;
        AnimalButtons.IsVisible = false;
        PlayAgainButton.IsVisible = true;
      }
    }
  }
}