using Bargreen.API.Controllers;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xunit;
using Bargreen.Services;

namespace Bargreen.Tests
{
    public class InventoryControllerTests
    {
        [Fact]
        public async void InventoryController_Can_Return_Inventory_Balances()
        {
            var controller = new InventoryController(new InventoryService());
            var result = await controller.GetInventoryBalances();
            Assert.NotEmpty(result);
        }

        [Fact]
        public void Controller_Methods_Are_Async()
        {
            var methods = typeof(InventoryController)
                .GetMethods()
                .Where(m=>m.DeclaringType==typeof(InventoryController));

            Assert.All(methods, m =>
            {
                Type attType = typeof(AsyncStateMachineAttribute); 
                var attrib = (AsyncStateMachineAttribute)m.GetCustomAttribute(attType);
                Assert.NotNull(attrib);
                Assert.Equal(typeof(Task), m.ReturnType.BaseType);
            });
        }
    }
}
