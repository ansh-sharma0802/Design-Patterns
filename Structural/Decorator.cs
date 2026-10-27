namespace Structural
{
    interface ITextView
    {
        void Render();
    }

    class PlainTextView : ITextView
    {
        private readonly string _text;

        public PlainTextView(string text)
        {
            _text = text;
        }

        public void Render()
        {
            Console.WriteLine(_text);
        }
    }

    abstract class TextDecorator : ITextView
    {
        protected readonly ITextView _inner;

        public TextDecorator(ITextView inner)
        {
            _inner = inner;
        }

        public abstract void Render();
    }

    class BoldDecorator : TextDecorator
    {
        public BoldDecorator(ITextView inner) : base(inner)
        {

        }

        public override void Render()
        {
            Console.WriteLine("<b>");
            _inner.Render();
            Console.WriteLine("</b>");
        }
    }

    class ItalicDecorator : TextDecorator
    {
        public ItalicDecorator(ITextView inner) : base(inner)
        {

        }

        public override void Render()
        {
            Console.WriteLine("<i>");
            _inner.Render();
            Console.WriteLine("</i>");
        }
    }

    class UnderlineDecorator : TextDecorator
    {
        public UnderlineDecorator(ITextView inner) : base(inner)
        {

        }

        public override void Render()
        {
            Console.WriteLine("<u>");
            _inner.Render();
            Console.WriteLine("</u>");
        }
    }

    class Decorator
    {
        static void Main()
        {
            ITextView text = new PlainTextView("Hello, World!");

            // Plain text
            Console.Write("Plain:                   ");
            text.Render();
            Console.WriteLine();

            // Single decorator: Bold
            Console.Write("Bold:                    ");
            ITextView boldText = new BoldDecorator(text);
            boldText.Render();
            Console.WriteLine();

            // Two decorators: Italic + Underline
            Console.Write("Italic + Underline:      ");
            ITextView italicUnderline = new UnderlineDecorator(new ItalicDecorator(text));
            italicUnderline.Render();
            Console.WriteLine();

            // Three decorators: Bold + Italic + Underline
            Console.Write("Bold + Italic + Underline: ");
            ITextView allStyles = new UnderlineDecorator(
                new ItalicDecorator(new BoldDecorator(text)));
            allStyles.Render();
            Console.WriteLine();
        }
    }
}
