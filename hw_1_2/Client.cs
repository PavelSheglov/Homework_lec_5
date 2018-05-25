using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw_1_2
{
    public sealed class Client : IComparable
    {
        private string _name;
        private uint _number;
        private List<BankAccount> _accounts;
        private uint _openedAccountsCount;


        public string Name => _name;

        public uint Number => _number;

        public uint CountOfOpenedAccounts => _openedAccountsCount;

        public decimal TotalBalance => CalculateTotalBalance();
        

        private Client() { }

        public Client(string name)
        {
            _name = name;
            _number = (uint)(name.GetHashCode() + DateTime.Now.Second);
            _accounts = new List<BankAccount>();
            _openedAccountsCount = 0;
        }
        

        public void OpenSavingsAccount(decimal initAmount)
        {
            try
            {
                if (initAmount < 0)
                    throw new InvalidOperationException("Первоначальная сумма не может быть отрицательной!");
                var account = "ACC" + (int)AccountType.SavingsAccount + (_openedAccountsCount + 1).ToString() + "000" + _number.ToString();
                var newAccount = new SavingsAccount(account, _name);
                newAccount.AddFunds(initAmount);
                _accounts.Add(newAccount);
                _openedAccountsCount++;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void OpenCumulativeAccount(decimal initAmount)
        {
            try
            {
                if (initAmount < 0)
                    throw new InvalidOperationException("Первоначальная сумма не может быть отрицательной!");
                var account = "ACC" + (int)AccountType.CumulativeAccount + (_openedAccountsCount + 1).ToString() + "000" + _number.ToString();
                var newAccount = new CumulativeAccount(account, _name, initAmount);
                _accounts.Add(newAccount);
                _openedAccountsCount++;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void OpenCheckingAccount(decimal initAmount)
        {
            try
            {
                if (initAmount < 0)
                    throw new InvalidOperationException("Первоначальная сумма не может быть отрицательной!");
                var account = "ACC" + (int)AccountType.CheckingAccount + (_openedAccountsCount + 1).ToString() + "000" + _number.ToString();
                var newAccount = new CheckingAccount(account, _name);
                newAccount.AddFunds(initAmount);
                _accounts.Add(newAccount);
                _openedAccountsCount++;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void OpenMetalAccount(MetalType type, uint initAmount, decimal price)
        {
            try
            {
                if (price <= 0)
                    throw new InvalidOperationException("Курс не может быть отрицательным или нулевым!");
                var account = "ACC" + (int)AccountType.MetalAccount + (_openedAccountsCount + 1).ToString() + (int)type + _number.ToString();
                var newAccount = new MetalAccount(account, _name, type, initAmount, price);
                _accounts.Add(newAccount);
                _openedAccountsCount++;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
                
        public List<BankAccount> GetListOfAccounts()
        {
            return _accounts;
        }

        public List<BankAccount> GetListOfAccounts(AccountType type)
        {
            return _accounts.FindAll(account => account.GetType().Name.ToString() == type.ToString());
        }

        public BankAccount FindAccount(string number)
        {
            return _accounts.FirstOrDefault(account => account.AccountNumber == number);
        }

        public void ReplenishAccount(BankAccount account, decimal amount)
        {
            try
            {
                if (!account.AddFunds(amount))
                    throw new InvalidOperationException("Пополнение счета не может быть проведено!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ReplenishMetalAccount(BankAccount account, uint weight)
        {
            try
            {
                if (!((account is MetalAccount) && (account as MetalAccount).AddFundsInMetal(weight)))
                    throw new InvalidOperationException("Пополнение счета не может быть проведено!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void WithdrawFundsFromAccount(BankAccount account, decimal amount)
        {
            try
            {
                if (!account.WithdrawFunds(amount))
                    throw new InvalidOperationException("Списание со счета не может быть проведено!");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void WithdrawFundsFromMetalAccount(BankAccount account, uint weight)
        {
            try
            {
                if (!((account is MetalAccount) && (account as MetalAccount).WithdrawFundsInMetal(weight)))
                    throw new InvalidOperationException("Списание со счета не может быть проведено!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void CapitalizeInterestsOnAccount(BankAccount account)
        {
            try
            {
                if (!((account is CumulativeAccount) && (account as CumulativeAccount).InterestsCapitalization()))
                    throw new InvalidOperationException("Капитализация процентов не может быть проведена!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ZeroingAccount(BankAccount account)
        {
            try
            {
                if (!account.ZeroingAccount())
                    throw new InvalidOperationException("Обнуление счета не может быть проведено!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void CloseAccount(BankAccount account)
        {
            try
            {
                if (!account.CloseAccount())
                    throw new InvalidOperationException("Закрытие счета не может быть проведено!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void ZeroingAndCloseAllAccounts()
        {
            try
            {
                if (_accounts.Count > 0)
                {
                    foreach (var acc in _accounts)
                    {
                        if (acc.ZeroingAccount() && acc.CloseAccount())
                            _openedAccountsCount--;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private decimal CalculateTotalBalance()
        {
            decimal total = 0;
            if (_openedAccountsCount > 0)
            {
                foreach (var acc in _accounts)
                    total += acc.AccountBalance;
            }
            return total;
        }
        
        public int CompareTo(object obj)
        {
            try
            {
                if (!(obj is Client))
                    throw new InvalidOperationException("Объект для сравнения не является Клиентом!!!");
                if (obj == null)
                    return 1;
                var temp = obj as Client;
                if (this.TotalBalance > temp.TotalBalance)
                    return 1;
                else if (this.TotalBalance < temp.TotalBalance)
                    return -1;
                else
                    return 0;
            }
            catch(InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
        }

        public override string ToString()
        {
            return string.Format("Клиент:{0},\nоткрытых счетов {1}, суммарный остаток на всех счетах {2:#.00}", _name, _openedAccountsCount, TotalBalance);
        }
    }
}
