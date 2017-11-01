using Microsoft.VisualStudio.TestTools.UnitTesting;
using BBBTeaSS.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBBTeaSS.DAL.Tests
{
    [TestClass()]
    public class StockDALTests
    {
        [TestMethod()]
        public void GetStockInfoByProductIDTest()
        {
            new StockDAL().GetStockInfoByProductID(1, 1);
        }
    }
}