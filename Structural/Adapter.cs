namespace Structural
{
    public interface IPaymentProcessor
    {
        void ProcessPayement(double amount, string currency);
        bool IsPaymentSuccessful();
        string? GetTransactionId();
    }

    public class InHousePaymentProcessor : IPaymentProcessor
    {
        private string? _transactionId = null;
        private bool _paymentSuccessful;

        public string? GetTransactionId()
            => _transactionId;

        public bool IsPaymentSuccessful()
            => _paymentSuccessful;

        public void ProcessPayement(double amount, string currency)
        {
            Console.WriteLine($"InHouseProcessor: Processing {amount} {currency}");
            _transactionId = "TXN_" + DateTimeOffset.Now.ToUnixTimeMilliseconds();
            _paymentSuccessful = true;
            Console.WriteLine($"InHouseProcessor: Success. Txn ID: {_transactionId}");

            
        }
    }

    public class CheckoutService
    {
        private readonly IPaymentProcessor _paymentProcessor;

        public CheckoutService(IPaymentProcessor paymentProcessor)
        {
            _paymentProcessor = paymentProcessor;
        }

        public void Checkout(double amount, string currency)
        {
            Console.WriteLine($"Processing order for {amount} {currency}");
            _paymentProcessor.ProcessPayement(amount, currency);

            if (_paymentProcessor.IsPaymentSuccessful())
            {
                Console.WriteLine($"Checkout: Order successful! Txn: {_paymentProcessor.GetTransactionId()}");
                return;
            }

            Console.WriteLine("Checkout: Order failed");
        }
    }

    public class LegacyGateway
    {
        private long transactionReference;
        private bool paymentSuccessful;

        public void ExecuteTransaction(double totalAmount, string currency)
        {
            Console.WriteLine($"LegacyGateway: Executing {currency} {totalAmount}");
            transactionReference = DateTimeOffset.Now.Ticks;
            paymentSuccessful = true;
            Console.WriteLine($"LegacyGateway: Done. Ref: {transactionReference}");
        }

        public bool CheckStatus(long reference)
        {
            Console.WriteLine($"LegacyGateway: Checking status for ref: {reference}");
            return paymentSuccessful;
        }

        public long GetReferenceNumber() => transactionReference;
    }

    public class LegacyGatewayAdapter : IPaymentProcessor
    {
        private readonly LegacyGateway _legacyGateway;
        private long _currentRef = 0;

        public LegacyGatewayAdapter(LegacyGateway legacyGateway)
        {
            _legacyGateway = legacyGateway;
        }

        public string? GetTransactionId()
            => "LEGACY_TXN_" + _currentRef;

        public bool IsPaymentSuccessful()
            => _legacyGateway.CheckStatus(_currentRef);

        public void ProcessPayement(double amount, string currency)
        {
            Console.WriteLine($"Adapter: Translating processPayment() for {amount} {currency}");
            
            _legacyGateway.ExecuteTransaction(amount, currency);
            _currentRef = _legacyGateway.GetReferenceNumber();
        }
    }

    //internal class Adapter
    //{
    //    static void Main()
    //    {
    //        IPaymentProcessor processor = new InHousePaymentProcessor();
    //        CheckoutService modernCheckout = new CheckoutService(processor);
    //        Console.WriteLine("--- Using Modern Processor ---");
    //        modernCheckout.Checkout(199.99, "USD");

    //        // Legacy gateway through adapter
    //        Console.WriteLine("\n--- Using Legacy Gateway via Adapter ---");
    //        LegacyGateway legacy = new();
    //        processor = new LegacyGatewayAdapter(legacy);
    //        CheckoutService legacyCheckout = new(processor);
    //        legacyCheckout.Checkout(75.50, "USD");
    //    }
    //}
}