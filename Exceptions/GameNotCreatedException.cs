namespace apiGames.Exceptions
{
    public class GameNotCreatedException : Excepetion
    {
        public GameNotCreatedException()
            : base("Este jogo não está cadastrado")
        { }
    }
}