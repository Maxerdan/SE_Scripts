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
//using Sandbox.ModAPI;

namespace Script1
{
    public sealed class Program3 : MyGridProgram
    {
        //------------BEGIN--------------

        bool workFlag = true;
        //IMyTextPanel LCD;

        IMyBlockGroup drills;
        List<IMyTerminalBlock> drillsList;

        IMyMotorAdvancedStator rotor;
        IMyTerminalBlock gyro;

        IMyBlockGroup pistons;
        List<IMyTerminalBlock> pistonsList;

        float currentRotorPos;

        bool firstPart = false;
        bool secondPart = false;


        Program()
        {
            //LCD = (IMyTextPanel)GridTerminalSystem.GetBlockWithName("LCD");
            //LCD.ContentType = VRage.Game.GUI.TextPanel.ContentType.TEXT_AND_IMAGE;

            drills = GridTerminalSystem.GetBlockGroupWithName("Barge_Drills");
            drillsList = new List<IMyTerminalBlock>();
            drills.GetBlocks(drillsList);

            pistons = GridTerminalSystem.GetBlockGroupWithName("Barge_Pistons");
            pistonsList = new List<IMyTerminalBlock>();
            pistons.GetBlocks(pistonsList);

            rotor = (IMyMotorAdvancedStator)GridTerminalSystem.GetBlockWithName("Drill_Rotor");
            gyro = GridTerminalSystem.GetBlockWithName("Drill_Gyro");

            
        }

        void Main(string args)
        {
            switch (args)
            {
                case "start":
                    {
                        On();
                        currentRotorPos = rotor.Angle;
                        //rotor.TargetVelocityRPM = 1;
                        Runtime.UpdateFrequency = UpdateFrequency.Update100;
                        foreach (IMyPistonBase piston in pistonsList)
                        {
                            piston.Velocity = 0.05f;
                        }
                            break;
                    }
                case "pause/continue":
                    {
                        if (workFlag)
                            Off();
                        else
                            On();
                        break;
                    }

                case "stop":
                    {
                        Off();
                        Runtime.UpdateFrequency = UpdateFrequency.None;
                        foreach (IMyPistonBase piston in pistonsList)
                        {
                            piston.MaxLimit = 0;
                            piston.Retract();
                        }
                            break;
                    }
            }

            if (workFlag)
            {
                if (rotor.Angle > currentRotorPos && rotor.Angle < (currentRotorPos + Math.PI / 2))
                {
                    if (!firstPart)
                        PistonMove();
                    firstPart = true;
                    secondPart = false;
                }
                else
                {
                    if (!secondPart)
                        PistonMove();
                    secondPart = true;
                    firstPart = false;
                }
            }
        }

        void PistonMove()
        {
            foreach (IMyPistonBase piston in pistonsList)
            {
                if (piston.CurrentPosition != 10)
                {
                    piston.MaxLimit++;
                    break;
                }
                    

            }
        }

        void On()
        {
            workFlag = true;
            foreach (IMyTerminalBlock drill in drillsList)
            {
                drill.ApplyAction("OnOff_On");
            }
            gyro.ApplyAction("OnOff_On");
            rotor.ApplyAction("OnOff_On");
        }

        void Off()
        {
            workFlag = false;
            foreach (IMyTerminalBlock drill in drillsList)
            {
                drill.ApplyAction("OnOff_Off");
            }
            //gyro.ApplyAction("OnOff_Off");
            rotor.ApplyAction("OnOff_Off");
        }

        void Save()
        {

        }

        //------------END--------------
    }
}