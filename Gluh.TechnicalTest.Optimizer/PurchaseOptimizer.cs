using System;
using System.Collections.Generic;
using System.Text;
using Gluh.TechnicalTest.Models;
using Gluh.TechnicalTest.Database;
using Gluh.TechnicalTest.Services;

namespace Gluh.TechnicalTest
{
    public class PurchaseOptimizer
    {
        private readonly ISupplierService _supplierService;

        public PurchaseOptimizer(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        /// <summary>
        /// Calculates the optimal set of supplier to purchase products from.
        /// ### Complete this method
        /// </summary>
        public void Optimize(List<PurchaseRequirement> purchaseRequirements)
        {
            var suppliers = _supplierService.GetSuppliers(purchaseRequirements);
        }
    }
}
