﻿using Desafio.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Infrastructure.Repository
{
    public interface IOperationRepository
    {
        Task<List<OperationDTO>> GetAllOperations(int id);
    }
}
