using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw_1_2
{
    public sealed class MetalAccount : BankAccount
    {
        private MetalType _type;
        private long _metalBalance=0;
        private decimal _priceRate;

        public MetalType Metal=>_type;
        public long MetalBalance => _metalBalance;
        public decimal PriceRate => _priceRate;

        private MetalAccount() : base() { }

        public MetalAccount(string clientAccount, string clientName, MetalType metal, uint weight, decimal price) : base(clientAccount, clientName)
        {
            try
            {
                _type = metal;
                _metalBalance = weight;
                if (price <= 0)
                    throw new InvalidOperationException("Курс не может быть отрицательным или нулевым!");
                _priceRate = price;
                this.IncreaseBalance((decimal)(_metalBalance * _priceRate));
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
                if (amount < 0 || (amount > 0 && amount < _priceRate))
                    throw new InvalidOperationException("Сумма пополнения не может быть отрицательной или меньше курсовой ставки!");
                return AddFundsInMetal((uint)Math.Truncate(amount / _priceRate));
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

        public bool AddFundsInMetal(uint weight)
        {
            try
            {
                if (this.IncreaseBalance((decimal)(weight * _priceRate)))
                {
                    _metalBalance += weight;
                    return true;
                }
                else
                    return false;
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
                if (amount < 0 || (amount > 0 && amount < _priceRate))
                    throw new InvalidOperationException("Сумма списания не может быть отрицательной или меньше курсовой ставки!");
                return WithdrawFundsInMetal((uint)Math.Truncate(amount / _priceRate));
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

        public bool WithdrawFundsInMetal(uint weight)
        {
            try
            {
                if (this.DecreaseBalance((decimal)(weight * _priceRate)))
                {
                    _metalBalance -= weight;
                    return true;
                }
                else
                    return false;
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
                if (this.DecreaseBalance(AccountBalance))
                {
                    _metalBalance = 0;
                    return true;
                }
                else
                    return false;
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
            var str = string.Format("Клиент:{0}\n, металлический счет:{1}, остаток:{2:0.00} д.е.,\n", this.AccountOwner, this.AccountNumber, this.AccountBalance);
            str += string.Format("металл:{0}, остаток:{1:0} г., курс:{2:0.00} д.е./г.\n", _type.ToString(), _metalBalance, _priceRate);
            str += string.Format("статус:{0}", this.Status.ToString());
            return str;
        }
    }
}
