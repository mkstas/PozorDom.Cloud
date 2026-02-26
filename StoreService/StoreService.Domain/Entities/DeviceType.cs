using StoreService.Domain.ValueObjects;

namespace StoreService.Domain.Entities
{
    public class DeviceType
    {
        public Guid Id { get; private set; }
        public Name Name { get; private set; }

#pragma warning disable CS8618
        private DeviceType() { }
#pragma warning restore CS8618

        private DeviceType(Name name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public static DeviceType Create(Name name)
        {
            return new DeviceType(name);
        }

        public void ChangeName(Name name)
        {
            Name = name;
        }
    }
}
