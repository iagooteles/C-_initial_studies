
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using Microsoft.VisualBasic;

ILogger logger = new FileLogger("myPath");
BankAccount account1 = new BankAccount("iago", 150);
BankAccount account2 = new BankAccount("bilas", 3000);

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
// Console.WriteLine(store.Value);

var calculate = new Calculate(Sum);
var result = calculate(10, 20);
// Console.WriteLine(result);

// Serializer //

BankAccount account3 = new BankAccount("Pan", 500);
string json = JsonSerializer.Serialize(account3);
Console.WriteLine(json);

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
BankAccount copiedBank = JsonSerializer.Deserialize<BankAccount>(json);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
Console.WriteLine("Copy: " + json);


///////////////////////////////////////////////////////
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

    public string Name 
    {
        get { return name; }
        private set { name = value; }
    }

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
        this.logger = logger;
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