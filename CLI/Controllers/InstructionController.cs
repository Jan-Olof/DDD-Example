using System;
using System.Linq;
using ApplicationLayer.Interfaces.Services;
using DomainLayer.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CLI.Controllers
{
    public class InstructionController : BaseController
    {
        private readonly IInstructionService _instructionService;

        public InstructionController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _instructionService = serviceProvider.GetService<IInstructionService>();
        }

        public void InstructionFlow()
        {
            if (ConsoleCommands.YesNoCommand("Add instruction? (y/n)"))
            {
                CreateInstruction();
            }

            if (ConsoleCommands.YesNoCommand("View instructions? (y/n)"))
            {
                ViewInstructions();
            }

            if (ConsoleCommands.YesNoCommand("Update instruction description? (y/n)"))
            {
                UpdateInstructions();
            }

            if (ConsoleCommands.YesNoCommand("View instructions? (y/n)"))
            {
                ViewInstructions();
            }
        }

        private void CreateInstruction()
        {
            var instruction = new Instruction();

            Console.WriteLine("Name?");
            instruction.Name = Console.ReadLine();

            Console.WriteLine("Description?");
            instruction.Description = Console.ReadLine();

            var createdInstruction = _instructionService.Create(instruction);

            Console.WriteLine($"Instruction created with id {createdInstruction.Id}.");
        }

        private void UpdateInstructions()
        {
            Console.WriteLine("Id of instruction to update?");
            string idEntered = Console.ReadLine();

            int id;
            bool ok = int.TryParse(idEntered, out id);

            if (!ok)
            {
                Console.WriteLine("Failed to parse entered id.");
                return;
            }

            var instruction = _instructionService.Get(id);

            if (instruction == null)
            {
                Console.WriteLine("Failed to get instruction from id.");
                return;
            }

            Console.WriteLine($"Old description: {instruction.Description}");
            Console.WriteLine("Enter new description:");
            string description = Console.ReadLine();

            instruction.Description = description;
            _instructionService.Update(instruction, id);

            Console.WriteLine("New description entered.");
        }

        private void ViewInstructions()
        {
            var instructions = _instructionService.Get();

            if (instructions == null || !instructions.Any())
            {
                Console.WriteLine("There are no instructions.");
                return;
            }

            Console.WriteLine("Showing all instructions:/n");

            foreach (var instruction in instructions)
            {
                Console.WriteLine($"Id: {instruction.Id}, Name: {instruction.Name}, Description: {instruction.Description}");
                Console.WriteLine();
            }
        }
    }
}