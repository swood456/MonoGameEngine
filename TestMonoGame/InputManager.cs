using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace TestMonoGame
{
    public enum InputAxes { PrimaryHorizontal, PrimaryVertical };
    public enum InputButtons { Fire };
    public sealed class InputManager
    {
        private static readonly InputManager instance = new InputManager();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static InputManager()
        {
        }

        private InputManager()
        {
            // TODO: refactor this code, very sloppy
            axisValues = new Dictionary<InputAxes, float>();
            buttonValues = new Dictionary<InputButtons, bool>();

            JObject inputJson;
            try
            {
                inputJson = JObject.Parse(File.ReadAllText("input_settings.json"));
            } catch
            {
                inputJson = new JObject();
            }
            
            JToken axisKeyToken;
            bool axisSetup = false;
            if (inputJson.TryGetValue("axisKeyMap", out axisKeyToken))
            {
                axisKeyMap = axisKeyToken.ToObject<Dictionary<InputAxes, HashSet<Keys>>>();
                JToken axisDirectionsToken;
                if (inputJson.TryGetValue("axisKeyDirections", out axisDirectionsToken))
                {
                    axisSetup = true;
                    axisKeyDirections = axisDirectionsToken.ToObject<Dictionary<Keys, bool>>();
                }
            }
            if (!axisSetup)
            {
                axisKeyDirections = new Dictionary<Keys, bool>();
                axisKeyMap = new Dictionary<InputAxes, HashSet<Keys>>();

                // set up default axis key controls
                addAxisKey(InputAxes.PrimaryVertical, Keys.W, true);
                addAxisKey(InputAxes.PrimaryVertical, Keys.Up, true);

                addAxisKey(InputAxes.PrimaryVertical, Keys.S, false);
                addAxisKey(InputAxes.PrimaryVertical, Keys.Down, false);

                addAxisKey(InputAxes.PrimaryHorizontal, Keys.D, true);
                addAxisKey(InputAxes.PrimaryHorizontal, Keys.Right, true);

                addAxisKey(InputAxes.PrimaryHorizontal, Keys.A, false);
                addAxisKey(InputAxes.PrimaryHorizontal, Keys.Left, false);
            }

            JToken buttonKeyToken;
            if (inputJson.TryGetValue("buttonKeyMap", out buttonKeyToken))
            {
                buttonKeyMap = buttonKeyToken.ToObject<Dictionary<InputButtons, HashSet<Keys>>>();
            } else
            {
                buttonKeyMap = new Dictionary<InputButtons, HashSet<Keys>>();
                // set up default buttons
            }
        }


        public void setInput()
        {
            KeyboardState kState = Keyboard.GetState();

            axisValues.Clear();
            buttonValues.Clear();

            foreach (KeyValuePair<InputAxes, HashSet<Keys>> axisPair in axisKeyMap)
            {
                float axisValue = 0.0f;

                foreach (Keys button in axisPair.Value)
                {
                    if (kState.IsKeyDown(button))
                    {
                        axisValue += axisKeyDirections[button] ? 1.0f : -1.0f;
                    }
                }

                // somehow C# is missing clamp function? Or just my C# version?
                // TODO: figure out why the clamp function is not working
                axisValue = Math.Max(Math.Min(axisValue, 1.0f), -1.0f);

                axisValues.Add(axisPair.Key, axisValue);
            }

            foreach (KeyValuePair<InputButtons, HashSet<Keys>> axisPair in buttonKeyMap)
            {
                foreach (Keys button in axisPair.Value)
                {
                    if (kState.IsKeyDown(button))
                    {
                        buttonValues.Add(axisPair.Key, true);
                    }
                }
            }
        }

        private Dictionary<InputAxes, HashSet<Keys>> axisKeyMap;
        private Dictionary<InputButtons, HashSet<Keys>> buttonKeyMap;
        private Dictionary<Keys, bool> axisKeyDirections;

        private Dictionary<InputAxes, float> axisValues;
        private Dictionary<InputButtons, bool> buttonValues;

        public static InputManager Instance
        {
            get
            {
                return instance;
            }
        }

        public float getAxis(InputAxes axis)
        {
            float result;
            // default float value of 0.0f is desired if key is missing
            axisValues.TryGetValue(axis, out result);
            return result;
        }

        public bool getButtonDown(InputButtons button)
        {
            bool result;
            // default bool value of false is desired if key is missing
            buttonValues.TryGetValue(button, out result);
            return result;
        }

        public void addAxisKey(InputAxes axis, Keys key, bool isPositive)
        {
            HashSet<Keys> axisKeys;
            if (!axisKeyMap.TryGetValue(axis, out axisKeys))
            {
                axisKeys = new HashSet<Keys>();
            }
            if(axisKeys.Add(key))
            {
                axisKeyMap[axis] = axisKeys;
                axisKeyDirections.Add(key, isPositive);
            }
        }

        public void addButtonKey(InputButtons button, Keys key)
        {
            HashSet<Keys> keys;
            if (!buttonKeyMap.TryGetValue(button, out keys))
            {
                keys = new HashSet<Keys>();
            }
            if(keys.Add(key))
            {
                buttonKeyMap[button] = keys;
            }
        }

        public void saveInputConfig()
        {
            using (StreamWriter file = File.CreateText(@"input_settings.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                Dictionary<String, Object> inputSettingsJson = new Dictionary<String, Object>();
                inputSettingsJson.Add("axisKeyMap", axisKeyMap);
                inputSettingsJson.Add("axisKeyDirections", axisKeyDirections);
                inputSettingsJson.Add("buttonKeyMap", buttonKeyMap);
                serializer.Serialize(file, inputSettingsJson);
            }
        }

        // TODO: remove/unmap keys
        
    }

}
