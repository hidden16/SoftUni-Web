namespace Watchlist.Data.Constants
{
    public class GlobalConstants
    {
        public class User
        {
            public const int UserNameMaxLength = 20;
            public const int UserNameMinLength = 5;

            public const int EmailMaxLength = 60;
            public const int EmailMinLength = 10;

            public const int PasswordMaxLength = 20;
            public const int PasswordMinLength = 5;
        }

        public class Movie
        {
            public const int TitleMaxLength = 50;
            public const int TitleMinLength = 10;

            public const int DirectorMaxLength = 50;
            public const int DirectorMinLength = 5;

            public const string RatingMaxLength = "10.00";
            public const string RatingMinLength = "0.00";
        }

        public class Genre
        {
            public const int NameMaxLength = 50;
            public const int NameMinLength = 5;
        }
    }
}
