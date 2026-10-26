namespace Creational
{
    interface IButton
    {
        void Paint();
        void OnClick();
    }

    interface ICheckbox
    {
        void Paint();
        void OnCheck();
    }

    class WindowsButton : IButton
    {
        public void Paint()
            => Console.WriteLine("Painting button for windows");

        public void OnClick()
            => Console.WriteLine("Windows button clicked");
    }

    class WindowsCheckbox : ICheckbox
    {
        public void Paint()
            => Console.WriteLine("Painting checkbox for windows");

        public void OnCheck()
            => Console.WriteLine("Windows checkbox checked");
    }

    class MacButton : IButton
    {
        public void Paint()
            => Console.WriteLine("Painting button for mac");

        public void OnClick()
            => Console.WriteLine("Mac button clicked");
    }

    class MacCheckbox : ICheckbox
    {
        public void Paint()
            => Console.WriteLine("Painting checkbox for mac");

        public void OnCheck()
            => Console.WriteLine("Mac checkbox checked");
    }

    //interface IGuiFactory
    //{
    //    IButton CreateButton();
    //    ICheckbox CreateCheckbox();
    //}

    abstract class GuiFactory
    {
        public abstract IButton CreateButton();
        public abstract ICheckbox CreateCheckbox();
    }

    class WindowsGuiFactory : GuiFactory
    {
        public override IButton CreateButton()
            => new WindowsButton();

        public override ICheckbox CreateCheckbox()
            => new WindowsCheckbox();
    }

    class MacGuiFactory : GuiFactory
    {
        public override IButton CreateButton()
            => new MacButton();

        public override ICheckbox CreateCheckbox()
            => new MacCheckbox();
    }

    class Application
    {
        private readonly IButton _button;
        private readonly ICheckbox _checkbox;

        internal Application(GuiFactory factory)
        {
            _button = factory.CreateButton();
            _checkbox = factory.CreateCheckbox();
        }

        public void RenderUI()
        {
            _button.Paint();
            _button.OnClick();

            _checkbox.Paint();
            _checkbox.OnCheck();
        }
    }

    internal class AbstractFactory
    {
        //static void Main()
        //{
        //    string os = Environment.OSVersion.Platform.ToString();
        //    GuiFactory factory;

        //    if (os.Contains("Win"))
        //    {
        //        factory = new WindowsGuiFactory();
        //    }
        //    else
        //    {
        //        factory = new MacGuiFactory();
        //    }

        //    Application application = new(factory);
        //    application.RenderUI();
        //}
    }
}
