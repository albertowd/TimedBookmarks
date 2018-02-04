using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;
using SlimDX.DirectInput;

namespace TimedBookmarks
{
    class Inputs
    {
        public static Guid INVALID_GUID = new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        /// <summary>
        /// Constructor not needed.
        /// </summary>
        private Inputs()
        { }

        /// <summary>
        /// Returns if there is a device saved.
        /// </summary>
        /// <returns></returns>
        public static bool hasDevice()
        {
            return ConfigurationManager.AppSettings["type"] != "-1";
        }

        /// <summary>
        /// Returns the device saved.
        /// </summary>
        /// <returns></returns>
        public static Device getDevice()
        {
            if (DeviceType.Keyboard == (DeviceType)Int32.Parse(ConfigurationManager.AppSettings["type"]))
                return new Keyboard(new DirectInput());
            else
                return new Joystick(new DirectInput(), new Guid(ConfigurationManager.AppSettings["guid"]));
        }

        /// <summary>
        /// Get the button saved.
        /// </summary>
        /// <returns></returns>
        public static int GetButton()
        {
            return Convert.ToInt32(ConfigurationManager.AppSettings["button"]);
        }

        /// <summary>
        /// Get all the connected devices from the DirectInput(keyboard and joysticks).
        /// </summary>
        /// <param name="formControl"></param>
        /// <returns></returns>
        public static List<Device> GetConnected(Control formControl)
        {
            List<Device> devices = new List<Device>();
            DirectInput di = new DirectInput();

            Device keyboard = new Keyboard(di);
            keyboard.SetCooperativeLevel(formControl, CooperativeLevel.Nonexclusive | CooperativeLevel.Background);
            devices.Add(keyboard);

            IList<DeviceInstance> instances = di.GetDevices(DeviceClass.GameController, DeviceEnumerationFlags.AttachedOnly);
            foreach (DeviceInstance instance in instances)
            {
                Device device = new Joystick(di, instance.InstanceGuid);
                device.SetCooperativeLevel(formControl, CooperativeLevel.Nonexclusive | CooperativeLevel.Background);
                devices.Add(device);
            }

            return devices;
        }

        /// <summary>
        /// Reset the saved Device.
        /// </summary>
        public static void ResetConfig()
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            settings["button"].Value = "-1";
            settings["guid"].Value = Inputs.INVALID_GUID.ToString();
            settings["name"].Value = "";
            settings["type"].Value = "-1";
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }

        /// <summary>
        /// Save new device.
        /// </summary>
        /// <param name="device"></param>
        /// <param name="button"></param>
        public static void SetConfig(Device device, int button)
        {
            var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var settings = configFile.AppSettings.Settings;
            settings["button"].Value = button.ToString();
            settings["guid"].Value = device.Information.InstanceGuid.ToString();
            settings["name"].Value = device.Information.InstanceName;
            settings["type"].Value = Convert.ToString((int)device.Information.Type);
            configFile.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        }
    }
}
