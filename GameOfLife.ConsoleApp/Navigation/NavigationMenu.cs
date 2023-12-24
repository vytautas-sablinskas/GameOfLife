using GameOfLife.ConsoleApp.ConsoleManagers;
using GameOfLife.Data.Entities;
using GameOfLife.ConsoleApp.Navigation.Actions;
using GameOfLife.ConsoleApp.Conditions;

public class NavigationMenu
{
    private readonly GameFieldDimensions _gameFieldDimensions = new GameFieldDimensions();

    private readonly NavigationDisplay _navigationDisplay;
    private readonly CommonInputsOutputsDisplay _commonInputsOutputsDisplay;
    private readonly IMenuActionFactory _menuActionFactory;
    private readonly IRunCondition _runCondition;

    public NavigationMenu(IMenuActionFactory menuActionFactory, NavigationDisplay navigationDisplay, CommonInputsOutputsDisplay commonInputsOutputsDisplay, IRunCondition runCondition)
    {
        _menuActionFactory = menuActionFactory;
        _navigationDisplay = navigationDisplay;
        _commonInputsOutputsDisplay = commonInputsOutputsDisplay;
        _runCondition = runCondition;
    }

    public void ShowMenu()
    {
        while (_runCondition.ShouldContinue())
        {
            _commonInputsOutputsDisplay.ClearMessages();
            var userChoice = _commonInputsOutputsDisplay.PromptInput(_navigationDisplay.GetMenuInformation());
            ExecuteUserSelectedAction(userChoice);
        }
    }

    private void ExecuteUserSelectedAction(string input)
    {
        var action = _menuActionFactory.CreateAction(input, _gameFieldDimensions);

        if (action == null)
        {
            _commonInputsOutputsDisplay.ClearMessages();
            _commonInputsOutputsDisplay.PromptInput(_navigationDisplay.InvalidChoiceError());
        }
        else
        {
            action.Execute();
        }
    }
}