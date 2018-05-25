using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using hw_1_2;

namespace UnitTest
{
    [TestClass]
    public class TestHW_1_2_MetalAccount
    {
        [TestMethod]
        public void TestConstructor()
        {
            var account = new MetalAccount("acc1", "client1", MetalType.Argentum, 100, 1500.00M);

            Assert.AreEqual("acc1", account.AccountNumber);
            Assert.AreEqual("client1", account.AccountOwner);
            Assert.AreEqual(150000.00M, account.AccountBalance);
            Assert.AreEqual(AccountStatus.Opened, account.Status);
            Assert.AreEqual(MetalType.Argentum, account.Metal);
            Assert.AreEqual((uint)100, account.MetalBalance);
            Assert.AreEqual(1500.00M, account.PriceRate);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void NegativeTestConstructor()
        {
            var account = new MetalAccount("acc1", "client1", MetalType.Argentum, 0, 0.00M);  
        }
        [TestMethod]
        public void PositiveTestAddFunds()
        {
            var account = new MetalAccount("acc1", "client1", MetalType.Argentum, 0, 500M);
            var result1 = account.AddFunds(1000M);
            var result2 = account.AddFunds(0M);

            Assert.AreEqual(true, result1);
            Assert.AreEqual(true, result2);
            Assert.AreEqual(1000M, account.AccountBalance);
            Assert.AreEqual(2L, account.MetalBalance);
        }
        [TestMethod]
        public void NegativeTestAddFunds()
        {
            var account = new MetalAccount("acc1", "client1", MetalType.Aurum, 0, 1500M);
            try
            {
                account.AddFunds(-50M);

                Assert.Fail();
            }
            catch(InvalidOperationException)  { }

            try
            {
                account.AddFunds(1000M);

                Assert.Fail();
            }
            catch (InvalidOperationException) { }

            account.CloseAccount();
            var result2 = account.AddFunds(1500M);

            Assert.AreEqual(false, result2);
            Assert.AreEqual(0M, account.AccountBalance);
            Assert.AreEqual(0L, account.MetalBalance);
        }
        [TestMethod]
        public void PositiveTestAddFundsInMetal()
        {
            var account = new MetalAccount("acc1", "client1", MetalType.Argentum, 0, 500M);
            var result1 = account.AddFundsInMetal(100);
            var result2 = account.AddFundsInMetal(0);

            Assert.AreEqual(true, result1);
            Assert.AreEqual(true, result2);
            Assert.AreEqual(50000M, account.AccountBalance);
            Assert.AreEqual(100, account.MetalBalance);
        }
        [TestMethod]
        public void NegativeTestAddFundsInMetal()
        {
            var account = new MetalAccount("acc1", "client1", MetalType.Aurum, 0, 1500M);
            account.CloseAccount();

            var result = account.AddFundsInMetal(50);

            Assert.AreEqual(false, result);
            Assert.AreEqual(0M, account.AccountBalance);
            Assert.AreEqual(0L, account.MetalBalance);
        }
        [TestMethod]
        public void PositiveTestWithdrawFunds()
        {
            var account = new MetalAccount("acc1", "client1", MetalType.Platinum, 0, 2500M);
            account.AddFunds(5000M);
            var result1 = account.WithdrawFunds(2500M);
            var result2 = account.WithdrawFunds(0);

            Assert.AreEqual(true, result1);
            Assert.AreEqual(true, result2);
            Assert.AreEqual(2500M, account.AccountBalance);
            Assert.AreEqual(1L, account.MetalBalance);
        }
        [TestMethod]
        public void NegativeTestWithdrawFunds()
        {
            var account = new MetalAccount("acc1", "client1", MetalType.Aurum, 0, 1500M);
            account.AddFundsInMetal(1);

            var result1 = account.WithdrawFunds(3000M);
            Assert.AreEqual(false, result1);
            Assert.AreEqual(1500M, account.AccountBalance);
            Assert.AreEqual(1, account.MetalBalance);

            try
            {
                account.WithdrawFunds(-1500M);

                Assert.Fail();
            }
            catch (InvalidOperationException) { }

            try
            {
                account.WithdrawFunds(1000M);

                Assert.Fail();
            }
            catch (InvalidOperationException) { }

            Assert.AreEqual(1500M, account.AccountBalance);
            Assert.AreEqual(1, account.MetalBalance);

            account.ZeroingAccount();
            account.CloseAccount();
            var result2 = account.WithdrawFunds(1500M);

            Assert.AreEqual(false, result2);
            Assert.AreEqual(0M, account.AccountBalance);
            Assert.AreEqual(0L, account.MetalBalance);
        }
        [TestMethod]
        public void PositiveTestWithdrawFundsInMetal()
        {
            var account = new MetalAccount("acc1", "client1", MetalType.Argentum, 0, 500M);
            account.AddFundsInMetal(100);
            var result1 = account.WithdrawFundsInMetal(50);
            var result2 = account.WithdrawFundsInMetal(0);

            Assert.AreEqual(true, result1);
            Assert.AreEqual(true, result2);
            Assert.AreEqual(25000M, account.AccountBalance);
            Assert.AreEqual(50, account.MetalBalance);
        }
        [TestMethod]
        public void NegativeTestWithdrawFundsInMetal()
        {
            var account = new MetalAccount("acc1", "client1", MetalType.Aurum, 0, 1500M);
            account.CloseAccount();

            var result = account.WithdrawFundsInMetal(50);

            Assert.AreEqual(false, result);
            Assert.AreEqual(0M, account.AccountBalance);
            Assert.AreEqual(0L, account.MetalBalance);
        }
        [TestMethod]
        public void PositiveTestZeroingAccount()
        {
            var account = new MetalAccount("acc1", "client1", MetalType.Argentum, 100, 500M);
            var result1 = account.ZeroingAccount();
            var result2 = account.ZeroingAccount();

            Assert.AreEqual(true, result1);
            Assert.AreEqual(true, result2);
            Assert.AreEqual(0M, account.AccountBalance);
            Assert.AreEqual(0L, account.MetalBalance);
        }
        [TestMethod]
        public void NegativeTestZeroingAccount()
        {
            var account = new MetalAccount("acc1", "client1", MetalType.Argentum, 0, 500M);
            account.CloseAccount();
            var result = account.ZeroingAccount();

            Assert.AreEqual(false, result);
            Assert.AreEqual(0M, account.AccountBalance);
            Assert.AreEqual(0L, account.MetalBalance);
        }
        [TestMethod]
        public void PositiveTestCloseAccount()
        {
            var account = new MetalAccount("acc1", "client1", MetalType.Argentum, 0, 500M);
            var result = account.CloseAccount();

            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void NegativeTestCloseAccount()
        {
            var account = new MetalAccount("acc1", "client1", MetalType.Argentum, 100, 500M);
            var result1 = account.CloseAccount();
            account.ZeroingAccount();
            account.CloseAccount();
            var result2 = account.CloseAccount();

            Assert.AreEqual(false, result1);
            Assert.AreEqual(false, result2);
        }
    }
}
