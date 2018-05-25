using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw_1_2
{
    public abstract class BankAccount
    {
        private string _number;
        private string _owner;
        private decimal _balance;
        private AccountStatus _status;

        protected BankAccount()
        {
            _number = "";
            _owner = "";
            _balance = 0.00M;
            _status = AccountStatus.Opened;
        }

        protected BankAccount(string number, string owner) : this()
        {
            _number = number;
            _owner = owner;
        }

        public string AccountNumber
        {
            get { return _number; }
            set { if (_status == AccountStatus.Opened && 
                       (string.IsNullOrEmpty(_number) || 
                        string.IsNullOrWhiteSpace(_number)))
                             _number = value; }
        }

        public string AccountOwner
        {
            get { return _owner; }
            set { if (_status == AccountStatus.Opened && 
                       (string.IsNullOrEmpty(_owner) ||
                        string.IsNullOrWhiteSpace(_owner)))
                            _owner = value; }
        }

        public decimal AccountBalance => _balance;

        public AccountStatus Status => _status;

        protected bool IncreaseBalance(decimal delta)
        {
            try
            {
                if (_status == AccountStatus.Opened && delta >= 0)
                {
                    _balance += Math.Truncate(delta * 100M) / 100M;
                    return true;
                }
                else
                    return false;
            }
            catch(Exception)
            {
                throw new InvalidOperationException("Недопустимая операция, счет закрыт или ошибка в расчетах!");
            }
            
        }

        protected bool DecreaseBalance(decimal delta)
        {
            try
            {
                if (_status == AccountStatus.Opened && delta >= 0 && (_balance - delta) >= 0)
                {
                    _balance -= Math.Truncate(delta * 100M) / 100M;
                    return true;
                }
                else
                    return false;
            }
            catch(Exception)
            {
                throw new InvalidOperationException("Недопустимая операция, счет закрыт или ошибка в расчетах!");
            }
        }

        protected bool Close()
        {
            if (_status == AccountStatus.Opened && _balance == 0)
            {
                _status = AccountStatus.Closed;
                return true;
            }
            else
                return false;
        }

        public abstract bool AddFunds(decimal amount);
        public abstract bool WithdrawFunds(decimal amount);
        public abstract bool ZeroingAccount();
        public abstract bool CloseAccount();
    }
}
