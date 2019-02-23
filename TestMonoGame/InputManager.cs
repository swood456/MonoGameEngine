using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

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
            axisValues = new Dictionary<InputAxes, float>();
            buttonValues = new Dictionary<InputButtons, bool>();
            axisKeyDirections = new Dictionary<Keys, bool>();

            axisKeyMap = new Dictionary<InputAxes, HashSet<Keys>>();
            buttonKeyMap = new Dictionary<InputButtons, HashSet<Keys>>();
        }

        public void setInput()
        {
            KeyboardState kState = Keyboard.GetState();

            axisValues.Clear();
            buttonValues.Clear();

            foreach (KeyValuePair<InputAxes, HashSet<Keys>> axisPair in axisKeyMap)
            {
                float axisValue = 0.0f;

                // TODO: figure out how I am storing negative valued items, ideally in an intelligent way

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

        public void addAxisKey(InputAxes axis, Keys button, bool isPositive)
        {
            HashSet<Keys> axisButtons;
            if (!axisKeyMap.TryGetValue(axis, out axisButtons))
            {
                axisButtons = new HashSet<Keys>();
            }
            if(axisButtons.Add(button))
            {
                axisKeyMap[axis] = axisButtons;
                axisKeyDirections.Add(button, isPositive);
            }
        }

        // TODO: remove axis and add+remove for buttons

        // design outward facing part before designing representation
        // copying unity's get axis thru string is bad, allows for too much user error
        // should have axes that return floats and buttons that return bools
        // eventually need to support onDown, onUp, isPressed, etc possibly.

        // hard-coding all the values is really bad, make button enum and axis enum
    }

}
