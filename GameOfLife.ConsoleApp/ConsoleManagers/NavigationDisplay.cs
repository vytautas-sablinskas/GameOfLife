namespace GameOfLife.ConsoleApp.ConsoleManagers
{
    public class NavigationDisplay
    {
        public virtual string GetMenuInformation()
        {
            return "1. Start Game\n" +
                   "2. Change Board Size\n" +
                   "3. Upload saved game and start playing\n" +
                   "4. Exit the application\n" +
                   "Enter your choice:";
        }

        public virtual string InvalidChoiceError()
        {
            return "Invalid choice. Please select again.";
        }
    }
}