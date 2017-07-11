using System;
using System.Linq;
using ApplicationLayer.Interfaces.Interactors;
using DomainLayer.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CLI.Controllers
{
    public class InstructionController : BaseController
    {
        private readonly IProductInteractor _productService;

        public InstructionController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _productService = serviceProvider.GetService<IProductInteractor>();
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
            var instruction = new Product();

            Console.WriteLine("Name?");
            instruction.Name = Console.ReadLine();

            Console.WriteLine("Description?");
            instruction.Description = Console.ReadLine();

            var createdInstruction = _productService.Create(instruction);

            Console.WriteLine($"Instruction created with id {createdInstruction.Id}.");
        }

        private void UpdateInstructions()
        {
            Console.WriteLine("Id of instruction to update?");
            string idEntered = Console.ReadLine();

            bool ok = int.TryParse(idEntered, out int id);

            if (!ok)
            {
                Console.WriteLine("Failed to parse entered id.");
                return;
            }

            var instruction = _productService.Get(id);

            if (instruction == null)
            {
                Console.WriteLine("Failed to get instruction from id.");
                return;
            }

            Console.WriteLine($"Old description: {instruction.Description}");
            Console.WriteLine("Enter new description:");
            string description = Console.ReadLine();

            instruction.Description = description;
            _productService.Update(instruction, id);

            Console.WriteLine("New description entered.");
        }

        private void ViewInstructions()
        {
            var instructions = _productService.Get();

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