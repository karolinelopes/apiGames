namespace apiGames.Exceptions
{
    public class GameCreateadException : Exception
    {
        public GameCreateadException()
            : base("Este já jogo está cadastrado")
        { }
    }
}