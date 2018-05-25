using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw_1_2
{
    public sealed class SavingsAccount : BankAccount
    {
        private SavingsAccount() : base() { }
        public SavingsAccount(string clientAccount, string clientName) : base(clientAccount, clientName) { }

        public override bool AddFunds(decimal amount)
        {
            try
            {
                return this.IncreaseBalance(amount);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new Exception("Произошла какая-то ошибка!");
            }
        }

        public override bool WithdrawFunds(decimal amount)
        {
            try
            {
                return this.DecreaseBalance(amount);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new Exception("Произошла какая-то ошибка!");
            }
        }

        public override bool ZeroingAccount()
        {
            try
            {
                return this.DecreaseBalance(AccountBalance);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new Exception("Произошла какая-то ошибка!");
            }
        }

        public override bool CloseAccount()
        {
            return this.Close();
        }

        public override string ToString()
        {
            var str = string.Format("Клиент:{0}\n, сберегательный счет:{1}, остаток:{2:0.00} д.е.\n", this.AccountOwner, this.AccountNumber, this.AccountBalance);
            str += string.Format("статус:{0}", this.Status.ToString());
            return str;
        }
    }
}
