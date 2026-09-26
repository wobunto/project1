using MyGame.Commands;
using MyGame.Trainers;
using MyGame.Moves;
using MyGame.Utilities;
using MyGame.Pokemons;
using static MyGame.Rules.PokemonRules;
using System.Linq.Expressions;

namespace MyGame.BattleCommanders
{
      public class AiCommander : IBattleCommander
      {
            private readonly IBattleTrainer _aiTrainer;
            private readonly IBattleTargetTrainer _target; 

            public int NameId { get; }

            public AiCommander(IBattleTrainer aiTrainer, IBattleTargetTrainer target)
            {
                  _aiTrainer = aiTrainer;
                  _target = target;
                  NameId = _aiTrainer.NameId;
            }

            public IBattleCommand SelectCommand()   //초급 AI (무작위로 스킬을 씀)
            {
                  NullCheckActivePokemon();

                  var currentPokemon = _aiTrainer.ActivePokemon!;
                  var usableMoves = GetUsableMoves(currentPokemon);
                  
                  if(usableMoves.Count == 0)   // 사용 가능한 기술이 0개이므로 발버둥 실행
                        return BattleCommandFactory.CreateStruggleCommand(currentPokemon, _target);
                  
                  int randomindex = Utility.RandomIndex(usableMoves.Count);
                  var selectMove = usableMoves[randomindex];

                  return BattleCommandFactory.CreateAttackCommand(currentPokemon,_target,selectMove);
            }

            public TurnResult IsActivePokemonFainted()
            {
                  NullCheckActivePokemon();
                  TurnResult result;

                  if(!_aiTrainer.ActivePokemon!.IsFainted)
                        return TurnResult.None;
                  
                  result = TrySwitchCommand();
                  
                  return result;            
            }

            private TurnResult TrySwitchCommand()
            {
                  for(int i = 0; i < _aiTrainer.Party.Count; i++)
                  {
                        if(_aiTrainer.Party[i] == _aiTrainer.ActivePokemon || 
                           _aiTrainer.Party[i].IsFainted)
                              continue;

                        if(!_aiTrainer.TrySetActivePokemon(i))
                            throw new InvalidOperationException("적 트레이너의 Party 인덱스와 실제 인덱스가 일치하지 않습니다.");  
                        return TurnResult.SwitchPokemon;
                  }

                  return TurnResult.AllFainted;
            }     

            private IReadOnlyList<MoveRuntime> GetUsableMoves(IBattlePokemon pokemon)
            {
                  List<MoveRuntime> usableMoves = new();

                  for (int i = 0; i < MaxMoveSlot; i++)
                  {
                        if (pokemon.TryGetUsableMove(i, out var move) == MoveUsageResult.Success)
                        {
                              usableMoves.Add(move!);
                        }
                  }

                  return usableMoves;
            }

            private void NullCheckActivePokemon()
            {
                  if(_aiTrainer.ActivePokemon == null)
                        throw new InvalidOperationException("AiTrainer의 ActivePokemon이 Null 입니다,");
            }
      }
}