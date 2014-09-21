﻿using System;
using System.Collections.Generic;
using System.Linq;
using MarsRoverKata.Commands;

namespace MarsRoverKata
{
    public class MissionManager
    {
        public Rover Rover { get; private set; }
        public Mars Planet { get; private set; }
        private readonly Commander _commander;
        
        public MissionManager(Rover rover)
        {
            Rover = rover;
            Planet = rover.Mars;
            _commander = new Commander();
            _commander.CommandExecuted += UpdateAliens;
        }

        private void UpdateAliens(object sender, EventArgs e)
        {
            var random = new Random();
            foreach (var alien in Planet.Obstacles.OfType<Alien>())
            {
                switch (random.Next(1, 4))
                {
                    case 1:
                        new ForwardCommand(alien).Execute();
                        break;
                    case 2:
                        new BackwardCommand(alien).Execute();
                        break;
                    case 3:
                        new TurnLeftCommand(alien).Execute();
                        break;
                    case 4:
                        new TurnRightCommand(alien).Execute();
                        break;
                }
            }
        }

        public string AcceptCommands(String commandString)
        {
            _commander.Commands = new List<ICommand>();

            var commands = commandString.ToLower().ToCharArray();
            bool success = true;
            foreach (Char command in commands)
            {
                success = AcceptCommand(success, command);
            }

            return success ? String.Empty : "Some invalid commands were found!!!";
        }

        private bool AcceptCommand(bool success, Char command)
        {
            if (command.Equals('f'))
                _commander.Accept(new ForwardCommand(Rover));
            else if (command.Equals('b'))
                _commander.Accept(new BackwardCommand(Rover));
            else if (command.Equals('r'))
                _commander.Accept(new TurnRightCommand(Rover));
            else if (command.Equals('l'))
                _commander.Accept(new TurnLeftCommand(Rover));
            else if (command.Equals('m'))
                _commander.Accept(new FireMissileCommand(Rover));
            else if (command.Equals('g'))
                _commander.Accept(new FireMortarCommand(Rover));
            else
                success = false;
            return success;
        }

        public string ExecuteMission()
        {
            bool success = _commander.ExecuteCommands();

            return success ? String.Empty : "An error occured while executing commands";
        }
    }
}