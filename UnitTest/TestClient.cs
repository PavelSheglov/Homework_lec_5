using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using hw_1_2;

namespace UnitTest
{
    [TestClass]
    public class TestClient
    {
        [TestMethod]
        public void TestConstructor()
        {
            var client = new Client("client1");

            Assert.AreEqual("client1", client.Name);
            Assert.AreNotEqual((uint)0, client.Number);
            Assert.AreEqual((uint)0, client.CountOfOpenedAccounts);
            Assert.AreEqual(0M, client.TotalBalance);
        }
        [TestMethod]
        public void TestOpenAccounts()
        {
            var client = new Client("client1");

            client.OpenSavingsAccount(1000M);
            client.OpenSavingsAccount(0M);
            client.OpenCumulativeAccount(1000M);
            client.OpenCumulativeAccount(0M);
            client.OpenCheckingAccount(1000M);
            client.OpenCheckingAccount(0M);
            client.OpenMetalAccount(MetalType.Argentum, 100, 500M);
            client.OpenMetalAccount(MetalType.Aurum, 100, 1500M);
            client.OpenMetalAccount(MetalType.Platinum, 100, 2500M);

            Assert.AreEqual((uint)9, client.CountOfOpenedAccounts);
            Assert.AreEqual(463000M, client.TotalBalance);
        }
        [TestMethod]
        public void NegativeTestOpenAccounts()
        {
            var client = new Client("client1");

            try
            {
                client.OpenSavingsAccount(-1000M);
                Assert.Fail();
            }
            catch(Exception) { }

            try
            {
                client.OpenCumulativeAccount(-1000M);
                Assert.Fail();
            }
            catch (Exception) { }

            try
            {
                client.OpenCheckingAccount(-1000M);
                Assert.Fail();
            }
            catch (Exception) { }

            try
            {
                client.OpenMetalAccount(MetalType.Argentum, 100, 0M);
                Assert.Fail();
            }
            catch (Exception) { }

            try
            {
                client.OpenMetalAccount(MetalType.Argentum, 0, -10M);
                Assert.Fail();
            }
            catch (Exception) { }

            Assert.AreEqual((uint)0, client.CountOfOpenedAccounts);
            Assert.AreEqual(0M, client.TotalBalance);
        }
        [TestMethod]
        public void TestGetLists()
        {
            var client = new Client("client1");

            client.OpenSavingsAccount(1000M);
            client.OpenSavingsAccount(0M);
            client.OpenCumulativeAccount(1000M);
            client.OpenCumulativeAccount(0M);
            client.OpenCheckingAccount(1000M);
            client.OpenCheckingAccount(0M);
            client.OpenMetalAccount(MetalType.Argentum, 100, 500M);
            client.OpenMetalAccount(MetalType.Aurum, 100, 1500M);
            client.OpenMetalAccount(MetalType.Platinum, 100, 2500M);

            var totalList = client.GetListOfAccounts();
            var savingsList = client.GetListOfAccounts(AccountType.SavingsAccount);
            var cumulativeList = client.GetListOfAccounts(AccountType.CumulativeAccount);
            var checkingList = client.GetListOfAccounts(AccountType.CheckingAccount);
            var metalList = client.GetListOfAccounts(AccountType.MetalAccount);

            Assert.AreEqual(9, totalList.Count);
            Assert.AreEqual(2, savingsList.Count);
            Assert.AreEqual(2, cumulativeList.Count);
            Assert.AreEqual(2, checkingList.Count);
            Assert.AreEqual(3, metalList.Count);
        }
        [TestMethod]
        public void NegativeTestGetLists()
        {
            var client = new Client("client1");

            var totalList = client.GetListOfAccounts();
            var savingsList = client.GetListOfAccounts(AccountType.SavingsAccount);
            var cumulativeList = client.GetListOfAccounts(AccountType.CumulativeAccount);
            var checkingList = client.GetListOfAccounts(AccountType.CheckingAccount);
            var metalList = client.GetListOfAccounts(AccountType.MetalAccount);

            Assert.AreEqual(0, totalList.Count);
            Assert.AreEqual(0, savingsList.Count);
            Assert.AreEqual(0, cumulativeList.Count);
            Assert.AreEqual(0, checkingList.Count);
            Assert.AreEqual(0, metalList.Count);
        }
        [TestMethod]
        public void TestFindAccount()
        {
            var client = new Client("client1");

            client.OpenSavingsAccount(1000M);
            var account = client.FindAccount(client.GetListOfAccounts().First().AccountNumber);

            Assert.AreNotEqual(null, account);
        }
        [TestMethod]
        public void NegativeTestFindAccount()
        {
            var client = new Client("client1");

            client.OpenSavingsAccount(1000M);
            var account = client.FindAccount("account");

            Assert.AreEqual(null, account);
        }
        [TestMethod]
        public void TestReplenishAccount()
        {
            var client = new Client("client1");

            client.OpenSavingsAccount(1000M);
            client.OpenCumulativeAccount(1000M);
            client.OpenCheckingAccount(1000M);
            client.OpenMetalAccount(MetalType.Argentum, 100, 500M);

            var account1 = client.FindAccount(client.GetListOfAccounts(AccountType.SavingsAccount).First().AccountNumber);
            var account2 = client.FindAccount(client.GetListOfAccounts(AccountType.CumulativeAccount).First().AccountNumber);
            var account3 = client.FindAccount(client.GetListOfAccounts(AccountType.CheckingAccount).First().AccountNumber);
            var account4 = client.FindAccount(client.GetListOfAccounts(AccountType.MetalAccount).First().AccountNumber);

            client.ReplenishAccount(account1, 500M);
            client.ReplenishAccount(account2, 500M);
            client.ReplenishAccount(account3, 500M);
            client.ReplenishAccount(account4, 5000M);

            Assert.AreEqual(59500, client.TotalBalance);

            account1.AddFunds(500M);

            Assert.AreEqual(60000, client.TotalBalance);
        }
        [TestMethod]
        public void NegativeTestReplenishAccount()
        {
            var client = new Client("client1");

            client.OpenSavingsAccount(1000M);
            client.OpenCumulativeAccount(1000M);
            client.OpenMetalAccount(MetalType.Argentum, 100, 500M);

            var account1 = client.FindAccount(client.GetListOfAccounts(AccountType.SavingsAccount).First().AccountNumber);
            var account2 = client.FindAccount(client.GetListOfAccounts(AccountType.CumulativeAccount).First().AccountNumber);
            var account3 = client.FindAccount(client.GetListOfAccounts(AccountType.MetalAccount).First().AccountNumber);

            try
            {
                client.ReplenishAccount(account1, -500M);
                Assert.Fail();
            }
            catch(Exception) { }

            try
            {
                client.ZeroingAccount(account2);
                client.CloseAccount(account2);
                client.ReplenishAccount(account2, 500M);
                Assert.Fail();
            }
            catch (Exception) { }

            try
            {
                client.ReplenishAccount(account3, 100M);
                Assert.Fail();
            }
            catch (Exception) { }
            
            Assert.AreEqual(51000, client.TotalBalance);
        }
        [TestMethod]
        public void TestReplenishMetalAccount()
        {
            var client = new Client("client1");
                        
            client.OpenMetalAccount(MetalType.Argentum, 100, 500M);

            var account = client.FindAccount(client.GetListOfAccounts(AccountType.MetalAccount).First().AccountNumber);

            client.ReplenishMetalAccount(account, 100);
            client.ReplenishMetalAccount(account, 0);

            Assert.AreEqual(100000, client.TotalBalance);
        }
        [TestMethod]
        public void NegativeTestReplenishMetalAccount()
        {
            var client = new Client("client1");

            client.OpenMetalAccount(MetalType.Argentum, 100, 500M);

            var account = client.FindAccount(client.GetListOfAccounts(AccountType.MetalAccount).First().AccountNumber);

            try
            {
                client.ZeroingAccount(account);
                client.CloseAccount(account);
                client.ReplenishMetalAccount(account, 100);
                Assert.Fail();
            }
            catch(Exception) { }
            
            Assert.AreEqual(0, client.TotalBalance);
        }
        [TestMethod]
        public void TestWithdrawAccount()
        {
            var client = new Client("client1");

            client.OpenSavingsAccount(1000M);
            client.OpenCumulativeAccount(1000M);
            client.OpenCheckingAccount(1000M);
            client.OpenMetalAccount(MetalType.Argentum, 100, 500M);

            var account1 = client.FindAccount(client.GetListOfAccounts(AccountType.SavingsAccount).First().AccountNumber);
            var account2 = client.FindAccount(client.GetListOfAccounts(AccountType.CumulativeAccount).First().AccountNumber);
            var account3 = client.FindAccount(client.GetListOfAccounts(AccountType.CheckingAccount).First().AccountNumber);
            var account4 = client.FindAccount(client.GetListOfAccounts(AccountType.MetalAccount).First().AccountNumber);

            client.WithdrawFundsFromAccount(account1, 0M);
            client.WithdrawFundsFromAccount(account1, 1000M);
            client.ReplenishAccount(account2, 100M);
            client.WithdrawFundsFromAccount(account2, 100M);
            client.WithdrawFundsFromAccount(account3, 900M);
            client.WithdrawFundsFromAccount(account4, 50000M);

            Assert.AreEqual(1000M, client.TotalBalance);
        }
        [TestMethod]
        public void NegativeTestWithdrawAccount()
        {
            var client = new Client("client1");

            client.OpenSavingsAccount(1000M);
            client.OpenCumulativeAccount(1000M);
            client.OpenCheckingAccount(1000M);
            client.OpenMetalAccount(MetalType.Argentum, 100, 500M);

            var account1 = client.FindAccount(client.GetListOfAccounts(AccountType.SavingsAccount).First().AccountNumber);
            var account2 = client.FindAccount(client.GetListOfAccounts(AccountType.CumulativeAccount).First().AccountNumber);
            var account3 = client.FindAccount(client.GetListOfAccounts(AccountType.CheckingAccount).First().AccountNumber);
            var account4 = client.FindAccount(client.GetListOfAccounts(AccountType.MetalAccount).First().AccountNumber);

            try
            {
                client.WithdrawFundsFromAccount(account1, -1000M);
                Assert.Fail();
            }
            catch(Exception) { }

            try
            {
                client.WithdrawFundsFromAccount(account1, 2000M);
                Assert.Fail();
            }
            catch (Exception) { }

            try
            {
                client.ZeroingAccount(account1);
                client.CloseAccount(account1);
                client.WithdrawFundsFromAccount(account1, 1000M);
                Assert.Fail();
            }
            catch (Exception) { }
            
            try
            {
                client.WithdrawFundsFromAccount(account2, 100M);
                Assert.Fail();
            }
            catch (Exception) { }

            try
            {
                client.WithdrawFundsFromAccount(account3, 1000M);
                Assert.Fail();
            }
            catch (Exception) { }

            try
            {
                client.WithdrawFundsFromAccount(account4, 100M);
            }
            catch (Exception) { }

            Assert.AreEqual(52000M, client.TotalBalance);
        }
        [TestMethod]
        public void TestWithdrawhMetalAccount()
        {
            var client = new Client("client1");

            client.OpenMetalAccount(MetalType.Argentum, 100, 500M);

            var account = client.FindAccount(client.GetListOfAccounts(AccountType.MetalAccount).First().AccountNumber);

            client.WithdrawFundsFromMetalAccount(account, 50);
            client.ReplenishMetalAccount(account, 0);

            Assert.AreEqual(25000M, client.TotalBalance);
        }
        [TestMethod]
        public void NegativeTestWithdrawMetalAccount()
        {
            var client = new Client("client1");

            client.OpenMetalAccount(MetalType.Argentum, 0, 500M);

            var account = client.FindAccount(client.GetListOfAccounts(AccountType.MetalAccount).First().AccountNumber);

            try
            {
                client.WithdrawFundsFromMetalAccount(account, 100);
                Assert.Fail();
            }
            catch (Exception) { }

            try
            {
                client.CloseAccount(account);
                client.WithdrawFundsFromMetalAccount(account, 100);
                Assert.Fail();
            }
            catch (Exception) { }

            Assert.AreEqual(0, client.TotalBalance);
        }
        [TestMethod]
        public void TestInterestsCapitalization()
        {
            var client = new Client("client1");
                        
            client.OpenCumulativeAccount(1000M);
                       
            var account = client.FindAccount(client.GetListOfAccounts(AccountType.CumulativeAccount).First().AccountNumber);
            client.CapitalizeInterestsOnAccount(account);

            Assert.AreEqual(1002.08M, client.TotalBalance);
        }
        [TestMethod]
        public void NegativeTestInterestsCapitalization()
        {
            var client = new Client("client1");

            client.OpenCumulativeAccount(1000M);
            client.OpenSavingsAccount(1000M);

            var account1 = client.FindAccount(client.GetListOfAccounts(AccountType.CumulativeAccount).First().AccountNumber);
            var account2 = client.FindAccount(client.GetListOfAccounts(AccountType.SavingsAccount).First().AccountNumber);

            try
            {
                client.ZeroingAccount(account1);
                client.CapitalizeInterestsOnAccount(account1);
                Assert.Fail();
            }
            catch (Exception) { }

            try
            {
                client.CloseAccount(account1);
                client.CapitalizeInterestsOnAccount(account1);
                Assert.Fail();
            }
            catch (Exception) { }

            try
            {
                client.CapitalizeInterestsOnAccount(account2);
                Assert.Fail();
            }
            catch (Exception) { }

            Assert.AreEqual(1000.00M, client.TotalBalance);
        }
        [TestMethod]
        public void TestZeroingAccount()
        {
            var client = new Client("client1");

            client.OpenCumulativeAccount(1000M);

            var account = client.FindAccount(client.GetListOfAccounts(AccountType.CumulativeAccount).First().AccountNumber);
            client.ZeroingAccount(account);

            Assert.AreEqual(0M, client.TotalBalance);
        }
        [TestMethod]
        public void NegativeTestZeroingAccount()
        {
            var client = new Client("client1");

            client.OpenSavingsAccount(0M);

            var account = client.FindAccount(client.GetListOfAccounts(AccountType.SavingsAccount).First().AccountNumber);
            client.CloseAccount(account);
            try
            {
                client.ZeroingAccount(account);
                Assert.Fail();
            }
            catch (Exception) { }
            Assert.AreEqual(0M, client.TotalBalance);
        }
        [TestMethod]
        public void TestCloseAccount()
        {
            var client = new Client("client1");

            client.OpenSavingsAccount(0M);

            var account = client.FindAccount(client.GetListOfAccounts(AccountType.SavingsAccount).First().AccountNumber);
            client.CloseAccount(account);

            Assert.AreEqual(AccountStatus.Closed, account.Status);
        }
        [TestMethod]
        public void NegativeTestCloseAccount()
        {
            var client = new Client("client1");

            client.OpenSavingsAccount(1000M);

            var account = client.FindAccount(client.GetListOfAccounts(AccountType.SavingsAccount).First().AccountNumber);
            try
            {
                client.CloseAccount(account);
                Assert.Fail();
            }
            catch (Exception) { }
            Assert.AreEqual(AccountStatus.Opened, account.Status);

            client.ZeroingAccount(account);
            client.CloseAccount(account);
            try
            {
                client.CloseAccount(account);
                Assert.Fail();
            }
            catch (Exception) { }
            Assert.AreEqual(AccountStatus.Closed, account.Status);
        }
        [TestMethod]
        public void TestZeroingAndCloseAllAccounts()
        {
            var client = new Client("client1");

            client.OpenSavingsAccount(1000M);
            client.OpenSavingsAccount(0M);
            client.OpenCumulativeAccount(1000M);
            client.OpenCumulativeAccount(0M);
            client.OpenCheckingAccount(1000M);
            client.OpenCheckingAccount(0M);
            client.OpenMetalAccount(MetalType.Argentum, 100, 500M);
            client.OpenMetalAccount(MetalType.Aurum, 100, 1500M);
            client.OpenMetalAccount(MetalType.Platinum, 100, 2500M);

            client.ZeroingAndCloseAllAccounts();
            Assert.AreEqual((uint)0, client.CountOfOpenedAccounts);
            Assert.AreEqual(0M, client.TotalBalance);
        }
        [TestMethod]
        public void NegativeTestZeroingAndCloseAllAccounts()
        {
            var client = new Client("client1");

            client.OpenSavingsAccount(1000M);
            client.OpenSavingsAccount(0M);
            client.OpenCumulativeAccount(1000M);
            client.OpenCumulativeAccount(0M);
            client.OpenCheckingAccount(1000M);
            client.OpenCheckingAccount(0M);
            client.OpenMetalAccount(MetalType.Argentum, 100, 500M);
            client.OpenMetalAccount(MetalType.Aurum, 100, 1500M);
            client.OpenMetalAccount(MetalType.Platinum, 100, 2500M);

            client.ZeroingAndCloseAllAccounts();
            
            try
            {
                client.ZeroingAndCloseAllAccounts();
                Assert.Fail();
            }
            catch (Exception) { }
        }
        [TestMethod]
        public void TestCompareTo()
        {
            var client1 = new Client("client1");

            client1.OpenSavingsAccount(1000M);
            client1.OpenCumulativeAccount(1000M);
            client1.OpenCheckingAccount(1000M);
            client1.OpenMetalAccount(MetalType.Argentum, 100, 500M);

            var client2 = new Client("client2");

            client2.OpenSavingsAccount(1000M);
            client2.OpenCumulativeAccount(1000M);
            client2.OpenCheckingAccount(1000M);
            client2.OpenMetalAccount(MetalType.Argentum, 100, 500M);

            var result1 = client1.CompareTo(client2);

            var account = client2.FindAccount(client2.GetListOfAccounts(AccountType.SavingsAccount).First().AccountNumber);
            client2.ReplenishAccount(account, 1000M);

            var result2 = client1.CompareTo(client2);
                        
            client2.WithdrawFundsFromAccount(account, 2000M);

            var result3 = client1.CompareTo(client2);

            Client client3 = null;

            var result4 = client1.CompareTo(client3);

            Assert.AreEqual(0, result1);
            Assert.AreEqual(-1, result2);
            Assert.AreEqual(1, result3);
            Assert.AreEqual(1, result4);
        }
        [TestMethod]
        public void NegativeTestCompareTo()
        {
            var client1 = new Client("client1");

            client1.OpenSavingsAccount(1000M);
            client1.OpenCumulativeAccount(1000M);
            client1.OpenCheckingAccount(1000M);
            client1.OpenMetalAccount(MetalType.Argentum, 100, 500M);

            var client2 = new List<int>();

            try
            {
                var result = client1.CompareTo(client2);
                Assert.Fail();
            }
            catch (InvalidOperationException) { }
       
        }
    }
}
