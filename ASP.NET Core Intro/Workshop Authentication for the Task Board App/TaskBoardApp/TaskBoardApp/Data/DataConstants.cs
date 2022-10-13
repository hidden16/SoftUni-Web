namespace TaskBoardApp.Data
{
    public class DataConstants
    {
        public class User
        {
            public const int MaxUserFirstNameLength = 15;
            public const int MaxUserLastNameLength = 15;
            public const int MaxUsernameLength = 20;
        }
        public class Task
        {
            public const int MaxTitleLength = 70;
            public const int MinTitleLength = 5;

            public const int MaxDescriptionLength = 1000;
            public const int MinDescriptionLength = 10;
        }
         public class Board
        {
            public const int MaxNameLength = 30;
            public const int MinNameLength = 3;
        }
    }
}
