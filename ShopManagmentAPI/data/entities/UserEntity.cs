using ShopManagmentAPI.domain.model.user;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ShopManagmentAPI.data.entities
{
    [Table("Users")]
    public class UserEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique]
        public string Email { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        [ForeignKey(typeof(UserRoleEntity))]
        public int RoleId { get; set; }
        [OneToOne]
        public UserRoleEntity Role { get; set; }
        [OneToMany]
        public List<ShopEntity> Shops { get; set; } = new List<ShopEntity>();
    }
}
