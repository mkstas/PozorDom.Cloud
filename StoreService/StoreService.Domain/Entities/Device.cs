using StoreService.Domain.ValueObjects;

namespace StoreService.Domain.Entities
{
    public record DeviceId(Guid Value);

    public class Device
    {
        public DeviceId Id { get; private set; }
        public DeviceTypeId DeviceTypeId { get; private set; }
        public DeviceType? DeviceType { get; private set; }
        public Name Name { get; private set; }
        public ImageUrl? ImageUrl { get; private set; }

#pragma warning disable CS8618
        private Device() { }
#pragma warning restore CS8618

        private Device(DeviceTypeId deviceTypeId, Name name, ImageUrl? imageUrl)
        {
            Id = new DeviceId(Guid.NewGuid());
            DeviceTypeId = deviceTypeId;
            Name = name;
            ImageUrl = imageUrl;
        }

        public static Device Create(DeviceTypeId deviceTypeId, Name name, ImageUrl? imageUrl) =>
            new(deviceTypeId, name, imageUrl);

        public void ChangeDeviceType(DeviceTypeId deviceTypeId) => DeviceTypeId = deviceTypeId;
        public void ChangeName(Name name) => Name = name;
        public void ChangeImageUrl(ImageUrl imageUrl) => ImageUrl = imageUrl;
        public void DeleteImageUrl() => ImageUrl = null;
    }
}
