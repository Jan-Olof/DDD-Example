using System;
using System.Linq;
using ApplicationLayer.Interfaces;
using ApplicationLayer.Interfaces.Services;
using DomainLayer.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CLI.Controllers
{
    public class InstructionController
    {
        private readonly IInstructionService _instructionService;

        public InstructionController(IServiceProvider serviceProvider)
        {
            if (serviceProvider == null)
            {
                throw new ArgumentNullException(nameof(serviceProvider));
            }

            _instructionService = serviceProvider.GetService<IInstructionService>();
        }

        public void CreateInstruction()
        {
            var instruction = new Instruction();

            Console.WriteLine("Name?");
            instruction.Name = Console.ReadLine();

            Console.WriteLine("Description?");
            instruction.Description = Console.ReadLine();

            var createdInstruction = _instructionService.Create(instruction);

            Console.WriteLine($"Instruction created with id {createdInstruction.Id}.");
        }

        public void ViewInstructions()
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