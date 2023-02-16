using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CrossyRoadConsoleCommands : CustomCommands
{
    private static readonly List<string> commands = new List<string>
    {
        "coins"
    };

    protected override void Commands(string command)
    {
        if (command.Contains("customcommands"))
        {
            console.Log("Crossy Road Command List:\n" + " - " + string.Join("\t\n - ", commands));
            return;
        }

        if (command.Contains(commands[0]))
        {
            string[] cmdCoins = command.Split(' ');

            if (cmdCoins.Length > 1)
            {
                if (cmdCoins[1] != "?")
                {
                    if (int.TryParse(cmdCoins[1], out int coinQuantity))
                    {
                        string operation = "";

                        if (coinQuantity >= 0)
                        {
                            operation = "Added";
                            CoinsManager.instance.Increase(coinQuantity);
                        }
                        else
                        {
                            operation = "Removed";
                            
                            if (!CoinsManager.instance.Deacrease(coinQuantity))
                            {
                                console.Log($"You don't have enough coins to remove from your Coins Storage.");
                                return;
                            }
                        }

                        coinQuantity = Mathf.Abs(coinQuantity);
                        console.Log($"{operation} {coinQuantity} to your Coins Storage.");
                    }
                    else
                    {
                        console.Log("Coins number not valid, please insert an integer number.");
                    }
                }
                else
                {
                    console.Log($"Use this command to add/remove coins to your coins storage.\nSyntax:\n - coins N => To add N amount of coins\n \n - fps => To see how many coins you have.");
                }
            }
            else
            {
                console.Log($"Current coins {CoinsManager.instance.CurrentCoins}");
            }

            return;
        }
    }


}
