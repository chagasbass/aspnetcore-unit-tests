namespace ProjetoCompeticao.Shared.Entities.Bases
{
    public abstract class BaseDto<T> where T : class
    {
        public abstract T ToEntity();
    }
}
