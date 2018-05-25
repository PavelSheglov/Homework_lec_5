using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using hw_1_2;

namespace UnitTest
{
    [TestClass]
    public class TestHW_1_2_SavingsAccount
    {
        [TestMethod]
        public void TestConstructor()
        {
            var account = new SavingsAccount("acc1", "client1");
            var account2 = new SavingsAccount(" ", " ");

            Assert.AreEqual("acc1", account.AccountNumber);
            Assert.AreEqual("client1", account.AccountOwner);
            Assert.AreEqual(0M, account.AccountBalance);
            Assert.AreEqual(AccountStatus.Opened, account.Status);

            Assert.AreEqual(" ", account2.AccountNumber);
            Assert.AreEqual(" ", account2.AccountOwner);

            account2.AccountOwner = "client2";
            account2.AccountNumber = "acc2";

            Assert.AreEqual("acc2", account2.AccountNumber);
            Assert.AreEqual("client2", account2.AccountOwner);
        }
        [TestMethod]
        public void PositiveTestAddFunds()
        {
            var account = new SavingsAccount("acc1", "client1");
            var result1=account.AddFunds(100M);
            var result2 = account.AddFunds(0M);

            Assert.AreEqual(true, result1);
            Assert.AreEqual(true, result2);
            Assert.AreEqual(100M, account.AccountBalance);
        }
        [TestMethod]
        public void NegativeTestAddFunds()
        {
            var account1 = new SavingsAccount("acc1", "client1");
            var result1 = account1.AddFunds(-100M);
            var account2 = new SavingsAccount("acc2", "client2");
            var result2 = account2.CloseAccount();
            var result3 = account2.AddFunds(100M);

            Assert.AreEqual(false, result1);
            Assert.AreEqual(0M, account1.AccountBalance);
            Assert.AreEqual(true, result2);
            Assert.AreEqual(false, result3);
            Assert.AreEqual(0M, account1.AccountBalance);
        }
        [TestMethod]
        public void PositiveTestWithdrawFunds()
        {
            var account = new SavingsAccount("acc1", "client1");
            account.AddFunds(100M);
            var result1 = account.WithdrawFunds(50M);
            var result2 = account.WithdrawFunds(0);

            Assert.AreEqual(true, result1);
            Assert.AreEqual(true, result2);
            Assert.AreEqual(50M, account.AccountBalance);
        }
        [TestMethod]
        public void NegativeTestWithdrawFunds()
        {
            var account = new SavingsAccount("acc1", "client1");
            account.AddFunds(100M);
            var result1 = account.WithdrawFunds(500M);
            var result2 = account.WithdrawFunds(-50M);

            Assert.AreEqual(false, result1);
            Assert.AreEqual(false, result2);
            Assert.AreEqual(100M, account.AccountBalance);

            account.WithdrawFunds(100M);
            var result3 = account.WithdrawFunds(10M);
            Assert.AreEqual(false, result3);
            Assert.AreEqual(0M, account.AccountBalance);

            account.CloseAccount();
            var result4 = account.WithdrawFunds(10M);
            Assert.AreEqual(false, result4);
            Assert.AreEqual(0M, account.AccountBalance);
        }
        [TestMethod]
        public void PositiveTestZeroingAccount()
        {
            var account = new SavingsAccount("acc1", "client1");
            account.AddFunds(100M);
            var result1 = account.ZeroingAccount();
            var result2 = account.ZeroingAccount();

            Assert.AreEqual(true, result1);
            Assert.AreEqual(true, result2);
            Assert.AreEqual(0M, account.AccountBalance);
        }
        [TestMethod]
        public void NegativeTestZeroingAccount()
        {
            var account = new SavingsAccount("acc1", "client1");
            account.CloseAccount();
            var result = account.ZeroingAccount();
            
            Assert.AreEqual(false, result);
            Assert.AreEqual(0M, account.AccountBalance);
        }
        [TestMethod]
        public void PositiveTestCloseAccount()
        {
            var account = new SavingsAccount("acc1", "client1");
            var result=account.CloseAccount();
            
            Assert.AreEqual(true, result);
        }
        [TestMethod]
        public void NegativeTestCloseAccount()
        {
            var account = new SavingsAccount("acc1", "client1");
            account.AddFunds(100M);
            var result1 = account.CloseAccount();
            account.ZeroingAccount();
            account.CloseAccount();
            var result2 = account.CloseAccount();

            Assert.AreEqual(false, result1);
            Assert.AreEqual(false, result2);
        }
    }
}
