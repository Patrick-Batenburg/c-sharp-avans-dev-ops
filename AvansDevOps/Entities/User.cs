using System.Text;

namespace AvansDevOps
{
    public class User
    {
        public User()
        {
        }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public virtual string FullName
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append($"{this.FirstName}");

                if (!string.IsNullOrWhiteSpace(this.MiddleName))
                {
                    stringBuilder.Append($" {this.MiddleName}");
                }

                stringBuilder.Append($" {this.LastName}");

                return stringBuilder.ToString();
            }
        }

        public string Email { get; set; }
    }
}
