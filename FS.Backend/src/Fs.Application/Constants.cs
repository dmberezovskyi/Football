namespace Fs.Application
{
    internal static class Constants
    {
        public static class User
        {
            public static class Query
            {
                public static class Parts
                {
                    public const string Snippet = "snippet";
                    public const string Profile = "profile";
                    public const string Team = "team";
                    public const string Organization = "organization";
                    public const string UserInfo = "userInfo";
                    public const string UserInfoDetailed = "userInfoDetailed";

                    public static string[] All => new[] { Snippet, Profile, Team, Organization, UserInfo, UserInfoDetailed };
                }
            }
        }
    }
}
