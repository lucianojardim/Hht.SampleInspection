using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hht.SampleInspection.Util
{
    public class GeneralUseFunction
    {
        //20160330 LCJ Get AppSetting from web.config
        public static string GetAppSettingUsingConfigurationManager(string customField)
        {
            return System.Configuration.ConfigurationManager.AppSettings[customField];
        }
        public static string GetAppSetting(string customField)
        {
            System.Configuration.Configuration config =
                System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(null);
            if (config.AppSettings.Settings.Count > 0)
            {
                var customSetting = config.AppSettings.Settings[customField].ToString();
                if (!string.IsNullOrEmpty(customSetting))
                {
                    return customSetting;
                }
            }
            return null;
        }
    }
}