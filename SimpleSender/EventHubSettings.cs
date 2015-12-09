using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace SimpleSender
{

    /// <summary>
    /// IoTHub and device related settings
    /// </summary>
    /// 
    public class EventHubSettings
    {
        /// <summary>
        /// ServicebusNamespace
        /// </summary>
        static public string EventHubNamespace
        {
            get; set;
        }

        /// <summary>
        /// EventHubName
        /// </summary>
        static public string EventHubName
        {
            get; set;
        }

        /// <summary>
        /// Device ID
        /// </summary>
        static public string PolicyName
        {
            get; set;
        }

        /// <summary>
        /// Device Key
        /// </summary>
        static public string Key
        {
            get; set;
        }

        /// <summary>
        /// Tries to load the settings first from the app local folder and then from application package. 
        /// Changing IotHubSettings.txt content in application local folder makes it possible
        /// to overwrite the app default settings
        /// Json structure is following:
        /// {
        /// "ServicebusNamespace": "ServicebusNamespace",
        /// "EventHubName": "EventHub Name",
        /// "PolicyName": "Policy Name",
        /// "Key": "Key"
        /// }
        /// </summary>
        static public async Task<bool> LoadSettingsFromFileAsync()
        {
            try
            {
                var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(@"ms-appx:///Settings/EventHubSettings.txt"));
                var stream = await ((StorageFile)file).OpenStreamForReadAsync();

                using (StreamReader reader = new StreamReader(stream))
                {
                    var fileText = reader.ReadToEnd();
                    JObject o = JObject.Parse(fileText);

                    EventHubNamespace = (string)o["EventHubNamespace"];
                    EventHubName = (string)o["EventHubName"];
                    PolicyName = (string)o["PolicyName"];
                    Key = (string)o["Key"];

                    if (EventHubNamespace != string.Empty && EventHubName != string.Empty && PolicyName != string.Empty && Key != string.Empty)
                        return true;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Can't load settings:");
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            return false;
        }

     }
}
