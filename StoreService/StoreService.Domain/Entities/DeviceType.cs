using StoreService.Domain.ValueObjects;

namespace StoreService.Domain.Entities
{
    public record DeviceTypeId(Guid Value);

    public class DeviceType
    {
        public DeviceTypeId Id { get; private set; }
        public Name Name { get; private set; }
        public List<Device> Devices { get; private set; } = [];

#pragma warning disable CS8618
        private DeviceType() { }
#pragma warning restore CS8618

        private DeviceType(Name name)
        {
            Id = new DeviceTypeId(Guid.NewGuid());
            Name = name;
        }

        public static DeviceType Create(Name name) => new(name);

        public void ChangeName(Name name) => Name = name;
    }
}
