namespace ConsoleLib
{
    public interface IAsker<out TOptionsType, out TResponseType>
    {
        TOptionsType Parameters { get; }
        string Question { get; }
        TResponseType Draw();
    }
}