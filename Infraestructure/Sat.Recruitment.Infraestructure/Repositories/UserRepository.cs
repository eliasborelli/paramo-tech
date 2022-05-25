using Microsoft.Extensions.Options;
using Sat.Recruitment.Core.Entities;
using Sat.Recruitment.Core.Enums;
using Sat.Recruitment.Core.Interfaces;
using Sat.Recruitment.Core.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infraestructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IOptionsMonitor<Settings> _settingOptions;
        public UserRepository(IOptionsMonitor<Settings> settingOptions)
        {
            _settingOptions = settingOptions;
        }

        public async Task Add(User user)
        {
            string _user = $"{user.Name},{user.Email},{user.Phone},{user.Address},{Enum.GetName(typeof(UserType), user.UserType)},{user.Money}";

            var append = File.AppendAllTextAsync(Directory.GetCurrentDirectory() + _settingOptions.CurrentValue.CurrentFile, _user + Environment.NewLine);

            await append;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = new List<User>();
            using (StreamReader reader = new StreamReader(GetFile(FileMode.Open)))
            {
                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLineAsync().Result;
                    users.Add(new User
                    {
                        Name = line.Split(',')[0].ToString(),
                        Email = line.Split(',')[1].ToString(),
                        Phone = line.Split(',')[2].ToString(),
                        Address = line.Split(',')[3].ToString(),
                        UserType = (UserType)Enum.Parse(typeof(UserType), line.Split(',')[4].ToString()),
                        Money = decimal.Parse(line.Split(',')[5].ToString()),
                    });
                }
                return users;
            }
        }

        private FileStream GetFile(FileMode fileMode)
        {
            var path = Directory.GetCurrentDirectory() + _settingOptions.CurrentValue.CurrentFile;

            FileStream fileStream = new FileStream(path, fileMode);
            return fileStream;
        }

    }
}
