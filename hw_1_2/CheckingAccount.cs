using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw_1_2
{
    public sealed class CheckingAccount : BankAccount
    {
        private decimal _fee = 100.00M;

        public decimal ServiceFee => _fee;

        private CheckingAccount() : base() { }

        public CheckingAccount(string clientAccount, string clientName) : base(clientAccount, clientName) { }

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
                return (amount<=0 ||
                       ((this.AccountBalance - amount) >= _fee &&
                         this.WithdrawFee())) &&
                         this.DecreaseBalance(amount);
            }
            catch(InvalidOperationException)
            {
                throw;
            }
            catch(Exception)
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

        private bool WithdrawFee()
        {
            try
            {
                return this.DecreaseBalance(_fee);
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

        public override string ToString()
        {
            var str = string.Format("Клиент:{0}\n, расчетный счет:{1}, остаток:{2:0.00} д.е.,\n", this.AccountOwner, this.AccountNumber, this.AccountBalance);
            str += string.Format("стоимость платежа:{0:0.00} д.е.,\n", _fee);
            str += string.Format("статус:{0}", this.Status.ToString());
            return str;
        }
    }
}
