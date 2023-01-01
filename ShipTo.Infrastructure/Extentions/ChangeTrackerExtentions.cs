using ShipTo.Infrastructure.UserResolverHandler;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShipTo.Core.Entities._Base;

namespace ShipTo.Infrastructure.Extentions
{
    public static class ChangeTrackerExtentions
    {
        public static void ApplyAuditInformation(this ChangeTracker changeTracker, IUserResolverHandler _userResolverHandler)
        {
            foreach (var entry in changeTracker.Entries().Where(e => e.Entity is IEntity/* && (e.State == EntityState.Added || e.State == EntityState.Modified)*/
            ))
            {
                var entity = (IEntity)entry.Entity;
                TirmStrings(entity);
                DateTime tempdate = DateTime.Now;
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreatedDate = tempdate;
                        entity.CreatedBy = !string.IsNullOrEmpty(entity.CreatedBy) ? entity.CreatedBy : _userResolverHandler?.GetUserId() ?? string.Empty;
                        break;
                    case EntityState.Modified:
                        var updateEntity = (IEntity)entry.Entity;
                        entry.Property(nameof(IEntity.CreatedBy)).IsModified = false;
                        entry.Property(nameof(IEntity.CreatedDate)).IsModified = false;
                        updateEntity.ModefiedDate = tempdate;
                        updateEntity.ModefiedBy = _userResolverHandler?.GetUserId();
                        break;
                    case EntityState.Deleted:
                        entity.IsDeleted = true;
                        entity.DeletedDate = tempdate;
                        entity.DeletedBy = _userResolverHandler?.GetUserId();
                        entry.State = EntityState.Modified;
                        //entity.CreatedBy = !string.IsNullOrEmpty(entity.CreatedBy) ? entity.CreatedBy : _userResolverHandler?.GetUserId() ?? string.Empty;
                        break;
                }
            }

            //foreach (var entity in changeTracker.Entries().Where(e => e.Entity is ISoftDeletable && e.State == EntityState.Deleted))
            //{
            //    var deletedEntity = (ISoftDeletable)entity.Entity;
            //    entity.Property(nameof(ISoftDeletable.IsDeleted)).CurrentValue = true;
            //    //entity.Property(nameof(ISoftDeletable.DeletedDate)).CurrentValue = DateTime.Now;
            //    entity.State = EntityState.Modified;
            //}
        }

        #region Helper Methods
        private static void TirmStrings(IEntity entity)
        {
            var stringProperties = entity.GetType().GetProperties()
                             .Where(p => p.PropertyType == typeof(string));

            foreach (var stringProperty in stringProperties)
            {
                string currentValue = (string)stringProperty.GetValue(entity, null);
                if (!string.IsNullOrEmpty(currentValue))
                    stringProperty.SetValue(entity, currentValue.Trim(), null);
            }
        }

        private static string GetDeletedPropertyName(Type propertyType)
        {
            string _isDeletedProperty = string.Empty;
            var properties = typeof(ISoftDeletable).GetProperties();
            if (properties != null)
            {
                var deletedProperty = properties.FirstOrDefault(p => p.PropertyType == propertyType);
                if (deletedProperty != null)
                    _isDeletedProperty = deletedProperty.Name;
            }
            return _isDeletedProperty;
        }
        #endregion
    }
}
