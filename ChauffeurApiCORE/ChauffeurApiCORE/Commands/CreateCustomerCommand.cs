﻿using ChauffeurApiCORE.Models;
using System;
using System.Threading.Tasks;

namespace ChauffeurApiCORE.Commands
{
    public class CreateCustomerCommand : ICreateCustomerCommand
    {
        private IChaufferDbContext context;
        public CreateCustomerCommand(IChaufferDbContext context)
        {
            this.context = context;
        }

        public async Task Execute(CustomerBindingModel model)
        {
            var newCustomer = new Customer
            {
                CustomerId = Guid.NewGuid().ToString(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Address = model.LastName,
                PostCode = model.PostCode,
                PreferredName = model.PreferredName,
                PhoneNumber = model.PhoneNumber,
                PreferredDriverUserId = model.PreferredDriverUserId,
                ExtraInformation = model.ExtraInformation,
                Email = model.Email
            };

            context.Customers.Add(newCustomer);
            await context.SaveChangesAsync();
        }
    }

    public interface ICreateCustomerCommand
    {
        Task Execute(CustomerBindingModel user);
    }
}