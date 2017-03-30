using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Docs.Scrapper.VSRF;
using Docs.Scrapper.SudRF;
using System.Linq;

namespace Docs.Scrapper.Tests
{
    [TestClass]
    public class ScrapperTests
    {
        [TestMethod]
        public void InitPerformerTest()
        {
            var list = Performer.Instance.GetAvalibleScrappers();
            Assert.IsTrue(list.Count > 0);
            Assert.IsNotNull(list.Where(x => x is VSRFEngine).FirstOrDefault());
            Assert.IsNotNull(list.Where(x => x is SudRFEngine).FirstOrDefault());
        }
        [TestMethod]
        public void ScrapVSRFTest()
        {
            var scrapper = Performer.Instance.GetAvalibleScrappers()
                .Where(x => x.ScrapperType == Base.ScrapperType.VSRF).FirstOrDefault();
            Assert.IsNotNull(scrapper);
            Exception myex = null;
            try
            {
                scrapper.DoScrap(DateTime.Now.AddDays(-7), DateTime.Now);
            }
            catch (Exception ex) { myex = ex; }
            Assert.IsNull(myex);
        }
    }
}
