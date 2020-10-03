using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using VRageMath;
using VRage.Game;
using Sandbox.ModAPI.Interfaces;
using Sandbox.ModAPI.Ingame;
using Sandbox.Game.EntityComponents;
using VRage.Game.Components;
using VRage.Collections;
using VRage.Game.ObjectBuilders.Definitions;
using VRage.Game.ModAPI.Ingame;
using SpaceEngineers.Game.ModAPI.Ingame;
using System.Linq;
using System.Threading;
//using Sandbox.ModAPI;

namespace Script1
{
    public sealed class Program4 : MyGridProgram
    {
        //------------BEGIN--------------

        IMyTextPanel LCD;
        IMyMotorAdvancedStator rotor;

        float currentRotorPos;


        Program()
        {
            LCD = (IMyTextPanel)GridTerminalSystem.GetBlockWithName("LCDRotor");
            LCD.ContentType = VRage.Game.GUI.TextPanel.ContentType.TEXT_AND_IMAGE;

            rotor = (IMyMotorAdvancedStator)GridTerminalSystem.GetBlockWithName("Drill_Rotor");

            currentRotorPos = rotor.Angle;
        }

        void Main(string args)
        {
            if (args == "On")
                On();
            if (args == "Off")
                Off();
            while(args != "Stop")
            {
            LCD.WriteText(currentRotorPos.ToString() + " " + rotor.Angle + "\n", true);
                Thread.Sleep(2000);
            }
        }

        void On()
        {
            rotor.ApplyAction("OnOff_On");
        }

        void Off()
        {
            rotor.ApplyAction("OnOff_Off");
        }

        void Save()
        {

        }

        //------------END--------------
    }
}