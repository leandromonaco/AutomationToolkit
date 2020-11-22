﻿using AutomationToolkit.Exchange;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Text;
using Xunit;

namespace AutomationToolkit.Test
{
    public class ExchangeTest
    {
        IConfiguration _configuration;
        ExchangeRepository _exchangeTestRepository;

        public ExchangeTest()
        {
            _configuration = new ConfigurationBuilder()
                                        //.SetBasePath(outputPath)
                                        .AddJsonFile("appsettings.json", optional: true)
                                        .AddUserSecrets("cf9af699-d3c2-4090-8231-fd3a1cb45a5f")
                                        .AddEnvironmentVariables()
                                        .Build();

            _exchangeTestRepository = new ExchangeRepository(_configuration["Exchange:Url"], _configuration["Exchange:Key"], Common.Http.AuthenticationType.Exchange);
            
        }

        [Fact]
        async void SendAppointment()
        {
            _exchangeTestRepository.SendAppointment("name.lastname@email.com", "automated test =)", "automated test =)", "works on my machine", DateTime.Now);
        }

        [Fact]
        async void GetAppointments()
        {
            var appointments = _exchangeTestRepository.GetAppointments(new DateTime(2020, 01, 01), DateTime.Now);

            StringBuilder sb = new StringBuilder();

            foreach (Appointment a in appointments)
            {
                sb.AppendLine($"{a.Subject}~{a.Start}");
            }

            System.IO.File.WriteAllText(@"C:\Temp\Output.txt", sb.ToString());
        }
    }
}
