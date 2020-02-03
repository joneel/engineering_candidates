using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bargreen.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Bargreen.API.Controllers
{
    //TODO-CHALLENGE: Make the methods in this controller follow the async/await pattern
    //TODO-CHALLENGE: Use dotnet core dependency injection to inject the InventoryService
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
           _inventoryService = inventoryService;
        }

        [Route("InventoryBalances")]
        [HttpGet]
        public async Task<IEnumerable<InventoryBalance>> GetInventoryBalances()
        {
            return await Task.FromResult(_inventoryService.GetInventoryBalances());
        }

        [Route("AccountingBalances")]
        [HttpGet]
        public async Task<IEnumerable<AccountingBalance>> GetAccountingBalances()
        {
            return await Task.FromResult(_inventoryService.GetAccountingBalances());
        }

        [Route("InventoryReconciliation")]
        [HttpGet]
        public IEnumerable<InventoryReconciliationResult> GetReconciliation()
        {        
            //depend on abstractions(interface), not concretions(instance of object) (SOLID)
            return InventoryService.ReconcileInventoryToAccounting(_inventoryService.GetInventoryBalances(), _inventoryService.GetAccountingBalances());
        }
    }
}