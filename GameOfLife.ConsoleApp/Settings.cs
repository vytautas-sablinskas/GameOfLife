using GameOfLife.ConsoleApp.Rules;

namespace GameOfLife.ConsoleApp
{
    public static class Settings
    {
        public static readonly List<IRuleStrategy> GAME_RULES = new List<IRuleStrategy>
        {
            new UnderpopulationRule(),
            new OverpopulationRule(),
            new ReproductionRule(),
            new SurvivalRule(),
        };
    }
}