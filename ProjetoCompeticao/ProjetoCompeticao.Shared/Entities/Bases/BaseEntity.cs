using Flunt.Notifications;

namespace ProjetoCompeticao.Shared.Entities.Bases
{
    public abstract class BaseEntity : Notifiable<Notification>
    {
        public Guid Id { get; private set; }
        public bool Ativo { get; set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            Ativo = true;
        }

        public void ChangeEntityVisibility(bool isActive) => Ativo = isActive;

        public override bool Equals(object obj)
        {
            var compareTo = obj as BaseEntity;

            if (!ReferenceEquals(this, compareTo))
            {
                if (compareTo is null) return false;

                return Id.Equals(compareTo.Id);
            }

            return true;
        }

        public static bool operator ==(BaseEntity a, BaseEntity b)
        {
            if (a is not null || b is not null)
            {
                if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                    return false;

                return a.Equals(b);
            }

            return true;
        }

        public static bool operator !=(BaseEntity a, BaseEntity b) => !(a == b);

        public override int GetHashCode() => (GetType().GetHashCode() * 907) + Id.GetHashCode();

        public override string ToString() => $"{GetType().Name} [Id= {Id}]";
    }
}
