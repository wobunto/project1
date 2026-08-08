using System.Runtime.CompilerServices;

namespace Pokemongame
{
    /*
    public class EnemyActor : BattleActor
    {
        public EnemyActor(IBattleParticipant pokemon)
            : base(pokemon)
        {
        }

        public override BattleAction SelectAction()
        {
            MoveRuntime move = SelectMove();

            return new AttackAction(this, move); 
        }   

        public MoveRuntime SelectMove()
        {    
            int firstEmptyindex = Pokemon.GetFirstEmptyIndex();

            if (firstEmptyindex == 0)
            throw new InvalidOperationException("[적 포켓몬]이 사용할 기술이 없습니다.");

            var validMoves = Pokemon.CurrentMoves;

            Random rand = new Random();
            int index = rand.Next(firstEmptyindex == -1 ? 4 : firstEmptyindex);
            
            return validMoves[index]!;
        }
    }
    */
}