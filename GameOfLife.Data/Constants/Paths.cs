namespace GameOfLife.Data.Constants
{
    public static class Paths
    {
        public static string SAVED_FILES_FOLDER
        {
            get
            {
                return Path.GetFullPath("../../../../GameOfLife.Data/SavedFiles");
            }
        }
    }
}