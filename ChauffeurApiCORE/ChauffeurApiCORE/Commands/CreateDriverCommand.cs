﻿using System;
using System.Linq;
using System.Threading.Tasks;
using ChauffeurApiCORE.Exceptions;
using ChauffeurApiCORE.Models;

namespace ChauffeurApiCORE.Commands
{
	public class CreateDriverCommand : ICreateDriverCommand
    {
        private IChaufferDbContext context;
        public CreateDriverCommand(IChaufferDbContext context)
        {
            this.context = context;
        }

        public async Task<Driver> Execute(Driver model)
        {
            var existingDriver = context.Dirvers.FirstOrDefault(d => d.LicenceNumber == model.LicenceNumber);
            if (existingDriver != null)
                throw new DuplicateDriverException("Driver already exists");

            var newDriver = new Driver
            {
                DriverId = Guid.NewGuid().ToString(),
                Address = model.Address,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PreferredName = model.PreferredName,
                PostCode = model.PostCode,
                PhoneNumber = model.PhoneNumber,
                LicenceNumber = model.LicenceNumber,
                CarDetails = model.CarDetails,
                CarType = model.CarType,
                IsActive = true
            };

            context.Dirvers.Add(newDriver);
            await context.SaveChangesAsync();

            return newDriver;
        }
    }

    public interface ICreateDriverCommand
    {
        Task<Driver> Execute(Driver model);
    }
}