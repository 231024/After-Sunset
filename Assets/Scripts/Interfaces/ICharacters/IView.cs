public interface IView
{
    string FirstKeyText { get; }
    string FirstText { get; }
    void Display(string nameOfKey, int numbers, string message);
}