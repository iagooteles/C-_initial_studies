
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;

ILogger logger = new FileLogger("myPath");
BankAccount account1 = new BankAccount("iago", 150, logger);
BankAccount account2 = new BankAccount("bilas", 3000, logger);

// Console.WriteLine("Saldo antes do deposito: " + account1.Balance);
// account1.Deposit(300);
// account1.Deposit(-300);
// Console.WriteLine("Saldo depois do deposito: " + account2.Balance);

List<BankAccount> accounts = new List<BankAccount>();
accounts.Add(account1);
accounts.Add(account2);

// foreach (var account in accounts) 
// {
//     Console.WriteLine(account.GetName());
// }

DataStore<int> store = new DataStore<int>();
store.Value = 32;
Console.WriteLine(store.Value);

var calculate = new Calculate(Sum);
var result = calculate(10, 20);
Console.WriteLine(result);

static int Sum(int a, int b) 
{
    return a + b;
}

delegate int Calculate(int x, int y);

class DataStore<T>
{
    public T Value { get; set; }
}

class FileLogger : ILogger
{

    private readonly string filePath;
    public FileLogger(string filePath) 
    {
        this.filePath = filePath;
    }

    public void Log(string message)
    {
        File.AppendAllText(filePath + ".txt", $"{message}{Environment.NewLine}");
    }
}

class ConsoleLogger : ILogger
{
}

interface ILogger
{
    void Log(string message)
    {
        Console.WriteLine($"LOGGER: {message}");
    }
}

class BankAccount 
{
    private string name;
    private decimal balance;
    private readonly ILogger logger;

    public decimal Balance 
    { 
        get { return balance; }
    }

    public BankAccount(string name, decimal balance, ILogger logger) {
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
        this.logger = logger;
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
        if (amount <= 0) 
        {
            logger.Log($"Não é possível depositar valor menor ou igual a 0.");
            return;
        }
        balance += amount;
    }
}