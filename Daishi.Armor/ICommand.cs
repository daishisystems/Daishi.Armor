namespace Daishi.Armor {
    public interface ICommand {
        object Result { get; }
        void Execute();
        void Undo();
    }
}