using System;

namespace MTQtoolEditor.Services;

public class UserProfileDirectory : IUserProfileDirectory
{
    public string GetAppDirectory()
    {
        return Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
    }
}