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
            public IBattleTrainer Trainer => _aiTrainer;

            public AiCommander(IBattleTrainer aiTrainer, IBattleTargetTrainer target)
            {
                  _aiTrainer = aiTrainer;
                  _target = target;
                  NameId = _aiTrainer.NameId;

                  if(!_aiTrainer.TrySetFirstActivePokemon())
                        throw new InvalidOperationException("현재 enemy의 ActivePokemon이 null 입니다.");
            }

            public IBattleCommand SelectCommand()   //초급 AI (무작위로 스킬을 씀)
            {
                  NullCheckActivePokemon();

                  var currentPokemon = _aiTrainer.ActivePokemon!;
                  var usableMoves = GetUsableMoves(currentPokemon);
                  
                  if(usableMoves.Count == 0)   // 사용 가능한 기술이 0개이므로 발버둥 실행
                        return BattleCommandFactory.CreateStruggleCommand(currentPokemon, _target, _aiTrainer.NameId);
                  
                  int randomindex = Utility.RandomIndex(usableMoves.Count);
                  var selectMove = usableMoves[randomindex - 1];

                  return BattleCommandFactory.CreateAttackCommand(currentPokemon,_target,selectMove, _aiTrainer.NameId);
            }

            public IBattleCommand GetForcedSwitchCommand()
            {       
                  int nextPokemonIndex = GetNextAlivePokemonIndex();

                  if (nextPokemonIndex == -1)    // CanBattle()을 통과하고 왔으므로 무조건 생존 포켓몬이 존재함
                  {
                        throw new InvalidOperationException("교체 가능한 포켓몬이 없는데 강제 교체가 호출되었습니다.");
                  }

                  return BattleCommandFactory.CreateSwitchCommand(_aiTrainer, nextPokemonIndex, _aiTrainer.NameId);
            }     

            private int GetNextAlivePokemonIndex()
            {
                  for (int i = 0; i < _aiTrainer.Party.Count; i++)
                  {
                        var pokemon = _aiTrainer.Party[i];
                        
                        // 현재 나와있는 포켓몬이 아니고, 기절하지 않은 포켓몬 선택
                        if (pokemon != _aiTrainer.ActivePokemon && !pokemon.IsFainted)
                        {
                        return i;
                        }
                  }
                  return -1;
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