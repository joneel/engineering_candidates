using System;
using System.Collections.Generic;
using System.Text;

namespace Bargreen.Services
{
    public class InventoryReconciliationResult
    {
        public string ItemNumber { get; set; }
        public decimal TotalValueOnHandInInventory { get; set; }
        public decimal TotalValueInAccountingBalance { get; set; }
    }

    public class InventoryBalance
    {
        public string ItemNumber { get; set; }
        public string WarehouseLocation { get; set; }
        public int QuantityOnHand { get; set; }
        public decimal PricePerItem { get; set; }
    }

    public class AccountingBalance
    {
        public string ItemNumber { get; set; }
        public decimal TotalInventoryValue { get; set; }
    }

    public interface IInventoryService
    {
        IEnumerable<InventoryBalance> GetInventoryBalances();
        IEnumerable<AccountingBalance> GetAccountingBalances();
    }

    public class InventoryService : IInventoryService
    {

        public IEnumerable<InventoryBalance> GetInventoryBalances()
        {
            return new List<InventoryBalance>()
            {
                new InventoryBalance()
                {
                     ItemNumber = "ABC123",
                     PricePerItem = 7.5M,
                     QuantityOnHand = 312,
                     WarehouseLocation = "WLA1"
                },
                new InventoryBalance()
                {
                     ItemNumber = "ABC123",
                     PricePerItem = 7.5M,
                     QuantityOnHand = 146,
                     WarehouseLocation = "WLA2"
                },
                new InventoryBalance()
                {
                     ItemNumber = "ZZZ99",
                     PricePerItem = 13.99M,
                     QuantityOnHand = 47,
                     WarehouseLocation = "WLA3"
                },
                new InventoryBalance()
                {
                     ItemNumber = "zzz99",
                     PricePerItem = 13.99M,
                     QuantityOnHand = 91,
                     WarehouseLocation = "WLA4"
                },
                new InventoryBalance()
                {
                     ItemNumber = "xxccM",
                     PricePerItem = 245.25M,
                     QuantityOnHand = 32,
                     WarehouseLocation = "WLA5"
                },
                new InventoryBalance()
                {
                     ItemNumber = "xxddM",
                     PricePerItem = 747.47M,
                     QuantityOnHand = 15,
                     WarehouseLocation = "WLA6"
                }
            };
        }

        public IEnumerable<AccountingBalance> GetAccountingBalances()
        {
            return new List<AccountingBalance>()
            {
                new AccountingBalance()
                {
                     ItemNumber = "ABC123",
                     TotalInventoryValue = 3435M
                },
                new AccountingBalance()
                {
                     ItemNumber = "ZZZ99",
                     TotalInventoryValue = 1930.62M
                },
                new AccountingBalance()
                {
                     ItemNumber = "xxccM",
                     TotalInventoryValue = 7602.75M
                },
                new AccountingBalance()
                {
                     ItemNumber = "fbr77",
                     TotalInventoryValue = 17.99M
                }
            };
        }

        public static IEnumerable<InventoryReconciliationResult> ReconcileInventoryToAccounting(IEnumerable<InventoryBalance> inventoryBalances, IEnumerable<AccountingBalance> accountingBalances)
        {
            //TODO-CHALLENGE: Compare inventory balances to accounting balances and find differences
//TODO check if null
//TODO use Dependency Injection instead
            List<InventoryReconciliationResult> totalReconciles = new List<InventoryReconciliationResult>();
            InventoryReconciliationResult currResult;

            foreach(AccountingBalance accountingBalance in accountingBalances)
            {
                currResult = new InventoryReconciliationResult();//reset currResult for next iteration
                
                foreach(InventoryBalance inventoryBalance in inventoryBalances)
                {
                    if(inventoryBalance.ItemNumber == accountingBalance.ItemNumber)//if ItemNumber matches
                    {
//TODO fix assigning ItemNumber and TotalValueInAccountingBalance multiple times
                        currResult.ItemNumber = inventoryBalance.ItemNumber;//assign item number to currResult
                        currResult.TotalValueInAccountingBalance = accountingBalance.TotalInventoryValue;//assign accounting total value to currResult
                        currResult.TotalValueOnHandInInventory += (inventoryBalance.PricePerItem * inventoryBalance.QuantityOnHand);//sum up totals from different warehouses
                    }
                }
                if(currResult.TotalValueOnHandInInventory != currResult.TotalValueInAccountingBalance)//if balances do not match up, add them to totals to reconcile
                {
                    totalReconciles.Add(currResult);
                }
            }
            
            return totalReconciles;
            //throw new NotImplementedException();
        }
    }
}