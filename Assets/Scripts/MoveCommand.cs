namespace TicTacToe
{
    public class MoveCommand : ICommand
    {
        private Cell cell;
        private string player;

        public MoveCommand(Cell cell, string player)
        {
            this.cell = cell;
            this.player = player;
        }
        public void Execute()
        {
            cell.SetMark(player);
        }

        public void Undo()
        {
            cell.Clear();
            
        }
    }
}