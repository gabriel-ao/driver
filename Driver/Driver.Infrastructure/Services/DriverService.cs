﻿using Driver.Domain.Interfaces.Repositories;
using Driver.Domain.Interfaces.Services;
using Driver.Domain.Models.Input;
using Driver.Domain.Models.Output;
using Driver.Infrastructure.Repositories;

namespace Driver.Infrastructure.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        
        public DriverService(IDriverRepository driverRepository)
        {
            _driverRepository = driverRepository;
        }

        public CreateDriverOutput CreateDriver(CreateDriverInput input)
        {
            var response = new CreateDriverOutput();

            input.Cnpj = input.Cnpj.Replace(".", "").Replace("-", "").Replace(" ", "");
            input.CnhNumber = input.CnhNumber.Replace(".", "").Replace("-", "").Replace(" ", "");

            input.CnhImage = ""; //TODO: CRIAR STORAGE PARA SALVAR IMAGEM

            var result = _driverRepository.CreateDriver(input);

            //TODO: ADICIONAR O TOKEN COM O USER ID
            response.Message = result.Message;
            response.Error = result.Error;

            return response;
        }
    }
}