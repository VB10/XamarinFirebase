using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace XamarinFirebaseHA.Helper
{
    public class UserLocalData
    {
        private static ISettings AppSettings => CrossSettings.Current;

        public static string userToken {
            get => AppSettings.GetValueOrDefault(DataKey.userToken.ToString(), string.Empty);
            set => AppSettings.AddOrUpdateValue(DataKey.userToken.ToString(), value);
        }

        public static void removeDataAll(){
            AppSettings.Remove(DataKey.userToken.ToString());
        }
    }

    public enum DataKey{
        userToken
    }
}
