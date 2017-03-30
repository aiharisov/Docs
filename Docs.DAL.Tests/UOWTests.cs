using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Docs.DAL.Tests
{
    [TestClass]
    public class UOWTests
    {
        [TestMethod]
        public void InitDatabasesTest()
        {
            using (UOW.UOWDocs docs = new UOW.UOWDocs())
            {
                docs.InitDB();
            }
            using (UOW.UOWLog log = new UOW.UOWLog())
            { }
        }
        [TestMethod]
        public void CheckSprTest()
        {
            using (UOW.UOWDocs docs = new UOW.UOWDocs())
            {
                Assert.IsTrue(docs.CaseRepository.GetAll().Count() >= 5);
                Assert.IsTrue(docs.CourtRepository.GetAll().Count() >= 2);
                Assert.IsTrue(docs.InstanceRepository.GetAll().Count() >= 4);
                Assert.IsTrue(docs.TypeDocRepository.GetAll().Count() >= 4);
            }
        }
        [TestMethod]
        public void CheckRepository()
        {
            using (UOW.UOWDocs docs = new UOW.UOWDocs())
            {
                Assert.IsNotNull(docs.DocRepository);
            }
            using (UOW.UOWLog log = new UOW.UOWLog())
            {
                Assert.IsNotNull(log.errorRepository);
                Assert.IsNotNull(log.ScrapperLogRepository);
            }
        }
    }
}
