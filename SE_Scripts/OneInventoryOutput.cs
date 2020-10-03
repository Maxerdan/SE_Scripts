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
//using Sandbox.ModAPI;

namespace Script1
{
    public sealed class Program : MyGridProgram
    {
        //------------BEGIN--------------

        IMyCargoContainer Container;
        IMyTextPanel LCD;
        List<MyInventoryItem> ContainerItems;
        Program()
        {
            Container = (IMyCargoContainer)GridTerminalSystem.GetBlockWithName("Large Cargo Container1 (Loot)");
            LCD = (IMyTextPanel)GridTerminalSystem.GetBlockWithName("LCD");
            ContainerItems = new List<MyInventoryItem>();
        }

        void Main()
        {
            IMyInventory ContainerInventory = Container.GetInventory(0);
            ContainerInventory.GetItems(ContainerItems);
            LCD.WriteText("Cargo:\n", false);
            foreach (MyInventoryItem Item in ContainerItems)
            {
                LCD.WriteText("\n" + Item.Type.SubtypeId + ": " + Item.Amount, true);
            }
        }

        void Save()
        { 

        }

        //------------END--------------
    }
}