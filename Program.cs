
BankAccount account1 = new BankAccount("iago", 150);
BankAccount account2 = new BankAccount("bilas", 3000);

Console.WriteLine("Saldo antes do deposito: " + account1.Balance);
account1.Deposit(300);
Console.WriteLine("Saldo depois do deposito: " + account2.Balance);

class ConsoleLogger
{
    
}

interface ILogger
{
    void Log(string message);
}

class BankAccount 
{
    private string name;
    private decimal balance;

    public decimal Balance 
    { 
        get { return balance; }
    }

    public BankAccount(string name, decimal balance) {
        if (string.IsNullOrWhiteSpace(name)) 
        {
            throw new Exception("Nome invalido.");
        }

        if (balance < 0)
        {
            throw new Exception("Saldo não pode ser negativo.");
        }

        this.name = name;
        this.balance = balance;
    }

    public string GetName()
    {
        return name;
    }

    public void SetName(string value)
    {
        name = value;
    }

    public void Deposit(decimal amount)
    {
        balance += amount;
    }

}