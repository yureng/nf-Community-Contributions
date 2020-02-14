﻿//
// Copyright (c) 2020 The nanoFramework project contributors
// See LICENSE file in the project root for full license information.
//

using nanoFramework.Hardware.Drivers;
using System;
using System.Threading;
using Windows.Devices.Gpio;

namespace keypad_4x4
{
    public class Program
    {
        public static void Main()
        {
            Console.WriteLine("Starting keypad driver...");

            // setup keypad driver
            // using 0x20 as the default PCF8574 address
            // assuming this is connected to a 4x4 keypad
            var keypad = new Keypad(
                0x20, 
                "I2C2", 
                GpioController.GetDefault().OpenStm32Pin('F', 9),
                4,
                4);

            // set event handlers
            keypad.KeyPressed += Keypad_KeyPressed;
            keypad.KeyRelesed += Keypad_KeyRelesed;

            // enable key press handling
            keypad.EnableKeyPress();

            Thread.Sleep(Timeout.Infinite);
        }

        private static void Keypad_KeyRelesed()
        {
            Console.WriteLine("key released");
        }

        private static void Keypad_KeyPressed(KeyPressedEventArgs e)
        {
            Console.WriteLine($"Key pressed: [{e.Row},{e.Column}]");
        }
    }
}