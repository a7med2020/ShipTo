using ShipTo.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipTo.Infrastructure.Extentions
{
    public static class SeedExtentions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            SeedDeliveryStatus(modelBuilder);
            //SeedEmailAddressType(modelBuilder);
            ////SeedEmailTemplateAddress(modelBuilder);
            //SeedEmailFromAddressConfiguration(modelBuilder);
        }

        private static void SeedDeliveryStatus(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DeliveryStatus>().HasData(
                 new DeliveryStatus
                 {
                     ID = "UnderDelivery",
                     Name = "قيد التسليم",
                 },
                new DeliveryStatus
                {
                    ID = "Delivered",
                    Name = "استلم",
                },
                new DeliveryStatus
                {
                    ID = "Refused",
                    Name = "رفض الاستلام",
                }
                ,
                new DeliveryStatus
                {
                    ID = "PartialDelivery",
                    Name = "تسليم جزئي",
                }
            );
        }

        private static void SeedEmailAddressType(this ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<EmailAddressType>().HasData(
            //    new EmailAddressType
            //    {
            //        EmailAddressTypeId = 1,
            //        Code = "From",
            //        Name = "From",
            //        Description = "From",
            //        IsDeleted = false,
            //        CreatedBy = "Auto Generated",
            //        CreationDate = DateTime.Now
            //    },
            //    new EmailAddressType
            //    {
            //        EmailAddressTypeId = 2,
            //        Code = "To",
            //        Name = "To",
            //        Description = "To",
            //        IsDeleted = false,
            //        CreatedBy = "Auto Generated",
            //        CreationDate = DateTime.Now
            //    },
            //    new EmailAddressType
            //    {
            //        EmailAddressTypeId = 3,
            //        Code = "Cc",
            //        Name = "Cc",
            //        Description = "Cc",
            //        IsDeleted = false,
            //        CreatedBy = "Auto Generated",
            //        CreationDate = DateTime.Now
            //    },
            //    new EmailAddressType
            //    {
            //        EmailAddressTypeId = 4,
            //        Code = "Bcc",
            //        Name = "Bcc",
            //        Description = "Bcc",
            //        IsDeleted = false,
            //        CreatedBy = "Auto Generated",
            //        CreationDate = DateTime.Now
            //    }
            //);
        }

        private static void SeedEmailFromAddressConfiguration(this ModelBuilder modelBuilder)
        {
           // modelBuilder.Entity<EmailFromAddressConfiguration>().HasData(
           //    new EmailFromAddressConfiguration
           //    {
           //        EmailFromAddressConfigurationId = 1,
           //        Port = 587,
           //        Password = "xlvyykaahvxukhce",
           //        SmtpServer = "smtp.gmail.com",
           //        UserName = "ahmed.moataz.13.9.92@gmail.com",
           //        FromAddress = "ahmed.moataz.13.9.92@gmail.com",
           //        IsDeleted = false,
           //        CreatedBy = "Auto Generated",
           //        CreationDate = DateTime.Now
           //    }
           //);
        }

        //private static void SeedEmailTemplateAddress(this ModelBuilder modelBuilder)
        //{
        //    SeedBrokerEmailAddressTemplate(modelBuilder);
        //    SeedCustomerEmailAddressTemplate(modelBuilder);
        //    SeedErrorEmailAddressTemplate(modelBuilder);
        //}

        private static void SeedBrokerEmailAddressTemplate(this ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<EmailTemplateAddress>().HasData(
            //    new EmailTemplateAddress
            //    {
            //        EmailTemplateAddressId = 1,
            //        Address = "BrokerService@Bestlife.com",
            //        EmailAddressTypeId = (byte)EmailAddressTypeEnum.From,
            //        CreatedBy = "General",
            //        //CreationDate = DateTime.Now,
            //        IsDeleted = false,
            //        EmailTemplateId = 1
            //    },
            //    new EmailTemplateAddress
            //    {
            //        EmailTemplateAddressId = 2,
            //        Address = "BrokerService@Bestlife.com",
            //        EmailAddressTypeId = (byte)EmailAddressTypeEnum.From,
            //        CreatedBy = "General",
            //        CreationDate = DateTime.Now,
            //        IsDeleted = false,
            //        EmailTemplateId = 2
            //    },
            //    new EmailTemplateAddress
            //    {
            //        EmailTemplateAddressId = 3,
            //        Address = "BrokerService@Bestlife.com",
            //        EmailAddressTypeId = (byte)EmailAddressTypeEnum.From,
            //        CreatedBy = "General",
            //        CreationDate = DateTime.Now,
            //        IsDeleted = false,
            //        EmailTemplateId = 3
            //    },
            //    new EmailTemplateAddress
            //    {
            //        EmailTemplateAddressId = 4,
            //        Address = "BrokerService@Bestlife.com",
            //        EmailAddressTypeId = (byte)EmailAddressTypeEnum.From,
            //        CreatedBy = "General",
            //        CreationDate = DateTime.Now,
            //        IsDeleted = false,
            //        EmailTemplateId = 4
            //    },
            //    new EmailTemplateAddress
            //    {
            //        EmailTemplateAddressId = 5,
            //        Address = "BrokerService@Bestlife.com",
            //        EmailAddressTypeId = (byte)EmailAddressTypeEnum.From,
            //        CreatedBy = "General",
            //        CreationDate = DateTime.Now,
            //        IsDeleted = false,
            //        EmailTemplateId = 5
            //    },
            //    new EmailTemplateAddress
            //    {
            //        EmailTemplateAddressId = 6,
            //        Address = "BrokerService@Bestlife.com",
            //        EmailAddressTypeId = (byte)EmailAddressTypeEnum.From,
            //        CreatedBy = "General",
            //        CreationDate = DateTime.Now,
            //        IsDeleted = false,
            //        EmailTemplateId = 6
            //    },
            //    new EmailTemplateAddress
            //    {
            //        EmailTemplateAddressId = 7,
            //        Address = "BrokerService@Bestlife.com",
            //        EmailAddressTypeId = (byte)EmailAddressTypeEnum.From,
            //        CreatedBy = "General",
            //        CreationDate = DateTime.Now,
            //        IsDeleted = false,
            //        EmailTemplateId = 7
            //    },
            //    new EmailTemplateAddress
            //    {
            //        EmailTemplateAddressId = 8,
            //        Address = "BrokerService@Bestlife.com",
            //        EmailAddressTypeId = (byte)EmailAddressTypeEnum.From,
            //        CreatedBy = "General",
            //        CreationDate = DateTime.Now,
            //        IsDeleted = false,
            //        EmailTemplateId = 8
            //    },
            //    new EmailTemplateAddress
            //    {
            //        EmailTemplateAddressId = 9,
            //        Address = "BrokerService@Bestlife.com",
            //        EmailAddressTypeId = (byte)EmailAddressTypeEnum.From,
            //        CreatedBy = "General",
            //        CreationDate = DateTime.Now,
            //        IsDeleted = false,
            //        EmailTemplateId = 9
            //    }
            //);
        }

   
    }
}
