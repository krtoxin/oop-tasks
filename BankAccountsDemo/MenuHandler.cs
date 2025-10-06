using System;

public class MenuHandler
{
    public void ShowMainMenu()
    {
        IBankAccount account = null;
        while (account == null)
        {
            Console.WriteLine("Оберіть тип рахунку:");
            foreach (var type in Enum.GetValues(typeof(AccountType)))
            {
                Console.WriteLine($"{(int)type}. {type}");
            }
            Console.Write("Ваш вибір (номер або q для виходу): ");
            string choice = Console.ReadLine();
            if (choice == "q") return;

            if (int.TryParse(choice, out int typeNum) && Enum.IsDefined(typeof(AccountType), typeNum))
            {
                Console.Write("Введіть номер рахунку: ");
                string accNum = Console.ReadLine();
                Console.Write("Введіть ім'я власника: ");
                string owner = Console.ReadLine();

                var selectedType = (AccountType)typeNum;
                switch (selectedType)
                {
                    case AccountType.Deposit:
                        Console.Write("Введіть відсоткову ставку: ");
                        decimal rate = decimal.Parse(Console.ReadLine());
                        account = new DepositAccount(accNum, owner, rate);
                        break;
                    case AccountType.Current:
                        account = new CurrentAccount(accNum, owner);
                        break;
                }
            }
            else
            {
                Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
            }
        }
        ShowAccountMenu(account);
    }

    public void ShowAccountMenu(IBankAccount account)
    {
        while (true)
        {
            Console.WriteLine("\nОберіть операцію:");
            foreach (var op in Enum.GetValues(typeof(OperationType)))
            {
                if (op.Equals(OperationType.ApplyInterestOrSetCredit))
                {
                    if (account is DepositAccount)
                        Console.WriteLine($"{(int)op}. Нарахувати відсотки");
                    else if (account is CurrentAccount)
                        Console.WriteLine($"{(int)op}. Встановити кредитний ліміт");
                }
                else if(op.Equals(OperationType.Exit))
                {
                    Console.WriteLine("q. Вийти в головне меню");
                }
                else
                {
                    Console.WriteLine($"{(int)op}. {op}");
                }
            }
            Console.Write("Ваш вибір: ");
            string opChoice = Console.ReadLine();
            if (opChoice == "q") break;

            if (int.TryParse(opChoice, out int opNum) && Enum.IsDefined(typeof(OperationType), opNum))
            {
                var selectedOp = (OperationType)opNum;
                switch (selectedOp)
                {
                    case OperationType.Deposit:
                        Console.Write("Введіть суму для депозиту: ");
                        decimal dep = decimal.Parse(Console.ReadLine());
                        account.Deposit(dep);
                        break;
                    case OperationType.Withdraw:
                        Console.Write("Введіть суму для зняття: ");
                        decimal wd = decimal.Parse(Console.ReadLine());
                        if (!account.Withdraw(wd))
                            Console.WriteLine("Недостатньо коштів!");
                        break;
                    case OperationType.ShowBalance:
                        account.DisplayBalance();
                        break;
                    case OperationType.ApplyInterestOrSetCredit:
                        if (account is DepositAccount da)
                            da.ApplyInterest();
                        else if (account is CurrentAccount ca)
                        {
                            Console.Write("Новий ліміт: ");
                            decimal lim = decimal.Parse(Console.ReadLine());
                            ca.SetCreditLimit(lim);
                        }
                        break;
                    case OperationType.Exit:
                        return;
                }
            }
            else if (opChoice != "q")
            {
                Console.WriteLine("Невірна операція.");
            }
        }
    }
}