using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw_1_2
{
    public sealed class CumulativeAccount : BankAccount
    {
        private decimal _initialBalance = 10000.00M;
        private decimal _rate = 2.5M; //%

        public decimal InitBalance => _initialBalance;
        public decimal InterestsRate => _rate;

        private CumulativeAccount() : base() { }

        public CumulativeAccount(string clientAccount, string clientName) : this(clientAccount, clientName, 0) { }

        public CumulativeAccount(string clientAccount, string clientName, decimal initAmount) : base(clientAccount, clientName)
        {
            try
            {
                if (initAmount < 0)
                    throw new InvalidOperationException("Начальная сумма не может быть отрицательной!");
                if (initAmount > 0) _initialBalance = initAmount;
                this.IncreaseBalance(_initialBalance);
                if (initAmount > 10000.00M && initAmount <= 100000.00M)
                    _rate = 3.5M;
                else if (initAmount > 100000.00M)
                    _rate = 4.5M;
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
                return (this.AccountBalance - amount) >= _initialBalance &&
                      this.DecreaseBalance(amount);
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
                return this.DecreaseBalance(this.AccountBalance);
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

        public bool InterestsCapitalization()
        {
            try
            {
                return this.AccountBalance > 0 &&
                     this.IncreaseBalance(this.AccountBalance / 36000 * _rate * 30);
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
            var str = string.Format("Клиент:{0}\n, накопительный счет:{1}, остаток:{2:0.00} д.е.,\n", this.AccountOwner, this.AccountNumber, this.AccountBalance);
            str += string.Format("неснижаемый остаток:{0:0.00} д.е., процентная ставка:{1:0.00}%,\n", _initialBalance, _rate);
            str += string.Format("статус:{0}", this.Status.ToString());
            return str;
        }
    }
}
