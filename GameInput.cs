﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Sensors;
using Windows.UI.Xaml;
using Windows.UI.Input;
using Windows.UI.Core;


namespace Project
{
    public class GameInput
    {
        public Accelerometer accelerometer;
        public CoreWindow window;
        public GestureRecognizer gestureRecognizer;
        public GameInput()
        {
            // Get the accelerometer object
            accelerometer = Accelerometer.GetDefault();
            window = Window.Current.CoreWindow;

            // Set up the gesture recognizer.  In this example, it only responds to TranslateX, Scale and Tap events
            gestureRecognizer = new Windows.UI.Input.GestureRecognizer();
            gestureRecognizer.GestureSettings = GestureSettings.ManipulationTranslateX | GestureSettings.ManipulationScale | GestureSettings.Tap;

            // Register event handlers for pointer events
            window.PointerPressed += OnPointerPressed;
            window.PointerMoved += OnPointerMoved;
            window.PointerReleased += OnPointerReleased;
        }

        // Call the gesture recognizer when a pointer event occurs
        //Added try/catch blocks to deal with input being handled out of order
        void OnPointerPressed(CoreWindow sender, PointerEventArgs args)
        {
            try
            {
                gestureRecognizer.ProcessDownEvent(args.CurrentPoint);
            }
            catch (System.Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Lets do the time warp.");
            }
        }

        void OnPointerMoved(CoreWindow sender, PointerEventArgs args)
        {
            try
            {
                gestureRecognizer.ProcessMoveEvents(args.GetIntermediatePoints());
            }
            catch (System.Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Lets do the time warp again.");
            }
        }

        void OnPointerReleased(CoreWindow sender, PointerEventArgs args)
        {
            try
            {
                gestureRecognizer.ProcessUpEvent(args.CurrentPoint);
            }
            catch (System.Exception e)
            {
                System.Diagnostics.Debug.WriteLine("Let's do yet another time warp.");
            }
        }
    }
}