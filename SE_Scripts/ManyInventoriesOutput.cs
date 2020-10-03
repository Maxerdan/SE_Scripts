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
using Sandbox.Game.Entities;
using System.Linq;
//using Sandbox.ModAPI;

namespace Script1
{
    public sealed class Program2 : MyGridProgram
    {
        //------------BEGIN--------------
        bool startFlag = false;
        IMyTextPanel LCD;
        List<IMyCargoContainer> CargosList;
        List<MyInventoryItem> ContainerItems;
        Dictionary<string, float> dict;
        int counter;
        Program2()
        {
            LCD = (IMyTextPanel)GridTerminalSystem.GetBlockWithName("LCD");
            LCD.ContentType = VRage.Game.GUI.TextPanel.ContentType.TEXT_AND_IMAGE;
        }

        void Main(string args)
        {
            switch (args)
            {
                case "start":
                    {
                        counter = 0;
                        startFlag = true;
                        Runtime.UpdateFrequency = UpdateFrequency.Update100;
                        break;
                    }

                case "stop":
                    {
                        startFlag = false;
                        Runtime.UpdateFrequency = UpdateFrequency.None;
                        break;
                    }

                case "":
                    {
                        if (counter == 10)
                            counter = 0;
                        else
                            counter++;
                        break;
                    }
            }

            if (startFlag && counter == 10)
            {

                CargosList = new List<IMyCargoContainer>();
                GridTerminalSystem.GetBlocksOfType(CargosList);


                dict = new Dictionary<string, float>();

                foreach (IMyCargoContainer cargo in CargosList)
                {
                    ContainerItems = new List<MyInventoryItem>();
                    IMyInventory ContainerInventory = cargo.GetInventory(0);
                    ContainerInventory.GetItems(ContainerItems);

                    foreach (MyInventoryItem Item in ContainerItems)
                    {
                        // два условия: предмета еще нет в списке - add, предмет есть - найти и увеличить значение
                        if (dict.ContainsKey(Item.Type.SubtypeId))
                        {
                            dict[Item.Type.SubtypeId] += (float)Item.Amount;
                        }
                        else
                        {
                            dict[Item.Type.SubtypeId] = (float)Item.Amount;
                        }
                        //LCD.WriteText("\n" + Item.Type.SubtypeId + ": " + Item.Amount, true);
                    }
                }

                dict = dict.OrderBy(pair => pair.Key).ToDictionary(pair => pair.Key, pair => pair.Value);
                LCD.WriteText("", false);
                foreach (var keyValue in dict)
                {
                    LCD.WriteText(keyValue.Key + ": " + keyValue.Value + "\n", true);
                }
            }
        }

        void Save()
        {

        }

        //------------END--------------
    }
}