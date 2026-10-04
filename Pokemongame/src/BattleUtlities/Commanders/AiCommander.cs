using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyGame.Commands;
using MyGame.Trainers;
using MyGame.Moves;
using MyGame.Utilities;
using MyGame.Pokemons;
using static MyGame.Rules.PokemonRules;

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

            if (!_aiTrainer.TrySetFirstActivePokemon())
                throw new InvalidOperationException("현재 enemy의 ActivePokemon이 null 입니다.");
        }

        // 1. 일반 명령 선택 (비동기 Task 반환)
        public Task<IBattleCommand> SelectCommandAsync()
        {
            NullCheckActivePokemon();

            var currentPokemon = _aiTrainer.ActivePokemon!;
            var usableMoves = GetUsableMoves(currentPokemon);
            
            // 사용 가능한 기술이 없으면 발버둥 실행
            if (usableMoves.Count == 0)
            {
                IBattleCommand struggleCmd = BattleCommandFactory.CreateStruggleCommand(currentPokemon, _target, _aiTrainer.NameId);
                return Task.FromResult(struggleCmd);
            }
            
            // 랜덤으로 기술 선택
            int randomIndex = Utility.RandomIndex(usableMoves.Count);
            var selectedMove = usableMoves[randomIndex];

            IBattleCommand attackCmd = BattleCommandFactory.CreateAttackCommand(currentPokemon, _target, selectedMove, _aiTrainer.NameId);
            
            // 즉시 결과를 Task로 감싸서 반환
            return Task.FromResult(attackCmd);
        }

        // 2. 강제 교체 처리 (비동기 Task 반환)
        public Task<IBattleCommand> GetForcedSwitchCommandAsync()
        {       
            int nextPokemonIndex = GetNextAlivePokemonIndex();

            if (nextPokemonIndex == -1)
            {
                throw new InvalidOperationException("교체 가능한 포켓몬이 없는데 강제 교체가 호출되었습니다.");
            }

            IBattleCommand switchCmd = BattleCommandFactory.CreateSwitchCommand(_aiTrainer, nextPokemonIndex);
            
            // 즉시 결과를 Task로 감싸서 반환
            return Task.FromResult(switchCmd);
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
            if (_aiTrainer.ActivePokemon == null)
                throw new InvalidOperationException("AiTrainer의 ActivePokemon이 Null 입니다.");
        }
    }
}