namespace Pokemongame
{
    public class BattleSystem
    {
        public void StartBattle(BattleActor player, BattleActor enemy)
        {
            GameLog.LogBattleStart();

            while(!player.Pokemon.IsFainted && !enemy.Pokemon.IsFainted) 
            //둘 중 하나가 기절하면 종료
            {
                GameLog.LogCurrentStat(player.Pokemon,enemy.Pokemon);

                BattleAction PlayerAction = player.SelectAction();
                BattleAction EnemyAction = enemy.SelectAction(); 

                ExecuteTurn(PlayerAction,EnemyAction);

                Console.WriteLine("-----------------------------------");
            }
        }
        public void ExecuteTurn(BattleAction first, BattleAction second)
        { 
            if(!Calculator.IsFaster(first.Actor.Pokemon,second.Actor.Pokemon))
            {
                (first, second) = (second,first);      
            }  
            ExecuteAction(first,second);

            if(second.Actor.Pokemon.IsFainted)
            {
                second.Actor.Pokemon.LogFaint();
                return;
            }   
            ExecuteAction(second,first);
        }
        public void ExecuteAction(BattleAction action,BattleAction deffender)
        {
            switch(action)
            {
                case AttackAction attack:
                    ExecuteAttack(attack.Actor.Pokemon,deffender.Actor.Pokemon,attack.Move);
                    break;


                case SwitchAction sw:
                    Console.WriteLine("아직 미완성 ㅎㅎ;");
                    break;


                case ItemAction item:
                    Console.WriteLine("아직 미완성 ㅎㅎ;");
                    break;
            }
        }
        public bool ExecuteAttack(CharacterComponent attacker, CharacterComponent deffender,MoveRuntime move)
        {
            // 내 공격력, 기술의 타입, 상대방의 타입을 가져와서 배틀 시스템 공식 적용
            
            float finalMultiplier = attacker.CalculateMultiplier(deffender,move.Data.Type);
            int damage = attacker.CalculateDamage(finalMultiplier);
            
            deffender.Runtime.TakeDamage(damage);

            attacker.LogFinal(deffender,damage,finalMultiplier);
            
            return false;
            }
    }
    public enum BattleActionType  
    {
        //행동
        Attack,
        Item,
        Switch,
        Run
    }

    public abstract class BattleActor
    {
        public CharacterComponent Pokemon { get; }
        protected BattleActor(CharacterComponent pokemon)
        {
            Pokemon = pokemon;
        }
        public abstract BattleAction SelectAction();
        
    }
    public class PlayerActor : BattleActor
    {
        public PlayerActor(CharacterComponent pokemon)
            : base(pokemon)
        {
        }

        public override BattleAction SelectAction()
        {
            GameLog.LogSelectAct();
            
            int input = InputManager.GetMoveSlotChoice();
            
            switch(input)
            {
                case 1:
                    MoveRuntime move = SelectMove();
                    return new AttackAction(this, move);

                case 2:
                    return new ItemAction(this);

                case 3:
                    return new SwitchAction(this, Pokemon);

                default:
                    return new RunAction(this);
            }
        }
        public MoveRuntime SelectMove()
        {
            Pokemon.LogChoiceMove();
            
            int input = InputManager.GetMoveSlotChoice();
                
            return Pokemon.Runtime.CurrentMoves[input-1]; 
        }
    }
    public class EnemyActor : BattleActor
    {
        public EnemyActor(CharacterComponent pokemon)
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
            Random rand = new Random();
            int index = rand.Next(4);
                
            return Pokemon.Runtime.CurrentMoves[index]; 
        }
    }

    public abstract class BattleAction
    {
        public BattleActor Actor { get; }

        protected BattleAction(BattleActor actor)
        {
            Actor = actor;
        }
    }

    public class AttackAction : BattleAction
    {
        public MoveRuntime Move { get; }
        public AttackAction(BattleActor actor,MoveRuntime move)
            : base(actor)
        {
            Move = move;
        }
    }
    public class ItemAction : BattleAction
    {
        public ItemAction (BattleActor actor)
            : base(actor)
        {
        // 아이템을 받는 코드
        }
    }
    public class SwitchAction : BattleAction
    {
        public SwitchAction(BattleActor actor, CharacterComponent pokemon)
            : base(actor)
            {
            // 포켓몬 교체 코드 
            }
    }
    public class RunAction : BattleAction
    {
        
        public RunAction (BattleActor actor)
            : base(actor)
            {
            // 도망가는 코드 
            }
    }

    public static class Calculator
    {
        
        public static bool IsFaster(CharacterComponent pokemon1, CharacterComponent pokemon2)
            => pokemon1.Runtime.CurrentSpeed > pokemon2.Runtime.CurrentSpeed;
        

        public static int CalculateMaxHp(this PokemonData data, int level) 
            => data.BaseHp + level * 3;
        public static int CalculateCurrentSpeed(this PokemonData data,int speedStage) 
            => data.BaseSpeed + speedStage;
        public static float CalculateMultiplier(this CharacterComponent attacker,CharacterComponent defender,PokemonType attackType) 
            => attackType.CalculateTypeMultipler(defender.Runtime.Data.Types);
        public static int CalculateDamage(this CharacterComponent attacker,float typeMultiplier) 
            {
            int damage = (int)((attacker.Runtime.Data.BaseAttack + attacker.Runtime.AttackStage * 10) * typeMultiplier);
            return Math.Max(1,damage);
            }
        public static float CalculateTypeMultipler(this PokemonType attackType, IReadOnlyList<PokemonType> defenseTypes)
        {
            float finalMultiplier =  1.0f;
            foreach (var defType in defenseTypes)
            {
                finalMultiplier *= attackType.GetTypeMultiplier(defType);
            }
            return finalMultiplier;
        }
    }
}
