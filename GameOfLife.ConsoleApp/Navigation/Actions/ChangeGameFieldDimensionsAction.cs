using GameOfLife.ConsoleApp.ConsoleManagers;
using GameOfLife.Data.Constants;
using GameOfLife.Data.Entities;
using GameOfLife.ConsoleApp.Navigation.Actions;
using GameOfLife.ConsoleApp.Navigation.Services;

public class ChangeGameFieldDimensionsAction : IMenuAction
{
    private readonly GameFieldDimensions _gameFieldDimensions;
    private readonly IObjectSelectionService _dimensionSelectionService;
    private readonly ChangeBoardSizeDisplay _changeBoardSizeDisplay;

    public ChangeGameFieldDimensionsAction(GameFieldDimensions gameFieldDimensions, IObjectSelectionService dimensionSelectionService, ChangeBoardSizeDisplay changeBoardSizeDisplay)
    {
        _gameFieldDimensions = gameFieldDimensions;
        _dimensionSelectionService = dimensionSelectionService;
        _changeBoardSizeDisplay = changeBoardSizeDisplay;
    }

    public void Execute()
    {
        bool userChangedValues = SelectDimensions();
        if (userChangedValues)
        {
            _changeBoardSizeDisplay.InformAboutSuccessfullyChangedValues(
                width: _gameFieldDimensions.Width,
                height: _gameFieldDimensions.Height
            );
        }
    }

    private bool SelectDimensions()
    {
        var width = (int?)_dimensionSelectionService.ExecuteService(Words.WIDTH.ToLower());
        if (!width.HasValue || width.Value == -1)
            return false;

        var height = (int?)_dimensionSelectionService.ExecuteService(Words.HEIGHT.ToLower());
        if (!height.HasValue || height.Value == -1)
            return false;

        SetDimensionValues(width.Value, height.Value);
        return true;
    }

    private void SetDimensionValues(int width, int height)
    {
        _gameFieldDimensions.Width = width;
        _gameFieldDimensions.Height = height;
    }
}