using System.Diagnostics.CodeAnalysis;
namespace Pokemongame
{
     public class UnityObject { public string Name {get; set; } = ""; }
     //기본 오브젝트
    public abstract class Component : UnityObject
    {
    //기본 컴포넌트
        public GameObject GameObject { get; internal set; } = null!;

        public virtual void Start(){ }
        public virtual void Update(){ }
    }
   
    public class GameObject : UnityObject
    {
        private readonly List<Component> _components = new(); 
        //게임 오브젝트가 컴포넌트들을 소유
        public T AddComponent<T>()where T : Component,new() 
        //컴포넌트 추가 및 초기화
        {
            T component = new();
            component.GameObject = this;
            _components.Add(component);
            return component;
        }
        public T? GetComponent<T>() where T : Component 
            => _components.OfType<T>().FirstOrDefault();
        //컴포넌트 뱉기
        public bool TryGetComponent<T>([NotNullWhen(true)]out T? component) where T : Component //컴포넌트가 있는지 없는지 확인 
        {
            component = GetComponent<T>();
            return component != null;
        }
    }
        //컴포넌트가 있는지 없는지 확인 밑 뱉기
    /*
        public void Start()
        {
            foreach (var component in _components)
                component.Start();
        }
        public void Update()
        {
            foreach (var component in _components)
                component.Update();
        }
    */
    
    public static class PokemonFactory
    {
        public static GameObject Create(PokemonData data, int level)
        {
            //게임 오브젝트 생성 후 캐릭터 컴포넌트 추가 및 초기화
            GameObject obj = new GameObject();
            obj.Name = data.Name;
            obj.AddComponent<MoveComponent>();
            obj.AddComponent<CharacterComponent>().Initialize(data, level);
            return obj;
        }
    }
}