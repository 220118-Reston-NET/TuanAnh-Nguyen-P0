namespace UI
{
  /*
    Interface are one of the best way to implement abstraction
    Every method is implicityly abstract meaning you don't have to write anything
    Every method is public
  */
  public interface IMenu
  {
    /// <summary>
    /// Will display the menu and user choices in the terminal
    /// </summary>
    void Display();

    /// <summary>
    /// Will record the user's choice and change/route your menu based on that choice
    /// </summary>
    /// <returns>Return the menu that will change your screen</returns>
    string UserChoice();
  }
}