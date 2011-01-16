using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Garfielder.Core.Infrastructure
{
    public class AppSettingsHelper
    {
        #region Member variables
        /// <summary>
        /// get appsettings via common service locator
        /// </summary>
        private static NameValueCollection appSettings
        {
            get
            {
                var settings = default(NameValueCollection);
                try
                {
                    settings = ServiceLocatorX<NameValueCollection>.GetService();
                }
                catch (Exception ex)
                {
                    settings = ConfigurationManager.AppSettings;
                }
                return settings;
            }
        }
        public static AppSettingsHelper Instance
        {
            get
            {
                return Nested.instance;
            }
        }
        private class Nested
        {
            static Nested() { }
            internal static readonly AppSettingsHelper instance = new AppSettingsHelper();
        }
        /// <summary>
        /// Parsed Result
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private class ParsedResult<T>
        {
            public bool IsOk { get; set; }
            public T Value { get; set; }
        }
        #endregion

        #region .ctor
        private AppSettingsHelper()
        {
        }
        #endregion

        #region GetString

        public string GetString(string name)
        {
            return getValue(name, true, null);
        }

        public string GetString(string name, string defaultValue)
        {
            return getValue(name, false, defaultValue);
        }

        #endregion

        #region GetStringArray

        public string[] GetStringArray(string name, string separator)
        {
            return getStringArray(name, separator, true, null);
        }

        public string[] GetStringArray(string name, string separator, string[] defaultValue)
        {
            return getStringArray(name, separator, false, defaultValue);
        }

        private string[] getStringArray(string name, string separator, bool valueRequired, string[] defaultValue)
        {
            string value = getValue(name, valueRequired, null);

            if (!string.IsNullOrEmpty(value))
                return value.Split(new[] { separator }, StringSplitOptions.RemoveEmptyEntries);
            else if (!valueRequired)
                return defaultValue;

            throw generateRequiredSettingException(name);
        }

        #endregion

        #region GetInt32

        public int GetInt32(string name)
        {
            return getInt32(name, null);
        }

        public int GetInt32(string name, int defaultValue)
        {
            return getInt32(name, defaultValue);
        }

        private int getInt32(string name, int? defaultValue)
        {
            return getValue<int>(name, (v, pv) =>
            {
                var isOk = int.TryParse(v, out pv);
                return new ParsedResult<int> { IsOk = isOk, Value = pv };
            }, defaultValue);
        }

        #endregion

        #region GetBoolean

        public bool GetBoolean(string name)
        {
            return getBoolean(name, null);
        }

        public bool GetBoolean(string name, bool defaultValue)
        {
            return getBoolean(name, defaultValue);
        }

        private bool getBoolean(string name, bool? defaultValue)
        {
            return getValue<bool>(name, (v, pv) =>
            {
                var isOk = bool.TryParse(v, out pv);
                return new ParsedResult<bool> { IsOk = isOk, Value = pv };
            }, defaultValue);
        }

        #endregion
        #region GetDecimal
        public decimal GetDecimal(string name)
        {
            return getDecimal(name, null);
        }
        public decimal GetDecimal(string name, decimal defaultValue)
        {
            return getDecimal(name, defaultValue);
        }
        private decimal getDecimal(string name, decimal? defaultValue)
        {
            return getValue<decimal>(name, (v, pv) =>
            {
                var isOk = decimal.TryParse(v, out pv);
                return new ParsedResult<decimal> { IsOk = isOk, Value = pv };
            }, defaultValue);
        }
        #endregion

        #region Private Methods
        private T getValue<T>(string name, Func<string, T, ParsedResult<T>> parseValue, T? defaultValue) where T : struct
        {
            string value = appSettings[name];

            if (value != null)
            {
                var p = default(T);
                var r = parseValue(value, p);
                if (r.IsOk)
                    return r.Value;
                else
                    throw new InvalidOperationException(string.Format("Setting '{0}' was not a valid {1}", name, typeof(T).FullName));
            }

            if (!defaultValue.HasValue)
                throw generateRequiredSettingException(name);
            else
                return defaultValue.Value;
        }

        private string getValue(string name, bool valueRequired, string defaultValue)
        {
            string value = appSettings[name];

            if (value != null)
                return value;
            else if (!valueRequired)
                return defaultValue;

            throw generateRequiredSettingException(name);
        }

        private static Exception generateRequiredSettingException(string name)
        {
            return new InvalidOperationException(string.Format("Could not find required setting '{0}'", name));
        }

        #endregion
    }

}
