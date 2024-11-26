using DigAccess.Data.Entities.Blind;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DigAccess.Data.Seeder
{
    public class EmailSettingsSeeder : IEntityTypeConfiguration<EmailSettings>
    {
        public void Configure(EntityTypeBuilder<EmailSettings> builder)
        {
            builder.HasData(
                new EmailSettings()
                {
                    Id = Guid.Parse("66f45e2c-256e-4bdc-a449-347bfc97d1fa"),
                    Name = "Google Gmail",
                    Port = 993,
                    IncomingServer = "imap.gmail.com",
                    OutgoingServer = "smpt.gmail.com"
                },
                new EmailSettings()
                {
                    Id = Guid.Parse("5cd666d8-0009-46f6-84dc-00e30fc07501"),
                    Name = "Microsoft Outlook",
                    Port = 993,
                    IncomingServer = "outlook.office365.com",
                    OutgoingServer = "smtp-mail.outlook.com"
                },
                new EmailSettings()
                {
                    Id = Guid.Parse("ddbc2773-69fd-499f-95f2-87068dbe2d58"),
                    Name = "Yahoo Mail",
                    Port = 993,
                    IncomingServer = "imap.mail.yahoo.com",
                    OutgoingServer = "smtp.mail.yahoo.com"
                }
                );
        } // Configure
    } // EmailSettingsSeeder
}
