namespace Pokemongame
{
    public class MoveRuntime
    {
        public MoveData Data { get; }                    //전투 중 기술의 데이터 pp 등
        public int CurrentPP { get; private set; }

        public MoveRuntime(MoveData data)
        {
            Data = data;
            CurrentPP = data.BasePP;
        }

        public void ConsumePP()
        {  
            CurrentPP--;  
        }
    }
}