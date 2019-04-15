using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace EcareMob.Helpers
{

    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get { return CrossSettings.Current; }
        }

        #region Setting Constants


        private const string LanguageKey = "language_key";
        private static readonly string LanguageDefault = "English";

        private const string CultureKey = "culture_key";
        private static readonly string CultureDefault = "en-US";

        private const string ServerUrlKey = "serverurl_key";
        //private static readonly string ServerUrlDefault = "http://mobile.ethesis.eu/inventory/";
        private static readonly string ServerUrlDefault = "http://ethesis.eu/ecareapi/";//  http://localhost:9000/

        private static readonly string EncryptionKey = "12!bm**46v@XAZ";
        private static readonly string EncryptionKeyDefault = "12!bm**46v@XAZ";


        private const string VersionKey = "version_key";
        private static readonly string VersionDefault = "1.0";

        private const string UserNameKey = "username_key";
        private static readonly string UserNameDefault = "demo";

        private const string FullNameKey = "fullname_key";
        private static readonly string FullNameDefault = "unknown";


        private const string CharismaCodeKey = "charisma_code";
        private static readonly string CharismaCodeDefault = "";


        private const string VatCodeKey = "vat_code";
        private static readonly string VatCodeDefault = "";

        private const string UserIdKey = "userid_key";
        private static readonly int UserIdDefault = -1;

        private const string ProfileImageKey = "profileimage_key";
        private static readonly string ProfileImageDefault = "";

        private const string AccessTokenKey = "accesstoken_key";
        private static readonly string AccessTokenDefault = "";

        private const string RefreshTokenKey = "refreshtoken_key";
        private static readonly string RefreshTokenDefault = "";

        private const string ExpireTokenDateKey = "expiretokendate_key";
        private static readonly DateTime ExpireTokenDateDefault = new DateTime(1980, 10, 19, 12, 12, 12, DateTimeKind.Local);

        #endregion
        public static DateTime ExpireTokenDate
        {
            get { return AppSettings.GetValueOrDefault(ExpireTokenDateKey, ExpireTokenDateDefault); }
            set { AppSettings.AddOrUpdateValue(ExpireTokenDateKey, value); }
        }
        public static string RefreshToken
        {
            get { return AppSettings.GetValueOrDefault(RefreshTokenKey, RefreshTokenDefault); }
            set { AppSettings.AddOrUpdateValue(RefreshTokenKey, value); }
        }
        public static string AccessToken
        {
            get { return AppSettings.GetValueOrDefault(AccessTokenKey, AccessTokenDefault); }
            set { AppSettings.AddOrUpdateValue(AccessTokenKey, value); }
        }
        public static string ProfileImage
        {
            get { return AppSettings.GetValueOrDefault(ProfileImageKey, ProfileImageDefault); }
            set { AppSettings.AddOrUpdateValue(ProfileImageKey, value); }
        }
        public static string UserName
        {
            get { return AppSettings.GetValueOrDefault(UserNameKey, UserNameDefault); }
            set { AppSettings.AddOrUpdateValue(UserNameKey, value); }
        }


        public static string CharismaCode
        {
            get { return AppSettings.GetValueOrDefault(CharismaCodeKey, CharismaCodeDefault); }
            set { AppSettings.AddOrUpdateValue(CharismaCodeKey, value); }
        }


        public static string Vat
        {
            get { return AppSettings.GetValueOrDefault(VatCodeKey, VatCodeDefault); }
            set { AppSettings.AddOrUpdateValue(VatCodeKey, value); }
        }


        public static string FullName
        {
            get { return AppSettings.GetValueOrDefault(FullNameKey, FullNameDefault); }
            set { AppSettings.AddOrUpdateValue(FullNameKey, value); }
        }
        public static int UserId
        {
            get { return AppSettings.GetValueOrDefault(UserIdKey, UserIdDefault); }
            set { AppSettings.AddOrUpdateValue(UserIdKey, value); }
        }
        public static string Version
        {
            get { return AppSettings.GetValueOrDefault(VersionKey, VersionDefault); }
            set { AppSettings.AddOrUpdateValue(VersionKey, value); }
        }
        public static string ServerUrl
        {
            get { return AppSettings.GetValueOrDefault(ServerUrlKey, ServerUrlDefault); }
            set { AppSettings.AddOrUpdateValue(ServerUrlKey, value); }
        }

        public static string EncKey
        {
            get { return AppSettings.GetValueOrDefault(EncryptionKey, EncryptionKeyDefault); }
            set { AppSettings.AddOrUpdateValue(EncryptionKey, value); }
        }



        public static string Language
        {
            get { return AppSettings.GetValueOrDefault(LanguageKey, LanguageDefault); }
            set { AppSettings.AddOrUpdateValue(LanguageKey, value); }
        }

        public static string Culture
        {
            get { return AppSettings.GetValueOrDefault(CultureKey, CultureDefault); }
            set { AppSettings.AddOrUpdateValue(CultureKey, value); }
        }

    }
}
