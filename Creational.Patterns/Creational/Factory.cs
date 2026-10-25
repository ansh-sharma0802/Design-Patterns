namespace Creational
{
    interface INotification
    {
        void Send(string message);
    }

    class EmailNotification : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine($"Sending Email: {message}");
        }
    }

    class SmsNotification : INotification
    {
        public void Send(string message)
        {
            Console.WriteLine($"Sending Sms {message}");
        }
    }

    abstract class NotificationCreator
    {
        public abstract INotification CreateNotification();

        public void Send(string message)
        {
            INotification notification = CreateNotification();
            notification.Send(message);
        }
    }

    class EmailNotificationCreator : NotificationCreator
    {
        public override INotification CreateNotification() => new EmailNotification();
    }

    class SmsNotificationCreator : NotificationCreator
    {
        public override INotification CreateNotification() => new SmsNotification();
    }

    //class Factory
    //{
    //    static void Main()
    //    {
    //        NotificationCreator creator;

    //        creator = new EmailNotificationCreator();
    //        creator.Send("Welcome");

    //        creator = new SmsNotificationCreator();
    //        creator.Send("OTP is 1234");
    //    }
    //}
}
